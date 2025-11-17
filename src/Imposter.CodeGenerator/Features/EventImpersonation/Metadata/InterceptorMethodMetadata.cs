using Imposter.CodeGenerator.SyntaxHelpers;

namespace Imposter.CodeGenerator.Features.EventImpersonation.Metadata;

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
