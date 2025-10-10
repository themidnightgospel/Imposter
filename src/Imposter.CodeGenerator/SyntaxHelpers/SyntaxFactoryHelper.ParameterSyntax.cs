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
    internal static ArgumentListSyntax ArgumentSyntaxList(IEnumerable<IParameterSymbol> parameters, bool includeRefKind = true)
        => ArgumentList(SeparatedList(parameters.Select(it => ArgumentSyntax(it, includeRefKind))));

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

    internal static ParameterListSyntax ParameterListSyntax(IEnumerable<IParameterSymbol> parameters) => ParameterList(SeparatedList(parameters.Select(ParameterSyntax)));

    internal static IEnumerable<ParameterSyntax> ParameterSyntaxes(IEnumerable<IParameterSymbol> parameters) => parameters.Select(ParameterSyntax);

    internal static ParameterSyntax ParameterSyntax(IParameterSymbol parameter) => ParameterSyntax(parameter, true);

    internal static ParameterSyntax ParameterSyntax(TypeSyntax type, string name)
        => new ParameterBuilder(type, "criteria").Build();

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
                : LiteralExpression(SyntaxKind.NullLiteralExpression);

            parameterBuilder.WithDefaultValue(defaultValue);
        }

        return parameterBuilder.Build();
    }

    internal static StatementSyntax AssignDefaultValueStatementSyntax(IParameterSymbol parameter) =>
        ExpressionStatement(
            AssignmentExpression(
                SyntaxKind.SimpleAssignmentExpression,
                IdentifierName(parameter.Name),
                DefaultExpression(TypeSyntax(parameter.Type))
            )
        );

    internal static TypeParameterListSyntax TypeParameterList(IReadOnlyList<NameSyntax> genericArguments)
    {
        return SyntaxFactory.TypeParameterList(
            SeparatedList(
                genericArguments.Select(name => SyntaxFactory.TypeParameter(Identifier(((IdentifierNameSyntax)name).Identifier.Text)))
            )
        );
    }

    internal static TypeParameterListSyntax TypeParameterList(IReadOnlyList<IdentifierNameSyntax> genericArguments)
    {
        return SyntaxFactory.TypeParameterList(
            SeparatedList(
                genericArguments.Select(name => SyntaxFactory.TypeParameter(Identifier(name.Identifier.Text)))
            )
        );
    }
}