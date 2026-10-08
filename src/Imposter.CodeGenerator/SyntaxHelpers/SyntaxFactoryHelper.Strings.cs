using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    internal static LiteralExpressionSyntax StringLiteral(this string value) =>
        LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(value));

    internal static InvocationExpressionSyntax JoinWithNewLines(ExpressionSyntax values) =>
        WellKnownTypes
            .String.Dot(IdentifierName("Join"))
            .Call([
                Argument(WellKnownTypes.System.Environment.Dot(IdentifierName("NewLine"))),
                Argument(values),
            ]);
}
