using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationSetup;

internal readonly struct CallbackMethodMetadata
{
    internal readonly string Name = "Callback";

    internal readonly TypeSyntax ReturnType;

    internal readonly ParameterMetadata CallbackParameter;

    internal readonly NameSyntax InterfaceSyntax;

    internal readonly string InterfaceCallbackParameterName;

    public CallbackMethodMetadata(
        in ReservedParameterNames reservedParameterNames,
        TypeSyntax returnType,
        NameSyntax interfaceSyntax,
        TypeSyntax callbackTypeSyntax
    )
    {
        var nameContext = reservedParameterNames.CreateNameSet();
        ReturnType = returnType;
        InterfaceSyntax = interfaceSyntax;
        InterfaceCallbackParameterName = "callback";
        CallbackParameter = new ParameterMetadata(
            nameContext.Use(InterfaceCallbackParameterName),
            callbackTypeSyntax
        );
    }
}
