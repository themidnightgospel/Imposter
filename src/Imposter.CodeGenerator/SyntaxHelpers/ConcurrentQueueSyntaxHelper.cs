using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

// TODO: Do the same foro ther collections
internal static class ConcurrentQueueSyntaxHelper
{
    internal static readonly IdentifierNameSyntax Enqueue = IdentifierName("Enqueue");
    
    internal static readonly IdentifierNameSyntax TryDequeue = IdentifierName("TryDequeue");
}