using System;
using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.EventImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.Features.Shared.Builders.FormatValueMethodBuilder;
using static Imposter.CodeGenerator.Features.Shared.Builders.VerificationFailedBuilder;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.EventImpersonation.Builders;

internal static class EventImposterVerificationBuilder
{
    // A history entry, as the verification predicates read it. The performed-invocations description reuses a
    // predicate's body inside its own loop over the history, so both declare the entry by this one name.
    private static readonly IdentifierNameSyntax Entry = IdentifierName("entry");

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
            .AddParameter(ParameterSyntax(@event.Builder.Methods.CountParameter))
            .WithBody(
                BuildHistoryVerificationBody(
                    @event,
                    historyField: history,
                    criteriaParameterNames: [criteriaName],
                    predicate: Matches(criteriaName, Entry),
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
        var eventName = @event.Core.Name;
        var parameters = @event.Core.MatchedParameters;
        var historyEntry = @event.Builder.Fields.HistoryEntry;

        return new MethodDeclarationBuilder(
            @event.BuilderInterface.VerificationInterfaceTypeSyntax,
            @event.Builder.Methods.RaisedVerification.Name
        )
            .WithExplicitInterfaceSpecifier(@event.BuilderInterface.VerificationInterfaceTypeSyntax)
            .AddParameters(
                @event.Builder.Methods.RaisedCriteriaParameters.Select(criteria =>
                    ParameterSyntax(criteria)
                )
            )
            .AddParameter(ParameterSyntax(@event.Builder.Methods.CountParameter))
            .WithBody(
                BuildHistoryVerificationBody(
                    @event,
                    historyField: @event.Builder.Fields.History,
                    criteriaParameterNames: @event.Builder.Methods.RaisedCriteriaParameters.Select(
                        criteria => criteria.Name
                    ),
                    predicate: BuildRaisedPredicate(@event),
                    descriptionFactory: entry =>
                        BuildRaisedDescription(eventName, parameters, historyEntry, entry)
                )
            )
            .Build();
    }

    private static ExpressionSyntax BuildRaisedPredicate(in ImposterEventMetadata @event)
    {
        var historyEntry = @event.Builder.Fields.HistoryEntry;
        var criteria = @event.Builder.Methods.RaisedCriteriaParameters;
        return @event
            .Core.MatchedParameters.Select(
                (parameter, index) =>
                    (ExpressionSyntax)Matches(
                        criteria[index].Name,
                        historyEntry.ParameterValue(Entry, parameter)
                    )
            )
            .DefaultIfEmpty(True)
            .Aggregate((current, next) => current.And(next));
    }

    internal static MethodDeclarationSyntax BuildHandlerInvokedVerificationMethod(
        in ImposterEventMetadata @event
    )
    {
        var method = @event.Builder.Methods.HandlerInvoked;
        var criteriaName = method.HandlerCriteriaParameter.Name;
        var eventName = @event.Core.Name;
        var parameters = @event.Core.MatchedParameters;
        var handlerInvocationEntry = @event.Builder.Fields.HandlerInvocationEntry;

        return new MethodDeclarationBuilder(
            @event.BuilderInterface.VerificationInterfaceTypeSyntax,
            method.Name
        )
            .WithExplicitInterfaceSpecifier(@event.BuilderInterface.VerificationInterfaceTypeSyntax)
            .AddParameter(ParameterSyntax(method.HandlerCriteriaParameter))
            .AddParameter(ParameterSyntax(@event.Builder.Methods.CountParameter))
            .WithBody(
                BuildHistoryVerificationBody(
                    @event,
                    historyField: @event.Builder.Fields.HandlerInvocations,
                    criteriaParameterNames: [criteriaName],
                    predicate: Matches(criteriaName, handlerInvocationEntry.Handler(Entry)),
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

    // The predicate and the description read the history entry as Entry.
    private static BlockSyntax BuildHistoryVerificationBody(
        in ImposterEventMetadata @event,
        in FieldMetadata historyField,
        IEnumerable<string> criteriaParameterNames,
        ExpressionSyntax predicate,
        Func<ExpressionSyntax, ExpressionSyntax> descriptionFactory
    )
    {
        var countParameterName = @event.Builder.Methods.CountParameter.Name;

        var blockBuilder = new BlockBuilder();
        foreach (var criteriaParameterName in criteriaParameterNames)
        {
            blockBuilder.AddStatement(ThrowIfNull(criteriaParameterName));
        }

        blockBuilder.AddStatement(ThrowIfNull(countParameterName));

        AddEnsureCountMatchesStatements(
            blockBuilder,
            countParameterName,
            IdentifierName(historyField.Name),
            predicate,
            BuildHistoryFailureBody(historyField, descriptionFactory, predicate, countParameterName)
        );

        blockBuilder.AddStatement(ReturnThis);

        return blockBuilder.Build();
    }

    private static InvocationExpressionSyntax Matches(string criteria, ExpressionSyntax value) =>
        IdentifierName(criteria).Dot(IdentifierName("Matches")).Call(Argument(value));

    private static void AddEnsureCountMatchesStatements(
        BlockBuilder blockBuilder,
        string countParameterName,
        ExpressionSyntax history,
        ExpressionSyntax predicate,
        BlockSyntax failureBody
    )
    {
        var actual = IdentifierName("actual");
        var expected = IdentifierName(countParameterName);
        blockBuilder.AddStatement(
            LocalVariableDeclarationSyntax(
                WellKnownTypes.Int,
                actual.Identifier.Text,
                LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0))
            )
        );
        blockBuilder.AddStatement(
            LazyCollectionSyntaxHelper.UnlessNull(
                ForEachStatement(
                    Var,
                    Entry.Identifier,
                    history,
                    Block(
                        IfStatement(
                            predicate,
                            Block(
                                PostfixUnaryExpression(SyntaxKind.PostIncrementExpression, actual)
                                    .ToStatementSyntax()
                            )
                        )
                    )
                )
            )
        );

        // Build descriptions only on failure; successful verification needs no predicate or closure allocation.
        blockBuilder.AddStatement(IfStatement(CountDoesNotMatch(expected, actual), failureBody));
    }

    private static BlockSyntax BuildHistoryFailureBody(
        in FieldMetadata historyField,
        Func<ExpressionSyntax, ExpressionSyntax> descriptionFactory,
        ExpressionSyntax predicateBody,
        string countParameterName
    )
    {
        var stringListType = WellKnownTypes.System.Collections.Generic.List(WellKnownTypes.String);
        var performedInvocations = IdentifierName("performedInvocations");

        return Block(
            LocalVariableDeclarationSyntax(
                Var,
                performedInvocations.Identifier.Text,
                stringListType.New()
            ),
            LazyCollectionSyntaxHelper.UnlessNull(
                ForEachStatement(
                    Var,
                    Entry.Identifier,
                    IdentifierName(historyField.Name),
                    Block(
                        IfStatement(
                            predicateBody,
                            Block(
                                performedInvocations
                                    .Dot(IdentifierName("Add"))
                                    .Call(Argument(descriptionFactory(Entry)))
                                    .ToStatementSyntax()
                            )
                        )
                    )
                )
            ),
            performedInvocations.Dot(IdentifierName("Reverse")).Call().ToStatementSyntax(),
            ThrowVerificationFailed(
                IdentifierName(countParameterName),
                IdentifierName("actual"),
                JoinWithNewLines(performedInvocations)
            )
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
}
