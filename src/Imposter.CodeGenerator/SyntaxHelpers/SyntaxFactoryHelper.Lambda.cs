using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    public static ParenthesizedLambdaExpressionSyntax Lambda(
        ParameterListSyntax parameterList,
        BlockSyntax body
    ) => ParenthesizedLambdaExpression(parameterList, body);

    public static ParenthesizedLambdaExpressionSyntax AsyncLambda(
        ParameterListSyntax parameterList,
        BlockSyntax body
    )
    {
        return ParenthesizedLambdaExpression(
            asyncKeyword: Token(SyntaxKind.AsyncKeyword),
            parameterList: parameterList,
            arrowToken: Token(SyntaxKind.EqualsGreaterThanToken),
            body: body
        );
    }

    public static ParenthesizedLambdaExpressionSyntax EmptyParametersGoesTo(
        CSharpSyntaxNode body
    ) => ParenthesizedLambdaExpression(ParameterList(), body);

    public static SimpleLambdaExpressionSyntax DiscardParameterGoesTo(CSharpSyntaxNode body) =>
        Identifier("_").Lambda(body);

    public static SimpleLambdaExpressionSyntax Lambda(
        this ParameterSyntax lambdaParameter,
        CSharpSyntaxNode body
    ) => SimpleLambdaExpression(lambdaParameter, body);

    public static SimpleLambdaExpressionSyntax Lambda(
        this in SyntaxToken lambdaParameter,
        CSharpSyntaxNode body
    ) => SimpleLambdaExpression(Parameter(lambdaParameter), body);
}
