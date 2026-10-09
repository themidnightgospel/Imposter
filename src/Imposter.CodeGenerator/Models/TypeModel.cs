using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Models;

/// <summary>
/// A type as generated code writes it. It holds no symbols, so equal types compare equal across compilations.
/// </summary>
internal sealed record TypeModel(
    string FullyQualifiedName,
    string FullyQualifiedNameIncludingNullable
)
{
    private static readonly SymbolDisplayFormat FullyQualifiedFormatIncludingNullable =
        SymbolDisplayFormat.FullyQualifiedFormat.WithMiscellaneousOptions(
            SymbolDisplayFormat.FullyQualifiedFormat.MiscellaneousOptions
                | SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier
        );

    internal static TypeModel From(ITypeSymbol type) =>
        new(
            type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
            type.ToDisplayString(FullyQualifiedFormatIncludingNullable)
        );
}
