using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    private static MemberAccessExpressionSyntax SimpleMemberAccess(
        ExpressionSyntax left,
        SimpleNameSyntax right
    ) => MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, left, right);

    internal static MemberAccessExpressionSyntax Dot(
        this ExpressionSyntax source,
        SimpleNameSyntax right
    ) => SimpleMemberAccess(source, right);
}
