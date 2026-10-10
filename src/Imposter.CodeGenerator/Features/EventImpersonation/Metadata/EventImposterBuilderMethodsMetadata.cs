using System.Linq;
using Imposter.CodeGenerator.SyntaxHelpers;

namespace Imposter.CodeGenerator.Features.EventImpersonation.Metadata;

internal readonly struct EventImposterBuilderMethodsMetadata
{
    internal readonly SubscriptionMethodMetadata Subscribe;

    internal readonly SubscriptionMethodMetadata Unsubscribe;

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

    internal readonly EventRaiseLocalNames RaiseLocalNames;

    internal readonly MethodMetadata EnumerateHandlers;

    internal readonly MethodMetadata EnsureCountMatches;

    internal readonly MethodMetadata RaisedVerification;

    internal EventImposterBuilderMethodsMetadata(in ImposterEventCoreMetadata core)
    {
        Subscribe = new SubscriptionMethodMetadata("Subscribe", "Combine", core);
        Unsubscribe = new SubscriptionMethodMetadata("Unsubscribe", "Remove", core);
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
            .MatchedParameters.Select(parameter => new ParameterMetadata(
                $"{parameter.Name}Criteria",
                parameter.ArgTypeSyntax
            ))
            .ToArray();

        var raiseMethodNames = core.CreateParameterNameSet();
        RaiseInternal = new MethodMetadata(
            raiseMethodNames.Use("RaiseInternal"),
            WellKnownTypes.Void
        );
        RaiseCoreAsync = new MethodMetadata(
            raiseMethodNames.Use("RaiseCoreAsync"),
            WellKnownTypes.System.Threading.Tasks.Task
        );
        RaiseLocalNames = new EventRaiseLocalNames(core);
        EnumerateHandlers = new MethodMetadata(
            raiseMethodNames.Use("EnumerateActiveHandlers"),
            WellKnownTypes.Void
        );
        EnsureCountMatches = new MethodMetadata("EnsureCountMatches", WellKnownTypes.Void);
        RaisedVerification = new MethodMetadata("Raised", WellKnownTypes.Void);
    }
}
