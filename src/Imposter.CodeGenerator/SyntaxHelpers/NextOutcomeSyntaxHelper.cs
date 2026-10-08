using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static class NextOutcomeSyntaxHelper
{
    /// <summary>
    /// The body of a method that takes the next outcome from a queue of configured outcomes, and returns the last
    /// published outcome once the queue is empty. An outcome is published before it leaves the queue, and outcomes
    /// leave it one at a time under a lock on the queue. So a concurrent caller that finds the queue empty always
    /// sees the newest outcome, never a missing or older one.
    /// </summary>
    internal static BlockSyntax TakeNextOutcomeBody(
        IdentifierNameSyntax queue,
        IdentifierNameSyntax lastOutcome,
        IdentifierNameSyntax outcome,
        ExpressionSyntax? publishCondition = null
    )
    {
        StatementSyntax publish = lastOutcome.Assign(outcome).ToStatementSyntax();
        if (publishCondition is not null)
        {
            publish = IfStatement(publishCondition, Block(publish));
        }

        return Block(
            IfStatement(
                queue.Dot(ConcurrentQueueSyntaxHelper.IsEmpty),
                Block(ReturnStatement(lastOutcome))
            ),
            LockStatement(
                queue,
                Block(
                    IfStatement(
                        Not(
                            queue
                                .Dot(ConcurrentQueueSyntaxHelper.TryPeek)
                                .Call(OutVarArgument(outcome.Identifier.Text))
                        ),
                        Block(ReturnStatement(lastOutcome))
                    ),
                    publish,
                    queue
                        .Dot(ConcurrentQueueSyntaxHelper.TryDequeue)
                        .Call(OutDiscardArgument())
                        .ToStatementSyntax(),
                    ReturnStatement(outcome)
                )
            )
        );
    }
}
