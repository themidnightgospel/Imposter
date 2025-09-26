using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Contexts;

internal class ImposterType
{
    internal readonly string Name;

    internal ImposterType(INamedTypeSymbol targetSymbol)
    {
        Name = targetSymbol.Name + "Imposter";
    }
}