using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static class TypeSymbolExtensions
{
    internal static bool IsWellKnownType(this ITypeSymbol? symbol, TypeSyntax typeSyntax, params string[] assemblyNames)
    {
        if (symbol is not INamedTypeSymbol named)
        {
            return false;
        }

        var definition = named.IsGenericType ? named.ConstructedFrom : named;
        var assemblyName = definition.ContainingAssembly?.Identity.Name;

        if (assemblyName is null ||
            assemblyNames.Length == 0 ||
            Array.IndexOf(assemblyNames, assemblyName) == -1)
        {
            return false;
        }

        var wellKnownTypeName = typeSyntax.NormalizeWhitespace().ToFullString();
        var candidateName = definition.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);

        return string.Equals(candidateName, wellKnownTypeName, StringComparison.Ordinal);
    }
}
