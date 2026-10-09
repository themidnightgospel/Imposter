using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Models;

/// <summary>
/// A parameter as generated code declares and passes it. <see cref="ReferencesMethodTypeParameter"/> is true
/// when the type uses a type parameter of the generic method that declares the parameter.
/// <see cref="SpanElementType"/> is the element type when the parameter is a <c>Span&lt;T&gt;</c> or
/// <c>ReadOnlySpan&lt;T&gt;</c> taken by value. A span itself can't be kept, so an imposter keeps a copy of its
/// elements in an array.
/// </summary>
internal sealed record ParameterModel(
    string Name,
    RefKind RefKind,
    TypeModel Type,
    ParameterDefaultValue? DefaultValue,
    bool ReferencesMethodTypeParameter,
    TypeModel? SpanElementType
)
{
    internal static ParameterModel From(IParameterSymbol parameter) =>
        new(
            parameter.Name,
            parameter.RefKind,
            TypeModel.From(parameter.Type),
            ParameterDefaultValue.From(parameter),
            parameter.ContainingSymbol is IMethodSymbol method
                && parameter.Type.ReferencesTypeParameterOf(method),
            CopiedSpanElementType(parameter) is { } elementType ? TypeModel.From(elementType) : null
        );

    // A Span<T> or ReadOnlySpan<T> taken by value can be copied into a T[] when the call comes in.
    internal static ITypeSymbol? CopiedSpanElementType(IParameterSymbol parameter) =>
        parameter
            is {
                RefKind: RefKind.None,
                Type: INamedTypeSymbol { IsRefLikeType: true, TypeArguments.Length: 1 } type,
            }
        && type.ContainingNamespace
            is { Name: "System", ContainingNamespace.IsGlobalNamespace: true }
        && type.Name is "Span" or "ReadOnlySpan"
            ? type.TypeArguments[0]
            : null;
}
