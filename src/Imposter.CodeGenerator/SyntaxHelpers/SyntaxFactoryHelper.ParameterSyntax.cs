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

    internal static ParameterListSyntax ParameterListSyntax(
        IEnumerable<ParameterSyntax> parameters
    ) => ParameterList(SeparatedList(parameters));

    internal static IEnumerable<ParameterSyntax> ParameterSyntaxes(
        IEnumerable<IParameterSymbol> parameters
    ) => parameters.Select(ParameterSyntax);

    internal static IEnumerable<ParameterSyntax> ParameterSyntaxesIncludingNullable(
        IEnumerable<IParameterSymbol> parameters
    ) => parameters.Select(parameter => ParameterSyntaxIncludingNullable(parameter));

    internal static ParameterSyntax ParameterSyntax(IParameterSymbol parameter) =>
        ParameterSyntax(parameter, includeRefKind: true);

    internal static ParameterSyntax ParameterSyntaxIncludingNullable(
        IParameterSymbol parameter,
        bool includeRefKind = true
    ) =>
        ParameterSyntaxInternal(
            parameter,
            includeRefKind,
            includeNullableReferenceAnnotations: true
        );

    internal static ParameterSyntax ParameterSyntax(TypeSyntax type, string name) =>
        new ParameterBuilder(type, name).Build();

    internal static ParameterSyntax ParameterSyntax(ParameterMetadata parameterMetadata) =>
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
            includeNullableReferenceAnnotations: false
        );

    private static ParameterSyntax ParameterSyntaxInternal(
        IParameterSymbol parameter,
        bool includeRefKind,
        bool includeNullableReferenceAnnotations
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

        if (parameter.HasExplicitDefaultValue)
        {
            var defaultValue =
                parameter.ExplicitDefaultValue != null
                    ? ParseExpression(parameter.ExplicitDefaultValue.ToString())
                    : DefaultExpression(parameterType);

            parameterBuilder.WithDefaultValue(defaultValue);
        }

        return parameterBuilder.Build();
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
}
