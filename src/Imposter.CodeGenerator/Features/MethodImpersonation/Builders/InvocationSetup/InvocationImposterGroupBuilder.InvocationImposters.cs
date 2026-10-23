using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationSetup;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.InvocationSetup;

internal static partial class InvocationImposterGroupBuilder
{
    internal static FieldDeclarationSyntax InvocationImpostersFieldDeclaration(
        in ImposterTargetMethodMetadata method
    )
    {
        var invocationImposterType = IdentifierName(
            method.MethodInvocationImposterGroup.MethodInvocationImposterTypeName
        );
        var queueType = WellKnownTypes.System.Collections.Concurrent.ConcurrentQueue(
            invocationImposterType
        );

        return SinglePrivateReadonlyVariableField(
            queueType,
            MethodInvocationImposterGroupMetadata.InvocationImpostersFieldName,
            queueType.New(ArgumentList())
        );
    }

    internal static FieldDeclarationSyntax LastInvocationImposterFieldDeclaration(
        in ImposterTargetMethodMetadata method
    ) =>
        SingleVariableField(
            IdentifierName(method.MethodInvocationImposterGroup.MethodInvocationImposterTypeName)
                .ToNullableType(),
            MethodInvocationImposterGroupMetadata.LatestInvocationImposterFieldName,
            TokenList(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.VolatileKeyword))
        );

    internal static MethodDeclarationSyntax AddInvocationImposterMethod(
        in ImposterTargetMethodMetadata method
    )
    {
        var invocationImposterType = IdentifierName(
            method.MethodInvocationImposterGroup.MethodInvocationImposterTypeName
        );

        var bodyBuilder = new BlockBuilder().AddStatement(
            LocalVariableDeclarationSyntax(
                invocationImposterType,
                "invocationImposter",
                invocationImposterType.New(ArgumentList())
            )
        );

        if (!method.HasReturnValue)
        {
            bodyBuilder.AddStatement(
                IdentifierName("invocationImposter")
                    .Dot(IdentifierName("UseDefaultResultGenerator"))
                    .Call(ArgumentList())
                    .ToStatementSyntax()
            );
        }

        bodyBuilder
            .AddStatement(
                IdentifierName(MethodInvocationImposterGroupMetadata.InvocationImpostersFieldName)
                    .Dot(ConcurrentQueueSyntaxHelper.Enqueue)
                    .Call(Argument(IdentifierName("invocationImposter")))
                    .ToStatementSyntax()
            )
            .AddStatement(ReturnStatement(IdentifierName("invocationImposter")));

        return new MethodDeclarationBuilder(
            invocationImposterType,
            MethodInvocationImposterGroupMetadata.AddInvocationImposterMethodName
        )
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .WithBody(bodyBuilder.Build())
            .Build();
    }

    internal static MethodDeclarationSyntax GetInvocationImposterMethod(
        in ImposterTargetMethodMetadata method
    )
    {
        var invocationImposterType = IdentifierName(
            method.MethodInvocationImposterGroup.MethodInvocationImposterTypeName
        );

        return new MethodDeclarationBuilder(
            invocationImposterType.ToNullableType(),
            method.MethodInvocationImposterGroup.GetInvocationImposterMethodName
        )
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .WithBody(
                NextOutcomeSyntaxHelper.TakeNextOutcomeBody(
                    IdentifierName(
                        MethodInvocationImposterGroupMetadata.InvocationImpostersFieldName
                    ),
                    IdentifierName(
                        MethodInvocationImposterGroupMetadata.LatestInvocationImposterFieldName
                    ),
                    IdentifierName("invocationImposter"),
                    // Do not consume a pending step or invoke its mutable default generator. A later Returns,
                    // Throws or Callback must still be able to publish this same step to the queue's readers.
                    IdentifierName("invocationImposter")
                        .Dot(IdentifierName(method.MethodInvocationImposter.IsConfiguredFieldName))
                )
            )
            .Build();
    }
}
