using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.IndexerImposter.Metadata.SetterImposterBuilderInterface;

internal readonly struct ThenMethodMetadata
{
    internal readonly string Name = "Then";

    internal readonly TypeSyntax ReturnType;

    internal ThenMethodMetadata(TypeSyntax interfaceType)
    {
        ReturnType = interfaceType;
    }
}
