using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Models;

/// <summary>
/// A <c>Span&lt;T&gt;</c> or <c>ReadOnlySpan&lt;T&gt;</c> parameter taken by value. A span itself can't be kept, so an
/// imposter keeps a copy of its elements in an array.
/// </summary>
internal sealed record SpanModel(TypeModel ElementType, bool IsReadOnly)
{
    internal static SpanModel? From(IParameterSymbol parameter) =>
        parameter
            is {
                RefKind: RefKind.None,
                Type: INamedTypeSymbol { IsRefLikeType: true, TypeArguments.Length: 1 } type,
            }
        && type.ContainingNamespace
            is { Name: "System", ContainingNamespace.IsGlobalNamespace: true }
        && type.Name is "Span" or "ReadOnlySpan"
            ? new SpanModel(TypeModel.From(type.TypeArguments[0]), type.Name == "ReadOnlySpan")
            : null;
}
