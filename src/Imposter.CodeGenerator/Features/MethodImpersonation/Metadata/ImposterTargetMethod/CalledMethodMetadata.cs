using Imposter.CodeGenerator.SyntaxHelpers;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;

internal readonly struct CalledMethodMetadata
{
    internal const string Name = "Called";

    internal readonly ParameterMetadata CountParameter;

    public CalledMethodMetadata()
    {
        CountParameter = new ParameterMetadata("count", WellKnownTypes.Imposter.Abstractions.Count);
    }
}
