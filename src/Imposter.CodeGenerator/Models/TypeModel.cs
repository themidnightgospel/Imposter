using Imposter.CodeGenerator.Helpers;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Models;

/// <summary>
/// A type as generated code writes it, its type parameters under their names there (see
/// <see cref="TypeParameterNames"/>). It holds no symbols, so equal types compare equal across compilations.
/// <see cref="IsDynamic"/> is true for <c>dynamic</c>, which is <c>object</c> at runtime, and
/// <see cref="ContainsDynamic"/> also for a type built from it, such as <c>List&lt;dynamic&gt;</c>. A type parameter
/// named <c>dynamic</c> is neither, though its name is written the same.
/// </summary>
internal sealed record TypeModel(
    string FullyQualifiedName,
    string FullyQualifiedNameIncludingNullable,
    bool IsDynamic,
    bool ContainsDynamic
)
{
    private static readonly SymbolDisplayFormat FullyQualifiedFormatIncludingNullable =
        SymbolDisplayFormat.FullyQualifiedFormat.WithMiscellaneousOptions(
            SymbolDisplayFormat.FullyQualifiedFormat.MiscellaneousOptions
                | SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier
        );

    internal static TypeModel From(ITypeSymbol type) =>
        new(
            type.ToGeneratedCodeString(SymbolDisplayFormat.FullyQualifiedFormat),
            type.ToGeneratedCodeString(FullyQualifiedFormatIncludingNullable),
            type.TypeKind == TypeKind.Dynamic,
            type.ContainsDynamic()
        );
}
