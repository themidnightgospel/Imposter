using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Builders;

internal static class IndexerHistoryBuilder
{
    internal static BlockSyntax BuildBody(
        in ImposterIndexerMetadata indexer,
        bool getter,
        BlockSyntax invocation
    )
    {
        var record = getter ? indexer.GetterRecord : indexer.SetterRecord;
        var history = getter
            ? indexer.GetterImplementation.InvocationHistoryField
            : indexer.SetterImplementation.InvocationHistoryField;
        var arguments = IdentifierName(
            getter
                ? indexer.GetterImplementation.ArgumentsVariableName
                : indexer.SetterImplementation.ArgumentsVariableName
        );
        var sequence = IdentifierName(record.SequenceLocal);
        var failure = IdentifierName(record.ExceptionLocal);
        var result = IdentifierName(record.ResultLocal);
        var statements = new List<StatementSyntax>
        {
            LocalVariableDeclarationSyntax(
                WellKnownTypes.Long,
                record.SequenceLocal,
                WellKnownTypes
                    .Imposter.Abstractions.InvocationSequence.Dot(IdentifierName("Next"))
                    .Call()
            ),
            LocalVariableDeclarationSyntax(
                WellKnownTypes.System.Exception.ToNullableType(),
                record.ExceptionLocal,
                Null
            ),
        };
        if (getter && !indexer.Core.IsValuePassedThrough)
            statements.Add(
                LocalVariableDeclarationSyntax(
                    indexer.Core.NullableAwareValueTypeSyntax,
                    record.ResultLocal,
                    DefaultNonNullable
                )
            );
        if (!getter && indexer.Core.HasSpanValue)
        {
            var value = IdentifierName(indexer.SetterImplementation.ValueParameterName);
            statements.Add(
                LocalVariableDeclarationSyntax(
                    indexer.Core.NullableAwareKeptTypeSyntax,
                    record.ResultLocal,
                    CopyArray(value)
                )
            );
        }
        statements.Add(
            IndexerImposterBuilderCommon.CreateArgumentsDeclaration(
                indexer,
                arguments.Identifier.Text
            )
        );
        ExpressionSyntax capturedArguments = arguments;
        if (indexer.Core.MatchedParameters.Any(parameter => parameter.Model.Span is not null))
        {
            capturedArguments = IdentifierName(record.ArgumentsLocal);
            statements.Add(
                LocalVariableDeclarationSyntax(
                    indexer.Arguments.TypeSyntax,
                    record.ArgumentsLocal,
                    indexer.Arguments.TypeSyntax.New(
                        ArgumentListSyntax(
                            indexer.Core.MatchedParameters.Select(parameter =>
                            {
                                ExpressionSyntax value = arguments.Dot(
                                    IdentifierName(parameter.FieldName)
                                );
                                return parameter.Model.Span is null
                                    ? parameter.ForwardingArgument(value)
                                    : Argument(CopyArray(value));
                            })
                        )
                    )
                )
            );
        }
        var recordArguments = new List<ArgumentSyntax> { Argument(capturedArguments) };
        if (!indexer.Core.IsValuePassedThrough)
        {
            ExpressionSyntax value =
                getter || indexer.Core.HasSpanValue
                    ? result
                    : IdentifierName(indexer.SetterImplementation.ValueParameterName);
            if (getter && indexer.Core.HasSpanValue)
                value = CopyArray(value);
            else if (!indexer.Core.HasSpanValue)
                value = indexer.Core.KeptValue(value);
            recordArguments.Add(Argument(value));
        }
        recordArguments.Add(Argument(sequence));
        recordArguments.Add(Argument(failure));
        var caught = IdentifierName(
            indexer.Core.CreateParameterNameSet().Use("caughtInvocationException")
        );
        var finallyBody = new BlockBuilder();
        finallyBody.AddStatement(
            LazyCollectionSyntaxHelper
                .EnsureInitialized(history.Name)
                .Dot(IdentifierName("Push"))
                .Call(Argument(record.Type.New(ArgumentListSyntax(recordArguments))))
                .ToStatementSyntax()
        );
        statements.Add(
            TryStatement(
                invocation,
                SingletonList(
                    CatchClause(
                        CatchDeclaration(WellKnownTypes.System.Exception, caught.Identifier),
                        null,
                        Block(failure.Assign(caught).ToStatementSyntax(), ThrowStatement())
                    )
                ),
                FinallyClause(finallyBody.Build())
            )
        );
        return Block(statements);
    }

