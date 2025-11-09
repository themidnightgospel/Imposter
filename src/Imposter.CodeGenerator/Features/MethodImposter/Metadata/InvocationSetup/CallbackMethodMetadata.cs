using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImposter.Metadata.InvocationSetup;

internal readonly struct CallbackMethodMetadata
{
    internal readonly string Name = "Callback";

    internal readonly TypeSyntax ReturnType;

    internal readonly ParameterMetadata CallbackParameter;

    internal readonly NameSyntax InterfaceSyntax;

    internal readonly string InterfaceCallbackParameterName;

    public CallbackMethodMetadata(
        IParameterNameContextProvider parameterNameContextProvider,
        NameSyntax interfaceSyntax,
        TypeSyntax callbackTypeSyntax)
    {
        var nameContext = parameterNameContextProvider.CreateParameterNameContext();
        InterfaceSyntax = interfaceSyntax;
        ReturnType = interfaceSyntax;
        InterfaceCallbackParameterName = "callback";
        CallbackParameter = new ParameterMetadata(nameContext.Use(InterfaceCallbackParameterName), callbackTypeSyntax);
    }
}
