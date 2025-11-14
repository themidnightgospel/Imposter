using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

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
        var candidateName = definition.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

        return string.Equals(candidateName, wellKnownTypeName, StringComparison.Ordinal);
    }
    
    internal static bool IsTaskLike(this ITypeSymbol? symbol)
    {
        if (symbol is not INamedTypeSymbol named)
        {
            return false;
        }

        var definition = named.IsGenericType ? named.ConstructedFrom : named;

        if (definition.IsWellKnownType(WellKnownTypes.System.Threading.Tasks.Task, WellKnownAssemblyNames.SystemAssemblies) ||
            definition.IsWellKnownType(WellKnownTypes.System.Threading.Tasks.ValueTask, WellKnownAssemblyNames.SystemAssemblies))
        {
            return true;
        }

        var genericParameter = IdentifierName("TResult");
        var genericTask = WellKnownTypes.System.Threading.Tasks.TaskOfT(genericParameter);
        var genericValueTask = WellKnownTypes.System.Threading.Tasks.ValueTaskOfT(genericParameter);

        return definition.IsWellKnownType(genericTask, WellKnownAssemblyNames.SystemAssemblies) ||
               definition.IsWellKnownType(genericValueTask, WellKnownAssemblyNames.SystemAssemblies);
    }
}
