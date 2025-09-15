using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Helpers;

internal static partial class SyntaxFactoryHelper
{
    internal static LiteralExpressionSyntax Literal(object? obj)
    {
        return obj switch
        {
            null => DefaultLiteralExpression(),
            int i => IntLiteralExpression(i),
            uint i => IntLiteralExpression(i),
            bool b => BooleanLiteralExpression(b),
            string s => StringLiteralExpression(s),
            char c => CharLiteralExpression(c),
            long l => LongLiteralExpression(l),
            ulong l => LongLiteralExpression(l),
            decimal d => DecimalLiteralExpression(d),
            double d => DoubleLiteralExpression(d),
            float f => FloatLiteralExpression(f),
            _ => throw new ArgumentException($"Object of type {obj.GetType()} cannot be converted to a literal expression."),
        };
    }

    internal static LiteralExpressionSyntax DefaultLiteralExpression() => LiteralExpression(SyntaxKind.DefaultLiteralExpression);

    internal static LiteralExpressionSyntax NullLiteral() => LiteralExpression(SyntaxKind.NullLiteralExpression);

    internal static LiteralExpressionSyntax BooleanLiteralExpression(bool b) =>
        LiteralExpression(b ? SyntaxKind.TrueLiteralExpression : SyntaxKind.FalseLiteralExpression);

    internal static LiteralExpressionSyntax IntLiteralExpression(int i) =>
        LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(i));

    internal static LiteralExpressionSyntax IntLiteralExpression(uint i) =>
        LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(i));

    internal static LiteralExpressionSyntax StringLiteralExpression(string content) =>
        LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(content));

    internal static LiteralExpressionSyntax CharLiteralExpression(char content) =>
        LiteralExpression(SyntaxKind.CharacterLiteralExpression, SyntaxFactory.Literal(content));

    internal static LiteralExpressionSyntax LongLiteralExpression(long content) =>
        LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(content));

    internal static LiteralExpressionSyntax LongLiteralExpression(ulong content) =>
        LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(content));

    internal static LiteralExpressionSyntax DecimalLiteralExpression(decimal content) =>
        LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(content));

    internal static LiteralExpressionSyntax DoubleLiteralExpression(double content) =>
        LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(content));

    internal static LiteralExpressionSyntax FloatLiteralExpression(float content) =>
        LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(content));
}