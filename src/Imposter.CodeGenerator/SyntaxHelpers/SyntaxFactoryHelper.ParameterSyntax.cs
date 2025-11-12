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
    internal static ArgumentListSyntax ArgumentListSyntax(IEnumerable<IParameterSymbol> parameters, bool includeRefKind = true)
        => ArgumentListSyntax(SeparatedList(parameters.Select(it => ArgumentSyntax(it, includeRefKind))));

    internal static ArgumentSyntax ArgumentSyntax(IParameterSymbol parameter, bool includeRefKind = true)
    {
        var refKindKeyword = (includeRefKind, parameter.RefKind) switch
        {
            (false, _) => SyntaxKind.None,
            (_, RefKind.Ref) => SyntaxKind.RefKeyword,
            (_, RefKind.Out) => SyntaxKind.OutKeyword,
            (_, RefKind.In) => SyntaxKind.InKeyword,
            _ => SyntaxKind.None
        };

        return Argument(
            null,
            Token(refKindKeyword),
            IdentifierName(parameter.Name)
        );
    }

    internal static ParameterListSyntax ParameterListSyntax(IEnumerable<IParameterSymbol> parameters, bool includeRefKind = true)
        => ParameterList(SeparatedList(parameters.Select(it => ParameterSyntax(it, includeRefKind))));

    internal static IEnumerable<ParameterSyntax> ParametersSyntax(IEnumerable<IParameterSymbol> parameters) => parameters.Select(ParameterSyntax);

    internal static ParameterListSyntax ParameterListSyntax(IEnumerable<ParameterSyntax> parameters) => ParameterList(SeparatedList(parameters));

    internal static ParameterListSyntax ParameterListSyntax(ParameterSyntax parameter) => ParameterList(SingletonSeparatedList(parameter));

    internal static IEnumerable<ParameterSyntax> ParameterSyntaxes(IEnumerable<IParameterSymbol> parameters) => parameters.Select(ParameterSyntax);

    internal static ParameterSyntax ParameterSyntax(IParameterSymbol parameter) => ParameterSyntax(parameter, true);

    internal static ParameterSyntax ParameterSyntax(TypeSyntax type, string name)
        => new ParameterBuilder(type, name).Build();

    internal static ParameterSyntax ParameterSyntax(ParameterMetadata parameterMetadata)
        => new ParameterBuilder(parameterMetadata.Type, parameterMetadata.Name)
            .WithDefaultValue(parameterMetadata.DefaultValue)
            .Build();

    internal static ParameterListSyntax ToSingleParameterListSyntax(this ParameterSyntax parameterSyntax)
        => ParameterList(SingletonSeparatedList(parameterSyntax));

    internal static ParameterSyntax ParameterSyntax(IParameterSymbol parameter, bool includeRefKind)
    {
        var parameterBuilder = new ParameterBuilder(TypeSyntax(parameter.Type), parameter.Name);

        if (includeRefKind)
        {
            var modifier = parameter.RefKind switch
            {
                RefKind.Ref => Token(SyntaxKind.RefKeyword),
                RefKind.Out => Token(SyntaxKind.OutKeyword),
                RefKind.In => Token(SyntaxKind.InKeyword),
                _ => default
            };

            if (modifier != default)
            {
                parameterBuilder.AddModifier(modifier);
            }
        }

        if (parameter.HasExplicitDefaultValue)
        {
            var defaultValue = parameter.ExplicitDefaultValue != null
                ? ParseExpression(parameter.ExplicitDefaultValue.ToString())
                : DefaultExpression(TypeSyntax(parameter.Type));

            parameterBuilder.WithDefaultValue(defaultValue);
        }

        return parameterBuilder.Build();
    }

    internal static StatementSyntax AssignDefaultValueStatementSyntax(IParameterSymbol parameter) =>
        IdentifierName(parameter.Name).Assign(DefaultExpression(TypeSyntax(parameter.Type))).ToStatementSyntax();

    internal static IEnumerable<TypeParameterSyntax> TypeParameters(IReadOnlyList<NameSyntax> genericArguments)
    {
        return genericArguments.Select(name => TypeParameter(Identifier(((IdentifierNameSyntax)name).Identifier.Text)));
    }

    internal static TypeParameterListSyntax? TypeParameterListSyntax(IReadOnlyList<NameSyntax> genericArguments) =>
        genericArguments.Count == 0
            ? null
            : TypeParameterList(
                SeparatedList(
                    genericArguments.Select(name => TypeParameter(Identifier(((IdentifierNameSyntax)name).Identifier.Text)))
                )
            );

    internal static TypeParameterListSyntax? TypeParameterListSyntax(IReadOnlyList<IdentifierNameSyntax> genericArguments) =>
        genericArguments.Count == 0
            ? default
            : TypeParameterList(
                SeparatedList(
                    genericArguments.Select(name => TypeParameter(Identifier(name.Identifier.Text)))
                )
            );


    // TODO Refactor
    public static ParameterListSyntax ToParameterListSyntax(this IEnumerable<IParameterSymbol> parameters, bool appendTargetSuffix = false)
    {
        return ParameterList(SeparatedList(parameters.Select(ToParameterSyntax)));

        ParameterSyntax ToParameterSyntax(IParameterSymbol symbol)
        {
            var typeSyntax = TypeSyntax(symbol.Type);

            if (appendTargetSuffix)
                typeSyntax = AddTargetSuffixToType(typeSyntax);

            // Determine ref/out/in modifiers
            var modifiers = new List<SyntaxToken>();
            if (symbol.RefKind == RefKind.Out)
                modifiers.Add(Token(SyntaxKind.OutKeyword));
            else if (symbol.RefKind == RefKind.Ref)
                modifiers.Add(Token(SyntaxKind.RefKeyword));
            else if (symbol.RefKind == RefKind.In)
                modifiers.Add(Token(SyntaxKind.InKeyword));

            return Parameter(Identifier(symbol.Name))
                .WithModifiers(TokenList(modifiers))
                .WithType(typeSyntax);
        }
    }

    private static TypeSyntax AddTargetSuffixToType(TypeSyntax type)
    {
        switch (type)
        {
            case IdentifierNameSyntax id:
                return IdentifierName(id.Identifier.Text + "Target");

            case GenericNameSyntax gen:
                return gen.WithTypeArgumentList(
                    TypeArgumentList(SeparatedList(
                        gen.TypeArgumentList.Arguments.Select(AddTargetSuffixToType)
                    ))
                );

            case ArrayTypeSyntax array:
                return array.WithElementType(AddTargetSuffixToType(array.ElementType));

            case QualifiedNameSyntax qualified:
                return qualified.WithRight((SimpleNameSyntax)AddTargetSuffixToType(qualified.Right));

            case NullableTypeSyntax nullable:
                return nullable.WithElementType(AddTargetSuffixToType(nullable.ElementType));

            default:
                return type;
        }
    }
}
