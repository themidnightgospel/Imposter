using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Imposter.CodeGenerator.Models;

/// <summary>
/// An explicit parameter default. <see cref="ValueText"/> also tells apart constants that compare equal but are
/// written differently, such as 0.0 and -0.0. A null value is written as default(T).
/// </summary>
internal sealed record ParameterDefaultValue(object? Value, string ValueText, TypeModel? EnumType)
{
    internal static ParameterDefaultValue? From(IParameterSymbol parameter)
    {
        if (!parameter.HasExplicitDefaultValue)
        {
            return null;
        }

        var value = parameter.ExplicitDefaultValue;
        var valueText = SymbolDisplay.FormatPrimitive(
            value!,
            quoteStrings: true,
            useHexadecimalNumbers: false
        );

        if (valueText is null)
        {
            return null;
        }

        var valueType = UnderlyingValueType(parameter.Type);

        return new ParameterDefaultValue(
            value,
            valueText,
            valueType.TypeKind == TypeKind.Enum ? TypeModel.From(valueType) : null
        );
    }

    // The default of a nullable value type, such as decimal? or E?, is a value of its underlying type.
    private static ITypeSymbol UnderlyingValueType(ITypeSymbol type) =>
        type
            is INamedTypeSymbol
            {
                OriginalDefinition.SpecialType: SpecialType.System_Nullable_T,
            } nullable
            ? nullable.TypeArguments[0]
            : type;
}
