using System.Linq;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Models;

/// <summary>
/// A type parameter and the constraints generated code repeats for it. <see cref="IsReferenceType"/> and
/// <see cref="IsValueType"/> also hold when only a constraint type, such as a base class, makes it one.
/// </summary>
internal sealed record TypeParameterModel(
    string Name,
    bool HasReferenceTypeConstraint,
    bool IsReferenceTypeConstraintNullable,
    bool HasUnmanagedTypeConstraint,
    bool HasValueTypeConstraint,
    bool HasNotNullConstraint,
    EquatableArray<TypeModel> ConstraintTypes,
    bool HasConstructorConstraint,
    bool IsReferenceType,
    bool IsValueType
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
            typeParameter.HasConstructorConstraint,
            typeParameter.IsReferenceType,
            typeParameter.IsValueType
        );
}
