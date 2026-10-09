using Microsoft.CodeAnalysis;
#if ROSLYN4_4_OR_GREATER
using System.Linq;
#endif

namespace Imposter.CodeGenerator.Models;

/// <summary>
/// A <c>Span&lt;T&gt;</c> or <c>ReadOnlySpan&lt;T&gt;</c> passed or returned by value. A span itself can't be kept, so
/// an imposter keeps its elements in an array.
/// </summary>
internal sealed record SpanModel(TypeModel ElementType, bool IsReadOnly)
{
    internal static SpanModel? From(IParameterSymbol parameter) =>
        parameter.RefKind == RefKind.None ? From(parameter.Type) : null;

    internal static SpanModel? FromReturnType(IMethodSymbol method) =>
        method.RefKind == RefKind.None && !HasScopedParameter(method)
            ? From(method.ReturnType)
            : null;

    private static SpanModel? From(ITypeSymbol type) =>
        type is INamedTypeSymbol { IsRefLikeType: true, TypeArguments.Length: 1 } span
        && span.ContainingNamespace
            is { Name: "System", ContainingNamespace.IsGlobalNamespace: true }
        && span.Name is "Span" or "ReadOnlySpan"
            ? new SpanModel(TypeModel.From(span.TypeArguments[0]), span.Name == "ReadOnlySpan")
            : null;

    // An implementation has to repeat a parameter's scoped modifier, and then can't return the span the imposter's
    // delegates hand back, which may come from that parameter. An out parameter is scoped implicitly, on both sides.
    private static bool HasScopedParameter(IMethodSymbol method) =>
#if ROSLYN4_4_OR_GREATER
        method.Parameters.Any(parameter =>
            parameter.RefKind != RefKind.Out && parameter.ScopedKind != ScopedKind.None
        );
#else
        false;
#endif
}
