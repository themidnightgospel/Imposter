using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    internal static InvocationExpressionSyntax IsAssignableTo(this ExpressionSyntax left, ExpressionSyntax right) =>
        InvocationExpression(
            MemberAccessExpression(
                SyntaxKind.SimpleMemberAccessExpression,
                left,
                IdentifierName("IsAssignableTo")
            ),
            ArgumentList(SingletonSeparatedList(Argument(right)))
        );
}