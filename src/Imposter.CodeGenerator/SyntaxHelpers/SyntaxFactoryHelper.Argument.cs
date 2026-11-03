using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    internal static readonly ArgumentListSyntax EmptyArgumentListSyntax = ArgumentList();

    internal static ArgumentListSyntax ArgumentListSyntax(IEnumerable<ArgumentSyntax> arguments) =>
        ArgumentList(SeparatedList(arguments));

    internal static ArgumentListSyntax ArgumentListSyntax(IEnumerable<ParameterModel> parameters) =>
        ArgumentListSyntax(parameters.Select(ArgumentSyntax));

    internal static ArgumentSyntax ArgumentSyntax(ParameterModel parameter)
    {
        var name = IdentifierName(EscapeKeyword(parameter.Name));

        return PassesAsObject(parameter)
            ? Argument(AsObject(name, parameter.Type))
            : Argument(null, Token(ForwardingKeyword(parameter.RefKind)), name);
    }

    // Preserves the parameter's ref kind when forwarding a variable. Explicit `in` keeps an `in` overload selected
    // over a by-value overload and avoids CS9192/CS9195 for `ref readonly` parameters.
    internal static ArgumentSyntax ForwardingArgument(string variableName, RefKind refKind) =>
        Argument(null, Token(ForwardingKeyword(refKind)), IdentifierName(variableName));

    private static SyntaxKind ForwardingKeyword(RefKind refKind) =>
        refKind switch
        {
            RefKind.Ref => SyntaxKind.RefKeyword,
            RefKind.Out => SyntaxKind.OutKeyword,
            RefKind.In or RefKinds.RefReadOnlyParameter => SyntaxKind.InKeyword,
            _ => SyntaxKind.None,
        };

    internal static ArgumentSyntax RefArgument(string name) => RefArgument(IdentifierName(name));

    internal static ArgumentSyntax RefArgument(ExpressionSyntax variable) =>
        Argument(null, Token(SyntaxKind.RefKeyword), variable);

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
