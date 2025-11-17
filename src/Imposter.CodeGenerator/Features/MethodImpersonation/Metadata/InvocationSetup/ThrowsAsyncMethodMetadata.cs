using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationSetup;

internal readonly struct ThrowsAsyncMethodMetadata
{
    internal readonly string Name = "ThrowsAsync";

    internal readonly TypeSyntax ReturnType;

    internal readonly ParameterMetadata ExceptionParameter;

    internal readonly NameSyntax InterfaceSyntax;

    internal readonly string InterfaceExceptionParameterName;

    internal ThrowsAsyncMethodMetadata(
        IParameterNameContextProvider parameterNameContextProvider,
        NameSyntax interfaceTypeSyntax,
        NameSyntax continuationInterfaceSyntax
    )
    {
        InterfaceSyntax = interfaceTypeSyntax;
        ReturnType = continuationInterfaceSyntax;
        var nameContext = parameterNameContextProvider.CreateParameterNameContext();
        InterfaceExceptionParameterName = "exception";
        ExceptionParameter = new ParameterMetadata(
            nameContext.Use(InterfaceExceptionParameterName),
            WellKnownTypes.System.Exception
        );
    }
}
