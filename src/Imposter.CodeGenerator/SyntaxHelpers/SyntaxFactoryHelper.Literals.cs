using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    internal static readonly LiteralExpressionSyntax True = LiteralExpression(
        SyntaxKind.TrueLiteralExpression
    );

    internal static readonly LiteralExpressionSyntax False = LiteralExpression(
        SyntaxKind.FalseLiteralExpression
    );

    internal static readonly LiteralExpressionSyntax Default = LiteralExpression(
        SyntaxKind.DefaultLiteralExpression
    );

    internal static readonly LiteralExpressionSyntax Null = LiteralExpression(
        SyntaxKind.NullLiteralExpression
    );

    internal static readonly PostfixUnaryExpressionSyntax DefaultNonNullable =
        PostfixUnaryExpression(SyntaxKind.SuppressNullableWarningExpression, Default);

    // Declared in this file, below DefaultNonNullable: the static fields of a partial class initialize in declaration
    // order within a file, but in no specified order across its files.
    internal static readonly ReturnStatementSyntax ReturnDefaultNonNullable = ReturnStatement(
        DefaultNonNullable
    );
}
