using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Builders.Shared;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationHistory;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationSetup;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.MethodImposter;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.Features.Shared.Builders.MissingImposterBuilder;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter;

internal static partial class MethodImposterBuilder
{
    private static MethodDeclarationSyntax InvokeMethod(
        in ImposterTargetMethodMetadata method,
        bool recordHistory = true,
        bool reuseCapturedSpans = false
    ) =>
        new MethodDeclarationBuilder(
            method.NullableAwareReturnTypeSyntax,
            reuseCapturedSpans
                ? method.MethodImposter.InvokeWithCapturedSpansMethodName
                : MethodImposterInvokeMethodMetadata.Name
        )
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .WithParameterList(
                reuseCapturedSpans
                    ? InvokeSignatureBuilder.WithCapturedSpans(method)
                    : InvokeSignatureBuilder.MethodImposterParameters(method)
            )
            .WithBody(
                new BlockBuilder()
                    .AddStatement(
                        recordHistory
                            ? LocalVariableDeclarationSyntax(
                                WellKnownTypes.Long,
                                method.InvocationHistory.SequenceVariableName,
                                WellKnownTypes
                                    .Imposter.Abstractions.InvocationSequence.Dot(
                                        IdentifierName("Next")
                                    )
                                    .Call()
                            )
                            : null
                    )
                    .AddStatements(DeclareCapturedArguments(method, reuseCapturedSpans))
                    .AddStatements(
                        method.Parameters.OutputParameters.Select(parameter =>
                            IdentifierName(SyntaxFactoryHelper.EscapeKeyword(parameter.Name))
                                .Assign(DefaultNonNullable)
                                .ToStatementSyntax()
                        )
                    )
                    .AddStatement(
                        recordHistory
                            ? TryStatement(
                                new BlockBuilder()
                                    .AddStatement(
                                        DeclareMatchingInvocationImposterGroupVariable(method)
                                    )
                                    .AddStatement(EnsureMatchingInvocationImposterGroup(method))
                                    .AddStatement(InvokeMatchingInvocationImposterGroup(method))
                                    .AddStatement(RecordInvocation(method))
                                    .AddStatement(ReturnResultStatement(method))
                                    .Build(),
                                SingletonList(
                                    CatchClause(
                                        CatchDeclaration(
                                            WellKnownTypes.System.Exception,
                                            Identifier(
                                                method
                                                    .MethodImposter
                                                    .InvokeMethod
                                                    .ExceptionVariableName
                                            )
                                        ),
                                        null,
                                        Block(
                                            RecordFailure(method).ToStatementSyntax(),
                                            ThrowStatement()
                                        )
                                    )
                                ),
                                default!
                            )
                            : new BlockBuilder()
                                .AddStatement(
                                    DeclareMatchingInvocationImposterGroupVariable(method)
                                )
                                .AddStatement(EnsureMatchingInvocationImposterGroup(method))
                                .AddStatement(InvokeMatchingInvocationImposterGroup(method))
                                .AddStatement(ReturnResultStatement(method))
                                .Build()
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
        in ImposterTargetMethodMetadata method,
        bool reuseCapturedSpans
    )
    {
        var names = method.InvocationHistory.ArgumentFields;
        return method.Parameters.InputParameterMetadata.Select(p =>
            reuseCapturedSpans && p.IsSpan
                ? (StatementSyntax)IfStatement(
                    BinaryExpression(
                        SyntaxKind.EqualsExpression,
                        IdentifierName(names[p.Name]).Dot(IdentifierName("Length")),
                        IdentifierName(p.Name).Dot(IdentifierName("Length"))
                    ),
                    IdentifierName(p.Name)
                        .Dot(IdentifierName("CopyTo"))
                        .Call(Argument(IdentifierName(names[p.Name])))
                        .ToStatementSyntax(),
                    ElseClause(
                        IdentifierName(names[p.Name]).Assign(p.KeptValue).ToStatementSyntax()
                    )
                )
                : LocalVariableDeclarationSyntax(
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

    // The catch records final out values after user finally blocks have completed.
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
        var arguments = new List<ArgumentSyntax>();
        foreach (var parameter in method.Parameters.MatchedParameterMetadata)
            arguments.Add(
                Argument(
                    parameter.Model.RefKind == Microsoft.CodeAnalysis.RefKind.Out
                        ? parameter.KeptValue
                        : IdentifierName(method.InvocationHistory.ArgumentFields[parameter.Name])
                )
            );

        if (method.KeepsResult)
        {
            arguments.Add(Argument(result));
        }

        arguments.Add(Argument(exception));
        arguments.Add(Argument(IdentifierName(method.InvocationHistory.SequenceVariableName)));

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
            IdentifierName(DefaultInvocationImposterGroupFieldMetadata.Name)
        );

        return IfStatement(
            matchingIdentifier.IsDefault(),
            Block(
                IfStatement(
                    IsExplicit(IdentifierName(method.MethodImposter.InvocationBehaviorFieldName)),
                    ThrowStatement(
                        WellKnownTypes.Imposter.Abstractions.MissingImposterException.New(
                            ArgumentListSyntax([
                                Argument(method.Model.Name.StringLiteral()),
                                Argument(MethodDiagnosticBuilder.Actual(method)),
                                Argument(
                                    method.Model.IsGenericMethod
                                        ? PostfixUnaryExpression(
                                                SyntaxKind.SuppressNullableWarningExpression,
                                                IdentifierName(
                                                        method
                                                            .InvocationHistory
                                                            .Collection
                                                            .AsField
                                                            .Name
                                                    )
                                                    .Dot(
                                                        IdentifierName(
                                                            InvocationHistoryCollectionMetadata.SetupSourceFieldName
                                                        )
                                                    )
                                            )
                                            .Dot(
                                                IdentifierName(
                                                    method.MethodImposter.ConfiguredSetupsMethodName
                                                )
                                            )
                                            .Call()
                                        : IdentifierName(
                                                method.MethodImposter.ConfiguredSetupsMethodName
                                            )
                                            .Call()
                                ),
                                Argument(method.DisplayName.StringLiteral()),
                            ])
                        )
                    )
                ),
                matchingIdentifier.Assign(defaultGroup).ToStatementSyntax()
            )
        );
    }
}
