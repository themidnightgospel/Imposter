using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata.GetterImposterBuilderInterface;

internal readonly struct UseBaseImplementationMethodMetadata
{
    internal readonly string Name = "UseBaseImplementation";

    internal readonly TypeSyntax ReturnType;

    internal readonly NameSyntax InterfaceSyntax;

    internal UseBaseImplementationMethodMetadata(NameSyntax interfaceSyntax, TypeSyntax returnType)
    {
        InterfaceSyntax = interfaceSyntax;
        ReturnType = returnType;
    }
}
