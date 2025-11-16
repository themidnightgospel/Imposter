using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static class LinqSyntaxHelper
{
    internal static readonly IdentifierNameSyntax Count = IdentifierName("Count");
}
