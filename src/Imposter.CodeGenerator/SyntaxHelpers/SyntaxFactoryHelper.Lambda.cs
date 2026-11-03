using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    internal static ParenthesizedLambdaExpressionSyntax Lambda(
        ParameterListSyntax parameterList,
        BlockSyntax body
    ) => ParenthesizedLambdaExpression(parameterList, body);

    internal static ParenthesizedLambdaExpressionSyntax EmptyParametersGoesTo(
        CSharpSyntaxNode body
    ) => ParenthesizedLambdaExpression(ParameterList(), body);

    internal static SimpleLambdaExpressionSyntax DiscardParameterGoesTo(CSharpSyntaxNode body) =>
        Identifier("_").Lambda(body);

    internal static SimpleLambdaExpressionSyntax Lambda(
        this in SyntaxToken lambdaParameter,
        CSharpSyntaxNode body
    ) => SimpleLambdaExpression(Parameter(lambdaParameter), body);
}
