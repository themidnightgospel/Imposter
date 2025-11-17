using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImposter.Metadata.GetterImposterBuilderInterface;

internal readonly struct CallbackMethodMetadata
{
    internal readonly string Name = "Callback";

    internal readonly TypeSyntax ReturnType;

    internal readonly NameSyntax InterfaceSyntax;

    internal readonly ParameterMetadata CallbackParameter;

    internal CallbackMethodMetadata(TypeSyntax returnType, NameSyntax interfaceSyntax)
    {
        ReturnType = returnType;
        InterfaceSyntax = interfaceSyntax;
        CallbackParameter = new ParameterMetadata("callback", WellKnownTypes.System.Action);
    }
}
