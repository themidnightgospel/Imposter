using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Models;

/// <summary>
/// A type parameter and the constraints generated code repeats for it. <see cref="IsReferenceType"/> and
/// <see cref="IsValueType"/> also hold when only a constraint type, such as a base class, makes it one.
/// <see cref="AllowsRefStructs"/> is true for a method's type parameter that allows ref structs, which generated code
/// repeats. A target's type parameter doesn't repeat it: its imposter keeps values of it, so it takes only type
/// arguments that aren't ref structs.
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
    bool IsValueType,
    bool AllowsRefStructs
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
            typeParameter.IsValueType,
            typeParameter.IsMethodTypeParameterAllowingRefStructs()
        );
}
