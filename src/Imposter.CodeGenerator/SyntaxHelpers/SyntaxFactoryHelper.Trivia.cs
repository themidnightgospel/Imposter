using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    public static SyntaxToken WithLeadingTriviaComment(this in SyntaxToken node, string comment) =>
        node.WithLeadingTrivia(CommentSyntaxTrivias(comment));

    public static TSyntax WithLeadingTriviaComment<TSyntax>(this TSyntax node, string comment)
        where TSyntax : SyntaxNode
    {
        return node.WithLeadingTrivia(CommentSyntaxTrivias(comment));
    }

    private static IEnumerable<SyntaxTrivia> CommentSyntaxTrivias(string comment)
    {
        yield return Comment($"// {comment}");
        yield return CarriageReturnLineFeed;
    }
}