    private static InvocationExpressionSyntax CopyArray(ExpressionSyntax value) =>
        WellKnownTypes
            .Imposter.Abstractions.InvocationSnapshots.Dot(IdentifierName("CopySpan"))
            .Call(Argument(value));

    internal static MethodDeclarationSyntax Snapshot(
        in ImposterIndexerMetadata indexer,
        bool getter
    )
    {
        var orderBySequence = indexer.Core.CreateParameterNameSet().Use("orderBySequence");
        var history = getter
            ? indexer.GetterImplementation.InvocationHistoryField
            : indexer.SetterImplementation.InvocationHistoryField;
        return new MethodDeclarationBuilder(
            WellKnownTypes.System.Collections.Generic.IReadOnlyList(
                WellKnownTypes.Imposter.Abstractions.IImposterInvocation
            ),
            "Snapshot"
        )
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(
                ParameterSyntax(WellKnownTypes.Bool, orderBySequence)
                    .WithDefault(EqualsValueClause(True))
            )
            .WithBody(
                Block(
                    ReturnStatement(
                        WellKnownTypes
                            .Imposter.Abstractions.InvocationSnapshots.Dot(IdentifierName("Copy"))
                            .Call([
                                Argument(IdentifierName(history.Name)),
                                Argument(IdentifierName(orderBySequence)),
                            ])
                    )
                )
            )
            .Build();
    }

    internal static MethodDeclarationSyntax Matching(
        in ImposterIndexerMetadata indexer,
        bool getter
    )
    {
        var record = getter ? indexer.GetterRecord : indexer.SetterRecord;
        var history = IdentifierName(
            (
                getter
                    ? indexer.GetterImplementation.InvocationHistoryField
                    : indexer.SetterImplementation.InvocationHistoryField
            ).Name
        );
        var result = IdentifierName(indexer.Locals["matches"]);
        var entry = IdentifierName(indexer.Locals["entry"]);
        return new MethodDeclarationBuilder(
            WellKnownTypes.System.Collections.Generic.IReadOnlyList(record.Type),
            "MatchingInvocations"
        )
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(
                ParameterSyntax(indexer.ArgumentsCriteria.TypeSyntax, indexer.Locals["criteria"])
            )
            .WithBody(
                Block(
                    LocalVariableDeclarationSyntax(
                        Var,
                        indexer.Locals["matches"],
                        WellKnownTypes.System.Collections.Generic.List(record.Type).New()
                    ),
                    IfStatement(
                        history.IsNotNull(),
                        ForEachStatement(
                            Var,
                            entry.Identifier,
                            history,
                            IfStatement(
                                IdentifierName(indexer.Locals["criteria"])
                                    .Dot(IdentifierName("Matches"))
                                    .Call(Argument(entry.Dot(IdentifierName("Arguments")))),
                                result
                                    .Dot(IdentifierName("Add"))
                                    .Call(Argument(entry))
                                    .ToStatementSyntax()
                            )
                        )
                    ),
                    ReturnStatement(
                        WellKnownTypes
                            .Imposter.Abstractions.InvocationSnapshots.Dot(IdentifierName("Order"))
                            .Call(Argument(result))
                    )
                )
            )
            .Build();
    }

    internal static PropertyDeclarationSyntax Invocations(
        in ImposterIndexerMetadata indexer,
        bool getter,
        bool implementation
    )
    {
        var record = getter ? indexer.GetterRecord : indexer.SetterRecord;
        var builder = new PropertyDeclarationBuilder(
            WellKnownTypes.System.Collections.Generic.IReadOnlyList(record.Type),
            "Invocations"
        );
        if (!implementation)
            return builder.WithGetter().Build();
        var accessor = getter
            ? indexer.GetterImplementation.Builder
            : indexer.SetterImplementation.Builder;
        var matching = IdentifierName(accessor.ImposterFieldName)
            .Dot(IdentifierName("MatchingInvocations"))
            .Call(Argument(IdentifierName(accessor.CriteriaFieldName)));
        return builder
            .WithExplicitInterfaceSpecifier(
                ExplicitInterfaceSpecifier(
                    getter
                        ? indexer.GetterBuilderInterface.VerificationInterfaceTypeSyntax
                        : indexer.SetterBuilderInterface.VerificationInterfaceTypeSyntax
                )
            )
            .WithGetterBody(Block(ReturnStatement(matching)))
            .Build();
    }

    internal static ReturnStatementSyntax Return(
        in ImposterIndexerMetadata indexer,
        ExpressionSyntax value
    ) =>
        ReturnStatement(
            indexer.Core.IsValuePassedThrough
                ? value
                : IdentifierName(indexer.GetterRecord.ResultLocal).Assign(value)
        );
}
