using System.Linq;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Models;

/// <summary>
/// A type parameter and the constraints generated code repeats for it.
/// </summary>
internal sealed record TypeParameterModel(
    string Name,
    bool HasReferenceTypeConstraint,
    bool IsReferenceTypeConstraintNullable,
    bool HasUnmanagedTypeConstraint,
    bool HasValueTypeConstraint,
    bool HasNotNullConstraint,
    EquatableArray<TypeModel> ConstraintTypes,
    bool HasConstructorConstraint
)
{
    internal static TypeParameterModel From(ITypeParameterSymbol typeParameter) =>
        new(
            typeParameter.Name,
            typeParameter.HasReferenceTypeConstraint,
            typeParameter.ReferenceTypeConstraintNullableAnnotation == NullableAnnotation.Annotated,
            typeParameter.HasUnmanagedTypeConstraint,
            typeParameter.HasValueTypeConstraint,
            typeParameter.HasNotNullConstraint,
            typeParameter.ConstraintTypes.Select(TypeModel.From).ToEquatableArray(),
            typeParameter.HasConstructorConstraint
        );
}
