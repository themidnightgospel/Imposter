using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static class ConcurrentQueueSyntaxHelper
{
    internal static readonly IdentifierNameSyntax Enqueue = IdentifierName("Enqueue");

    internal static readonly IdentifierNameSyntax TryDequeue = IdentifierName("TryDequeue");

    internal static readonly IdentifierNameSyntax TryPeek = IdentifierName("TryPeek");

    internal static readonly IdentifierNameSyntax IsEmpty = IdentifierName("IsEmpty");

    // Enumerating a concurrent queue allocates an enumerator, even an empty one's. A loop that runs on every call to a
    // member, over registrations most setups don't make, runs only when its queue has any.
    internal static IfStatementSyntax UnlessEmpty(CommonForEachStatementSyntax loop) =>
        IfStatement(Not(loop.Expression.Dot(IsEmpty)), Block(loop));
}
