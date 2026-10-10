using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationHistory;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.Features.Shared.Builders.VerificationFailedBuilder;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter.Builder;

internal static partial class MethodImposterBuilderBuilder
{
    private static MethodDeclarationSyntax BuildCallCountMethod(
        in ImposterTargetMethodMetadata method
    ) =>
        new MethodDeclarationBuilder(
            WellKnownTypes.Int,
            InvocationVerifierInterfaceMetadata.CallCountMethodName
        )
            .WithExplicitInterfaceSpecifier(method.InvocationVerifierInterface.Syntax)
            .WithBody(Block(ReturnStatement(BuildInvocationCountExpression(method))))
            .Build();

    private static InvocationExpressionSyntax BuildInvocationCountExpression(
        in ImposterTargetMethodMetadata method
    ) =>
        IdentifierName(method.InvocationHistory.Collection.AsField.Name)
            .Dot(method.WithGenericArguments(InvocationHistoryCollectionCountMethodMetadata.Name))
            .Call(
                method.Parameters.HasInputParameters
                    ? Argument(
                            IdentifierName(
                                method.MethodImposter.Builder.ArgumentsCriteriaField.Name
                            )
                        )
                        .ToSingleArgumentList()
                    : EmptyArgumentListSyntax
            );

    private static MethodDeclarationSyntax BuildCalledMethod(in ImposterTargetMethodMetadata method)
    {
        var called = method.InvocationVerifierInterface.CalledMethod;
        var count = IdentifierName(called.CountParameter.Name);
        var invocationCount = IdentifierName("invocationCount");
        var invocationHistory = IdentifierName(method.InvocationHistory.Collection.AsField.Name);

        return new MethodDeclarationBuilder(called.ReturnType, called.Name)
            .AddParameter(ParameterSyntax(called.CountParameter))
            .WithExplicitInterfaceSpecifier(method.InvocationVerifierInterface.Syntax)
            .WithBody(
                Block(
                    LocalVariableDeclarationSyntax(
                        Var,
                        invocationCount.Identifier.Text,
                        BuildInvocationCountExpression(method)
                    ),
                    IfStatement(
                        CountDoesNotMatch(count, invocationCount),
                        Block(
                            ThrowVerificationFailed(
                                count,
                                invocationCount,
                                invocationHistory.Dot(IdentifierName("ToString")).Call()
                            )
                        )
                    )
                )
            )
            .Build();
    }
}
