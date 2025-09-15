using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Helpers;

public static class InterfaceSymbolExtensions
{
    public static List<IMethodSymbol> GetAllInterfaceMethods(this INamedTypeSymbol interfaceSymbol)
    {
        var methods = new List<IMethodSymbol>();
        var visitedInterfaces = new HashSet<INamedTypeSymbol>(SymbolEqualityComparer.Default);

        CollectInterfaceMethodsRecursive(interfaceSymbol, methods, visitedInterfaces);

        return methods;
    }

    private static void CollectInterfaceMethodsRecursive(
        INamedTypeSymbol interfaceSymbol,
        List<IMethodSymbol> methods,
        HashSet<INamedTypeSymbol> visitedInterfaces)
    {
        // Avoid infinite recursion with circular interface inheritance
        if (interfaceSymbol == null || !visitedInterfaces.Add(interfaceSymbol))
        {
            return;
        }

        // Get ordinary methods from current interface
        methods.AddRange(interfaceSymbol.GetMembers()
            .OfType<IMethodSymbol>()
            .Where(m => m.MethodKind == MethodKind.Ordinary));

        // Recursively process all implemented interfaces
        foreach (var implementedInterface in interfaceSymbol.Interfaces)
        {
            CollectInterfaceMethodsRecursive(implementedInterface, methods, visitedInterfaces);
        }
    }
}