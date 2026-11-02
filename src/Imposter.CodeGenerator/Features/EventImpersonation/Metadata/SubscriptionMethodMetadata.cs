using Imposter.CodeGenerator.SyntaxHelpers;

namespace Imposter.CodeGenerator.Features.EventImpersonation.Metadata;

// Subscribe or Unsubscribe. ActiveHandlersOperation is the EventHandlerCollection method that adds the handler to the
// active handlers or removes it.
internal readonly struct SubscriptionMethodMetadata
{
    internal readonly string Name;
    internal readonly string ActiveHandlersOperation;
    internal readonly ParameterMetadata HandlerParameter;
    internal readonly ParameterMetadata? BaseImplementationParameter;

    internal SubscriptionMethodMetadata(
        string name,
        string activeHandlersOperation,
        in ImposterEventCoreMetadata core
    )
    {
        Name = name;
        ActiveHandlersOperation = activeHandlersOperation;
        HandlerParameter = new ParameterMetadata("handler", core.HandlerTypeSyntax);
        BaseImplementationParameter = core.SupportsBaseImplementation
            ? new ParameterMetadata(
                "baseImplementation",
                WellKnownTypes.System.Action.ToNullableType(),
                SyntaxFactoryHelper.Null
            )
            : null;
    }
}
