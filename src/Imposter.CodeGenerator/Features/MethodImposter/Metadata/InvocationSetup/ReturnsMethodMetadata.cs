using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImposter.Metadata.InvocationSetup;

internal readonly struct ReturnsMethodMetadata
{
    internal readonly string Name = "Returns";

    internal readonly ParameterMetadata ValueParameter;

    internal readonly ParameterMetadata ResultGeneratorParameter;

    internal readonly TypeSyntax ReturnType;

    internal readonly NameSyntax InterfaceSyntax;

    internal readonly string InterfaceValueParameterName;

    internal readonly string InterfaceResultGeneratorParameterName;

    public ReturnsMethodMetadata(
        IParameterNameContextProvider parameterNameContextProvider,
        TypeSyntax imposterMethodReturnType,
        NameSyntax interfaceSyntax,
        NameSyntax continuationInterfaceSyntax,
        TypeSyntax methodDelegateTypeSyntax)
    {
        InterfaceSyntax = interfaceSyntax;
        ReturnType = continuationInterfaceSyntax;
        InterfaceValueParameterName = "value";
        InterfaceResultGeneratorParameterName = "resultGenerator";
        var nameContext = parameterNameContextProvider.CreateParameterNameContext();
        ValueParameter = new ParameterMetadata(nameContext.Use(InterfaceValueParameterName), imposterMethodReturnType);
        ResultGeneratorParameter = new ParameterMetadata(nameContext.Use(InterfaceResultGeneratorParameterName), methodDelegateTypeSyntax);
    }
}
