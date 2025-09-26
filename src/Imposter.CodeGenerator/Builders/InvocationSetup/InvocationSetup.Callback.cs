using Imposter.CodeGenerator.Contexts;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.InvocationSetup;

internal static partial class InvocationSetup
{
    private static MethodDeclarationSyntax CallBeforeReturnMethodDeclarationSyntax(ImposterTargetMethod method)
        => CallbackMethodDeclarationSyntax(method, "CallBefore");

    private static MethodDeclarationSyntax CallAfterReturnMethodDeclarationSyntax(ImposterTargetMethod method)
        => CallbackMethodDeclarationSyntax(method, "CallAfter");

    private static MethodDeclarationSyntax CallbackMethodDeclarationSyntax(ImposterTargetMethod method, string callbackName) =>
        MethodDeclaration(
                IdentifierName(method.InvocationsSetupInterface),
                Identifier(callbackName)
            )
            .AddModifiers(Token(SyntaxKind.PublicKeyword))
            .AddParameterListParameters(
                Parameter(Identifier("callback"))
                    .WithType(IdentifierName(method.CallbackDelegateName))
            )
            .WithBody(Block(
                ExpressionStatement(
                    AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        GetMethodCallSetupAccessExpressionSyntax(callbackName),
                        IdentifierName("callback"))
                ),
                ReturnStatement(ThisExpression())
            ));
}