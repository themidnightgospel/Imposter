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
        var historyType = method.InvocationHistory.Collection.NodeSyntax.ToNullableType();

        return new ClassDeclarationBuilder(method.InvocationHistory.Collection.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddMember(
                SingleVariableField(
                    historyType,
                    InvocationHistoryCollectionMetadata.HeadFieldName,
                    TokenList(Token(SyntaxKind.PrivateKeyword))
                )
            )
            .AddMember(
                new ClassDeclarationBuilder(InvocationHistoryCollectionMetadata.NodeTypeName)
                    .AddModifier(Token(SyntaxKind.InternalKeyword))
                    .AddModifier(Token(SyntaxKind.AbstractKeyword))
                    .AddMember(
                        SingleVariableField(
                            historyType,
                            InvocationHistoryCollectionMetadata.NextFieldName,
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
                new BlockBuilder()
                    .AddStatement(
                        LocalVariableDeclarationSyntax(
                            Var,
                            "descriptions",
                            WellKnownTypes
                                .System.Collections.Generic.List(
                                    WellKnownTypes.String.ToNullableType()
                                )
                                .New()
                        )
                    )
                    .AddStatements(
                        ForEachEntry(
                            "invocation",
                            IdentifierName("descriptions")
                                .Dot(IdentifierName("Add"))
                                .Call(
                                    Argument(
                                        IdentifierName("invocation")
                                            .Dot(IdentifierName("ToString"))
                                            .Call()
                                    )
                                )
                                .ToStatementSyntax()
                        )
                    )
                    .AddStatement(ReturnStatement(JoinWithNewLines(IdentifierName("descriptions"))))
                    .Build()
            )
            .Build();

    // Declares the entry variable and runs the body for each entry, newest first.
    private static StatementSyntax[] ForEachEntry(string entryName, StatementSyntax body)
    {
        var entry = IdentifierName(entryName);

        return
        [
            LocalVariableDeclarationSyntax(
                Var,
                entryName,
                VolatileSyntaxHelper.VolatileRead(InvocationHistoryCollectionMetadata.HeadFieldName)
            ),
            WhileStatement(
                entry.IsNotNull(),
                Block(
                    body,
                    entry
                        .Assign(
                            entry.Dot(
                                IdentifierName(InvocationHistoryCollectionMetadata.NextFieldName)
                            )
                        )
                        .ToStatementSyntax()
                )
            ),
        ];
    }
}
