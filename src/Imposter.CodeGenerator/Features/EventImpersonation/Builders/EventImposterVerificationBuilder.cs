using System;
using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.EventImpersonation.Metadata;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.Features.Shared.Builders.FormatValueMethodBuilder;
using static Imposter.CodeGenerator.Features.Shared.Builders.VerificationFailedBuilder;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.EventImpersonation.Builders;

internal static class EventImposterVerificationBuilder
{
    // A history entry, as the verification predicates and failure-only descriptions read it.
    private static IdentifierNameSyntax Entry(in ImposterEventMetadata @event) =>
        IdentifierName(@event.Locals["entry"]);

    internal static MethodDeclarationSyntax BuildSubscribedVerificationMethod(
        in ImposterEventMetadata @event,
        bool withReason = false
    ) =>
        BuildSubscriptionVerificationMethod(
            @event,
            @event.Builder.Methods.Subscribed,
            @event.Builder.Fields.SubscribeHistory,
            "subscribed",
            withReason
        );

    internal static MethodDeclarationSyntax BuildUnsubscribedVerificationMethod(
        in ImposterEventMetadata @event,
        bool withReason = false
    ) =>
        BuildSubscriptionVerificationMethod(
            @event,
            @event.Builder.Methods.Unsubscribed,
            @event.Builder.Fields.UnsubscribeHistory,
            "unsubscribed",
            withReason
        );

    private static MethodDeclarationSyntax BuildSubscriptionVerificationMethod(
        in ImposterEventMetadata @event,
        in CriteriaMethodMetadata method,
        in FieldMetadata history,
        string action,
        bool withReason
    )
    {
        var eventName = @event.Core.Name;

        return new MethodDeclarationBuilder(
            @event.BuilderInterface.VerificationInterfaceTypeSyntax,
            method.Name
        )
            .WithExplicitInterfaceSpecifier(@event.BuilderInterface.VerificationInterfaceTypeSyntax)
            .AddParameter(ParameterSyntax(method.CriteriaParameter))
            .AddParameter(ParameterSyntax(@event.Builder.Methods.CountParameter))
            .AddParameter(
                withReason
                    ? ParameterSyntax(
                        WellKnownTypes.String.ToNullableType(),
                        @event.Locals["because"]
                    )
                    : null
            )
            .WithBody(
                BuildSubscriptionVerificationBody(
                    @event,
                    method,
                    history,
                    eventName,
                    action,
                    withReason
                )
            )
            .Build();
    }

    private static BlockSyntax BuildSubscriptionVerificationBody(
        in ImposterEventMetadata @event,
        in CriteriaMethodMetadata method,
        in FieldMetadata historyField,
        string eventName,
        string action,
        bool withReason
    )
    {
        var history = IdentifierName(historyField.Name);
        var snapshot = IdentifierName(@event.Locals["snapshot"]);
        var cursor = IdentifierName(@event.Locals["cursor"]);
        var actual = IdentifierName(@event.Locals["actual"]);
        var count = IdentifierName(@event.Builder.Methods.CountParameter.Name);
        var predicate = Matches(
            method.CriteriaParameter.Name,
            Entry(@event).Dot(IdentifierName("Value"))
        );
        var loopCount = WhileStatement(
            cursor.Dot(IdentifierName("MoveNext")).Call(),
            Block(
                LocalVariableDeclarationSyntax(
                    Var,
                    Entry(@event).Identifier.Text,
                    cursor.Dot(IdentifierName("Current"))
                ),
                IfStatement(
                    predicate,
                    PostfixUnaryExpression(SyntaxKind.PostIncrementExpression, actual)
                        .ToStatementSyntax()
                )
            )
        );
        var loopCover = WhileStatement(
            cursor.Dot(IdentifierName("MoveNext")).Call(),
            Block(
                LocalVariableDeclarationSyntax(
                    Var,
                    Entry(@event).Identifier.Text,
                    cursor.Dot(IdentifierName("Current"))
                ),
                IfStatement(
                    predicate,
                    historyField
                        .Type.Dot(IdentifierName("MarkVerified"))
                        .Call(Argument(Entry(@event)))
                        .ToStatementSyntax()
                )
            )
        );
        return Block(
            ThrowIfNull(method.CriteriaParameter.Name),
            ThrowIfNull(@event.Builder.Methods.CountParameter.Name),
            LocalVariableDeclarationSyntax(Var, @event.Locals["actual"], NumericLiteral(0)),
            LocalVariableDeclarationSyntax(
                Var,
                @event.Locals["snapshot"],
                ConditionalExpression(
                    history.IsNull(),
                    DefaultExpression(
                        QualifiedName((NameSyntax)historyField.Type, IdentifierName("Enumerator"))
                    ),
                    history.Dot(IdentifierName("GetEnumerator")).Call()
                )
            ),
            LocalVariableDeclarationSyntax(Var, @event.Locals["cursor"], snapshot),
            loopCount,
            IfStatement(
                CountDoesNotMatch(count, actual),
                BuildHistoryFailureBody(
                    @event,
                    historyField,
                    handler =>
                        BuildSubscriptionDescription(
                            eventName,
                            action,
                            handler.Dot(IdentifierName("Value"))
                        ),
                    @event.Builder.Methods.CountParameter.Name,
                    eventName,
                    Expected(eventName, action, [("handler", method.CriteriaParameter.Name)]),
                    withReason
                )
            ),
            cursor.Assign(snapshot).ToStatementSyntax(),
            loopCover,
            ReturnThis
        );
    }

