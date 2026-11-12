using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;
using Imposter.CodeGenerator.Features.Shared.Builders;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.Features.IndexerImpersonation.Builders.IndexerImposterBuilderCommon;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Builders;

internal static class IndexerVerificationBuilder
{
    internal static BlockSyntax BuildBody(
        in ImposterIndexerMetadata indexer,
        bool getter,
        StatementSyntax verification
    )
    {
        var history = getter
            ? indexer.GetterImplementation.InvocationHistoryField
            : indexer.SetterImplementation.InvocationHistoryField;
        var criteria = getter
            ? indexer.GetterImplementation.CriteriaParameterName
            : indexer.SetterImplementation.CriteriaParameterName;
        var enumeratorType = QualifiedName((NameSyntax)history.Type, IdentifierName("Enumerator"));
        var cursor = IdentifierName(indexer.Locals["cursor"]);
        var current = cursor.Dot(IdentifierName("Current"));
        var matches = IdentifierName(criteria)
            .Dot(IdentifierName("Matches"))
            .Call(Argument(current.Dot(IdentifierName("Arguments"))));
        var count = IdentifierName(indexer.Locals["invocationCount"]);
        return Block(
            List<StatementSyntax>([
                LocalVariableDeclarationSyntax(
                    enumeratorType,
                    indexer.Locals["snapshot"],
                    ConditionalExpression(
                        IdentifierName(history.Name).IsNull(),
                        DefaultExpression(enumeratorType),
                        IdentifierName(history.Name).Dot(IdentifierName("GetEnumerator")).Call()
                    )
                ),
                LocalVariableDeclarationSyntax(
                    Var,
                    indexer.Locals["cursor"],
                    IdentifierName(indexer.Locals["snapshot"])
                ),
                LocalVariableDeclarationSyntax(
                    WellKnownTypes.Int,
                    indexer.Locals["invocationCount"],
                    NumericLiteral(0)
                ),
                WhileStatement(
                    cursor.Dot(IdentifierName("MoveNext")).Call(),
                    Block(
                        IfStatement(
                            matches,
                            PostfixUnaryExpression(SyntaxKind.PostIncrementExpression, count)
                                .ToStatementSyntax()
                        )
                    )
                ),
                verification,
                cursor.Assign(IdentifierName(indexer.Locals["snapshot"])).ToStatementSyntax(),
                WhileStatement(
                    cursor.Dot(IdentifierName("MoveNext")).Call(),
                    Block(
                        IfStatement(
                            matches,
                            ParenthesizedExpression(
                                    CastExpression(
                                        WellKnownTypes.Imposter.Abstractions.IVerifiableInvocation,
                                        current
                                    )
                                )
                                .Dot(IdentifierName("MarkVerified"))
                                .Call()
                                .ToStatementSyntax()
                        )
                    )
                ),
            ])
        );
    }

    internal static MemberDeclarationSyntax[] Step(in ImposterIndexerMetadata indexer, bool getter)
    {
        var builder = getter
            ? indexer.GetterImplementation.Builder
            : indexer.SetterImplementation.Builder;
        var imposter = IdentifierName(builder.ImposterFieldName);
        var criteria = IdentifierName(builder.CriteriaFieldName);
        return InvocationStepMembersBuilder.Build(
            imposter.Dot(IdentifierName(InvocationStepNames.OwnerPropertyName)),
            ((getter ? "get " : "set ") + indexer.Core.Name)
                .StringLiteral()
                .Add(ExpectedIndices(indexer, criteria, parentheses: true)),
            indexer.Core.CreateParameterNameSet(),
            (sequence, match) =>
                imposter
                    .Dot(IdentifierName(InvocationStepNames.TryFindAfterMethodName))
                    .Call(
                        ArgumentListSyntax([
                            Argument(sequence),
                            Argument(criteria),
                            Argument(match).WithRefOrOutKeyword(Token(SyntaxKind.OutKeyword)),
                        ])
                    )
        );
    }

    internal static MethodDeclarationSyntax Lenient(in ImposterIndexerMetadata indexer, bool getter)
    {
        var continuation = getter
            ? indexer.GetterBuilderInterface.ContinuationInterfaceTypeSyntax
            : indexer.SetterBuilderInterface.ContinuationInterfaceTypeSyntax;
        var target = getter
            ? IdentifierName(IndexerGetterImposterMetadata.InvocationImposterPropertyName)
                .Dot(IdentifierName("IsLenient"))
            : IdentifierName(indexer.SetterImplementation.Builder.CriteriaFieldName)
                .Dot(IdentifierName(indexer.ArgumentsCriteria.SetterLenientFieldName));
        return new MethodDeclarationBuilder(continuation, VerificationSourceNames.LenientMethodName)
            .WithExplicitInterfaceSpecifier(continuation)
            .WithBody(Block(target.Assign(True).ToStatementSyntax(), ReturnThis))
            .Build();
    }

