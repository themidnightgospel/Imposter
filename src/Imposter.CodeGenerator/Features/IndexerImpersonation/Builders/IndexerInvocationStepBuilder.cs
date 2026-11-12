using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Builders;

internal static class IndexerInvocationStepBuilder
{
    internal static MethodDeclarationSyntax Build(in ImposterIndexerMetadata indexer, bool getter)
    {
        var names = indexer.Core.CreateParameterNameSet();
        var sequence = IdentifierName(names.Use("sequence"));
        var criteria = IdentifierName(names.Use("criteria"));
        var match = IdentifierName(names.Use("match"));
        var capturedHistory = IdentifierName(names.Use("history"));
        var cursor = IdentifierName(names.Use("cursor"));
        var selected = IdentifierName(names.Use("selected"));
        var entry = IdentifierName(names.Use("entry"));
        var record = getter ? indexer.GetterRecord : indexer.SetterRecord;
        var history = getter
            ? indexer.GetterImplementation.InvocationHistoryField
            : indexer.SetterImplementation.InvocationHistoryField;

        return new MethodDeclarationBuilder(
            WellKnownTypes.Bool,
            InvocationStepNames.TryFindAfterMethodName
        )
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(ParameterSyntax(WellKnownTypes.Long, sequence.Identifier.Text))
            .AddParameter(
                ParameterSyntax(indexer.ArgumentsCriteria.TypeSyntax, criteria.Identifier.Text)
            )
            .AddParameter(
                ParameterSyntax(
                        WellKnownTypes.Imposter.Abstractions.InvocationMatch,
                        match.Identifier.Text
                    )
                    .WithModifiers(TokenList(Token(SyntaxKind.OutKeyword)))
            )
            .WithBody(
                Block(
                    match
                        .Assign(
                            DefaultExpression(WellKnownTypes.Imposter.Abstractions.InvocationMatch)
                        )
                        .ToStatementSyntax(),
                    LocalVariableDeclarationSyntax(
                        Var,
                        capturedHistory.Identifier.Text,
                        IdentifierName(history.Name)
                    ),
                    IfStatement(capturedHistory.IsNull(), ReturnStatement(False)),
                    LocalVariableDeclarationSyntax(
                        Var,
                        cursor.Identifier.Text,
                        capturedHistory.Dot(IdentifierName("GetEnumerator")).Call()
                    ),
                    LocalVariableDeclarationSyntax(
                        record.Type.ToNullableType(),
                        selected.Identifier.Text,
                        Null
                    ),
                    WhileStatement(
                        cursor.Dot(IdentifierName("MoveNext")).Call(),
                        Block(
                            LocalVariableDeclarationSyntax(
                                Var,
                                entry.Identifier.Text,
                                cursor.Dot(IdentifierName("Current"))
                            ),
                            IfStatement(
                                BinaryExpression(
                                    SyntaxKind.LogicalOrExpression,
                                    BinaryExpression(
                                        SyntaxKind.LessThanOrEqualExpression,
                                        entry.Dot(IdentifierName("Sequence")),
                                        sequence
                                    ),
                                    ParenthesizedExpression(
                                        selected
                                            .IsNotNull()
                                            .And(
                                                BinaryExpression(
                                                    SyntaxKind.GreaterThanOrEqualExpression,
                                                    entry.Dot(IdentifierName("Sequence")),
                                                    selected.Dot(IdentifierName("Sequence"))
                                                )
                                            )
                                    )
                                ),
                                ContinueStatement()
                            ),
                            IfStatement(
                                criteria
                                    .Dot(
                                        IdentifierName(
                                            IndexerArgumentsCriteriaMetadata.MatchesMethodName
                                        )
                                    )
                                    .Call(Argument(entry.Dot(IdentifierName("Arguments")))),
                                selected.Assign(entry).ToStatementSyntax()
                            )
                        )
                    ),
                    IfStatement(selected.IsNull(), ReturnStatement(False)),
                    match
                        .Assign(
                            WellKnownTypes
                                .Imposter.Abstractions.InvocationMatch.Dot(
                                    IdentifierName("FromInvocation")
                                )
                                .Call(Argument(selected))
                        )
                        .ToStatementSyntax(),
                    ReturnStatement(True)
                )
            )
            .Build();
    }
}
