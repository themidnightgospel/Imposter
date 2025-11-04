using Imposter.CodeGenerator.Features.MethodImposter.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImposter.Builders.InvocationSetup;

internal static partial class InvocationSetupBuilder
{
    internal static MethodDeclarationSyntax InvokeMethodDeclarationSyntax(in ImposterTargetMethodMetadata method)
    {
        var invocationImposterType = IdentifierName(InvocationSetupMetadata.MethodInvocationImposterTypeName);
        var invocationImposterIdentifier = IdentifierName("invocationImposter");
        var invocationImposterAssignment = LocalDeclarationStatement(
            VariableDeclaration(invocationImposterType)
                .WithVariables(
                    SingletonSeparatedList(
                        VariableDeclarator(Identifier("invocationImposter"))
                            .WithInitializer(
                                EqualsValueClause(
                                    BinaryExpression(
                                        SyntaxKind.CoalesceExpression,
                                        InvocationExpression(IdentifierName("GetInvocationImposter")),
                                        MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            invocationImposterType,
                                            IdentifierName("Default")))
                                )
                            )
                    )
                )
        );

        var invokeCall = InvocationExpression(
            MemberAccessExpression(
                SyntaxKind.SimpleMemberAccessExpression,
                invocationImposterIdentifier,
                IdentifierName("Invoke")),
            SyntaxFactoryHelper.ArgumentListSyntax(method.Symbol.Parameters)
        );

        var methodDeclaration = new MethodDeclarationBuilder(method.ReturnTypeSyntax, "Invoke")
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .WithParameterList(method.Parameters.ParameterListSyntax)
            .WithBody(
                Block(
                    invocationImposterAssignment,
                    method.Symbol.ReturnsVoid
                        ? ExpressionStatement(invokeCall)
                        : ReturnStatement(invokeCall)
                )
            )
            .Build();

        return methodDeclaration;
    }
}
