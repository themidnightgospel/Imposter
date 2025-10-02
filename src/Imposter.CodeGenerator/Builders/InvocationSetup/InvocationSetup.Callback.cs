using Imposter.CodeGenerator.Contexts;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.InvocationSetup;

internal static partial class InvocationSetup
{
    private static MethodDeclarationSyntax CallBeforeReturnMethodDeclarationSyntax(ImposterTargetMethodMetadata method)
        => CallbackMethodDeclarationSyntax(method, "CallBefore");

    private static MethodDeclarationSyntax CallAfterReturnMethodDeclarationSyntax(ImposterTargetMethodMetadata method)
        => CallbackMethodDeclarationSyntax(method, "CallAfter");

    private static MethodDeclarationSyntax CallbackMethodDeclarationSyntax(ImposterTargetMethodMetadata method, string callbackName) =>
        MethodDeclaration(
                method.InvocationSetupType.Interface.Syntax,
                Identifier(callbackName)
            )
            .AddModifiers(Token(SyntaxKind.PublicKeyword))
            .AddParameterListParameters(
                Parameter(Identifier("callback"))
                    .WithType(method.CallbackDelegate.Syntax)
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