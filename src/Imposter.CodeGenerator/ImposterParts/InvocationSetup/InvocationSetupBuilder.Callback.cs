using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.ImposterParts.InvocationSetup;

internal static partial class InvocationSetupBuilder
{
    private static MethodDeclarationSyntax CallbackMethodDeclarationSyntax(ImposterTargetMethod method, string callbackName)
    {
        return MethodDeclaration(
                IdentifierName(method.InvocationsSetupBuilder),
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
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            GetMethodCallSetupForCallbackSyntax,
                            IdentifierName("CallAfter")
                        ),
                        IdentifierName("callback"))
                ),
                ReturnStatement(ThisExpression())
            ));
    }

    private static MethodDeclarationSyntax CallBeforeReturnMethodDeclarationSyntax(ImposterTargetMethod method)
        => CallbackMethodDeclarationSyntax(method, "CallBefore");

    private static MethodDeclarationSyntax CallAfterReturnMethodDeclarationSyntax(ImposterTargetMethod method)
        => CallbackMethodDeclarationSyntax(method, "CallAfter");
}
