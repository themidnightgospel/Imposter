using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.EventImpersonation.Metadata;

internal readonly struct EventImposterBuilderFieldsMetadata
{
    // The subscribed handlers, with snapshots rebuilt only when subscriptions change.
    internal readonly FieldMetadata ActiveHandlers;

    internal readonly FieldMetadata Callbacks;

    internal readonly FieldMetadata History;

    internal readonly FieldMetadata SubscribeHistory;

    internal readonly FieldMetadata UnsubscribeHistory;

    internal readonly FieldMetadata SubscribeInterceptors;

    internal readonly FieldMetadata UnsubscribeInterceptors;

    internal readonly FieldMetadata HandlerInvocations;

    internal readonly FieldMetadata UseBaseImplementation;

    internal readonly FieldMetadata EventDisplayName;

    internal readonly EventHistoryEntryMetadata HistoryEntry;

    internal readonly EventHandlerInvocationEntryMetadata HandlerInvocationEntry;

    internal EventImposterBuilderFieldsMetadata(in ImposterEventCoreMetadata core)
    {
        HistoryEntry = new EventHistoryEntryMetadata(core.MatchedParameters);
        HandlerInvocationEntry = new EventHandlerInvocationEntryMetadata(core);
        var callbackType = WellKnownTypes.Imposter.Abstractions.CallbackCollection(
            core.HandlerTypeSyntax
        );
        var handlerHistoryType = WellKnownTypes.System.Collections.Concurrent.ConcurrentStack(
            core.HandlerTypeSyntax
        );
        var activeHandlersType = WellKnownTypes.Imposter.Abstractions.EventHandlerCollection(
            core.HandlerTypeSyntax
        );
        var historyType = WellKnownTypes.System.Collections.Concurrent.ConcurrentStack(
            HistoryEntry.Type
        );
        var handlerInvocationType = WellKnownTypes.System.Collections.Concurrent.ConcurrentStack(
            HandlerInvocationEntry.Type
        );
        var interceptorType = WellKnownTypes.Imposter.Abstractions.CallbackCollection(
            WellKnownTypes.System.ActionOfT(core.HandlerTypeSyntax)
        );

        var privateReadonlyModifiers = TokenList(
            Token(SyntaxKind.PrivateKeyword),
            Token(SyntaxKind.ReadOnlyKeyword)
        );
        var fieldNames = core.CreateParameterNameSet();

        ActiveHandlers = new FieldMetadata(fieldNames.Use("_activeHandlers"), activeHandlersType);
        Callbacks = new FieldMetadata(fieldNames.Use("_callbacks"), callbackType);
        History = new FieldMetadata(fieldNames.Use("_history"), historyType);
        SubscribeHistory = new FieldMetadata(
            fieldNames.Use("_subscribeHistory"),
            handlerHistoryType
        );
        UnsubscribeHistory = new FieldMetadata(
            fieldNames.Use("_unsubscribeHistory"),
            handlerHistoryType
        );
        SubscribeInterceptors = new FieldMetadata(
            fieldNames.Use("_subscribeInterceptors"),
            interceptorType
        );
        UnsubscribeInterceptors = new FieldMetadata(
            fieldNames.Use("_unsubscribeInterceptors"),
            interceptorType
        );
        HandlerInvocations = new FieldMetadata(
            fieldNames.Use("_handlerInvocations"),
            handlerInvocationType
        );

        UseBaseImplementation = new FieldMetadata(
            fieldNames.Use("_useBaseImplementation"),
            WellKnownTypes.Bool,
            TokenList(Token(SyntaxKind.PrivateKeyword)),
            null
        );

        EventDisplayName = new FieldMetadata(
            fieldNames.Use("_eventDisplayName"),
            WellKnownTypes.String,
            privateReadonlyModifiers,
            core.DisplayName.StringLiteral()
        );
    }
}
