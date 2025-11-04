using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImposter.Metadata.InvocationSetup;

internal readonly struct ThrowsMethodMetadata
{
    internal readonly string Name = "Throws";

    internal readonly TypeSyntax ReturnType;

    internal readonly ParameterMetadata ExceptionParameter;

    internal readonly ParameterMetadata ExceptionGeneratorParameter;

    public ThrowsMethodMetadata(
        IParameterNameContextProvider parameterNameContextProvider,
        NameSyntax exceptionGeneratorDelegateSyntax,
        TypeSyntax interfaceTypeSyntax)
    {
        ReturnType = interfaceTypeSyntax;
        var nameContext = parameterNameContextProvider.CreateParameterNameContext();
        ExceptionParameter = new ParameterMetadata(nameContext.Use("exception"), WellKnownTypes.System.Exception);
        ExceptionGeneratorParameter = new ParameterMetadata(nameContext.Use("exceptionGenerator"), exceptionGeneratorDelegateSyntax);
    }
}
