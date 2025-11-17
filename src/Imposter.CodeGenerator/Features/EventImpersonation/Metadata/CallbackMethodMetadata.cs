namespace Imposter.CodeGenerator.Features.EventImpersonation.Metadata;

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
