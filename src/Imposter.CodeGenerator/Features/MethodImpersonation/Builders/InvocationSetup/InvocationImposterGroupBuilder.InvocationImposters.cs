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
            "_invocationImposters",
            queueType.New(ArgumentList())
        );
    }

    internal static FieldDeclarationSyntax LastInvocationImposterFieldDeclaration(
        in ImposterTargetMethodMetadata method
    ) =>
        SingleVariableField(
            IdentifierName(method.MethodInvocationImposterGroup.MethodInvocationImposterTypeName)
                .ToNullableType(),
            "_lastestInvocationImposter",
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
                IdentifierName("_invocationImposters")
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
                    IdentifierName("_invocationImposters"),
                    IdentifierName("_lastestInvocationImposter"),
                    IdentifierName("invocationImposter"),
                    // An invocation imposter without an outcome applies to its own call but never repeats.
                    Not(IdentifierName("invocationImposter").Dot(IdentifierName("IsEmpty")))
                )
            )
            .Build();
    }
}
