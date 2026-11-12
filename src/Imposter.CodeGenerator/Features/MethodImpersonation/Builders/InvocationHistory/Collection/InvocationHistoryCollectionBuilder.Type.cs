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
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddMember(
                SingleVariableField(
                    historyType,
                    InvocationHistoryCollectionMetadata.HeadFieldName,
                    SyntaxKind.PrivateKeyword
                )
            )
            .AddMember(
                new ClassDeclarationBuilder(InvocationHistoryCollectionMetadata.NodeTypeName)
                    .AddModifier(Token(SyntaxKind.PublicKeyword))
                    .AddModifier(Token(SyntaxKind.AbstractKeyword))
                    .AddBaseType(
                        SimpleBaseType(WellKnownTypes.Imposter.Abstractions.IVerifiableInvocation)
                    )
                    .AddMember(
                        new PropertyDeclarationBuilder(WellKnownTypes.String, "Member")
                            .AddModifier(Token(SyntaxKind.PublicKeyword))
                            .AddModifier(Token(SyntaxKind.AbstractKeyword))
                            .WithGetter()
                            .Build()
                    )
                    .AddMember(
                        new PropertyDeclarationBuilder(WellKnownTypes.Long, "Sequence")
                            .AddModifier(Token(SyntaxKind.PublicKeyword))
                            .AddModifier(Token(SyntaxKind.AbstractKeyword))
                            .WithGetter()
                            .Build()
                    )
                    .AddMember(
                        new PropertyDeclarationBuilder(
                            WellKnownTypes.System.Exception.ToNullableType(),
                            "Exception"
                        )
                            .AddModifier(Token(SyntaxKind.PublicKeyword))
                            .AddModifier(Token(SyntaxKind.AbstractKeyword))
                            .WithGetter()
                            .Build()
                    )
                    .AddMember(
                        new PropertyDeclarationBuilder(WellKnownTypes.Bool, "IsVerified")
                            .AddModifier(Token(SyntaxKind.InternalKeyword))
                            .AddModifier(Token(SyntaxKind.AbstractKeyword))
                            .WithGetter()
                            .Build()
                    )
                    .AddMember(
                        new MethodDeclarationBuilder(WellKnownTypes.Void, "MarkVerified")
                            .AddModifier(Token(SyntaxKind.InternalKeyword))
                            .AddModifier(Token(SyntaxKind.AbstractKeyword))
                            .WithSemicolon()
                            .Build()
                    )
                    .AddMember(
                        new PropertyDeclarationBuilder(WellKnownTypes.Bool, "IsVerified")
                            .WithExplicitInterfaceSpecifier(
                                ExplicitInterfaceSpecifier(
                                    WellKnownTypes.Imposter.Abstractions.IVerifiableInvocation
                                )
                            )
                            .WithGetterBody(Block(ReturnStatement(IdentifierName("IsVerified"))))
                            .Build()
                    )
                    .AddMember(
                        new MethodDeclarationBuilder(WellKnownTypes.Void, "MarkVerified")
                            .WithExplicitInterfaceSpecifier(
                                WellKnownTypes.Imposter.Abstractions.IVerifiableInvocation
                            )
                            .WithBody(
                                Block(IdentifierName("MarkVerified").Call().ToStatementSyntax())
                            )
                            .Build()
                    )
                    .AddMember(
                        SingleVariableField(
                            historyType,
                            InvocationHistoryCollectionMetadata.NextFieldName,
                            SyntaxKind.InternalKeyword
                        )
                    )
                    .Build()
            )
            .AddMember(BuildAddMethod(method))
            .AddMember(BuildRecordFailureMethod(method))
            .AddMember(BuildCountMethod(method))
            .AddMember(BuildVerifyMethod(method))
            .AddMember(BuildSnapshotMethod(method, false))
            .AddMember(BuildSnapshotMethod(method, true))
            .AddMember(BuildClearMethod())
            .AddMember(BuildToStringMethod(method))
            .Build();
    }

    private static MethodDeclarationSyntax BuildToStringMethod(
        in ImposterTargetMethodMetadata method
    ) =>
        new MethodDeclarationBuilder(WellKnownTypes.String, "ToString")
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddModifier(Token(SyntaxKind.OverrideKeyword))
            .WithBody(
                new BlockBuilder()
                    .AddStatement(
                        LocalVariableDeclarationSyntax(
                            Var,
                            method.Locals["descriptions"],
                            WellKnownTypes
                                .System.Collections.Generic.List(
                                    WellKnownTypes.String.ToNullableType()
                                )
                                .New()
                        )
                    )
                    .AddStatements(
                        ForEachEntry(
                            method.Locals["invocation"],
                            IdentifierName(method.Locals["descriptions"])
                                .Dot(IdentifierName("Add"))
                                .Call(
                                    Argument(
                                        IdentifierName(method.Locals["invocation"])
                                            .Dot(IdentifierName("ToString"))
                                            .Call()
                                    )
                                )
                                .ToStatementSyntax()
                        )
                    )
                    .AddStatement(
                        ReturnStatement(
                            JoinWithNewLines(IdentifierName(method.Locals["descriptions"]))
                        )
                    )
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
