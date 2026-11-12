using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationHistory;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.InvocationHistory.Collection;

internal static partial class InvocationHistoryCollectionBuilder
{
    private static MethodDeclarationSyntax BuildTryFindAfterMethod(
        in ImposterTargetMethodMetadata method
    )
    {
        var names = method.ReservedParameterNames.CreateNameSet();
        var sequence = IdentifierName(names.Use("sequence"));
        var criteria = IdentifierName(names.Use("criteria"));
        var match = IdentifierName(names.Use("match"));
        var entry = IdentifierName(names.Use("entry"));
        var found = IdentifierName(names.Use("found"));
        var matchType = WellKnownTypes.Imposter.Abstractions.InvocationMatch;
        var builder = new MethodDeclarationBuilder(
            WellKnownTypes.Bool,
            InvocationStepNames.TryFindAfterMethodName
        )
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .WithTypeParameters(method.GenericTypeParameterListSyntax)
            .AddConstraintClauses(method.GenericTypeConstraintClauses)
            .AddParameter(ParameterSyntax(WellKnownTypes.Long, sequence.Identifier.Text));
        if (method.Parameters.HasInputParameters)
            builder.AddParameter(
                ParameterSyntax(method.ArgumentsCriteria.Syntax, criteria.Identifier.Text)
            );
        builder.AddParameter(
            ParameterSyntax(matchType, match.Identifier.Text)
                .WithModifiers(TokenList(Token(SyntaxKind.OutKeyword)))
        );
        var matches = ParenthesizedExpression(
                CastExpression(method.InvocationHistory.Interface.Syntax, entry)
            )
            .Dot(method.WithGenericArguments(InvocationHistoryMatchesMethodMetadata.Name))
            .Call(
                method.Parameters.HasInputParameters
                    ? ArgumentListSyntax([Argument(criteria)])
                    : EmptyArgumentListSyntax
            );
        var entrySequence = entry.Dot(
            IdentifierName(InvocationHistoryTypeMetadata.SequencePropertyName)
        );
        var eligible = BinaryExpression(SyntaxKind.GreaterThanExpression, entrySequence, sequence)
            .And(
                ParenthesizedExpression(
                    BinaryExpression(
                        SyntaxKind.LogicalOrExpression,
                        Not(found),
                        BinaryExpression(
                            SyntaxKind.LessThanExpression,
                            entrySequence,
                            match.Dot(
                                IdentifierName(InvocationHistoryTypeMetadata.SequencePropertyName)
                            )
                        )
                    )
                )
            )
            .And(matches);
        return builder
            .WithBody(
                new BlockBuilder()
                    .AddStatement(match.Assign(DefaultExpression(matchType)).ToStatementSyntax())
                    .AddStatement(LocalVariableDeclarationSyntax(Var, found.Identifier.Text, False))
                    .AddStatements(
                        ForEachEntry(
                            entry.Identifier.Text,
                            IfStatement(
                                eligible,
                                Block(
                                    match
                                        .Assign(
                                            matchType
                                                .Dot(IdentifierName("FromInvocation"))
                                                .Call(Argument(entry))
                                        )
                                        .ToStatementSyntax(),
                                    found.Assign(True).ToStatementSyntax()
                                )
                            )
                        )
                    )
                    .AddStatement(ReturnStatement(found))
                    .Build()
            )
            .Build();
    }
}
