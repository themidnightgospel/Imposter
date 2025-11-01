using Imposter.CodeGenerator.Features.MethodImposter.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImposter.Builders.MethodImposter.Builder;

internal static partial class MethodImposterBuilderBuilder
{
    private static MethodDeclarationSyntax GetOrAddInvocationSetupMethod(in ImposterTargetMethodMetadata method)
    {
        var invocationSetupType = method.InvocationSetup.Syntax;
        var argumentsCriteriaIdentifier = IdentifierName("_argumentsCriteria");
        var existingInvocationSetupIdentifier = IdentifierName(MethodImposterMetadata.ExistingInvocationSetupFieldName);

        var ifBody = Block(
            ExpressionStatement(
                existingInvocationSetupIdentifier
                    .Assign(invocationSetupType
                        .New(method.Parameters.HasInputParameters
                            ? Argument(argumentsCriteriaIdentifier).ToSingleArgumentList()
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
            .Dot(IdentifierName(method.MethodImposter.InvocationSetupsField.Name))
            .Dot(IdentifierName("Push"))
            .Call(ArgumentList(SingletonSeparatedList(Argument(existingInvocationSetupIdentifier))))
            .ToStatementSyntax()
        );

        return MethodDeclaration(method.InvocationSetup.Interface.Syntax, "GetOrAddInvocationSetup")
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