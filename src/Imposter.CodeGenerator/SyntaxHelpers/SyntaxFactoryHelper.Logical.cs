using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    public static BinaryExpressionSyntax And(this ExpressionSyntax left, ExpressionSyntax right)
        => BinaryExpression(SyntaxKind.LogicalAndExpression, left.ParenthesizeIfNeeded(), right.ParenthesizeIfNeeded());

    private static ExpressionSyntax ParenthesizeIfNeeded(this ExpressionSyntax expr)
        => expr is BinaryExpressionSyntax ? ParenthesizedExpression(expr) : expr;

    internal static IsPatternExpressionSyntax Is(this ExpressionSyntax expression, PatternSyntax pattern) =>
        IsPatternExpression(
            expression,
            pattern);

    internal static BinaryExpressionSyntax IsNotNull(this ExpressionSyntax left) =>
        BinaryExpression(
            SyntaxKind.NotEqualsExpression,
            left,
            LiteralExpression(SyntaxKind.NullLiteralExpression)
        );

    internal static BinaryExpressionSyntax IsNull(this ExpressionSyntax left) =>
        BinaryExpression(
            SyntaxKind.EqualsExpression,
            left,
            LiteralExpression(SyntaxKind.NullLiteralExpression)
        );

    internal static PrefixUnaryExpressionSyntax Not(ExpressionSyntax operand) => PrefixUnaryExpression(SyntaxKind.LogicalNotExpression, operand);
}