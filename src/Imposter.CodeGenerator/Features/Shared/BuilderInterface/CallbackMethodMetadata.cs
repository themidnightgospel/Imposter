using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.Shared.BuilderInterface;

// A getter or setter builder's Callback(callback). Each accessor's callback is a delegate of its own type.
internal readonly struct CallbackMethodMetadata
{
    internal readonly string Name = "Callback";

    internal readonly TypeSyntax ReturnType;

    internal readonly NameSyntax InterfaceSyntax;

    internal readonly ParameterMetadata CallbackParameter;

    internal CallbackMethodMetadata(
        TypeSyntax returnType,
        NameSyntax interfaceSyntax,
        TypeSyntax callbackType
    )
    {
        ReturnType = returnType;
        InterfaceSyntax = interfaceSyntax;
        CallbackParameter = new ParameterMetadata("callback", callbackType);
    }
}
