using Imposter.CodeGenerator.SyntaxHelpers;

namespace Imposter.CodeGenerator.Features.EventImpersonation.Metadata;

// Subscribe or Unsubscribe. DelegateOperation is the System.Delegate method that adds the handler to the active
// handlers or removes it.
internal readonly struct SubscriptionMethodMetadata
{
    internal readonly string Name;
    internal readonly string DelegateOperation;
    internal readonly ParameterMetadata HandlerParameter;
    internal readonly ParameterMetadata? BaseImplementationParameter;

    internal SubscriptionMethodMetadata(
        string name,
        string delegateOperation,
        in ImposterEventCoreMetadata core
    )
    {
        Name = name;
        DelegateOperation = delegateOperation;
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