    internal static MethodDeclarationSyntax BuildRaisedVerificationMethod(
        in ImposterEventMetadata @event,
        bool withReason = false
    )
    {
        var eventName = @event.Core.Name;
        var parameters = @event.Core.MatchedParameters;
        var historyEntry = @event.Builder.Fields.HistoryEntry;
        var criteriaParameters = @event.Builder.Methods.RaisedCriteriaParameters;

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
            .AddParameter(
                withReason
                    ? ParameterSyntax(
                        WellKnownTypes.String.ToNullableType(),
                        @event.Locals["because"]
                    )
                    : null
            )
            .WithBody(
                BuildHistoryVerificationBody(
                    @event,
                    withReason,
                    historyField: @event.Builder.Fields.History,
                    criteriaParameters: criteriaParameters.Select(
                        (criteria, index) => (parameters[index].Name, criteria.Name)
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
        var entry = Entry(@event);
        var criteria = @event.Builder.Methods.RaisedCriteriaParameters;
        return @event
            .Core.MatchedParameters.Select(
                (parameter, index) =>
                    (ExpressionSyntax)Matches(
                        criteria[index].Name,
                        historyEntry.ParameterValue(entry, parameter)
                    )
            )
            .DefaultIfEmpty(True)
            .Aggregate((current, next) => current.And(next));
    }

    internal static MethodDeclarationSyntax BuildHandlerInvokedVerificationMethod(
        in ImposterEventMetadata @event,
        bool withReason = false
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
            .AddParameter(
                withReason
                    ? ParameterSyntax(
                        WellKnownTypes.String.ToNullableType(),
                        @event.Locals["because"]
                    )
                    : null
            )
            .WithBody(
                BuildHistoryVerificationBody(
                    @event,
                    withReason,
                    historyField: @event.Builder.Fields.HandlerInvocations,
                    criteriaParameters: [("handler", criteriaName)],
                    predicate: Matches(criteriaName, handlerInvocationEntry.Handler(Entry(@event))),
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
        bool withReason,
        in FieldMetadata historyField,
        IEnumerable<(string Name, string CriteriaParameterName)> criteriaParameters,
        ExpressionSyntax predicate,
        Func<ExpressionSyntax, ExpressionSyntax> descriptionFactory
    )
    {
        var countParameterName = @event.Builder.Methods.CountParameter.Name;

        var blockBuilder = new BlockBuilder();
        foreach (var (_, criteriaParameterName) in criteriaParameters)
        {
            blockBuilder.AddStatement(ThrowIfNull(criteriaParameterName));
        }

        blockBuilder.AddStatement(ThrowIfNull(countParameterName));

        AddEnsureCountMatchesStatements(
            @event,
            blockBuilder,
            countParameterName,
            IdentifierName(historyField.Name),
            predicate,
            BuildHistoryFailureBody(
                @event,
                historyField,
                descriptionFactory,
                countParameterName,
                @event.Core.Name,
                Expected(
                    @event.Core.Name,
                    historyField.Name == @event.Builder.Fields.History.Name
                        ? "raised"
                        : "handler invoked",
                    criteriaParameters
                ),
                withReason
            )
        );

        blockBuilder.AddStatement(ReturnThis);

        return blockBuilder.Build();
    }

    private static InvocationExpressionSyntax Matches(string criteria, ExpressionSyntax value) =>
        IdentifierName(criteria).Dot(IdentifierName("Matches")).Call(Argument(value));

    private static void AddEnsureCountMatchesStatements(
        in ImposterEventMetadata @event,
        BlockBuilder blockBuilder,
        string countParameterName,
        ExpressionSyntax history,
        ExpressionSyntax predicate,
        BlockSyntax failureBody
    )
    {
        var actual = IdentifierName(@event.Locals["actual"]);
        var expected = IdentifierName(countParameterName);
        blockBuilder.AddStatement(
            LocalVariableDeclarationSyntax(
                WellKnownTypes.Int,
                actual.Identifier.Text,
                NumericLiteral(0)
            )
        );
        blockBuilder.AddStatement(
            LazyCollectionSyntaxHelper.UnlessNull(
                ForEachStatement(
                    Var,
                    Entry(@event).Identifier,
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
        in ImposterEventMetadata @event,
        in FieldMetadata historyField,
        Func<ExpressionSyntax, ExpressionSyntax> descriptionFactory,
        string countParameterName,
        string member,
        ExpressionSyntax expected,
        bool withReason
    )
    {
        var stringListType = WellKnownTypes.System.Collections.Generic.List(WellKnownTypes.String);
        var performedInvocations = IdentifierName(@event.Locals["performedInvocations"]);

        return Block(
            LocalVariableDeclarationSyntax(
                Var,
                performedInvocations.Identifier.Text,
                stringListType.New()
            ),
            LazyCollectionSyntaxHelper.UnlessNull(
                ForEachStatement(
                    Var,
                    Entry(@event).Identifier,
                    IdentifierName(historyField.Name),
                    Block(
                        performedInvocations
                            .Dot(IdentifierName("Add"))
                            .Call(Argument(descriptionFactory(Entry(@event))))
                            .ToStatementSyntax()
                    )
                )
            ),
            performedInvocations.Dot(IdentifierName("Reverse")).Call().ToStatementSyntax(),
            ThrowStatement(
                WellKnownTypes.Imposter.Abstractions.VerificationFailedException.New(
                    ArgumentListSyntax([
                        Argument(member.StringLiteral()),
                        Argument(expected),
                        Argument(IdentifierName(countParameterName)),
                        Argument(IdentifierName(@event.Locals["actual"])),
                        Argument(JoinWithNewLines(performedInvocations)),
                        Argument(withReason ? IdentifierName(@event.Locals["because"]) : Null),
                        Argument(
                            ConditionalExpression(
                                BinaryExpression(
                                    SyntaxKind.EqualsExpression,
                                    performedInvocations.Dot(IdentifierName("Count")),
                                    NumericLiteral(0)
                                ),
                                WellKnownTypes
                                    .Imposter.Abstractions.DiagnosticValue.Dot(
                                        IdentifierName("OtherInteractions")
                                    )
                                    .Call(
                                        Argument(
                                            IdentifierName(InvocationStepNames.OwnerPropertyName)
                                        )
                                    ),
                                Null
                            )
                        ),
                    ])
                )
            )
        );
    }

    private static BinaryExpressionSyntax Expected(
        string member,
        string action,
        IEnumerable<(string Name, string CriteriaParameterName)> criteria
    )
    {
        ExpressionSyntax result = BuildActionDescription(member, action).Add("(".StringLiteral());
        var first = true;
        foreach (var (name, criteriaParameterName) in criteria)
        {
            result = result
                .Add(((first ? "" : ", ") + name + ": ").StringLiteral())
                .Add(IdentifierName(criteriaParameterName).Dot(IdentifierName("ToString")).Call());
            first = false;
        }
        return result.Add(")".StringLiteral());
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
