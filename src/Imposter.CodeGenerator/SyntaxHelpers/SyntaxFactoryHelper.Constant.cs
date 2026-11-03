using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal partial class SyntaxFactoryHelper
{
    public static readonly LiteralExpressionSyntax True = LiteralExpression(
        SyntaxKind.TrueLiteralExpression
    );

    public static readonly LiteralExpressionSyntax False = LiteralExpression(
        SyntaxKind.FalseLiteralExpression
    );

    internal static LiteralExpressionSyntax NumericLiteral(int value) =>
        NumericLiteral(Literal(value));

    private static LiteralExpressionSyntax NumericLiteral(SyntaxToken literal) =>
        LiteralExpression(SyntaxKind.NumericLiteralExpression, literal);
}
