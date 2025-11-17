using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.EventImpersonation.Metadata;

internal readonly struct EventImposterBuilderFieldsMetadata
{
    internal readonly FieldMetadata HandlerOrder;

    internal readonly FieldMetadata HandlerCounts;

    internal readonly FieldMetadata Callbacks;

    internal readonly FieldMetadata History;

    internal readonly FieldMetadata SubscribeHistory;

    internal readonly FieldMetadata UnsubscribeHistory;

    internal readonly FieldMetadata SubscribeInterceptors;

    internal readonly FieldMetadata UnsubscribeInterceptors;

    internal readonly FieldMetadata HandlerInvocations;

    internal readonly FieldMetadata UseBaseImplementation;

    internal readonly FieldMetadata EventDisplayName;

    internal EventImposterBuilderFieldsMetadata(in ImposterEventCoreMetadata core)
    {
        var handlerQueueType = WellKnownTypes.System.Collections.Concurrent.ConcurrentQueue(
            core.HandlerTypeSyntax
        );
        var handlerCountsType = WellKnownTypes.System.Collections.Concurrent.ConcurrentDictionary(
            core.HandlerTypeSyntax,
            WellKnownTypes.Int
        );
        var historyQueueType = WellKnownTypes.System.Collections.Concurrent.ConcurrentQueue(
            BuildHistoryEntryType(core)
        );
        var handlerInvocationType = WellKnownTypes.System.Collections.Concurrent.ConcurrentQueue(
            BuildHandlerInvocationEntryType(core)
        );
        var interceptorQueueType = WellKnownTypes.System.Collections.Concurrent.ConcurrentQueue(
            WellKnownTypes.System.ActionOfT(core.HandlerTypeSyntax)
        );

        var privateReadonlyModifiers = TokenList(
            Token(SyntaxKind.PrivateKeyword),
            Token(SyntaxKind.ReadOnlyKeyword)
        );

        HandlerOrder = new FieldMetadata(
            "_handlerOrder",
            handlerQueueType,
            privateReadonlyModifiers,
            handlerQueueType.New()
        );
        HandlerCounts = new FieldMetadata(
            "_handlerCounts",
            handlerCountsType,
            privateReadonlyModifiers,
            handlerCountsType.New()
        );
        Callbacks = new FieldMetadata(
            "_callbacks",
            handlerQueueType,
            privateReadonlyModifiers,
            handlerQueueType.New()
        );
        History = new FieldMetadata(
            "_history",
            historyQueueType,
            privateReadonlyModifiers,
            historyQueueType.New()
        );
        SubscribeHistory = new FieldMetadata(
            "_subscribeHistory",
            handlerQueueType,
            privateReadonlyModifiers,
            handlerQueueType.New()
        );
        UnsubscribeHistory = new FieldMetadata(
            "_unsubscribeHistory",
            handlerQueueType,
            privateReadonlyModifiers,
            handlerQueueType.New()
        );
        SubscribeInterceptors = new FieldMetadata(
            "_subscribeInterceptors",
            interceptorQueueType,
            privateReadonlyModifiers,
            interceptorQueueType.New()
        );
        UnsubscribeInterceptors = new FieldMetadata(
            "_unsubscribeInterceptors",
            interceptorQueueType,
            privateReadonlyModifiers,
            interceptorQueueType.New()
        );
        HandlerInvocations = new FieldMetadata(
            "_handlerInvocations",
            handlerInvocationType,
            privateReadonlyModifiers,
            handlerInvocationType.New()
        );

        UseBaseImplementation = new FieldMetadata(
            "_useBaseImplementation",
            WellKnownTypes.Bool,
            TokenList(Token(SyntaxKind.PrivateKeyword)),
            null
        );

        EventDisplayName = new FieldMetadata(
            "_eventDisplayName",
            PredefinedType(Token(SyntaxKind.StringKeyword)),
            privateReadonlyModifiers,
            core.DisplayName.StringLiteral()
        );
    }

    private static TypeSyntax BuildHistoryEntryType(in ImposterEventCoreMetadata core)
    {
        if (core.Parameters.Length == 0)
        {
            return WellKnownTypes.Bool;
        }

        if (core.Parameters.Length == 1)
        {
            return core.Parameters[0].TypeSyntax;
        }

        var tupleElements = core.Parameters.Select(parameter =>
            TupleElement(parameter.TypeSyntax).WithIdentifier(Identifier(parameter.Name))
        );

        return TupleType(SeparatedList(tupleElements));
    }

    private static TypeSyntax BuildHandlerInvocationEntryType(in ImposterEventCoreMetadata core)
    {
        if (core.Parameters.Length == 0)
        {
            return core.HandlerTypeSyntax;
        }

        var elements = new List<TupleElementSyntax>
        {
            TupleElement(core.HandlerTypeSyntax).WithIdentifier(Identifier("Handler")),
        };

        elements.AddRange(
            core.Parameters.Select(parameter =>
                TupleElement(parameter.TypeSyntax).WithIdentifier(Identifier(parameter.Name))
            )
        );

        return TupleType(SeparatedList(elements));
    }
}
