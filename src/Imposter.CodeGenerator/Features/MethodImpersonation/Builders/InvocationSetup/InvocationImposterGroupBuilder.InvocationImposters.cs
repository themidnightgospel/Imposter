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
        var sequenceType = WellKnownTypes.Imposter.Abstractions.ReturnValueSequence(
            IdentifierName(method.MethodInvocationImposterGroup.MethodInvocationImposterTypeName)
                .ToNullableType()
        );

        return SinglePrivateReadonlyVariableField(
            sequenceType,
            MethodInvocationImposterGroupMetadata.InvocationImpostersFieldName,
            sequenceType.New(ArgumentList())
        );
    }

    internal static MethodDeclarationSyntax CreateInvocationImposterMethod(
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

        bodyBuilder.AddStatement(ReturnStatement(IdentifierName("invocationImposter")));

        return new MethodDeclarationBuilder(
            invocationImposterType,
            MethodInvocationImposterGroupMetadata.CreateInvocationImposterMethodName
        )
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .WithBody(bodyBuilder.Build())
            .Build();
    }

    // Calls take a step only once it's configured, so a pending step is never consumed and a bare void setup's
    // default no-op never counts as an outcome. Configuring an added step again changes the step calls already take.
    internal static MethodDeclarationSyntax AddInvocationImposterMethod(
        in ImposterTargetMethodMetadata method
    )
    {
        var invocationImposter = IdentifierName("invocationImposter");
        var isConfigured = invocationImposter.Dot(
            IdentifierName(method.MethodInvocationImposter.IsConfiguredFieldName)
        );

        return new MethodDeclarationBuilder(
            WellKnownTypes.Void,
            MethodInvocationImposterGroupMetadata.AddInvocationImposterMethodName
        )
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(
                ParameterSyntax(
                    IdentifierName(
                        method.MethodInvocationImposterGroup.MethodInvocationImposterTypeName
                    ),
                    invocationImposter.Identifier.Text
                )
            )
            .WithBody(
                Block(
                    IfStatement(isConfigured, Block(ReturnStatement())),
                    isConfigured.Assign(True).ToStatementSyntax(),
                    IdentifierName(
                            MethodInvocationImposterGroupMetadata.InvocationImpostersFieldName
                        )
                        .Dot(IdentifierName("Add"))
                        .Call(Argument(invocationImposter))
                        .ToStatementSyntax(),
                    IdentifierName(
                            method.MethodInvocationImposterGroup.HasConfiguredOutcomeFieldName
                        )
                        .Assign(True)
                        .ToStatementSyntax()
                )
            )
            .Build();
    }
}
