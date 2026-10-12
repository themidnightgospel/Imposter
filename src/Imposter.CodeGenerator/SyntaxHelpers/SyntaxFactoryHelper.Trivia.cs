using Microsoft.CodeAnalysis;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    public static SyntaxToken WithLeadingTriviaComment(this in SyntaxToken node, string comment) =>
        node.WithLeadingTrivia(CommentSyntaxTrivia(comment));

    public static TSyntax WithLeadingTriviaComment<TSyntax>(this TSyntax node, string comment)
        where TSyntax : SyntaxNode
    {
        return node.WithLeadingTrivia(CommentSyntaxTrivia(comment));
    }

    private static SyntaxTrivia CommentSyntaxTrivia(string comment) => Comment($"// {comment}");
}
