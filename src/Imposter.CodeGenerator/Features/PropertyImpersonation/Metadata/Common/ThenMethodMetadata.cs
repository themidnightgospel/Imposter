using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImposter.Metadata.Common;

internal readonly struct ThenMethodMetadata
{
    internal readonly string Name = "Then";

    internal readonly TypeSyntax ReturnType;

    internal readonly NameSyntax InterfaceSyntax;

    internal ThenMethodMetadata(NameSyntax interfaceSyntax, TypeSyntax returnType)
    {
        InterfaceSyntax = interfaceSyntax;
        ReturnType = returnType;
    }
}
