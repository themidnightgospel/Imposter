using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationHistory;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.InvocationHistory.Collection;

internal static partial class InvocationHistoryCollectionBuilder
{
    internal static MethodDeclarationSyntax BuildAddMethod(
        in ImposterTargetMethodMetadata method
    ) =>
        new MethodDeclarationBuilder(WellKnownTypes.Void, "Add")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(
                ParameterSyntax(method.InvocationHistory.Interface.Syntax, "invocationHistory")
            )
            .WithBody(
                Block(
                    IdentifierName(
                            InvocationHistoryCollectionMetadata.InvocationHistoryCollectionFieldName
                        )
                        .Dot(ConcurrentStackSyntaxHelper.Push)
                        .Call(Argument(IdentifierName("invocationHistory")))
                        .ToStatementSyntax()
                )
            )
            .Build();

    // Filters run before stack unwinding: the history is visible to finally blocks in user delegates.
    // Always decline to handle the exception, so its original dispatch continues without a rethrow.
    internal static MethodDeclarationSyntax BuildRecordFailureMethod(
        in ImposterTargetMethodMetadata method
    ) =>
        new MethodDeclarationBuilder(
            WellKnownTypes.Bool,
            InvocationHistoryCollectionMetadata.RecordFailureMethodName
        )
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(
                ParameterSyntax(method.InvocationHistory.Interface.Syntax, "invocationHistory")
            )
            .WithBody(
                Block(
                    IdentifierName("Add")
                        .Call(Argument(IdentifierName("invocationHistory")))
                        .ToStatementSyntax(),
                    ReturnStatement(False)
                )
            )
            .Build();
}
