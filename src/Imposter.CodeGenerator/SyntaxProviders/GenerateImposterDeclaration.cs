using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.SyntaxProviders;

internal sealed record GenerateImposterDeclaration(INamedTypeSymbol ImposterTarget, bool PutInTheSameNamespace)
{
    public bool Equals(GenerateImposterDeclaration? other)
    {
        if (ReferenceEquals(this, other))
        {
            return true;
        }

        if (other is null)
        {
            return false;
        }

        return SymbolEqualityComparer.Default.Equals(ImposterTarget, other.ImposterTarget);
    }

    public override int GetHashCode()
    {
        return SymbolEqualityComparer.Default.GetHashCode(ImposterTarget);
    }
}