using Imposter.CodeGenerator.Contexts;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.InvocationSetup;

internal static partial class InvocationSetupBuilder
{
    private static MethodDeclarationSyntax CallBeforeReturnMethodDeclarationSyntax(in ImposterTargetMethodMetadata method)
        => CallbackMethodDeclarationSyntax(method, "CallBefore");

    private static MethodDeclarationSyntax CallAfterReturnMethodDeclarationSyntax(in ImposterTargetMethodMetadata method)
        => CallbackMethodDeclarationSyntax(method, "CallAfter");

    private static MethodDeclarationSyntax CallbackMethodDeclarationSyntax(in ImposterTargetMethodMetadata method, in string callbackName) =>
        MethodDeclaration(
                method.InvocationSetup.Interface.Syntax,
                Identifier(callbackName)
            )
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(method.InvocationSetup.Interface.Syntax))
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