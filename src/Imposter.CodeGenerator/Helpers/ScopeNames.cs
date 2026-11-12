using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Models;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Imposter.CodeGenerator.Helpers;

internal static class ScopeNames
{
    internal static IEnumerable<string> In(TypeModel containingType) =>
        SyntaxFactoryHelper
            .TypeSyntax(containingType)
            .DescendantTokens()
            .Where(token => token.IsKind(SyntaxKind.IdentifierToken))
            .Select(token => token.ValueText);
}
