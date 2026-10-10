using System.Collections.Generic;
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

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter;

internal partial class MethodImposterBuilder
{
    private static MethodDeclarationSyntax InvokeMethod(in ImposterTargetMethodMetadata method) =>
        new MethodDeclarationBuilder(
            method.NullableAwareReturnTypeSyntax,
            MethodImposterInvokeMethodMetadata.Name
        )
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .WithParameterList(InvokeSignatureBuilder.MethodImposterParameters(method))
            .WithBody(
                new BlockBuilder()
                    .AddStatement(DeclareAndInitializeArgumentsVariable(method))
                    .AddStatement(DeclareMatchingInvocationImposterGroupVariable(method))
                    .AddStatement(EnsureMatchingInvocationImposterGroup(method))
                    .AddStatement(
                        TryStatement(
                            new BlockBuilder()
                                .AddStatement(InvokeMatchingInvocationImposterGroup(method))
                                .AddStatement(
                                    AddToInvocationHistoryCollection(method, threwException: false)
                                )
                                .AddStatement(ReturnResultStatement(method))
                                .Build(),
                            SingletonList(
                                CatchClause(
                                    CatchDeclaration(
                                        WellKnownTypes.System.Exception,
                                        Identifier(
                                            method.MethodImposter.InvokeMethod.ExceptionVariableName
                                        )
                                    ),
                                    null,
                                    new BlockBuilder()
                                        .AddStatement(
                                            AddToInvocationHistoryCollection(
                                                method,
                                                threwException: true
                                            )
                                        )
                                        .AddStatement(ThrowStatement())
                                        .Build()
                                )
                            ),
                            default!
                        )
                    )
                    .Build()
            )
            .Build();

    private static ReturnStatementSyntax? ReturnResultStatement(
        in ImposterTargetMethodMetadata method
    )
    {
        return method.HasReturnValue
            ? ReturnStatement(IdentifierName(method.MethodImposter.InvokeMethod.ResultVariableName))
            : null;
    }

    private static LocalDeclarationStatementSyntax? DeclareAndInitializeArgumentsVariable(
        in ImposterTargetMethodMetadata method
    ) =>
        method.Parameters.HasInputParameters
            ? LocalVariableDeclarationSyntax(
                typeSyntax: Var,
                name: method.MethodImposter.InvokeMethod.ArgumentsVariableName,
                initializer: method.Arguments.Syntax.New(
                    method.Parameters.InputParametersAsArgumentListSyntaxWithoutRef
                )
            )
            : null;

    private static ExpressionStatementSyntax AddToInvocationHistoryCollection(
        in ImposterTargetMethodMetadata method,
        bool threwException
    )
    {
        return IdentifierName(method.InvocationHistory.Collection.AsField.Name)
            .Dot(IdentifierName("Add"))
            .Call(
                Argument(
                    method.InvocationHistory.Syntax.New(
                        ArgumentListSyntax(GetArguments(method, threwException))
                    )
                )
            )
            .ToStatementSyntax();

        static IReadOnlyList<ArgumentSyntax> GetArguments(
            in ImposterTargetMethodMetadata method,
            bool threwException
        )
        {
            List<ArgumentSyntax> arguments = [];

            if (method.Parameters.HasInputParameters)
            {
                arguments.Add(
                    method.MethodImposter.InvokeMethod.ArgumentsVariableName.ToArgument()
                );
            }

            if (method.KeepsResult)
            {
                arguments.Add(
                    threwException
                        ? Argument(DefaultNonNullable)
                        : Argument(
                            method.ReturnType.KeptValue(
                                IdentifierName(
                                    method.MethodImposter.InvokeMethod.ResultVariableName
                                )
                            )
                        )
                );
            }

            arguments.Add(
                threwException
                    ? method.MethodImposter.InvokeMethod.ExceptionVariableName.ToArgument()
                    : Argument(Default)
            );

            return arguments;
        }
    }

    private static StatementSyntax InvokeMatchingInvocationImposterGroup(
        in ImposterTargetMethodMetadata method
    )
    {
        var invokeExpression = IdentifierName(
                method.MethodImposter.InvokeMethod.MatchingInvocationImposterGroupVariableName
            )
            .Dot(IdentifierName(MethodImposterInvokeMethodMetadata.Name))
            .Call(
                InvokeSignatureBuilder.InvocationImposterArguments(
                    method,
                    IdentifierName(method.MethodImposter.InvocationBehaviorFieldName),
                    method.DisplayName.StringLiteral()
                )
            );

        if (method.Model.ReturnType.IsVoid)
        {
            return invokeExpression.ToStatementSyntax();
        }

        return LocalVariableDeclarationSyntax(
            Var,
            method.MethodImposter.InvokeMethod.ResultVariableName,
            invokeExpression
        );
    }

    private static LocalDeclarationStatementSyntax DeclareMatchingInvocationImposterGroupVariable(
        in ImposterTargetMethodMetadata method
    ) =>
        LocalVariableDeclarationSyntax(
            Var,
            method.MethodImposter.InvokeMethod.MatchingInvocationImposterGroupVariableName,
            IdentifierName(method.MethodImposter.FindMatchingInvocationImposterGroupMethod.Name)
                .Call(
                    method.Parameters.HasInputParameters
                        ? Argument(
                                IdentifierName(
                                    method.MethodImposter.InvokeMethod.ArgumentsVariableName
                                )
                            )
                            .ToSingleArgumentList()
                        : ArgumentList()
                )
        );

    private static IfStatementSyntax EnsureMatchingInvocationImposterGroup(
        in ImposterTargetMethodMetadata method
    )
    {
        var matchingIdentifier = IdentifierName(
            method.MethodImposter.InvokeMethod.MatchingInvocationImposterGroupVariableName
        );
        var defaultGroup = method.MethodInvocationImposterGroup.Syntax.Dot(
            IdentifierName(
                method.MethodInvocationImposterGroup.DefaultInvocationImposterGroupField.Name
            )
        );

        return IfStatement(
            matchingIdentifier.IsDefault(),
            Block(
                ThrowIfExplicit(
                    IdentifierName(method.MethodImposter.InvocationBehaviorFieldName),
                    method.DisplayName.StringLiteral()
                ),
                matchingIdentifier.Assign(defaultGroup).ToStatementSyntax()
            )
        );
    }
}
