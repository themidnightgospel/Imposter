using System;
using System.Linq;
using Imposter.CodeGenerator.Features.EventImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.Features.EventImpersonation.Builders.EventImposterBuilderCommon;
using static Imposter.CodeGenerator.Features.Shared.Builders.FormatValueMethodBuilder;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.EventImpersonation.Builders;

internal static class EventImposterVerificationBuilder
{
    internal static MethodDeclarationSyntax BuildSubscribedVerificationMethod(
        in ImposterEventMetadata @event
    ) =>
        BuildSubscriptionVerificationMethod(
            @event,
            @event.Builder.Methods.Subscribed,
            @event.Builder.Fields.SubscribeHistory,
            "subscribed"
        );

    internal static MethodDeclarationSyntax BuildUnsubscribedVerificationMethod(
        in ImposterEventMetadata @event
    ) =>
        BuildSubscriptionVerificationMethod(
            @event,
            @event.Builder.Methods.Unsubscribed,
            @event.Builder.Fields.UnsubscribeHistory,
            "unsubscribed"
        );

    private static MethodDeclarationSyntax BuildSubscriptionVerificationMethod(
        in ImposterEventMetadata @event,
        in CriteriaMethodMetadata method,
        in FieldMetadata history,
        string action
    )
    {
        var criteriaName = method.CriteriaParameter.Name;
        var eventName = @event.Core.Name;

        return new MethodDeclarationBuilder(
            @event.BuilderInterface.VerificationInterfaceTypeSyntax,
            method.Name
        )
            .WithExplicitInterfaceSpecifier(@event.BuilderInterface.VerificationInterfaceTypeSyntax)
            .AddParameter(ParameterSyntax(method.CriteriaParameter))
            .AddParameter(CountParameter(@event))
            .WithBody(
                BuildHistoryVerificationBody(
                    @event,
                    historyField: history,
                    criteriaParameterName: criteriaName,
                    predicateFactory: handler =>
                        IdentifierName(criteriaName)
                            .Dot(IdentifierName("Matches"))
                            .Call(Argument(handler)),
                    descriptionFactory: handler =>
                        BuildSubscriptionDescription(eventName, action, handler)
                )
            )
            .Build();
    }

    internal static MethodDeclarationSyntax BuildRaisedVerificationMethod(
        in ImposterEventMetadata @event
    )
    {
        var methodBuilder = new MethodDeclarationBuilder(
            @event.BuilderInterface.VerificationInterfaceTypeSyntax,
            @event.Builder.Methods.RaisedVerification.Name
        )
            .WithExplicitInterfaceSpecifier(@event.BuilderInterface.VerificationInterfaceTypeSyntax)
            .AddParameters(
                @event.Builder.Methods.RaisedCriteriaParameters.Select(criteria =>
                    ParameterSyntax(criteria)
                )
            )
            .AddParameter(CountParameter(@event));

        return methodBuilder.WithBody(BuildRaisedBody(@event)).Build();
    }

    private static BlockSyntax BuildRaisedBody(in ImposterEventMetadata @event)
    {
        var blockBuilder = new BlockBuilder();
        var countParameterName = @event.Builder.Methods.CountParameter.Name;
        var ensureCountMatchesName = @event.Builder.Methods.EnsureCountMatches.Name;
        foreach (var criteria in @event.Builder.Methods.RaisedCriteriaParameters)
        {
            blockBuilder.AddStatement(ThrowIfNull(criteria.Name));
        }

        blockBuilder.AddStatement(ThrowIfNull(countParameterName));

        var predicate = BuildRaisedPredicate(@event);

        AddEnsureCountMatchesStatements(
            blockBuilder,
            countParameterName,
            ensureCountMatchesName,
            FieldIdentifier(@event.Builder.Fields.History)
                .Dot(LinqSyntaxHelper.Count)
                .Call([Argument(predicate)]),
            BuildRaisedPerformedInvocationsFactory(@event, GetPredicateBody(predicate))
        );

        blockBuilder.AddStatement(ReturnThis);

        return blockBuilder.Build();
    }

    private static SimpleLambdaExpressionSyntax BuildRaisedPredicate(
        in ImposterEventMetadata @event
    )
    {
        var entry = IdentifierName("entry");
        var historyEntry = @event.Builder.Fields.HistoryEntry;
        var parameters = @event.Core.Parameters;
        var criteria = @event.Builder.Methods.RaisedCriteriaParameters;
        ExpressionSyntax? predicateBody = null;

        for (var index = 0; index < parameters.Length; index++)
        {
            var matchCall = IdentifierName(criteria[index].Name)
                .Dot(IdentifierName("Matches"))
                .Call(Argument(historyEntry.ParameterValue(entry, parameters[index])));

            predicateBody = predicateBody is null ? matchCall : predicateBody.And(matchCall);
        }

        return SimpleLambdaExpression(Parameter(entry.Identifier), predicateBody ?? True);
    }

