using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    public static BinaryExpressionSyntax And(this ExpressionSyntax left, ExpressionSyntax right)
    {
        // Produces: (left) && (right) with appropriate tokens
        return BinaryExpression(SyntaxKind.LogicalAndExpression, left.ParenthesizeIfNeeded(), right.ParenthesizeIfNeeded());
    }

    // Extension helper: parenthesize simple to avoid precedence problems
    private static ExpressionSyntax ParenthesizeIfNeeded(this ExpressionSyntax expr)
    {
        // Naive rule: if it's a BinaryExpression, wrap in parentheses
        return expr is BinaryExpressionSyntax ? ParenthesizedExpression(expr) : expr;
    }

    internal static IsPatternExpressionSyntax Is(this ExpressionSyntax expression, PatternSyntax pattern)
    {
        return IsPatternExpression(
            expression,
            pattern);
    }

    internal static BinaryExpressionSyntax IsNotNull(this ExpressionSyntax left)
    {
        return BinaryExpression(
            SyntaxKind.NotEqualsExpression,
            left,
            LiteralExpression(SyntaxKind.NullLiteralExpression)
        );
    }
    
    internal static BinaryExpressionSyntax IsNull(this ExpressionSyntax left)
    {
        return BinaryExpression(
            SyntaxKind.EqualsExpression,
            left,
            LiteralExpression(SyntaxKind.NullLiteralExpression)
        );
    }

    internal static PrefixUnaryExpressionSyntax Not(ExpressionSyntax operand) => PrefixUnaryExpression(SyntaxKind.LogicalNotExpression, operand);
}