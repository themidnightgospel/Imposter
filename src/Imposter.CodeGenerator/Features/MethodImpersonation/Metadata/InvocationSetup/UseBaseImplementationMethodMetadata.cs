using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationSetup;

internal readonly struct UseBaseImplementationMethodMetadata
{
    internal readonly string Name = "UseBaseImplementation";

    internal readonly TypeSyntax ReturnType;

    internal readonly NameSyntax InterfaceSyntax;

    internal UseBaseImplementationMethodMetadata(
        NameSyntax interfaceSyntax,
        NameSyntax continuationInterfaceSyntax
    )
    {
        InterfaceSyntax = interfaceSyntax;
        ReturnType = continuationInterfaceSyntax;
    }
}
