using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.CodeGenerator.SyntaxProviders;

// SameImposterTypeAs is another registered target whose imposter would get the same type, so neither is generated.
internal readonly record struct GenerateImposterDeclaration(
    INamedTypeSymbol ImposterTarget,
    bool PutInTheSameNamespace,
    INamedTypeSymbol? SameImposterTypeAs = null
)
{
    public override int GetHashCode()
    {
        return SymbolEqualityComparer.Default.GetHashCode(ImposterTarget);
    }
}
