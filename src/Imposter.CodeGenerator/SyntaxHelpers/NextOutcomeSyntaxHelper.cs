using Imposter.CodeGenerator.SyntaxHelpers.Builders;
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
    /// sees the newest outcome, never a missing or older one. If readiness is supplied, an unfinished head stays
    /// queued and calls use the previous outcome until it is ready to be consumed.
    /// </summary>
    internal static BlockSyntax TakeNextOutcomeBody(
        IdentifierNameSyntax queue,
        IdentifierNameSyntax lastOutcome,
        IdentifierNameSyntax outcome,
        ExpressionSyntax? readyCondition = null
    )
    {
        return Block(
            IfStatement(
                queue.Dot(ConcurrentQueueSyntaxHelper.IsEmpty),
                Block(ReturnStatement(lastOutcome))
            ),
            LockStatement(
                queue,
                new BlockBuilder()
                    .AddStatement(
                        IfStatement(
                            Not(
                                queue
                                    .Dot(ConcurrentQueueSyntaxHelper.TryPeek)
                                    .Call(OutVarArgument(outcome.Identifier.Text))
                            ),
                            Block(ReturnStatement(lastOutcome))
                        )
                    )
                    .AddStatement(
                        readyCondition is null
                            ? null
                            : IfStatement(Not(readyCondition), Block(ReturnStatement(lastOutcome)))
                    )
                    .AddStatement(lastOutcome.Assign(outcome).ToStatementSyntax())
                    .AddStatement(
                        queue
                            .Dot(ConcurrentQueueSyntaxHelper.TryDequeue)
                            .Call(OutDiscardArgument())
                            .ToStatementSyntax()
                    )
                    .AddStatement(ReturnStatement(outcome))
                    .Build()
            )
        );
    }
}
