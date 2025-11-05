using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImposter.Metadata.InvocationSetup;

internal readonly struct ThrowsAsyncMethodMetadata
{
    internal readonly string Name = "ThrowsAsync";

    internal readonly TypeSyntax ReturnType;

    internal readonly ParameterMetadata ExceptionParameter;

    internal ThrowsAsyncMethodMetadata(
        IParameterNameContextProvider parameterNameContextProvider,
        NameSyntax interfaceTypeSyntax)
    {
        ReturnType = interfaceTypeSyntax;
        var nameContext = parameterNameContextProvider.CreateParameterNameContext();
        ExceptionParameter = new ParameterMetadata(nameContext.Use("exception"), WellKnownTypes.System.Exception);
    }
}
