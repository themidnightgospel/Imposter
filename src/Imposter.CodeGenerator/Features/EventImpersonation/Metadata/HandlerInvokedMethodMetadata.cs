namespace Imposter.CodeGenerator.Features.EventImpersonation.Metadata;

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
