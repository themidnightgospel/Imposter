using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;

internal readonly struct IndexerArgumentsCriteriaMetadata
{
    internal readonly string Name;

    internal readonly NameSyntax TypeSyntax;

    internal IndexerArgumentsCriteriaMetadata(in ImposterIndexerCoreMetadata core)
    {
        // The class keeps each parameter in a field named after it, which can't share the class's name.
        Name = core.CreateParameterNameSet().Use($"{core.UniqueName}IndexerArgumentsCriteria");
        TypeSyntax = IdentifierName(Name);
    }
}
