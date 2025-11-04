using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImposter.Metadata.InvocationSetup;

internal readonly struct ReturnsMethodMetadata
{
    internal readonly string Name = "Returns";

    internal readonly ParameterMetadata ValueParameter;

    internal readonly ParameterMetadata ResultGeneratorParameter;

    internal readonly TypeSyntax ReturnType;

    public ReturnsMethodMetadata(
        IParameterNameContextProvider parameterNameContextProvider,
        TypeSyntax imposterMethodReturnType,
        NameSyntax interfaceSyntax,
        TypeSyntax methodDelegateTypeSyntax)
    {
        ReturnType = interfaceSyntax;
        var nameContext = parameterNameContextProvider.CreateParameterNameContext();
        ValueParameter = new ParameterMetadata(nameContext.Use("value"), imposterMethodReturnType);
        ResultGeneratorParameter = new ParameterMetadata(nameContext.Use("resultGenerator"), methodDelegateTypeSyntax);
    }
}
