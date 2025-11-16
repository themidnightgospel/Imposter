using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    internal static LiteralExpressionSyntax StringLiteral(this string value) =>
        LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(value));
}
