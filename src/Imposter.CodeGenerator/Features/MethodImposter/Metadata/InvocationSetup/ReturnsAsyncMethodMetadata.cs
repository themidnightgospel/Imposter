using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImposter.Metadata.InvocationSetup;

internal readonly struct ReturnsAsyncMethodMetadata
{
    internal readonly string Name = "ReturnsAsync";

    internal readonly TypeSyntax ReturnType;

    internal readonly ParameterMetadata ValueParameter;

    internal ReturnsAsyncMethodMetadata(
        IParameterNameContextProvider parameterNameContextProvider,
        NameSyntax interfaceSyntax,
        TypeSyntax valueParameterType)
    {
        ReturnType = interfaceSyntax;
        var nameContext = parameterNameContextProvider.CreateParameterNameContext();
        ValueParameter = new ParameterMetadata(nameContext.Use("value"), valueParameterType);
    }
}
