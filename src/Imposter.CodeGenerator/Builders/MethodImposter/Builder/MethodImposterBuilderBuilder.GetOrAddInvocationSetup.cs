using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.MethodImposter.Builder;

internal static partial class MethodImposterBuilderBuilder
{
    private static MethodDeclarationSyntax GetOrAddInvocationSetupMethod(ImposterTargetMethodMetadata method)
    {
        var invocationSetupType = method.InvocationSetupType.Syntax;
        var argumentsCriteriaIdentifier = IdentifierName("_argumentsCriteria");
        var existingInvocationSetupIdentifier = IdentifierName("_existingInvocationSetup");

        var ifBody = Block(
            ExpressionStatement(
                existingInvocationSetupIdentifier
                    .Assign(invocationSetupType
                        .New(method.ParametersExceptOut.Count > 0
                            ? Argument(argumentsCriteriaIdentifier).AsSingleArgument()
                            : null
                        )
                    )
            ),
            (method.Symbol.IsGenericMethod
                ? IdentifierName("_imposterCollection")
                    .Dot(GenericName(Identifier("AddNew"), method.GenericTypeArguments.AsTypeArguments()))
                    .Call()
                : (ExpressionSyntax)IdentifierName("_imposter")
            )
            .Dot(IdentifierName("_invocationSetups"))
            .Dot(IdentifierName("Push"))
            .Call(ArgumentList(SingletonSeparatedList(Argument(existingInvocationSetupIdentifier))))
            .AsStatement()
        );

        return MethodDeclaration(invocationSetupType, "GetOrAddInvocationSetup")
            .AddModifiers(Token(SyntaxKind.PrivateKeyword))
            .WithBody(
                Block(
                    IfStatement(
                        IsPatternExpression(
                            existingInvocationSetupIdentifier,
                            ConstantPattern(LiteralExpression(SyntaxKind.NullLiteralExpression))
                        ),
                        ifBody
                    ),
                    ReturnStatement(existingInvocationSetupIdentifier)
                )
            );
    }
}