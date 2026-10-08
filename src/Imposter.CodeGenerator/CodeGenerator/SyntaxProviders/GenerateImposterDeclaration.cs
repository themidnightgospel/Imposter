using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.CodeGenerator.SyntaxProviders;

internal readonly record struct GenerateImposterDeclaration(
    INamedTypeSymbol ImposterTarget,
    bool PutInTheSameNamespace
)
{
    public override int GetHashCode()
    {
        return SymbolEqualityComparer.Default.GetHashCode(ImposterTarget);
    }
}
