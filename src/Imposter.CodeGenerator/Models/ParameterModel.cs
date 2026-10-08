using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Models;

/// <summary>
/// A parameter as generated code declares and passes it. <see cref="ReferencesMethodTypeParameter"/> is true
/// when the type uses a type parameter of the generic method that declares the parameter.
/// </summary>
internal sealed record ParameterModel(
    string Name,
    RefKind RefKind,
    TypeModel Type,
    ParameterDefaultValue? DefaultValue,
    bool ReferencesMethodTypeParameter
)
{
    internal static ParameterModel From(IParameterSymbol parameter) =>
        new(
            parameter.Name,
            parameter.RefKind,
            TypeModel.From(parameter.Type),
            ParameterDefaultValue.From(parameter),
            parameter.ContainingSymbol is IMethodSymbol method
                && parameter.Type.ReferencesTypeParameterOf(method)
        );
}
