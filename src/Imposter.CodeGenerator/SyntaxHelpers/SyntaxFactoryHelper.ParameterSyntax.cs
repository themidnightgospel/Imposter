using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    internal static ArgumentListSyntax ArgumentListSyntax(
        IEnumerable<IParameterSymbol> parameters,
        bool includeRefKind = true
    ) =>
        ArgumentListSyntax(
            SeparatedList(parameters.Select(it => ArgumentSyntax(it, includeRefKind)))
        );

    internal static ArgumentSyntax ArgumentSyntax(
        IParameterSymbol parameter,
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

        return Argument(null, Token(refKindKeyword), IdentifierName(parameter.Name));
    }

    internal static ParameterListSyntax ParameterListSyntax(
        IEnumerable<IParameterSymbol> parameters,
        bool includeRefKind = true
    ) => ParameterList(SeparatedList(parameters.Select(it => ParameterSyntax(it, includeRefKind))));

    internal static ParameterListSyntax ParameterListSyntaxWithoutDefaultValues(
        IEnumerable<IParameterSymbol> parameters,
        bool includeRefKind = true
    ) =>
        ParameterList(
            SeparatedList(
                parameters.Select(parameter =>
                    ParameterSyntaxWithoutDefaultValue(parameter, includeRefKind)
                )
            )
        );

    internal static ParameterListSyntax ParameterListSyntax(
        IEnumerable<ParameterSyntax> parameters
    ) => ParameterList(SeparatedList(parameters));

    internal static ParameterListSyntax ParameterListSyntaxIncludingNullable(
        IEnumerable<IParameterSymbol> parameters,
        bool includeRefKind = true
    ) =>
        ParameterList(
            SeparatedList(
                parameters.Select(it => ParameterSyntaxIncludingNullable(it, includeRefKind))
            )
        );

    internal static IEnumerable<ParameterSyntax> ParameterSyntaxes(
        IEnumerable<IParameterSymbol> parameters
    ) => parameters.Select(ParameterSyntax);

    internal static IEnumerable<ParameterSyntax> ParameterSyntaxesIncludingNullable(
        IEnumerable<IParameterSymbol> parameters
    ) => parameters.Select(parameter => ParameterSyntaxIncludingNullable(parameter));

    internal static ParameterSyntax ParameterSyntax(IParameterSymbol parameter) =>
        ParameterSyntax(parameter, includeRefKind: true);

    internal static ParameterSyntax ParameterSyntaxWithoutDefaultValue(
        IParameterSymbol parameter,
        bool includeRefKind = true
    ) =>
        ParameterSyntaxInternal(
            parameter,
            includeRefKind,
            includeNullableReferenceAnnotations: true,
            includeDefaultValue: false
        );

    internal static ParameterSyntax ParameterSyntaxIncludingNullable(
        IParameterSymbol parameter,
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
        IParameterSymbol parameter,
        bool includeRefKind
    ) =>
        ParameterSyntaxInternal(
            parameter,
            includeRefKind,
            includeNullableReferenceAnnotations: false,
            includeDefaultValue: true
        );

    private static ParameterSyntax ParameterSyntaxInternal(
        IParameterSymbol parameter,
        bool includeRefKind,
        bool includeNullableReferenceAnnotations,
        bool includeDefaultValue
    )
    {
        var parameterType = includeNullableReferenceAnnotations
            ? TypeSyntaxIncludingNullable(parameter.Type)
            : TypeSyntax(parameter.Type);

        var parameterBuilder = new ParameterBuilder(parameterType, parameter.Name);

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

        if (includeDefaultValue && parameter.HasExplicitDefaultValue)
        {
            var explicitDefaultValue = parameter.ExplicitDefaultValue;
            if (explicitDefaultValue is not null)
            {
                var defaultValue = GetDefaultValue(parameter, explicitDefaultValue);
                if (defaultValue is not null)
                {
                    parameterBuilder.WithDefaultValue(defaultValue);
                }
            }
            else
            {
                parameterBuilder.WithDefaultValue(DefaultExpression(parameterType));
            }
        }

        return parameterBuilder.Build();

        static ExpressionSyntax? GetDefaultValue(
            IParameterSymbol parameter,
            object explicitDefaultValue
        )
        {
            var defaultValueText = SymbolDisplay.FormatPrimitive(
                explicitDefaultValue,
                quoteStrings: true,
                useHexadecimalNumbers: false
            );

            if (defaultValueText is null)
            {
                return null;
            }

            return parameter.Type.TypeKind == TypeKind.Enum
                ? EnumDefaultExpression(parameter, defaultValueText)
                : parameter.Type.SpecialType switch
                {
                    SpecialType.System_Decimal => LiteralExpression(
                        SyntaxKind.NumericLiteralExpression,
                        Literal((decimal)explicitDefaultValue)
                    ),
                    SpecialType.System_Single => LiteralExpression(
                        SyntaxKind.NumericLiteralExpression,
                        Literal((float)explicitDefaultValue)
                    ),
                    SpecialType.System_Double => LiteralExpression(
                        SyntaxKind.NumericLiteralExpression,
                        Literal((double)explicitDefaultValue)
                    ),
                    _ => ParseExpression(defaultValueText),
                };
        }
    }

    internal static StatementSyntax AssignDefaultValueStatementSyntax(IParameterSymbol parameter) =>
        IdentifierName(parameter.Name).Assign(DefaultNonNullable).ToStatementSyntax();

    internal static TypeParameterListSyntax? TypeParameterListSyntax(
        IReadOnlyList<NameSyntax> genericArguments
    ) =>
        genericArguments.Count == 0
            ? null
            : TypeParameterList(
                SeparatedList(
                    genericArguments.Select(name =>
                        TypeParameter(Identifier(((IdentifierNameSyntax)name).Identifier.Text))
                    )
                )
            );

    private static CastExpressionSyntax EnumDefaultExpression(
        IParameterSymbol parameter,
        string defaultValueText
    ) => CastExpression(TypeSyntax(parameter.Type), ParseExpression(defaultValueText));
}
