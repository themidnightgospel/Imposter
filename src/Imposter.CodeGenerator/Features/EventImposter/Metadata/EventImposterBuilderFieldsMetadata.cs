using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.EventImposter.Metadata;

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

    internal EventImposterBuilderFieldsMetadata(in ImposterEventCoreMetadata core)
    {
        var handlerQueueType = WellKnownTypes.System.Collections.Concurrent.ConcurrentQueue(core.HandlerTypeSyntax);
        var handlerCountsType = WellKnownTypes.System.Collections.Concurrent.ConcurrentDictionary(
            core.HandlerTypeSyntax,
            WellKnownTypes.Int);
        var historyQueueType = WellKnownTypes.System.Collections.Concurrent.ConcurrentQueue(BuildHistoryEntryType(core));
        var handlerInvocationType = WellKnownTypes.System.Collections.Concurrent.ConcurrentQueue(BuildHandlerInvocationEntryType(core));
        var interceptorQueueType = WellKnownTypes.System.Collections.Concurrent.ConcurrentQueue(
            WellKnownTypes.System.ActionOfT(core.HandlerTypeSyntax));

        HandlerOrder = new FieldMetadata("_handlerOrder", handlerQueueType);
        HandlerCounts = new FieldMetadata("_handlerCounts", handlerCountsType);
        Callbacks = new FieldMetadata("_callbacks", handlerQueueType);
        History = new FieldMetadata("_history", historyQueueType);
        SubscribeHistory = new FieldMetadata("_subscribeHistory", handlerQueueType);
        UnsubscribeHistory = new FieldMetadata("_unsubscribeHistory", handlerQueueType);
        SubscribeInterceptors = new FieldMetadata("_subscribeInterceptors", interceptorQueueType);
        UnsubscribeInterceptors = new FieldMetadata("_unsubscribeInterceptors", interceptorQueueType);
        HandlerInvocations = new FieldMetadata("_handlerInvocations", handlerInvocationType);
    }

    private static TypeSyntax BuildHistoryEntryType(in ImposterEventCoreMetadata core)
    {
        if (core.Parameters.Length == 0)
        {
            return PredefinedType(Token(SyntaxKind.BoolKeyword));
        }

        var tupleElements = core.Parameters
            .Select(parameter => TupleElement(parameter.TypeSyntax).WithIdentifier(Identifier(parameter.Name)));

        return TupleType(SeparatedList(tupleElements));
    }

    private static TupleTypeSyntax BuildHandlerInvocationEntryType(in ImposterEventCoreMetadata core)
    {
        var elements = new List<TupleElementSyntax>
        {
            TupleElement(core.HandlerTypeSyntax).WithIdentifier(Identifier("Handler"))
        };

        elements.AddRange(core.Parameters.Select(parameter =>
            TupleElement(parameter.TypeSyntax).WithIdentifier(Identifier(parameter.Name))));

        return TupleType(SeparatedList(elements));
    }
}
