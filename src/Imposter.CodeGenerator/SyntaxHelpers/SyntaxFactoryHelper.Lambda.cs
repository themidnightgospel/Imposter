using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    public static ParenthesizedLambdaExpressionSyntax Lambda(IEnumerable<IParameterSymbol> parameters, BlockSyntax body) =>
        ParenthesizedLambdaExpression(ParameterListSyntax(parameters), body);

    public static ParenthesizedLambdaExpressionSyntax EmptyParametersGoesTo(CSharpSyntaxNode body) =>
        ParenthesizedLambdaExpression(
            ParameterList(),
            body
        );

    public static SimpleLambdaExpressionSyntax Lambda(this ParameterSyntax lambdaParameter, CSharpSyntaxNode body)
        => SimpleLambdaExpression(lambdaParameter, body);

    public static SimpleLambdaExpressionSyntax Lambda(this SyntaxToken lambdaParameter, CSharpSyntaxNode body)
        => SimpleLambdaExpression(Parameter(lambdaParameter), body);
}