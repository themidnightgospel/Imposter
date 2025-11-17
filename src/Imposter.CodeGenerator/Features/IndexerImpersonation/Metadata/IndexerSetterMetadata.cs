using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;

internal readonly struct IndexerSetterMetadata
{
    internal readonly string BuilderName;

    internal readonly NameSyntax BuilderTypeSyntax;

    internal readonly string InterfaceName;

    internal readonly NameSyntax InterfaceTypeSyntax;

    internal IndexerSetterMetadata(in ImposterIndexerCoreMetadata core)
    {
        BuilderName = $"{core.UniqueName}IndexerSetterImposter";
        BuilderTypeSyntax = IdentifierName(BuilderName);
        InterfaceName = $"I{core.UniqueName}IndexerSetterBuilder";
        InterfaceTypeSyntax = IdentifierName(InterfaceName);
    }
}
