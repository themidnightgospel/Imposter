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
    private static MethodDeclarationSyntax BuildSnapshotMethod(
        in ImposterTargetMethodMetadata method,
        bool filtered
    )
    {
        TypeSyntax recordType = filtered
            ? method.InvocationHistory.Syntax
            : WellKnownTypes.Imposter.Abstractions.IImposterInvocation;
        var names = method.ReservedParameterNames.CreateNameSet();
        var result = IdentifierName(names.Use("result"));
        var entry = IdentifierName(names.Use("entry"));
        var criteria = IdentifierName(names.Use("criteria"));
        var orderBySequence = IdentifierName(names.Use("orderBySequence"));
        var builder = new MethodDeclarationBuilder(
            WellKnownTypes.System.Collections.Generic.IReadOnlyList(recordType),
            filtered ? "MatchingInvocations" : "Snapshot"
        ).AddModifier(Token(SyntaxKind.InternalKeyword));
        if (filtered)
        {
            builder
                .WithTypeParameters(method.GenericTypeParameterListSyntax)
                .AddConstraintClauses(method.GenericTypeConstraintClauses);
            if (method.Parameters.HasInputParameters)
                builder.AddParameter(
                    ParameterSyntax(method.ArgumentsCriteria.Syntax, criteria.Identifier.Text)
                );
        }
        if (!filtered)
            builder.AddParameter(
                ParameterSyntax(WellKnownTypes.Bool, orderBySequence.Identifier.Text)
                    .WithDefault(EqualsValueClause(True))
            );
        StatementSyntax add = result
            .Dot(IdentifierName("Add"))
            .Call(Argument(CastExpression(recordType, entry)))
            .ToStatementSyntax();
        if (filtered)
        {
            var matches = ParenthesizedExpression(
                    CastExpression(method.InvocationHistory.Interface.Syntax, entry)
                )
                .Dot(method.WithGenericArguments(InvocationHistoryMatchesMethodMetadata.Name))
                .Call(
                    method.Parameters.HasInputParameters
                        ? ArgumentListSyntax([Argument(criteria)])
                        : EmptyArgumentListSyntax
                );
            add = IfStatement(matches, add);
        }
        return builder
            .WithBody(
                new BlockBuilder()
                    .AddStatement(
                        LocalVariableDeclarationSyntax(
                            Var,
                            result.Identifier.Text,
                            WellKnownTypes.System.Collections.Generic.List(recordType).New()
                        )
                    )
                    .AddStatements(ForEachEntry(entry.Identifier.Text, add))
                    .AddStatement(
                        ReturnStatement(
                            WellKnownTypes
                                .Imposter.Abstractions.InvocationSnapshots.Dot(
                                    IdentifierName("Order")
                                )
                                .Call([
                                    Argument(result),
                                    Argument(filtered ? True : orderBySequence),
                                ])
                        )
                    )
                    .Build()
            )
            .Build();
    }
}
