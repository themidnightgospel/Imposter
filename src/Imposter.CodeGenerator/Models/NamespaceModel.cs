using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Models;

/// <summary>
/// The namespace a target is declared in. <see cref="DisplayName"/> is null for the global namespace.
/// <see cref="IsNested"/> is true when its own parent is not the global namespace.
/// </summary>
internal sealed record NamespaceModel(string? DisplayName, string FullyQualifiedName, bool IsNested)
{
    internal static NamespaceModel From(INamespaceSymbol @namespace) =>
        new(
            @namespace.IsGlobalNamespace ? null : @namespace.ToDisplayString(),
            @namespace.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
            !@namespace.IsGlobalNamespace && !@namespace.ContainingNamespace.IsGlobalNamespace
        );
}
