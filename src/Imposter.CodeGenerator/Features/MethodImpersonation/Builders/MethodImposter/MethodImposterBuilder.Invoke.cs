using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Builders.Shared;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationHistory;
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
                    .AddStatements(DeclareCapturedArguments(method))
                    .AddStatement(DeclareMatchingInvocationImposterGroupVariable(method))
                    .AddStatement(EnsureMatchingInvocationImposterGroup(method))
                    .AddStatement(
                        TryStatement(
                            new BlockBuilder()
                                .AddStatement(InvokeMatchingInvocationImposterGroup(method))
                                .AddStatement(RecordInvocation(method))
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
                                    CatchFilterClause(RecordFailure(method)),
                                    Block(ThrowStatement())
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

    private static IEnumerable<StatementSyntax> DeclareCapturedArguments(
        in ImposterTargetMethodMetadata method
    )
    {
        var names = method.InvocationHistory.ArgumentFields;
        return method.Parameters.InputParameterMetadata.Select(p =>
            LocalVariableDeclarationSyntax(
                p.NullableAwareKeptTypeSyntax,
                names[p.Name],
                p.KeptValue
            )
        );
    }

    private static ExpressionStatementSyntax RecordInvocation(
        in ImposterTargetMethodMetadata method
    ) =>
        CallInvocationHistoryCollection(
                method,
                InvocationHistoryCollectionMetadata.AddMethodName,
                NewInvocationHistory(
                    method,
                    method.ReturnType.KeptValue(
                        IdentifierName(method.MethodImposter.InvokeMethod.ResultVariableName)
                    ),
                    Default
                )
            )
            .ToStatementSyntax();

    // The filter records the failed invocation before the stack unwinds.
    private static InvocationExpressionSyntax RecordFailure(
        in ImposterTargetMethodMetadata method
    ) =>
        CallInvocationHistoryCollection(
            method,
            InvocationHistoryCollectionMetadata.RecordFailureMethodName,
            NewInvocationHistory(
                method,
                DefaultNonNullable,
                IdentifierName(method.MethodImposter.InvokeMethod.ExceptionVariableName)
            )
        );

    private static InvocationExpressionSyntax CallInvocationHistoryCollection(
        in ImposterTargetMethodMetadata method,
        string methodName,
        ExpressionSyntax invocationHistory
    ) =>
        IdentifierName(method.InvocationHistory.Collection.AsField.Name)
            .Dot(IdentifierName(methodName))
            .Call(Argument(invocationHistory));

    // The result is left out when the method keeps none.
    private static ObjectCreationExpressionSyntax NewInvocationHistory(
        in ImposterTargetMethodMetadata method,
        ExpressionSyntax result,
        ExpressionSyntax exception
    )
    {
        List<ArgumentSyntax> arguments =
        [
            .. CapturedArgumentsBuilder.CapturedValues(method).Arguments,
        ];

        if (method.KeepsResult)
        {
            arguments.Add(Argument(result));
        }

        arguments.Add(Argument(exception));

        return method.InvocationHistory.Syntax.New(ArgumentListSyntax(arguments));
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
                .Call(CapturedArgumentsBuilder.CapturedValues(method))
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
