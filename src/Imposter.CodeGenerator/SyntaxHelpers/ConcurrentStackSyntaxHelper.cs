using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

// TODO: Do the same foro ther collections
internal static class ConcurrentStackSyntaxHelper
{
    internal static readonly IdentifierNameSyntax TryPeek = IdentifierName("TryPeek");
}