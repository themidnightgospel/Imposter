using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Models;

/// <summary>
/// A property or indexer the imposter implements or overrides. <see cref="Parameters"/> is empty for a property.
/// <see cref="OverrideAccessibility"/> is the accessibility an override in the imposter's assembly must declare.
/// <see cref="IsRequired"/> is true for a C# 11 <c>required</c> property, whose override must be required too.
/// </summary>
internal sealed record PropertyModel(
    string Name,
    string DisplayName,
    TypeModel Type,
    TypeModel ContainingType,
    bool IsClassMember,
    Accessibility OverrideAccessibility,
    PropertyAccessorModel? Getter,
    PropertyAccessorModel? Setter,
    EquatableArray<ParameterModel> Parameters,
    bool IsRequired
)
{
    internal static PropertyModel From(IPropertySymbol property, MemberAccess memberAccess) =>
        new(
            property.Name,
            GetDisplayName(property),
            TypeModel.From(property.Type),
            TypeModel.From(property.ContainingType),
            property.ContainingType.TypeKind == TypeKind.Class,
            memberAccess.GetOverrideAccessibility(property),
            PropertyAccessorModel.FromAccessible(property.GetMethod, memberAccess),
            PropertyAccessorModel.FromAccessible(property.SetMethod, memberAccess),
            property.Parameters.Select(ParameterModel.From).ToEquatableArray(),
            property.IsRequiredMember()
        );

    private static string GetDisplayName(IPropertySymbol property)
    {
        var containingType = property.ContainingType.ToDisplayString(
            SymbolDisplayFormat.CSharpErrorMessageFormat
        );

        if (!property.IsIndexer)
        {
            return $"{containingType}.{property.Name}";
        }

        var parameters = string.Join(
            ", ",
            property.Parameters.Select(parameter =>
                parameter.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat)
            )
        );

        return $"{containingType}.this[{parameters}]";
    }
}
