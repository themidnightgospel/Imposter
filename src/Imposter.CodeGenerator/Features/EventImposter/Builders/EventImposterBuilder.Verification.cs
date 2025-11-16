using System;
using System.Linq;
using Imposter.CodeGenerator.Features.EventImposter.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.Features.Shared.Builders.FormatValueMethodBuilder;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.EventImposter.Builders;

internal static partial class EventImposterBuilder
{
    private static MethodDeclarationSyntax BuildSubscribedVerificationMethod(
        in ImposterEventMetadata @event
    )
    {
        var method = @event.Builder.Methods.Subscribed;
        var criteriaName = method.CriteriaParameter.Name;
        var eventName = @event.Core.Name;

        return ExplicitInterfaceMethod(
                @event.BuilderInterface.VerificationInterfaceTypeSyntax,
                @event.BuilderInterface.VerificationInterfaceTypeSyntax,
                method.Name
            )
            .AddParameter(ParameterSyntax(method.CriteriaParameter))
            .AddParameter(CountParameter(@event))
            .WithBody(
                BuildHistoryVerificationBody(
                    @event,
                    historyField: @event.Builder.Fields.SubscribeHistory,
                    criteriaParameterName: criteriaName,
                    predicateFactory: handler =>
                        IdentifierName(criteriaName)
                            .Dot(IdentifierName("Matches"))
                            .Call(Argument(handler)),
                    descriptionFactory: handler =>
                        BuildSubscriptionDescription(eventName, "subscribed", handler)
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildUnsubscribedVerificationMethod(
        in ImposterEventMetadata @event
    )
    {
        var method = @event.Builder.Methods.Unsubscribed;
        var criteriaName = method.CriteriaParameter.Name;
        var eventName = @event.Core.Name;

        return ExplicitInterfaceMethod(
                @event.BuilderInterface.VerificationInterfaceTypeSyntax,
                @event.BuilderInterface.VerificationInterfaceTypeSyntax,
                method.Name
            )
            .AddParameter(ParameterSyntax(method.CriteriaParameter))
            .AddParameter(CountParameter(@event))
            .WithBody(
                BuildHistoryVerificationBody(
                    @event,
                    historyField: @event.Builder.Fields.UnsubscribeHistory,
                    criteriaParameterName: criteriaName,
                    predicateFactory: handler =>
                        IdentifierName(criteriaName)
                            .Dot(IdentifierName("Matches"))
                            .Call(Argument(handler)),
                    descriptionFactory: handler =>
                        BuildSubscriptionDescription(eventName, "unsubscribed", handler)
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildRaisedVerificationMethod(
        in ImposterEventMetadata @event
    )
    {
        var methodBuilder = ExplicitInterfaceMethod(
                @event.BuilderInterface.VerificationInterfaceTypeSyntax,
                @event.BuilderInterface.VerificationInterfaceTypeSyntax,
                @event.Builder.Methods.RaisedVerificationName
            )
            .AddParameters(
                @event.Core.Parameters.Select(parameter =>
                    ParameterSyntax(parameter.ArgTypeSyntax, $"{parameter.Name}Criteria")
                )
            )
            .AddParameter(CountParameter(@event));

        return methodBuilder.WithBody(BuildRaisedBody(@event)).Build();
    }

    private static BlockSyntax BuildRaisedBody(in ImposterEventMetadata @event)
    {
        var blockBuilder = new BlockBuilder();
        var countParameterName = @event.Builder.Methods.CountParameter.Name;
        var ensureCountMatchesName = @event.Builder.Methods.EnsureCountMatchesName;
        var eventName = @event.Core.Name;
        var parameters = @event.Core.Parameters;

        foreach (var parameter in parameters)
        {
            blockBuilder.AddExpression(ThrowIfNull($"{parameter.Name}Criteria"));
        }

        blockBuilder.AddExpression(ThrowIfNull(countParameterName));

        var predicate = BuildRaisedPredicate(@event);

        AddEnsureCountMatchesStatements(
            blockBuilder,
            countParameterName,
            ensureCountMatchesName,
            FieldIdentifier(@event.Builder.Fields.History)
                .Dot(LinqSyntaxHelper.Count)
                .Call([Argument(predicate)]),
            BuildRaisedPerformedInvocationsFactory(
                @event.Builder.Fields.History,
                eventName,
                parameters,
                GetPredicateBody(predicate)
            )
        );

        blockBuilder.AddStatement(ReturnStatement(ThisExpression()));

        return blockBuilder.Build();
    }

    private static SimpleLambdaExpressionSyntax BuildRaisedPredicate(
        in ImposterEventMetadata @event
    )
    {
        var entryIdentifier = IdentifierName("entry");
        ExpressionSyntax? predicateBody = null;

        if (@event.Core.Parameters.Length == 0)
        {
            predicateBody = True;
        }
        else if (@event.Core.Parameters.Length == 1)
        {
            var parameter = @event.Core.Parameters[0];
            predicateBody = IdentifierName($"{parameter.Name}Criteria")
                .Dot(IdentifierName("Matches"))
                .Call(Argument(entryIdentifier));
        }
        else
        {
            foreach (var parameter in @event.Core.Parameters)
            {
                var matchCall = IdentifierName($"{parameter.Name}Criteria")
                    .Dot(IdentifierName("Matches"))
                    .Call(Argument(entryIdentifier.Dot(IdentifierName(parameter.Name))));

                predicateBody = predicateBody is null ? matchCall : predicateBody.And(matchCall);
            }
        }

        return SimpleLambdaExpression(Parameter(Identifier("entry")), predicateBody!);
    }

    private static MethodDeclarationSyntax BuildHandlerInvokedVerificationMethod(
        in ImposterEventMetadata @event
    )
    {
        var method = @event.Builder.Methods.HandlerInvoked;
        var criteriaName = method.HandlerCriteriaParameter.Name;
        var eventName = @event.Core.Name;
        var parameters = @event.Core.Parameters;

        Func<ExpressionSyntax, ExpressionSyntax> predicateFactory =
            @event.Core.Parameters.Length == 0
                ? entry =>
                    IdentifierName(criteriaName)
                        .Dot(IdentifierName("Matches"))
                        .Call(Argument(entry))
                : entry =>
                    IdentifierName(criteriaName)
                        .Dot(IdentifierName("Matches"))
                        .Call(Argument(entry.Dot(IdentifierName("Handler"))));

        return ExplicitInterfaceMethod(
                @event.BuilderInterface.VerificationInterfaceTypeSyntax,
                @event.BuilderInterface.VerificationInterfaceTypeSyntax,
                method.Name
            )
            .AddParameter(ParameterSyntax(method.HandlerCriteriaParameter))
            .AddParameter(CountParameter(@event))
            .WithBody(
                BuildHistoryVerificationBody(
                    @event,
                    historyField: @event.Builder.Fields.HandlerInvocations,
                    criteriaParameterName: criteriaName,
                    predicateFactory: predicateFactory,
                    descriptionFactory: entry =>
                        BuildHandlerInvocationDescription(eventName, parameters, entry)
                )
            )
            .Build();
    }

    private static BlockSyntax BuildHistoryVerificationBody(
        in ImposterEventMetadata @event,
        in FieldMetadata historyField,
        string criteriaParameterName,
        Func<ExpressionSyntax, ExpressionSyntax> predicateFactory,
        Func<ExpressionSyntax, ExpressionSyntax> descriptionFactory
    )
    {
        var countParameterName = @event.Builder.Methods.CountParameter.Name;
        var ensureCountMatchesName = @event.Builder.Methods.EnsureCountMatchesName;

        var blockBuilder = new BlockBuilder()
            .AddExpression(ThrowIfNull(criteriaParameterName))
            .AddExpression(ThrowIfNull(countParameterName));

        var entryIdentifier = IdentifierName("entry");
        var predicateLambda = SimpleLambdaExpression(
            Parameter(Identifier("entry")),
            predicateFactory(entryIdentifier)
        );

        AddEnsureCountMatchesStatements(
            blockBuilder,
            countParameterName,
            ensureCountMatchesName,
            FieldIdentifier(historyField)
                .Dot(LinqSyntaxHelper.Count)
                .Call([Argument(predicateLambda)]),
            BuildHistoryPerformedInvocationsFactory(
                historyField,
                descriptionFactory,
                GetPredicateBody(predicateLambda)
            )
        );

        blockBuilder.AddStatement(ReturnStatement(ThisExpression()));

        return blockBuilder.Build();
    }

    private static MethodDeclarationSyntax BuildEnsureCountMatchesMethod(
        in EventImposterBuilderMethodsMetadata methods
    ) =>
        new MethodDeclarationBuilder(WellKnownTypes.Void, methods.EnsureCountMatchesName)
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddModifier(Token(SyntaxKind.StaticKeyword))
            .AddParameter(ParameterSyntax(WellKnownTypes.Int, "actual"))
            .AddParameter(ParameterSyntax(WellKnownTypes.Imposter.Abstractions.Count, "expected"))
            .AddParameter(
                ParameterSyntax(
                    WellKnownTypes.System.FuncOfT(PredefinedType(Token(SyntaxKind.StringKeyword))),
                    "performedInvocationsFactory"
                )
            )
            .WithBody(
                Block(
                    IfStatement(
                        Not(
                            IdentifierName("expected")
                                .Dot(IdentifierName("Matches"))
                                .Call(Argument(IdentifierName("actual")))
                        ),
                        Block(
                            ThrowStatement(
                                ObjectCreationExpression(
                                        WellKnownTypes
                                            .Imposter
                                            .Abstractions
                                            .VerificationFailedException
                                    )
                                    .WithArgumentList(
                                        ArgumentList(
                                            SeparatedList([
                                                Argument(IdentifierName("expected")),
                                                Argument(IdentifierName("actual")),
                                                Argument(
                                                    IdentifierName("performedInvocationsFactory")
                                                        .Call()
                                                ),
                                            ])
                                        )
                                    )
                            )
                        )
                    )
                )
            )
            .Build();

    private static void AddEnsureCountMatchesStatements(
        BlockBuilder blockBuilder,
        string countParameterName,
        string ensureCountMatchesName,
        ExpressionSyntax actualValueExpression,
        ExpressionSyntax performedInvocationsFactoryExpression
    )
    {
        blockBuilder.AddStatement(
            LocalVariableDeclarationSyntax(WellKnownTypes.Int, "actual", actualValueExpression)
        );

        blockBuilder.AddExpression(
            IdentifierName(ensureCountMatchesName)
                .Call([
                    Argument(IdentifierName("actual")),
                    Argument(IdentifierName(countParameterName)),
                    Argument(performedInvocationsFactoryExpression),
                ])
        );
    }

    private static ParenthesizedLambdaExpressionSyntax BuildHistoryPerformedInvocationsFactory(
        in FieldMetadata historyField,
        Func<ExpressionSyntax, ExpressionSyntax> descriptionFactory,
        ExpressionSyntax predicateBody
    )
    {
        var stringListType = WellKnownTypes.System.Collections.Generic.List(
            PredefinedType(Token(SyntaxKind.StringKeyword))
        );
        var entryIdentifier = IdentifierName("entry");

        return ParenthesizedLambdaExpression()
            .WithParameterList(ParameterList())
            .WithBlock(
                Block(
                    LocalVariableDeclarationSyntax(
                        Var,
                        "performedInvocations",
                        stringListType.New()
                    ),
                    ForEachStatement(
                        Var,
                        Identifier("entry"),
                        FieldIdentifier(historyField),
                        Block(
                            IfStatement(
                                predicateBody,
                                Block(
                                    IdentifierName("performedInvocations")
                                        .Dot(IdentifierName("Add"))
                                        .Call(Argument(descriptionFactory(entryIdentifier)))
                                        .ToStatementSyntax()
                                )
                            )
                        )
                    ),
                    ReturnStatement(JoinWithNewLines(IdentifierName("performedInvocations")))
                )
            );
    }

    private static ParenthesizedLambdaExpressionSyntax BuildRaisedPerformedInvocationsFactory(
        in FieldMetadata historyField,
        string eventName,
        EventParameterMetadata[] parameters,
        ExpressionSyntax predicateBody
    ) =>
        BuildHistoryPerformedInvocationsFactory(
            historyField,
            entry => BuildRaisedDescription(eventName, parameters, entry),
            predicateBody
        );

    private static BinaryExpressionSyntax BuildSubscriptionDescription(
        string eventName,
        string action,
        ExpressionSyntax handlerExpression
    )
    {
        var description = BuildActionDescription(eventName, action);
        return AppendDetail(description, "handler", handlerExpression);
    }

    private static BinaryExpressionSyntax BuildHandlerInvocationDescription(
        string eventName,
        EventParameterMetadata[] parameters,
        ExpressionSyntax entry
    )
    {
        var description = BuildActionDescription(eventName, "handler invoked");
        var handlerExpression =
            parameters.Length == 0 ? entry : entry.Dot(IdentifierName("Handler"));

        description = AppendDetail(description, "handler", handlerExpression);

        foreach (var parameter in parameters)
        {
            description = AppendDetail(
                description,
                parameter.Name,
                entry.Dot(IdentifierName(parameter.Name))
            );
        }

        return description;
    }

    private static BinaryExpressionSyntax BuildRaisedDescription(
        string eventName,
        EventParameterMetadata[] parameters,
        ExpressionSyntax entry
    )
    {
        var description = BuildActionDescription(eventName, "raised");

        if (parameters.Length == 0)
        {
            return description;
        }

        if (parameters.Length == 1)
        {
            return AppendDetail(description, parameters[0].Name, entry);
        }

        foreach (var parameter in parameters)
        {
            description = AppendDetail(
                description,
                parameter.Name,
                entry.Dot(IdentifierName(parameter.Name))
            );
        }

        return description;
    }

    private static BinaryExpressionSyntax BuildActionDescription(string eventName, string action)
    {
        var actionText = $"{eventName} {action} ".StringLiteral();

        return AddStrings(actionText, Invocation(eventName.StringLiteral()));
    }

    private static BinaryExpressionSyntax AppendDetail(
        ExpressionSyntax description,
        string label,
        ExpressionSyntax valueExpression
    ) =>
        AddStrings(
            description,
            AddStrings($" {label}: ".StringLiteral(), Invocation(valueExpression))
        );

    private static InvocationExpressionSyntax JoinWithNewLines(ExpressionSyntax values) =>
        IdentifierName("string")
            .Dot(IdentifierName("Join"))
            .Call(
                ArgumentList(
                    SeparatedList<ArgumentSyntax>(
                        new SyntaxNodeOrToken[]
                        {
                            Argument(IdentifierName("Environment").Dot(IdentifierName("NewLine"))),
                            Token(SyntaxKind.CommaToken),
                            Argument(values),
                        }
                    )
                )
            );

    private static ExpressionSyntax GetPredicateBody(SimpleLambdaExpressionSyntax predicate) =>
        predicate.Body as ExpressionSyntax
        ?? throw new InvalidOperationException("Predicate body must be an expression.");

    private static ParameterSyntax CountParameter(in ImposterEventMetadata @event) =>
        ParameterSyntax(@event.Builder.Methods.CountParameter);
}
