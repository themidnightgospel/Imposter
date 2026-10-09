using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Models;

/// <summary>
/// A parameter as generated code declares and passes it. <see cref="ReferencesMethodTypeParameter"/> is true
/// when the type uses a type parameter of the generic method that declares the parameter.
/// <see cref="Span"/> is set when the parameter is a <c>Span&lt;T&gt;</c> or <c>ReadOnlySpan&lt;T&gt;</c> taken by value.
/// </summary>
internal sealed record ParameterModel(
    string Name,
    RefKind RefKind,
    TypeModel Type,
    ParameterDefaultValue? DefaultValue,
    bool ReferencesMethodTypeParameter,
    SpanModel? Span
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
            SpanModel.From(parameter)
        );
}
