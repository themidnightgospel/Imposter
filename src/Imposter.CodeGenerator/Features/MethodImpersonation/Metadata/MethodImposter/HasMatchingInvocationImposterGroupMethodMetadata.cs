using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImposter.Metadata.MethodImposter;

internal readonly struct HasMatchingInvocationImposterGroupMethodMetadata
{
    internal readonly TypeSyntax ReturnType;

    internal readonly string Name;

    internal readonly string ArgumentsParameterName;

    public HasMatchingInvocationImposterGroupMethodMetadata(
        IParameterNameContextProvider parameterNameContextProvider
    )
    {
        ReturnType = WellKnownTypes.Bool;
        Name = "HasMatchingInvocationImposterGroup";
        var nameContext = parameterNameContextProvider.CreateParameterNameContext();
        ArgumentsParameterName = nameContext.Use("arguments");
    }
}
