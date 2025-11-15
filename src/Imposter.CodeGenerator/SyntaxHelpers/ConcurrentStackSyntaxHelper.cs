using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static class ConcurrentStackSyntaxHelper
{
    internal static readonly IdentifierNameSyntax TryPeek = IdentifierName("TryPeek");
}
