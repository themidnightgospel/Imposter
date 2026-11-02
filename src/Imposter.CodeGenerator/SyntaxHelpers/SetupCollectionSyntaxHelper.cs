using Imposter.Abstractions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static class SetupCollectionSyntaxHelper
{
    internal static readonly IdentifierNameSyntax Push = IdentifierName(
        nameof(SetupCollection<object>.Push)
    );

    internal static readonly IdentifierNameSyntax TryPeek = IdentifierName(
        nameof(SetupCollection<object>.TryPeek)
    );
}
