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
    // Only this writer owns the entry until CompareExchange publishes it. Retry by replacing its Next link,
    // without allocating another node. Published entries and links never change, so readers keep a stable snapshot.
    internal static MethodDeclarationSyntax BuildAddMethod(in ImposterTargetMethodMetadata method)
    {
        var head = IdentifierName(method.Locals["head"]);
        var entry = IdentifierName(method.Locals["invocationHistory"]);
        var field = IdentifierName(InvocationHistoryCollectionMetadata.HeadFieldName);
        return new MethodDeclarationBuilder(
            WellKnownTypes.Void,
            InvocationHistoryCollectionMetadata.AddMethodName
        )
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(
                ParameterSyntax(
                    method.InvocationHistory.Collection.NodeSyntax,
                    method.Locals["invocationHistory"]
                )
            )
            .WithBody(
                Block(
                    LocalVariableDeclarationSyntax(
                        method.InvocationHistory.Collection.NodeSyntax.ToNullableType(),
                        method.Locals["head"],
                        null
                    ),
                    DoStatement(
                        Block(
                            head.Assign(VolatileSyntaxHelper.VolatileRead(field))
                                .ToStatementSyntax(),
                            entry
                                .Dot(
                                    IdentifierName(
                                        InvocationHistoryCollectionMetadata.NextFieldName
                                    )
                                )
                                .Assign(head)
                                .ToStatementSyntax()
                        ),
                        BinaryExpression(
                            SyntaxKind.NotEqualsExpression,
                            WellKnownTypes
                                .System.Threading.Interlocked.Dot(IdentifierName("CompareExchange"))
                                .Call([RefArgument(field), Argument(entry), Argument(head)]),
                            head
                        )
                    )
                )
            )
            .Build();
    }

    // The caller records the failure after user finally blocks have completed and then rethrows.
    internal static MethodDeclarationSyntax BuildRecordFailureMethod(
        in ImposterTargetMethodMetadata method
    ) =>
        new MethodDeclarationBuilder(
            WellKnownTypes.Bool,
            InvocationHistoryCollectionMetadata.RecordFailureMethodName
        )
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(
                ParameterSyntax(
                    method.InvocationHistory.Collection.NodeSyntax,
                    method.Locals["invocationHistory"]
                )
            )
            .WithBody(
                Block(
                    IdentifierName(InvocationHistoryCollectionMetadata.AddMethodName)
                        .Call(Argument(IdentifierName(method.Locals["invocationHistory"])))
                        .ToStatementSyntax(),
                    ReturnStatement(False)
                )
            )
            .Build();
}
