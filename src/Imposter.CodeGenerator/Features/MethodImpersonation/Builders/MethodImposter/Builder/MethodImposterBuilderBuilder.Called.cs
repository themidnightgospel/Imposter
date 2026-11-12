using Imposter.CodeGenerator.Features.MethodImpersonation.Builders.Shared;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationHistory;
using Imposter.CodeGenerator.Helpers;
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

    private static MethodDeclarationSyntax BuildCalledMethod(
        in ImposterTargetMethodMetadata method,
        bool withReason = false
    )
    {
        var called = method.InvocationVerifierInterface.CalledMethod;
        var count = IdentifierName(called.CountParameter.Name);
        var invocationCount = IdentifierName(method.Locals["invocationCount"]);
        var invocationHistory = IdentifierName(method.InvocationHistory.Collection.AsField.Name);

        var builder = new MethodDeclarationBuilder(called.ReturnType, called.Name).AddParameter(
            ParameterSyntax(called.CountParameter)
        );
        if (withReason)
            builder.AddParameter(ParameterSyntax(called.BecauseParameter));
        return builder
            .WithExplicitInterfaceSpecifier(method.InvocationVerifierInterface.Syntax)
            .WithBody(
                Block(
                    LocalVariableDeclarationSyntax(
                        Var,
                        invocationCount.Identifier.Text,
                        BuildVerificationCountExpression(method, count)
                    ),
                    IfStatement(
                        CountDoesNotMatch(count, invocationCount),
                        Block(
                            ThrowStatement(
                                WellKnownTypes.Imposter.Abstractions.VerificationFailedException.New(
                                    ArgumentListSyntax([
                                        Argument(method.Model.Name.StringLiteral()),
                                        Argument(BuildExpectedInvocation(method)),
                                        Argument(count),
                                        Argument(invocationCount),
                                        Argument(
                                            invocationHistory.Dot(IdentifierName("ToString")).Call()
                                        ),
                                        Argument(
                                            withReason
                                                ? IdentifierName(called.BecauseParameter.Name)
                                                : Null
                                        ),
                                        Argument(
                                            ConditionalExpression(
                                                BinaryExpression(
                                                    Microsoft
                                                        .CodeAnalysis
                                                        .CSharp
                                                        .SyntaxKind
                                                        .EqualsExpression,
                                                    invocationHistory
                                                        .Dot(IdentifierName("Snapshot"))
                                                        .Call()
                                                        .Dot(IdentifierName("Count")),
                                                    NumericLiteral(0)
                                                ),
                                                WellKnownTypes
                                                    .Imposter.Abstractions.DiagnosticValue.Dot(
                                                        IdentifierName("OtherInteractions")
                                                    )
                                                    .Call(
                                                        Argument(
                                                            invocationHistory.Dot(
                                                                IdentifierName(
                                                                    InvocationStepNames.OwnerPropertyName
                                                                )
                                                            )
                                                        )
                                                    ),
                                                Null
                                            )
                                        ),
                                    ])
                                )
                            )
                        )
                    )
                )
            )
            .Build();
    }

    private static InvocationExpressionSyntax BuildVerificationCountExpression(
        in ImposterTargetMethodMetadata method,
        ExpressionSyntax count
    )
    {
        var arguments = new System.Collections.Generic.List<ArgumentSyntax> { Argument(count) };
        if (method.Parameters.HasInputParameters)
            arguments.Add(
                Argument(IdentifierName(method.MethodImposter.Builder.ArgumentsCriteriaField.Name))
            );
        return IdentifierName(method.InvocationHistory.Collection.AsField.Name)
            .Dot(method.WithGenericArguments(InvocationHistoryCollectionMetadata.VerifyMethodName))
            .Call(ArgumentListSyntax(arguments));
    }

    private static BinaryExpressionSyntax BuildExpectedInvocation(
        in ImposterTargetMethodMetadata method
    ) =>
        MethodDiagnosticBuilder.Expected(
            method,
            IdentifierName(method.MethodImposter.Builder.ArgumentsCriteriaField.Name)
        );
}
