using Imposter.CodeGenerator.Features.MethodImpersonation.Builders.Shared;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.MethodImposter;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.Features.Shared.Builders.MissingImposterBuilder;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.InvocationSetup;

internal static partial class InvocationSetupBuilder
{
    internal static MethodDeclarationSyntax InvokeMethodDeclarationSyntax(
        in ImposterTargetMethodMetadata method
    )
    {
        var invocationImposterType = IdentifierName(
            method.MethodInvocationImposterGroup.MethodInvocationImposterTypeName
        );
        var invocationImposterIdentifier = IdentifierName(
            method.MethodImposter.InvokeMethod.InvocationImposterVariableName
        );
        var invocationImposterAssignment = LocalVariableDeclarationSyntax(
            Var,
            method.MethodImposter.InvokeMethod.InvocationImposterVariableName,
            IdentifierName(method.MethodInvocationImposterGroup.GetInvocationImposterMethodName)
                .Call()
        );

        var guardMissingImposter = IfStatement(
            invocationImposterIdentifier.IsNull(),
            Block(
                ThrowIfExplicit(
                    IdentifierName(
                        method.MethodImposter.InvokeMethod.InvocationBehaviorParameterName
                    ),
                    IdentifierName(
                        method.MethodImposter.InvokeMethod.MethodDisplayNameParameterName
                    )
                ),
                invocationImposterIdentifier
                    .Assign(invocationImposterType.Dot(IdentifierName("Default")))
                    .ToStatementSyntax()
            )
        );

        var invokeCall = invocationImposterIdentifier
            .Dot(IdentifierName(MethodImposterInvokeMethodMetadata.Name))
            .Call(
                InvokeSignatureBuilder.InvocationImposterArguments(
                    method,
                    IdentifierName(
                        method.MethodImposter.InvokeMethod.InvocationBehaviorParameterName
                    ),
                    IdentifierName(
                        method.MethodImposter.InvokeMethod.MethodDisplayNameParameterName
                    )
                )
            );

        var methodDeclaration = new MethodDeclarationBuilder(
            method.NullableAwareReturnTypeSyntax,
            MethodImposterInvokeMethodMetadata.Name
        )
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .WithParameterList(InvokeSignatureBuilder.InvocationImposterParameters(method))
            .WithBody(
                Block(
                    invocationImposterAssignment,
                    guardMissingImposter,
                    method.Model.ReturnType.IsVoid
                        ? invokeCall.ToStatementSyntax()
                        : ReturnStatement(invokeCall)
                )
            )
            .Build();

        return methodDeclaration;
    }
}
