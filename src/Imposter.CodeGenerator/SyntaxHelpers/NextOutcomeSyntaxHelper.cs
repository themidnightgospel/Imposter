using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static class NextOutcomeSyntaxHelper
{
    /// <summary>
    /// The body of a method that takes the next outcome from a lazily created <c>ReturnValueSequence</c>, which
    /// repeats its last outcome once all are taken, or returns the fallback before any outcome is configured.
    /// </summary>
    internal static BlockSyntax TakeNextOutcomeBody(
        string sequenceFieldName,
        ExpressionSyntax fallback
    )
    {
        var sequence = IdentifierName("returnValues");

        return Block(
            LocalVariableDeclarationSyntax(
                Var,
                sequence.Identifier.Text,
                VolatileSyntaxHelper.VolatileRead(sequenceFieldName)
            ),
            ReturnStatement(
                ConditionalExpression(
                    sequence.IsNotNull(),
                    sequence.Dot(IdentifierName("Next")).Call(Argument(fallback)),
                    fallback
                )
            )
        );
    }
}
