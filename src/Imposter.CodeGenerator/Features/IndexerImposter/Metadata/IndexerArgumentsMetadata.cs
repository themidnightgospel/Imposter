using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImposter.Metadata;

internal readonly struct IndexerArgumentsMetadata
{
    internal readonly string Name;

    internal readonly NameSyntax TypeSyntax;

    internal IndexerArgumentsMetadata(in ImposterIndexerCoreMetadata core)
    {
        Name = $"{core.UniqueName}IndexerArguments";
        TypeSyntax = IdentifierName(Name);
    }
}
