using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImposter.Metadata.SetterImposterBuilderInterface;

internal readonly struct SetterUseBaseImplementationMethodMetadata
{
    internal readonly string Name = "UseBaseImplementation";

    internal readonly NameSyntax InterfaceSyntax;

    internal readonly TypeSyntax ReturnType;

    internal SetterUseBaseImplementationMethodMetadata(
        NameSyntax interfaceSyntax,
        TypeSyntax returnType
    )
    {
        InterfaceSyntax = interfaceSyntax;
        ReturnType = returnType;
    }
}
