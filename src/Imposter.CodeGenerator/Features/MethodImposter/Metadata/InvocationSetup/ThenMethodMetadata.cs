using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImposter.Metadata.InvocationSetup;

internal readonly struct ThenMethodMetadata
{
    internal readonly string Name = "Then";

    internal readonly TypeSyntax ReturnType;

    public ThenMethodMetadata(NameSyntax interfaceSyntax)
    {
        ReturnType = interfaceSyntax;
    }
}
