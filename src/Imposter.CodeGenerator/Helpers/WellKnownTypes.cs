using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Helpers;

internal static class WellKnownTypes
{
    internal static class System
    {
        internal static NameSyntax Namespace = IdentifierName("System");

        internal static TypeSyntax Exception = QualifiedName(
            Namespace,
            IdentifierName("Exception")
        );
    }
}