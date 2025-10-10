using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal partial class SyntaxFactoryHelper
{
    /// <summary>
    /// Creates a null-coalescing expression (leftExpr ?? rightExpr).
    /// </summary>
    public static BinaryExpressionSyntax CreateNullCoalescingOperator(ExpressionSyntax leftExpression, ExpressionSyntax rightExpression) =>
        BinaryExpression(
            SyntaxKind.CoalesceExpression,
            leftExpression,
            Token(SyntaxKind.QuestionQuestionToken),
            rightExpression
        );
}