    internal static MemberDeclarationSyntax[] Source(
        in ImposterIndexerMetadata indexer,
        bool? getter
    )
    {
        var members = new List<MemberDeclarationSyntax>();
        var resultType = WellKnownTypes.System.Collections.Generic.IEnumerable(
            WellKnownTypes.String
        );
        foreach (
            var name in new[]
            {
                VerificationSourceNames.InteractionsMethodName,
                VerificationSourceNames.UncoveredInteractionsMethodName,
            }
        )
            members.Add(
                new MethodDeclarationBuilder(resultType, name)
                    .WithExplicitInterfaceSpecifier(
                        WellKnownTypes.Imposter.Abstractions.IImposterVerificationSource
                    )
                    .WithBody(
                        Block(
                            ReturnStatement(
                                WellKnownTypes
                                    .Imposter.Abstractions.ImposterVerification.Dot(
                                        IdentifierName("Describe")
                                    )
                                    .Call([
                                        Argument(IdentifierName("Snapshot").Call()),
                                        Argument(
                                            name
                                            == VerificationSourceNames.UncoveredInteractionsMethodName
                                                ? True
                                                : False
                                        ),
                                    ])
                            )
                        )
                    )
                    .Build()
            );
        members.Add(
            new MethodDeclarationBuilder(resultType, VerificationSourceNames.UnusedSetupsMethodName)
                .WithExplicitInterfaceSpecifier(
                    WellKnownTypes.Imposter.Abstractions.IImposterVerificationSource
                )
                .WithBody(Block(Unused(indexer, getter)))
                .Build()
        );
        return members.ToArray();
    }

    private static StatementSyntax[] Unused(in ImposterIndexerMetadata indexer, bool? getter)
    {
        switch (getter)
        {
            case null:
                break;
            case true:
                return
                [
                    ForEachStatement(
                        Var,
                        Identifier(indexer.Locals["setup"]),
                        IdentifierName(indexer.GetterImplementation.SetupsField.Name),
                        IfStatement(
                            Not(
                                    IdentifierName(indexer.Locals["setup"])
                                        .Dot(IdentifierName("IsUsed"))
                                )
                                .And(
                                    Not(
                                        IdentifierName(indexer.Locals["setup"])
                                            .Dot(IdentifierName("IsLenient"))
                                    )
                                )
                                .And(
                                    IdentifierName(indexer.Locals["setup"])
                                        .Dot(
                                            IdentifierName(
                                                IndexerGetterImposterMetadata
                                                    .GetterInvocationMetadata
                                                    .OutcomeDescriptionsFieldName
                                            )
                                        )
                                        .IsNotNull()
                                ),
                            YieldStatement(
                                SyntaxKind.YieldReturnStatement,
                                IdentifierName(indexer.Locals["setup"])
                                    .Dot(IdentifierName("ToString"))
                                    .Call()
                            )
                        )
                    ),
                ];
            default:
            {
                var setter = indexer.SetterImplementation;
                var statements = new List<StatementSyntax>
                {
                    UnusedSetter(indexer, setter.CallbacksField, false),
                };
                if (setter.BaseImplementationCriteriaField is { } baseField)
                    statements.Add(UnusedSetter(indexer, baseField, true));
                return statements.ToArray();
            }
        }
        var result = new List<StatementSyntax>();
        foreach (
            var field in new[]
            {
                indexer.Core.HasGetter ? indexer.Builder.GetterImposterField.Name : null,
                indexer.Core.HasSetter ? indexer.Builder.SetterImposterField.Name : null,
            }.OfType<string>()
        )
            result.Add(
                ForEachStatement(
                    Var,
                    Identifier(indexer.Locals["unused"]),
                    ParenthesizedExpression(
                            CastExpression(
                                WellKnownTypes.Imposter.Abstractions.IImposterVerificationSource,
                                IdentifierName(field)
                            )
                        )
                        .Dot(IdentifierName(VerificationSourceNames.UnusedSetupsMethodName))
                        .Call(),
                    YieldStatement(
                        SyntaxKind.YieldReturnStatement,
                        IdentifierName(indexer.Locals["unused"])
                    )
                )
            );
        return result.ToArray();
    }

    private static IfStatementSyntax UnusedSetter(
        in ImposterIndexerMetadata indexer,
        in FieldMetadata field,
        bool isBase
    )
    {
        var setup = IdentifierName(indexer.Locals["setup"]);
        ExpressionSyntax criteria = setup;
        if (!isBase)
            criteria = criteria.Dot(IdentifierName("Criteria"));
        return IfStatement(
            IdentifierName(field.Name).IsNotNull(),
            ForEachStatement(
                Var,
                Identifier(indexer.Locals["setup"]),
                IdentifierName(field.Name),
                IfStatement(
                    Not(criteria.Dot(IdentifierName(indexer.ArgumentsCriteria.SetterUsedFieldName)))
                        .And(
                            Not(
                                criteria.Dot(
                                    IdentifierName(indexer.ArgumentsCriteria.SetterLenientFieldName)
                                )
                            )
                        ),
                    YieldStatement(
                        SyntaxKind.YieldReturnStatement,
                        ("set " + indexer.Core.DisplayName)
                            .StringLiteral()
                            .Add(ExpectedIndices(indexer, criteria))
                            .Add(
                                (
                                    isBase ? " -> UseBaseImplementation" : " -> Callback"
                                ).StringLiteral()
                            )
                    )
                )
            )
        );
    }
}
