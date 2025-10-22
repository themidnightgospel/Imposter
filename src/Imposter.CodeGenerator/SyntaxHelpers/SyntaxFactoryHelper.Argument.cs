using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    internal static readonly ArgumentListSyntax EmptyArgumentListSyntax = ArgumentList();

    internal static ArgumentListSyntax ArgumentListSyntax(IEnumerable<ArgumentSyntax> arguments) => ArgumentList(SeparatedList(arguments));

    internal static ArgumentListSyntax AsSingleArgumentListSyntax(this ArgumentSyntax argument) => ArgumentList(SeparatedList([argument]));

    internal static ArgumentListSyntax ArgumentListSyntax(ArgumentSyntax? argument)
        => argument is null ? ArgumentList() : ArgumentListSyntax(SingletonSeparatedList(argument));

    internal static ArgumentSyntax OutVarArgument(string name) => Argument(
        null,
        Token(SyntaxKind.OutKeyword),
        DeclarationExpression(IdentifierName("var"), SingleVariableDesignation(Identifier(name)))
    );

    internal static ArgumentListSyntax ToSingleArgumentList(this ExpressionSyntax expression)
        => ArgumentList(SingletonSeparatedList(Argument(expression)));

    internal static ArgumentListSyntax ToSingleArgumentList(this ArgumentSyntax argument)
        => ArgumentListSyntax(SingletonSeparatedList(argument));

    internal static ArgumentSyntax AsArgument(this string argumentName)
        => Argument(IdentifierName(argumentName));
}