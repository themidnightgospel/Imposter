using Imposter.CodeGenerator.SyntaxHelpers;

namespace Imposter.CodeGenerator.Features.EventImpersonation.Metadata;

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
                SyntaxFactoryHelper.Null
            )
            : null;
    }
}
