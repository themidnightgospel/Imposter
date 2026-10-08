using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    public static BinaryExpressionSyntax And(this ExpressionSyntax left, ExpressionSyntax right) =>
        BinaryExpression(
            SyntaxKind.LogicalAndExpression,
            left.ParenthesizeIfNeeded(),
            right.ParenthesizeIfNeeded()
        );

    internal static PrefixUnaryExpressionSyntax Not(ExpressionSyntax operand) =>
        PrefixUnaryExpression(SyntaxKind.LogicalNotExpression, operand);

    private static ExpressionSyntax ParenthesizeIfNeeded(this ExpressionSyntax expr) =>
        expr is BinaryExpressionSyntax ? ParenthesizedExpression(expr) : expr;

    internal static BinaryExpressionSyntax IsNotNull(this ExpressionSyntax left) =>
        BinaryExpression(SyntaxKind.NotEqualsExpression, left, Null);

    internal static BinaryExpressionSyntax IsNull(this ExpressionSyntax left) =>
        BinaryExpression(SyntaxKind.EqualsExpression, left, Null);

    // ArgumentNullException.ThrowIfNull is missing from .NET Standard 2.0 and .NET Framework.
    internal static IfStatementSyntax ThrowIfNull(string parameterName) =>
        IfStatement(
            IsPatternExpression(IdentifierName(parameterName), ConstantPattern(Null)),
            Block(
                ThrowStatement(
                    WellKnownTypes.System.ArgumentNullException.New(
                        IdentifierName("nameof")
                            .Call(Argument(IdentifierName(parameterName)))
                            .ToSingleArgumentList()
                    )
                )
            )
        );

    internal static BinaryExpressionSyntax IsNotDefault(this ExpressionSyntax left) =>
        BinaryExpression(SyntaxKind.NotEqualsExpression, left, Default);

    internal static BinaryExpressionSyntax IsDefault(this ExpressionSyntax left) =>
        BinaryExpression(SyntaxKind.EqualsExpression, left, Default);
}