    internal static MethodDeclarationSyntax BuildHandlerInvokedVerificationMethod(
        in ImposterEventMetadata @event
    )
    {
        var method = @event.Builder.Methods.HandlerInvoked;
        var criteriaName = method.HandlerCriteriaParameter.Name;
        var eventName = @event.Core.Name;
        var parameters = @event.Core.Parameters;
        var handlerInvocationEntry = @event.Builder.Fields.HandlerInvocationEntry;

        return new MethodDeclarationBuilder(
            @event.BuilderInterface.VerificationInterfaceTypeSyntax,
            method.Name
        )
            .WithExplicitInterfaceSpecifier(@event.BuilderInterface.VerificationInterfaceTypeSyntax)
            .AddParameter(ParameterSyntax(method.HandlerCriteriaParameter))
            .AddParameter(CountParameter(@event))
            .WithBody(
                BuildHistoryVerificationBody(
                    @event,
                    historyField: @event.Builder.Fields.HandlerInvocations,
                    criteriaParameterName: criteriaName,
                    predicateFactory: entry =>
                        IdentifierName(criteriaName)
                            .Dot(IdentifierName("Matches"))
                            .Call(Argument(handlerInvocationEntry.Handler(entry))),
                    descriptionFactory: entry =>
                        BuildHandlerInvocationDescription(
                            eventName,
                            parameters,
                            handlerInvocationEntry,
                            entry
                        )
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
        var ensureCountMatchesName = @event.Builder.Methods.EnsureCountMatches.Name;

        var blockBuilder = new BlockBuilder()
            .AddStatement(ThrowIfNull(criteriaParameterName))
            .AddStatement(ThrowIfNull(countParameterName));

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

        blockBuilder.AddStatement(ReturnThis);

        return blockBuilder.Build();
    }

    internal static MethodDeclarationSyntax BuildEnsureCountMatchesMethod(
        in EventImposterBuilderMethodsMetadata methods
    ) =>
        new MethodDeclarationBuilder(
            methods.EnsureCountMatches.ReturnType,
            methods.EnsureCountMatches.Name
        )
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddModifier(Token(SyntaxKind.StaticKeyword))
            .AddParameter(ParameterSyntax(WellKnownTypes.Int, "actual"))
            .AddParameter(ParameterSyntax(WellKnownTypes.Imposter.Abstractions.Count, "expected"))
            .AddParameter(
                ParameterSyntax(
                    WellKnownTypes.System.FuncOfT(WellKnownTypes.String),
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
        var stringListType = WellKnownTypes.System.Collections.Generic.List(WellKnownTypes.String);
        var entryIdentifier = IdentifierName("entry");

        return EmptyParametersGoesTo(
            Block(
                LocalVariableDeclarationSyntax(Var, "performedInvocations", stringListType.New()),
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
        in ImposterEventMetadata @event,
        ExpressionSyntax predicateBody
    )
    {
        var eventName = @event.Core.Name;
        var parameters = @event.Core.Parameters;
        var historyEntry = @event.Builder.Fields.HistoryEntry;

        return BuildHistoryPerformedInvocationsFactory(
            @event.Builder.Fields.History,
            entry => BuildRaisedDescription(eventName, parameters, historyEntry, entry),
            predicateBody
        );
    }

    private static BinaryExpressionSyntax BuildSubscriptionDescription(
        string eventName,
        string action,
        ExpressionSyntax handlerExpression
    )
    {
        var description = BuildActionDescription(eventName, action);
        return AppendDetail(description, "handler", handlerExpression);
    }

    private static ExpressionSyntax BuildHandlerInvocationDescription(
        string eventName,
        EventParameterMetadata[] parameters,
        in EventHandlerInvocationEntryMetadata handlerInvocationEntry,
        ExpressionSyntax entry
    )
    {
        ExpressionSyntax description = BuildActionDescription(eventName, "handler invoked");
        description = AppendDetail(description, "handler", handlerInvocationEntry.Handler(entry));

        foreach (var parameter in parameters)
        {
            description = AppendDetail(
                description,
                parameter.Name,
                EventHandlerInvocationEntryMetadata.ParameterValue(entry, parameter)
            );
        }

        return description;
    }

    private static ExpressionSyntax BuildRaisedDescription(
        string eventName,
        EventParameterMetadata[] parameters,
        in EventHistoryEntryMetadata historyEntry,
        ExpressionSyntax entry
    )
    {
        ExpressionSyntax description = BuildActionDescription(eventName, "raised");

        foreach (var parameter in parameters)
        {
            description = AppendDetail(
                description,
                parameter.Name,
                historyEntry.ParameterValue(entry, parameter)
            );
        }

        return description;
    }

    private static LiteralExpressionSyntax BuildActionDescription(
        string eventName,
        string action
    ) => $"{eventName} {action}".StringLiteral();

    private static BinaryExpressionSyntax AppendDetail(
        ExpressionSyntax description,
        string label,
        ExpressionSyntax valueExpression
    ) => description.Add($" {label}: ".StringLiteral().Add(Invocation(valueExpression)));

    private static ExpressionSyntax GetPredicateBody(SimpleLambdaExpressionSyntax predicate) =>
        predicate.Body as ExpressionSyntax
        ?? throw new InvalidOperationException("Predicate body must be an expression.");

    internal static ParameterSyntax CountParameter(in ImposterEventMetadata @event) =>
        ParameterSyntax(@event.Builder.Methods.CountParameter);
}
