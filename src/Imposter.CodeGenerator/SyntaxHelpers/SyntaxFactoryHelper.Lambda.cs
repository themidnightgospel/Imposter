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

    public static SimpleLambdaExpressionSyntax GoesTo(this ParameterSyntax lambdaParameter, CSharpSyntaxNode body)
        => SyntaxFactory.SimpleLambdaExpression(lambdaParameter, body);
    
    public static SimpleLambdaExpressionSyntax GoesTo(this SyntaxToken lambdaParameter, CSharpSyntaxNode body)
        => SyntaxFactory.SimpleLambdaExpression(Parameter(lambdaParameter), body);
}