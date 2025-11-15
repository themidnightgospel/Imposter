using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.EventImposter.Metadata;

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

    internal readonly string RaiseInternalName;

    internal readonly string RaiseCoreAsyncName;

    internal readonly string EnumerateHandlersName;

    internal readonly string CountMatchesName;

    internal readonly string EnsureCountMatchesName;

    internal readonly string RaisedVerificationName;

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
        RaiseInternalName = "RaiseInternal";
        RaiseCoreAsyncName = "RaiseCoreAsync";
        EnumerateHandlersName = "EnumerateActiveHandlers";
        CountMatchesName = "CountMatches";
        EnsureCountMatchesName = "EnsureCountMatches";
        RaisedVerificationName = "Raised";
    }

    internal readonly struct SubscribeMethodMetadata
    {
        internal readonly string Name;
        internal readonly ParameterMetadata HandlerParameter;
        internal readonly ParameterMetadata? BaseImplementationParameter;

        internal SubscribeMethodMetadata(in ImposterEventCoreMetadata core)
        {
            Name = "Subscribe";
            HandlerParameter = new ParameterMetadata("handler", core.HandlerTypeSyntax);
            BaseImplementationParameter = core.SupportsBaseImplementation
                ? new ParameterMetadata(
                    "baseImplementation",
                    WellKnownTypes.System.Action.ToNullableType(),
                    LiteralExpression(SyntaxKind.NullLiteralExpression)
                )
                : null;
        }
    }

    internal readonly struct UnsubscribeMethodMetadata
    {
        internal readonly string Name;
        internal readonly ParameterMetadata HandlerParameter;
        internal readonly ParameterMetadata? BaseImplementationParameter;

        internal UnsubscribeMethodMetadata(in ImposterEventCoreMetadata core)
        {
            Name = "Unsubscribe";
            HandlerParameter = new ParameterMetadata("handler", core.HandlerTypeSyntax);
            BaseImplementationParameter = core.SupportsBaseImplementation
                ? new ParameterMetadata(
                    "baseImplementation",
                    WellKnownTypes.System.Action.ToNullableType(),
                    Null
                )
                : null;
        }
    }

    internal readonly struct CallbackMethodMetadata
    {
        internal readonly string Name;
        internal readonly ParameterMetadata CallbackParameter;

        internal CallbackMethodMetadata(in ImposterEventCoreMetadata core)
        {
            Name = "Callback";
            CallbackParameter = new ParameterMetadata("callback", core.HandlerTypeSyntax);
        }
    }

    internal readonly struct InterceptorMethodMetadata
    {
        internal readonly string Name;
        internal readonly ParameterMetadata InterceptorParameter;

        internal InterceptorMethodMetadata(string name, in ImposterEventCoreMetadata core)
        {
            Name = name;
            InterceptorParameter = new ParameterMetadata(
                "interceptor",
                WellKnownTypes.System.ActionOfT(core.HandlerTypeSyntax)
            );
        }
    }

    internal readonly struct CriteriaMethodMetadata
    {
        internal readonly string Name;
        internal readonly ParameterMetadata CriteriaParameter;

        internal CriteriaMethodMetadata(string name, string parameterName, TypeSyntax parameterType)
        {
            Name = name;
            CriteriaParameter = new ParameterMetadata(parameterName, parameterType);
        }
    }

    internal readonly struct HandlerInvokedMethodMetadata
    {
        internal readonly string Name;
        internal readonly ParameterMetadata HandlerCriteriaParameter;

        internal HandlerInvokedMethodMetadata(in ImposterEventCoreMetadata core)
        {
            Name = "HandlerInvoked";
            HandlerCriteriaParameter = new ParameterMetadata(
                "handlerCriteria",
                core.HandlerArgTypeSyntax
            );
        }
    }
}
