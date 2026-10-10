using System.Collections.Immutable;
using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Models;

/// <summary>
/// A parameter as generated code declares and passes it. <see cref="ReferencesMethodTypeParameter"/> is true
/// when the type uses a type parameter of the generic method that declares the parameter.
/// <see cref="Span"/> is set for a <c>Span&lt;T&gt;</c> or <c>ReadOnlySpan&lt;T&gt;</c> parameter of any ref kind.
/// <see cref="IsPassedThrough"/> is true for another <c>ref struct</c>, or a value of a method's type parameter that
/// allows ref structs, which an imposter can't keep or match: its setups, verification and history leave the argument
/// out, and it only passes it on to the delegates and the base implementation.
/// </summary>
internal sealed record ParameterModel(
    string Name,
    RefKind RefKind,
    bool IsScoped,
    TypeModel Type,
    ParameterDefaultValue? DefaultValue,
    bool ReferencesMethodTypeParameter,
    SpanModel? Span,
    bool IsPassedThrough
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
            SpanModel.From(parameter),
            PassesThrough(parameter)
        );

    // The parameters a setup matches: all but those it only passes through.
    internal static ImmutableArray<IParameterSymbol> MatchedParameters(
        ImmutableArray<IParameterSymbol> parameters
    ) => parameters.Where(parameter => !PassesThrough(parameter)).ToImmutableArray();

    internal static bool PassesThrough(IParameterSymbol parameter) =>
        IsCustomRefStruct(parameter) || parameter.Type.IsMethodTypeParameterAllowingRefStructs();

    internal static bool IsCustomRefStruct(IParameterSymbol parameter) =>
        parameter.Type.IsRefLikeType && SpanModel.From(parameter) is null;

    // Setups take the same matchers for parameters of the same types, whatever their ref kinds, except out, which gets
    // OutArg<T>. A method's own type parameters compare by position (see TypeSymbolExtensions.ToSignatureKey).
    internal static bool HaveTheSameMatchers(
        ImmutableArray<IParameterSymbol> parameters,
        ImmutableArray<IParameterSymbol> others
    ) =>
        parameters.Length == others.Length
        && parameters.Zip(others, HaveTheSameMatcher).All(same => same);

    private static bool HaveTheSameMatcher(IParameterSymbol parameter, IParameterSymbol other) =>
        (parameter.RefKind == RefKind.Out) == (other.RefKind == RefKind.Out)
        && parameter.Type.ToSignatureKey() == other.Type.ToSignatureKey();

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
