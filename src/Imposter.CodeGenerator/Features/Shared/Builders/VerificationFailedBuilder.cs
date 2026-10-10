using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.Shared.Builders;

// The VerificationFailedException a verification throws when the number of matching invocations isn't the expected
// count.
internal static class VerificationFailedBuilder
{
    // Each entry of History, in the loop variable Entry, is listed in the exception's message as Description.
    internal readonly record struct PerformedInvocations(
        ExpressionSyntax History,
        IdentifierNameSyntax Entry,
        ExpressionSyntax Description
    );

    internal static IfStatementSyntax ThrowIfCountDoesNotMatch(
        ExpressionSyntax count,
        ExpressionSyntax invocationCount,
        PerformedInvocations performedInvocations
    )
    {
        var performedInvocationsList = IdentifierName("performedInvocations");
        var stringListType = WellKnownTypes.System.Collections.Generic.List(WellKnownTypes.String);

        return IfStatement(
            CountDoesNotMatch(count, invocationCount),
            Block(
                LocalVariableDeclarationSyntax(
                    Var,
                    performedInvocationsList.Identifier.Text,
                    stringListType.New()
                ),
                ForEachStatement(
                    Var,
                    performedInvocations.Entry.Identifier,
                    performedInvocations.History,
                    Block(
                        performedInvocationsList
                            .Dot(IdentifierName("Add"))
                            .Call(Argument(performedInvocations.Description))
                            .ToStatementSyntax()
                    )
                ),
                ThrowVerificationFailed(
                    count,
                    invocationCount,
                    JoinWithNewLines(performedInvocationsList)
                )
            )
        );
    }

    internal static PrefixUnaryExpressionSyntax CountDoesNotMatch(
        ExpressionSyntax count,
        ExpressionSyntax invocationCount
    ) => Not(count.Dot(IdentifierName("Matches")).Call(Argument(invocationCount)));

    internal static ThrowStatementSyntax ThrowVerificationFailed(
        ExpressionSyntax count,
        ExpressionSyntax invocationCount
    ) => ThrowVerificationFailed(ArgumentListSyntax([Argument(count), Argument(invocationCount)]));

    internal static ThrowStatementSyntax ThrowVerificationFailed(
        ExpressionSyntax count,
        ExpressionSyntax invocationCount,
        ExpressionSyntax performedInvocations
    ) =>
        ThrowVerificationFailed(
            ArgumentListSyntax([
                Argument(count),
                Argument(invocationCount),
                Argument(performedInvocations),
            ])
        );

    private static ThrowStatementSyntax ThrowVerificationFailed(ArgumentListSyntax arguments) =>
        ThrowStatement(
            WellKnownTypes.Imposter.Abstractions.VerificationFailedException.New(arguments)
        );
}
