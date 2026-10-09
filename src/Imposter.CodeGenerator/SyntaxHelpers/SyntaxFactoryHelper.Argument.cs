using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    internal static readonly ArgumentListSyntax EmptyArgumentListSyntax = ArgumentList();

    internal static ArgumentListSyntax ArgumentListSyntax(IEnumerable<ArgumentSyntax> arguments) =>
        ArgumentList(SeparatedList(arguments));

    internal static ArgumentSyntax OutDiscardArgument() =>
        Argument(null, Token(SyntaxKind.OutKeyword), IdentifierName("_"));

    internal static ArgumentSyntax OutVarArgument(string name) =>
        Argument(
            null,
            Token(SyntaxKind.OutKeyword),
            DeclarationExpression(Var, SingleVariableDesignation(Identifier(name)))
        );

    internal static ArgumentListSyntax ToSingleArgumentList(this ExpressionSyntax expression) =>
        Argument(expression).ToSingleArgumentList();

    internal static ArgumentListSyntax ToSingleArgumentList(this ArgumentSyntax argument) =>
        ArgumentList(SingletonSeparatedList(argument));

    internal static ArgumentSyntax ToArgument(this string argumentName) =>
        Argument(IdentifierName(argumentName));
}
