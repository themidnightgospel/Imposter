using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImposter.Metadata.InvocationSetup;

internal readonly struct ThenMethodMetadata
{
    internal readonly string Name = "Then";

    internal readonly TypeSyntax ReturnType;

    internal readonly NameSyntax InterfaceSyntax;

    public ThenMethodMetadata(NameSyntax interfaceSyntax, NameSyntax continuationInterfaceSyntax)
    {
        ReturnType = interfaceSyntax;
        InterfaceSyntax = continuationInterfaceSyntax;
    }
}
