using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    public static ParenthesizedLambdaExpressionSyntax Lambda(
        IEnumerable<IParameterSymbol> parameters,
        BlockSyntax body
    ) => ParenthesizedLambdaExpression(ParameterListSyntax(parameters), body);

    public static ParenthesizedLambdaExpressionSyntax AsyncLambda(
        IEnumerable<IParameterSymbol> parameters,
        BlockSyntax body
    )
    {
        return ParenthesizedLambdaExpression(
            asyncKeyword: Token(SyntaxKind.AsyncKeyword),
            parameterList: ParameterListSyntax(parameters),
            arrowToken: Token(SyntaxKind.EqualsGreaterThanToken),
            body: body
        );
    }

    public static ParenthesizedLambdaExpressionSyntax EmptyParametersGoesTo(
        CSharpSyntaxNode body
    ) => ParenthesizedLambdaExpression(ParameterList(), body);

    public static SimpleLambdaExpressionSyntax DiscardParameterGoesTo(
        CSharpSyntaxNode body,
        string parameterName = "_"
    ) => Identifier(parameterName).Lambda(body);

    public static SimpleLambdaExpressionSyntax Lambda(
        this ParameterSyntax lambdaParameter,
        CSharpSyntaxNode body
    ) => SimpleLambdaExpression(lambdaParameter, body);

    public static SimpleLambdaExpressionSyntax Lambda(
        this in SyntaxToken lambdaParameter,
        CSharpSyntaxNode body
    ) => SimpleLambdaExpression(Parameter(lambdaParameter), body);

    public static ParenthesizedLambdaExpressionSyntax CounterIncrementLambda(
        string discardParameterName = "_",
        string counterParameterName = "count"
    ) =>
        ParenthesizedLambdaExpression()
            .WithParameterList(
                ParameterList(
                    SeparatedList([
                        Parameter(Identifier(discardParameterName)),
                        Parameter(Identifier(counterParameterName)),
                    ])
                )
            )
            .WithExpressionBody(
                BinaryExpression(
                    SyntaxKind.AddExpression,
                    IdentifierName(counterParameterName),
                    LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(1))
                )
            );

    public static ParenthesizedLambdaExpressionSyntax CounterDecrementLambda(
        string discardParameterName = "_",
        string counterParameterName = "count"
    ) =>
        ParenthesizedLambdaExpression()
            .WithParameterList(
                ParameterList(
                    SeparatedList([
                        Parameter(Identifier(discardParameterName)),
                        Parameter(Identifier(counterParameterName)),
                    ])
                )
            )
            .WithBlock(
                Block(
                    IfStatement(
                        BinaryExpression(
                            SyntaxKind.GreaterThanExpression,
                            IdentifierName(counterParameterName),
                            LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0))
                        ),
                        Block(
                            ReturnStatement(
                                BinaryExpression(
                                    SyntaxKind.SubtractExpression,
                                    IdentifierName(counterParameterName),
                                    LiteralExpression(
                                        SyntaxKind.NumericLiteralExpression,
                                        Literal(1)
                                    )
                                )
                            )
                        )
                    ),
                    ReturnStatement(
                        LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0))
                    )
                )
            );
}
