using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Models;

/// <summary>
/// A <c>Span&lt;T&gt;</c> or <c>ReadOnlySpan&lt;T&gt;</c> parameter, one returned by value, or a property's. A span
/// itself can't be kept, so an imposter keeps the elements a span argument or value arrives with, or a span result
/// has, in an array.
/// </summary>
internal sealed record SpanModel(TypeModel ElementType, bool IsReadOnly)
{
    internal static SpanModel? From(IParameterSymbol parameter) => From(parameter.Type);

    internal static SpanModel? FromReturnType(IMethodSymbol method) =>
        method.RefKind == RefKind.None ? From(method.ReturnType) : null;

    internal static SpanModel? FromProperty(IPropertySymbol property) =>
        property.RefKind == RefKind.None ? From(property.Type) : null;

    private static SpanModel? From(ITypeSymbol type) =>
        type is INamedTypeSymbol { IsRefLikeType: true, TypeArguments.Length: 1 } span
        && span.ContainingNamespace
            is { Name: "System", ContainingNamespace.IsGlobalNamespace: true }
        && span.Name is "Span" or "ReadOnlySpan"
            ? new SpanModel(TypeModel.From(span.TypeArguments[0]), span.Name == "ReadOnlySpan")
            : null;
}
