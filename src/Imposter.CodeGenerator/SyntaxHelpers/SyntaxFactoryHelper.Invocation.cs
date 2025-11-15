using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    internal static InvocationExpressionSyntax Call(
        this ExpressionSyntax source,
        ArgumentSyntax argumentSyntax
    ) => source.Call(argumentSyntax.AsSingleArgumentListSyntax());

    internal static InvocationExpressionSyntax Call(
        this ExpressionSyntax source,
        IEnumerable<ArgumentSyntax> argumentSyntaxes
    ) => source.Call(ArgumentListSyntax(argumentSyntaxes));

    internal static InvocationExpressionSyntax Call(
        this ExpressionSyntax source,
        ArgumentListSyntax? argumentListSyntax = null
    ) =>
        argumentListSyntax is null
            ? InvocationExpression(source)
            : InvocationExpression(source, argumentListSyntax);
}
