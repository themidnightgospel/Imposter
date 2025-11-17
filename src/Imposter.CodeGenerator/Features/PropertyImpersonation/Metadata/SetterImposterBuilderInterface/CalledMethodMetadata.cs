using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata.SetterImposterBuilderInterface;

internal readonly struct CalledMethodMetadata
{
    internal readonly string Name = "Called";

    internal readonly TypeSyntax ReturnType;

    internal readonly ParameterMetadata CountParameter;

    internal readonly string SetterInvocationCountVariableName = "setterInvocationCount";

    public CalledMethodMetadata()
    {
        ReturnType = WellKnownTypes.Void;
        CountParameter = new ParameterMetadata("count", WellKnownTypes.Imposter.Abstractions.Count);
    }
}
