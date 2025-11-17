using System.Linq;
using Imposter.CodeGenerator.SyntaxHelpers;

namespace Imposter.CodeGenerator.Features.EventImpersonation.Metadata;

internal readonly struct EventImposterBuilderMethodsMetadata
{
    internal readonly SubscribeMethodMetadata Subscribe;

    internal readonly UnsubscribeMethodMetadata Unsubscribe;

    internal readonly CallbackMethodMetadata Callback;

    internal readonly InterceptorMethodMetadata OnSubscribe;

    internal readonly InterceptorMethodMetadata OnUnsubscribe;

    internal readonly CriteriaMethodMetadata Subscribed;

    internal readonly CriteriaMethodMetadata Unsubscribed;

    internal readonly HandlerInvokedMethodMetadata HandlerInvoked;

    internal readonly ParameterMetadata CountParameter;

    internal readonly ParameterMetadata[] RaisedCriteriaParameters;

    internal readonly MethodMetadata RaiseInternal;

    internal readonly MethodMetadata RaiseCoreAsync;

    internal readonly MethodMetadata EnumerateHandlers;

    internal readonly MethodMetadata EnsureCountMatches;

    internal readonly MethodMetadata RaisedVerification;

    internal EventImposterBuilderMethodsMetadata(in ImposterEventCoreMetadata core)
    {
        Subscribe = new SubscribeMethodMetadata(core);
        Unsubscribe = new UnsubscribeMethodMetadata(core);
        Callback = new CallbackMethodMetadata(core);
        OnSubscribe = new InterceptorMethodMetadata("OnSubscribe", core);
        OnUnsubscribe = new InterceptorMethodMetadata("OnUnsubscribe", core);
        Subscribed = new CriteriaMethodMetadata(
            "Subscribed",
            "criteria",
            core.HandlerArgTypeSyntax
        );
        Unsubscribed = new CriteriaMethodMetadata(
            "Unsubscribed",
            "criteria",
            core.HandlerArgTypeSyntax
        );
        HandlerInvoked = new HandlerInvokedMethodMetadata(core);
        CountParameter = new ParameterMetadata("count", WellKnownTypes.Imposter.Abstractions.Count);

        RaisedCriteriaParameters = core
            .Parameters.Select(parameter => new ParameterMetadata(
                $"{parameter.Name}Criteria",
                parameter.ArgTypeSyntax
            ))
            .ToArray();

        RaiseInternal = new MethodMetadata("RaiseInternal", WellKnownTypes.Void);
        RaiseCoreAsync = new MethodMetadata(
            "RaiseCoreAsync",
            WellKnownTypes.System.Threading.Tasks.Task
        );
        EnumerateHandlers = new MethodMetadata("EnumerateActiveHandlers", WellKnownTypes.Void);
        EnsureCountMatches = new MethodMetadata("EnsureCountMatches", WellKnownTypes.Void);
        RaisedVerification = new MethodMetadata("Raised", WellKnownTypes.Void);
    }
}
