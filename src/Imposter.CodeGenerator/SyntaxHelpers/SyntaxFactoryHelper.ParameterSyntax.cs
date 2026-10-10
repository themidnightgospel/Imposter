using System;
using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Helpers;
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
            (_, RefKind.In or RefKinds.RefReadOnlyParameter) => SyntaxKind.InKeyword,
            _ => SyntaxKind.None,
        };

        return Argument(null, Token(refKindKeyword), IdentifierName(EscapeKeyword(parameter.Name)));
    }

    // Passes a variable on to a parameter of the given kind, with its `ref` or `out`. A `ref readonly` parameter needs
    // the `in` written out (CS9192, CS9195); an `in` parameter takes the bare variable.
    internal static ArgumentSyntax ForwardingArgument(string variableName, RefKind refKind) =>
        Argument(null, Token(ForwardingKeyword(refKind)), IdentifierName(variableName));

    private static SyntaxKind ForwardingKeyword(RefKind refKind) =>
        refKind switch
        {
            RefKind.Ref => SyntaxKind.RefKeyword,
            RefKind.Out => SyntaxKind.OutKeyword,
            RefKinds.RefReadOnlyParameter => SyntaxKind.InKeyword,
            _ => SyntaxKind.None,
        };

    internal static ParameterListSyntax ParameterListSyntaxWithoutDefaultValues(
        IEnumerable<ParameterModel> parameters,
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
        ParameterModel parameter,
        bool includeRefKind = true
    ) =>
        ParameterSyntaxInternal(
            parameter,
            includeRefKind,
            includeNullableReferenceAnnotations: true,
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

#if ROSLYN4_4_OR_GREATER
        // A scoped ref parameter declared without its ref kind is a plain value, which can't be scoped.
        if (parameter.IsScoped && (includeRefKind || parameter.RefKind == RefKind.None))
        {
            parameterBuilder.AddModifier(Token(SyntaxKind.ScopedKeyword));
        }
#endif

        if (includeRefKind)
        {
            foreach (var modifier in RefKindModifiers(parameter.RefKind))
            {
                parameterBuilder.AddModifier(modifier);
            }
        }

        if (includeDefaultValue && parameter.DefaultValue is { } defaultValue)
        {
            parameterBuilder.WithDefaultValue(DefaultValueExpression(defaultValue, parameterType));
        }

        return parameterBuilder.Build();
    }

    private static SyntaxToken[] RefKindModifiers(RefKind refKind) =>
        refKind switch
        {
            RefKind.Ref => [Token(SyntaxKind.RefKeyword)],
            RefKind.Out => [Token(SyntaxKind.OutKeyword)],
            RefKind.In => [Token(SyntaxKind.InKeyword)],
            RefKinds.RefReadOnlyParameter =>
            [
                Token(SyntaxKind.RefKeyword),
                Token(SyntaxKind.ReadOnlyKeyword),
            ],
            _ => [],
        };

    private static ExpressionSyntax DefaultValueExpression(
        ParameterDefaultValue defaultValue,
        TypeSyntax parameterType
    ) =>
        defaultValue switch
        {
            { Value: null } => DefaultExpression(parameterType),
            { EnumType: { } enumType } => CastExpression(
                TypeSyntax(enumType),
                EnumValue(defaultValue.ValueText)
            ),
            { Value: decimal value } => LiteralExpression(
                SyntaxKind.NumericLiteralExpression,
                Literal(value)
            ),
            { Value: float value } => FloatingPointDefault(
                SyntaxKind.FloatKeyword,
                value,
                Literal(value)
            ),
            { Value: double value } => FloatingPointDefault(
                SyntaxKind.DoubleKeyword,
                value,
                Literal(value)
            ),
            _ => ParseExpression(defaultValue.ValueText),
        };

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
