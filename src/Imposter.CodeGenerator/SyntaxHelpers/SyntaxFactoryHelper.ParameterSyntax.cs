using System;
using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata;
using Imposter.CodeGenerator.Models;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    internal static ArgumentListSyntax ArgumentListSyntax(
        IEnumerable<ParameterModel> parameters,
        bool includeRefKind = true
    ) =>
        ArgumentListSyntax(
            SeparatedList(parameters.Select(it => ArgumentSyntax(it, includeRefKind)))
        );

    internal static ArgumentSyntax ArgumentSyntax(
        ParameterModel parameter,
        bool includeRefKind = true
    )
    {
        var refKindKeyword = (includeRefKind, parameter.RefKind) switch
        {
            (false, _) => SyntaxKind.None,
            (_, RefKind.Ref) => SyntaxKind.RefKeyword,
            (_, RefKind.Out) => SyntaxKind.OutKeyword,
            (_, RefKind.In) => SyntaxKind.InKeyword,
            _ => SyntaxKind.None,
        };

        return Argument(null, Token(refKindKeyword), IdentifierName(EscapeKeyword(parameter.Name)));
    }

    internal static ParameterListSyntax ParameterListSyntax(
        IEnumerable<ParameterModel> parameters,
        bool includeRefKind = true
    ) => ParameterList(SeparatedList(parameters.Select(it => ParameterSyntax(it, includeRefKind))));

    internal static ParameterListSyntax ParameterListSyntaxWithoutDefaultValues(
        IEnumerable<MethodParameterMetadata> parameters,
        bool includeRefKind = true
    ) =>
        ParameterList(
            SeparatedList(
                parameters.Select(parameter =>
                    ParameterSyntaxWithoutDefaultValue(parameter, includeRefKind)
                )
            )
        );

    internal static ParameterSyntax ParameterSyntaxWithoutDefaultValue(
        in MethodParameterMetadata parameter,
        bool includeRefKind = true
    ) =>
        ParameterSyntaxInternal(
            parameter.Model,
            parameter.NullableAwareTypeSyntax,
            includeRefKind,
            includeDefaultValue: false
        );

    internal static ParameterListSyntax ParameterListSyntax(
        IEnumerable<ParameterSyntax> parameters
    ) => ParameterList(SeparatedList(parameters));

    internal static IEnumerable<ParameterSyntax> ParameterSyntaxes(
        IEnumerable<ParameterModel> parameters
    ) => parameters.Select(ParameterSyntax);

    internal static ParameterSyntax ParameterSyntax(ParameterModel parameter) =>
        ParameterSyntax(parameter, includeRefKind: true);

    internal static ParameterSyntax ParameterSyntaxIncludingNullable(
        ParameterModel parameter,
        bool includeRefKind = true
    ) =>
        ParameterSyntaxInternal(
            parameter,
            includeRefKind,
            includeNullableReferenceAnnotations: true,
            includeDefaultValue: true
        );

    internal static ParameterSyntax ParameterSyntax(TypeSyntax type, string name) =>
        new ParameterBuilder(type, name).Build();

    internal static ParameterSyntax ParameterSyntax(in ParameterMetadata parameterMetadata) =>
        new ParameterBuilder(parameterMetadata.Type, parameterMetadata.Name)
            .WithDefaultValue(parameterMetadata.DefaultValue)
            .Build();

    internal static ParameterListSyntax ToSingleParameterListSyntax(
        this ParameterSyntax parameterSyntax
    ) => ParameterList(SingletonSeparatedList(parameterSyntax));

    internal static ParameterSyntax ParameterSyntax(
        ParameterModel parameter,
        bool includeRefKind
    ) =>
        ParameterSyntaxInternal(
            parameter,
            includeRefKind,
            includeNullableReferenceAnnotations: false,
            includeDefaultValue: true
        );

    private static ParameterSyntax ParameterSyntaxInternal(
        ParameterModel parameter,
        bool includeRefKind,
        bool includeNullableReferenceAnnotations,
        bool includeDefaultValue
    ) =>
        ParameterSyntaxInternal(
            parameter,
            includeNullableReferenceAnnotations
                ? TypeSyntaxIncludingNullable(parameter.Type)
                : TypeSyntax(parameter.Type),
            includeRefKind,
            includeDefaultValue
        );

    private static ParameterSyntax ParameterSyntaxInternal(
        ParameterModel parameter,
        TypeSyntax parameterType,
        bool includeRefKind,
        bool includeDefaultValue
    )
    {
        var parameterBuilder = new ParameterBuilder(parameterType, EscapeKeyword(parameter.Name));

        if (includeRefKind)
        {
            var modifier = parameter.RefKind switch
            {
                RefKind.Ref => Token(SyntaxKind.RefKeyword),
                RefKind.Out => Token(SyntaxKind.OutKeyword),
                RefKind.In => Token(SyntaxKind.InKeyword),
                _ => default,
            };

            if (modifier != default)
            {
                parameterBuilder.AddModifier(modifier);
            }
        }

        if (includeDefaultValue && parameter.DefaultValue is { } defaultValue)
        {
            parameterBuilder.WithDefaultValue(
                defaultValue.Expression is null
                    ? DefaultExpression(parameterType)
                    : ParseExpression(defaultValue.Expression)
            );
        }

        return parameterBuilder.Build();
    }

    // Null when the value has no C# form.
    internal static ExpressionSyntax? DefaultValueExpression(ITypeSymbol type, object value)
    {
        var valueText = SymbolDisplay.FormatPrimitive(
            value,
            quoteStrings: true,
            useHexadecimalNumbers: false
        );

        if (valueText is null)
        {
            return null;
        }

        var valueType = UnderlyingValueType(type);

        return valueType.TypeKind == TypeKind.Enum
            ? CastExpression(TypeSyntax(valueType), EnumValue(valueText))
            : value switch
            {
                decimal decimalValue => LiteralExpression(
                    SyntaxKind.NumericLiteralExpression,
                    Literal(decimalValue)
                ),
                float floatValue => FloatingPointDefault(
                    SyntaxKind.FloatKeyword,
                    floatValue,
                    Literal(floatValue)
                ),
                double doubleValue => FloatingPointDefault(
                    SyntaxKind.DoubleKeyword,
                    doubleValue,
                    Literal(doubleValue)
                ),
                _ => ParseExpression(valueText),
            };
    }

    internal static StatementSyntax AssignDefaultValueStatementSyntax(ParameterModel parameter) =>
        IdentifierName(EscapeKeyword(parameter.Name))
            .Assign(DefaultNonNullable)
            .ToStatementSyntax();

    internal static TypeParameterListSyntax? TypeParameterListSyntax(
        IReadOnlyList<NameSyntax> genericArguments
    ) =>
        genericArguments.Count == 0
            ? null
            : TypeParameterList(
                SeparatedList(
                    genericArguments.Select(name =>
                        TypeParameter(((IdentifierNameSyntax)name).Identifier)
                    )
                )
            );

    // The default of a nullable value type, such as decimal? or E?, is a value of its underlying type.
    private static ITypeSymbol UnderlyingValueType(ITypeSymbol type) =>
        type
            is INamedTypeSymbol
            {
                OriginalDefinition.SpecialType: SpecialType.System_Nullable_T,
            } nullable
            ? nullable.TypeArguments[0]
            : type;

    // C# reads (E)-1 as a subtraction, so a negative value is parenthesized: (E)(-1).
    private static ExpressionSyntax EnumValue(string valueText)
    {
        var value = ParseExpression(valueText);

        return valueText.StartsWith("-", StringComparison.Ordinal)
            ? ParenthesizedExpression(value)
            : value;
    }

    // NaN and the infinities have no literal form, so they are written as members such as double.NaN.
    private static ExpressionSyntax FloatingPointDefault(
        SyntaxKind typeKeyword,
        double value,
        SyntaxToken literal
    )
    {
        var nonFiniteMember =
            double.IsNaN(value) ? "NaN"
            : double.IsPositiveInfinity(value) ? "PositiveInfinity"
            : double.IsNegativeInfinity(value) ? "NegativeInfinity"
            : null;

        return nonFiniteMember is null
            ? LiteralExpression(SyntaxKind.NumericLiteralExpression, literal)
            : PredefinedType(Token(typeKeyword)).Dot(IdentifierName(nonFiniteMember));
    }
}
