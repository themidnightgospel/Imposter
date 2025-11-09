using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImposter.Metadata.InvocationSetup;

internal readonly struct ThrowsMethodMetadata
{
    internal readonly string Name = "Throws";

    internal readonly TypeSyntax ReturnType;

    internal readonly ParameterMetadata ExceptionParameter;

    internal readonly ParameterMetadata ExceptionGeneratorParameter;

    internal readonly NameSyntax InterfaceSyntax;

    internal readonly string InterfaceExceptionParameterName;

    internal readonly string InterfaceExceptionGeneratorParameterName;

    public ThrowsMethodMetadata(
        IParameterNameContextProvider parameterNameContextProvider,
        NameSyntax exceptionGeneratorDelegateSyntax,
        NameSyntax interfaceTypeSyntax,
        NameSyntax continuationInterfaceSyntax)
    {
        InterfaceSyntax = interfaceTypeSyntax;
        ReturnType = continuationInterfaceSyntax;
        var nameContext = parameterNameContextProvider.CreateParameterNameContext();
        InterfaceExceptionParameterName = "exception";
        InterfaceExceptionGeneratorParameterName = "exceptionGenerator";
        ExceptionParameter = new ParameterMetadata(nameContext.Use(InterfaceExceptionParameterName), WellKnownTypes.System.Exception);
        ExceptionGeneratorParameter = new ParameterMetadata(nameContext.Use(InterfaceExceptionGeneratorParameterName), exceptionGeneratorDelegateSyntax);
    }
}
