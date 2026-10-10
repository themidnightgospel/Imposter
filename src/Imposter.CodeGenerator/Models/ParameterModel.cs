using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Models;

/// <summary>
/// A parameter as generated code declares and passes it. <see cref="ReferencesMethodTypeParameter"/> is true
/// when the type uses a type parameter of the generic method that declares the parameter.
/// <see cref="Span"/> is set for a <c>Span&lt;T&gt;</c> or <c>ReadOnlySpan&lt;T&gt;</c> parameter of any ref kind.
/// </summary>
internal sealed record ParameterModel(
    string Name,
    RefKind RefKind,
    bool IsScoped,
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
            IsDeclaredScoped(parameter),
            TypeModel.From(parameter.Type),
            ParameterDefaultValue.From(parameter),
            parameter.ContainingSymbol is IMethodSymbol method
                && parameter.Type.ReferencesTypeParameterOf(method),
            SpanModel.From(parameter)
        );

    // Generated code that declares the parameter again repeats scoped: an implementation has to (CS8987), and so do the
    // imposter's methods and delegates the argument passes through, or what they return couldn't leave the
    // implementation. A params span is scoped implicitly, and generated code, which doesn't repeat params, says so. An
    // out parameter is scoped on both sides already.
    private static bool IsDeclaredScoped(IParameterSymbol parameter) =>
#if ROSLYN4_4_OR_GREATER
        parameter.ScopedKind != ScopedKind.None && parameter.RefKind != RefKind.Out;
#else
        false;
#endif
}
