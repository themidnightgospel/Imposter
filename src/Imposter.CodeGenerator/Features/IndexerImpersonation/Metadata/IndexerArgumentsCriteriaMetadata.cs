using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;

internal readonly struct IndexerArgumentsCriteriaMetadata
{
    internal readonly string Name;

    internal readonly NameSyntax TypeSyntax;

    internal IndexerArgumentsCriteriaMetadata(in ImposterIndexerCoreMetadata core)
    {
        Name = $"{core.UniqueName}IndexerArgumentsCriteria";
        TypeSyntax = IdentifierName(Name);
    }
}
