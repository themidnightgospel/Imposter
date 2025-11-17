using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImposter.Metadata.GetterImposterBuilderInterface;

internal readonly struct GetterUseBaseImplementationMethodMetadata
{
    internal readonly string Name = "UseBaseImplementation";

    internal readonly NameSyntax InterfaceSyntax;

    internal readonly TypeSyntax ReturnType;

    internal GetterUseBaseImplementationMethodMetadata(
        NameSyntax interfaceSyntax,
        TypeSyntax returnType
    )
    {
        InterfaceSyntax = interfaceSyntax;
        ReturnType = returnType;
    }
}
