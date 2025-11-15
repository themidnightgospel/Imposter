using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.CodeGenerator.SyntaxProviders;

internal readonly record struct GenerateImposterDeclaration(
    INamedTypeSymbol ImposterTarget,
    bool PutInTheSameNamespace
)
{
    public bool Equals(GenerateImposterDeclaration? other)
    {
        return other is not null
            && SymbolEqualityComparer.Default.Equals(ImposterTarget, other.Value.ImposterTarget);
    }

    public override int GetHashCode()
    {
        return SymbolEqualityComparer.Default.GetHashCode(ImposterTarget);
    }
}
