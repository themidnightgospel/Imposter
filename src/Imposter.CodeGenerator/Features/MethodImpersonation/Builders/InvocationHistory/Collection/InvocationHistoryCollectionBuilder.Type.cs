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
    internal static MemberDeclarationSyntax Build(in ImposterTargetMethodMetadata method)
    {
        var historyType = method.InvocationHistory.NodeSyntax.ToNullableType();

        return new ClassDeclarationBuilder(method.InvocationHistory.Collection.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddMember(
                SingleVariableField(
                    historyType,
                    InvocationHistoryCollectionMetadata.InvocationHistoryCollectionFieldName,
                    TokenList(Token(SyntaxKind.PrivateKeyword))
                )
            )
            .AddMember(
                new ClassDeclarationBuilder(InvocationHistoryTypeMetadata.NodeTypeName)
                    .AddModifier(Token(SyntaxKind.InternalKeyword))
                    .AddModifier(Token(SyntaxKind.AbstractKeyword))
                    .AddMember(
                        SingleVariableField(
                            historyType,
                            InvocationHistoryTypeMetadata.NextFieldName,
                            TokenList(Token(SyntaxKind.InternalKeyword))
                        )
                    )
                    .Build()
            )
            .AddMember(BuildAddMethod(method))
            .AddMember(BuildRecordFailureMethod(method))
            .AddMember(BuildCountMethod(method))
            .AddMember(BuildToStringMethod())
            .Build();
    }

    private static MethodDeclarationSyntax BuildToStringMethod() =>
        new MethodDeclarationBuilder(WellKnownTypes.String, "ToString")
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddModifier(Token(SyntaxKind.OverrideKeyword))
            .WithBody(
                Block(
                    LocalVariableDeclarationSyntax(
                        Var,
                        "descriptions",
                        WellKnownTypes
                            .System.Collections.Generic.List(WellKnownTypes.String.ToNullableType())
                            .New()
                    ),
                    LocalVariableDeclarationSyntax(Var, "invocation", ReadHead()),
                    WhileStatement(
                        IdentifierName("invocation").IsNotNull(),
                        Block(
                            IdentifierName("descriptions")
                                .Dot(IdentifierName("Add"))
                                .Call(
                                    Argument(
                                        IdentifierName("invocation")
                                            .Dot(IdentifierName("ToString"))
                                            .Call()
                                    )
                                )
                                .ToStatementSyntax(),
                            IdentifierName("invocation")
                                .Assign(
                                    IdentifierName("invocation")
                                        .Dot(
                                            IdentifierName(
                                                InvocationHistoryTypeMetadata.NextFieldName
                                            )
                                        )
                                )
                                .ToStatementSyntax()
                        )
                    ),
                    ReturnStatement(JoinWithNewLines(IdentifierName("descriptions")))
                )
            )
            .Build();

    private static InvocationExpressionSyntax ReadHead() =>
        WellKnownTypes
            .System.Threading.Volatile.Dot(IdentifierName("Read"))
            .Call(
                Argument(
                        IdentifierName(
                            InvocationHistoryCollectionMetadata.InvocationHistoryCollectionFieldName
                        )
                    )
                    .WithRefOrOutKeyword(Token(SyntaxKind.RefKeyword))
            );
}
