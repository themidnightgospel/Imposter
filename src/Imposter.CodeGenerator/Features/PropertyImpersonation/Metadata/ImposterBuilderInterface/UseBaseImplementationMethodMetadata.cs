using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImposter.Metadata.ImposterBuilderInterface;

internal readonly struct UseBaseImplementationMethodMetadata
{
    internal readonly string Name = "UseBaseImplementation";

    internal readonly TypeSyntax ReturnType;

    internal UseBaseImplementationMethodMetadata(NameSyntax interfaceSyntax)
    {
        ReturnType = interfaceSyntax;
    }
}
