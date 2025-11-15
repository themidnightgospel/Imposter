using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    public static readonly LiteralExpressionSyntax Default = LiteralExpression(
        SyntaxKind.DefaultLiteralExpression
    );

    public static readonly LiteralExpressionSyntax Null = LiteralExpression(
        SyntaxKind.NullLiteralExpression
    );

    internal static readonly PostfixUnaryExpressionSyntax DefaultNonNullable =
        PostfixUnaryExpression(SyntaxKind.SuppressNullableWarningExpression, Default);

    internal static readonly ReturnStatementSyntax ReturnDefault = ReturnStatement(Default);

    internal static readonly ReturnStatementSyntax ReturnDefaultNonNullable = ReturnStatement(
        DefaultNonNullable
    );

    internal static readonly ReturnStatementSyntax ReturnVoid = ReturnStatement();
}
