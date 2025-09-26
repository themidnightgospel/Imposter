using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SymbolExtensions
{
    private static readonly SymbolDisplayFormat FullyQualifiedFormat =
        SymbolDisplayFormat.FullyQualifiedFormat.AddMiscellaneousOptions(
            SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier
        );

    internal static string ToFullyQualifiedName(this ITypeSymbol typeSymbol) =>
        typeSymbol.ToDisplayString(FullyQualifiedFormat);
}