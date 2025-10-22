using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.SyntaxProviders;

internal readonly record struct GenerateImposterDeclaration(INamedTypeSymbol ImposterTarget, bool PutInTheSameNamespace)
{
    public bool Equals(GenerateImposterDeclaration? other)
    {
        if (other is null)
        {
            return false;
        }
        
        return SymbolEqualityComparer.Default.Equals(ImposterTarget, other.Value.ImposterTarget);
    }

    public override int GetHashCode()
    {
        return SymbolEqualityComparer.Default.GetHashCode(ImposterTarget);
    }
}