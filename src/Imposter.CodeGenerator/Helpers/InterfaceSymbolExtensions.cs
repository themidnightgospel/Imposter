using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Helpers;

public static class InterfaceSymbolExtensions
{
    public static IReadOnlyCollection<IMethodSymbol> GetAllInterfaceMethods(this INamedTypeSymbol interfaceSymbol)
    {
        var methods = new HashSet<IMethodSymbol>(SymbolEqualityComparer.Default);
        var visitedInterfaces = new HashSet<INamedTypeSymbol>(SymbolEqualityComparer.Default);

        CollectInterfaceMethodsRecursive(interfaceSymbol, methods, visitedInterfaces);

        return methods;
    }

    private static void CollectInterfaceMethodsRecursive(
        INamedTypeSymbol interfaceSymbol,
        HashSet<IMethodSymbol> methods,
        HashSet<INamedTypeSymbol> visitedInterfaces)
    {
        if (!visitedInterfaces.Add(interfaceSymbol))
        {
            return;
        }

        foreach (var methodSymbol in interfaceSymbol
                     .GetMembers()
                     .OfType<IMethodSymbol>()
                     .Where(m => m.MethodKind == MethodKind.Ordinary))
        {
            methods.Add(methodSymbol);
        }

        foreach (var implementedInterface in interfaceSymbol.Interfaces)
        {
            CollectInterfaceMethodsRecursive(implementedInterface, methods, visitedInterfaces);
        }
    }
}