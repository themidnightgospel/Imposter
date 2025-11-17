using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.EventImpersonation.Metadata;

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
