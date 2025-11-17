using Imposter.CodeGenerator.SyntaxHelpers;

namespace Imposter.CodeGenerator.Features.EventImpersonation.Metadata;

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
                SyntaxFactoryHelper.Null
            )
            : null;
    }
}
