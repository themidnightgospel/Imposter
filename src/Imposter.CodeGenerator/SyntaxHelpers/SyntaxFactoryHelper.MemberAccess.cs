using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    internal static MemberAccessExpressionSyntax SimpleMemberAccess(ExpressionSyntax left, SimpleNameSyntax right) =>
        MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, left, right);

    internal static MemberAccessExpressionSyntax Dot(this ExpressionSyntax source, SimpleNameSyntax right)
        => SimpleMemberAccess(source, right);

    internal static InvocationExpressionSyntax Call(this ExpressionSyntax source, ArgumentListSyntax? argumentListSyntax = null)
    {
        return argumentListSyntax is null
            ? InvocationExpression(source)
            : InvocationExpression(
                source,
                argumentListSyntax
            );
    }

    internal static ExpressionStatementSyntax AsStatement(this ExpressionSyntax source)
        => ExpressionStatement(source);

    internal static AssignmentExpressionSyntax Assign(this ExpressionSyntax left, ExpressionSyntax right)
        => AssignmentExpression(SyntaxKind.SimpleAssignmentExpression, left, right);

    internal static ObjectCreationExpressionSyntax New(this TypeSyntax type, ArgumentListSyntax? arguments = null, InitializerExpressionSyntax? initializer = default)
        => ObjectCreationExpression(type, arguments ?? ArgumentList(), initializer);

    internal static BinaryExpressionSyntax QuestionMarkQuestionMark(this ExpressionSyntax left, ExpressionSyntax right)
        => BinaryExpression(SyntaxKind.CoalesceExpression, left, right);
}