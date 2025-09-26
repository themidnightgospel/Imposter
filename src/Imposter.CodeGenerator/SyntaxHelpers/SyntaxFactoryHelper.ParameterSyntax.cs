using System.Collections.Generic;
using System.Linq;
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

    internal static ParameterListSyntax ParameterListSyntax(IEnumerable<IParameterSymbol> parameters) => SyntaxFactory.ParameterList(SeparatedList(parameters.Select(ParameterSyntax)));

    internal static ParameterSyntax ParameterSyntax(IParameterSymbol parameter) => ParameterSyntax(parameter, true);

    internal static ParameterSyntax ParameterSyntax(IParameterSymbol parameter, bool includeRefKind)
    {
        var paramSyntax = Parameter(Identifier(parameter.Name))
            .WithType(TypeSyntax(parameter.Type));

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
                paramSyntax = paramSyntax.WithModifiers(TokenList(modifier));
            }
        }

        if (parameter.HasExplicitDefaultValue)
        {
            var defaultValue = parameter.ExplicitDefaultValue != null
                ? ParseExpression(parameter.ExplicitDefaultValue.ToString())
                : LiteralExpression(SyntaxKind.NullLiteralExpression);

            paramSyntax = paramSyntax.WithDefault(
                EqualsValueClause(defaultValue));
        }

        return paramSyntax;
    }

    internal static StatementSyntax AssignDefaultValueStatementSyntax(IParameterSymbol parameter) =>
        ExpressionStatement(
            AssignmentExpression(
                SyntaxKind.SimpleAssignmentExpression,
                IdentifierName(parameter.Name),
                DefaultExpression(TypeSyntax(parameter.Type))
            )
        );
}