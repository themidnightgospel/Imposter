using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    internal static BinaryExpressionSyntax Add(
        this ExpressionSyntax left,
        ExpressionSyntax right
    ) => BinaryExpression(SyntaxKind.AddExpression, left, right);
}
