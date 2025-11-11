using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Helpers;

public static class InterfaceSymbolExtensions
{
    internal static IReadOnlyCollection<IMethodSymbol> GetAllInterfaceMethods(this INamedTypeSymbol interfaceSymbol)
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
    
    internal static IReadOnlyCollection<IPropertySymbol> GetAllInterfaceProperties(this INamedTypeSymbol interfaceSymbol)
    {
        var properties = new HashSet<IPropertySymbol>(SymbolEqualityComparer.Default);
        var visitedInterfaces = new HashSet<INamedTypeSymbol>(SymbolEqualityComparer.Default);

        CollectInterfacePropertiesRecursive(interfaceSymbol, properties, visitedInterfaces);

        return properties;
    }

    private static void CollectInterfacePropertiesRecursive(
        INamedTypeSymbol interfaceSymbol,
        HashSet<IPropertySymbol> properties,
        HashSet<INamedTypeSymbol> visitedInterfaces)
    {
        if (!visitedInterfaces.Add(interfaceSymbol))
        {
            return;
        }

        foreach (var propertySymbol in interfaceSymbol
                     .GetMembers()
                     .OfType<IPropertySymbol>())
        {
            properties.Add(propertySymbol);
        }

        foreach (var implementedInterface in interfaceSymbol.Interfaces)
        {
            CollectInterfacePropertiesRecursive(implementedInterface, properties, visitedInterfaces);
        }
    }

    internal static IReadOnlyCollection<IEventSymbol> GetAllInterfaceEvents(this INamedTypeSymbol interfaceSymbol)
    {
        var events = new List<IEventSymbol>();
        var visitedInterfaces = new HashSet<INamedTypeSymbol>(SymbolEqualityComparer.Default);

        CollectInterfaceEventsRecursive(interfaceSymbol, events, visitedInterfaces);

        return Deduplicate(events);
    }

    private static void CollectInterfaceEventsRecursive(
        INamedTypeSymbol interfaceSymbol,
        ICollection<IEventSymbol> events,
        HashSet<INamedTypeSymbol> visitedInterfaces)
    {
        if (!visitedInterfaces.Add(interfaceSymbol))
        {
            return;
        }

        foreach (var eventSymbol in interfaceSymbol
                     .GetMembers()
                     .OfType<IEventSymbol>())
        {
            events.Add(eventSymbol);
        }

        foreach (var implementedInterface in interfaceSymbol.Interfaces)
        {
            CollectInterfaceEventsRecursive(implementedInterface, events, visitedInterfaces);
        }
    }

    private static List<IEventSymbol> Deduplicate(IEnumerable<IEventSymbol> events)
    {
        var result = new List<IEventSymbol>();

        foreach (var eventSymbol in events)
        {
            var alreadyAdded = result.Any(existing =>
                string.Equals(existing.Name, eventSymbol.Name, StringComparison.Ordinal)
                && SymbolEqualityComparer.Default.Equals(existing.Type, eventSymbol.Type));

            if (!alreadyAdded)
            {
                result.Add(eventSymbol);
            }
        }

        return result;
    }

}
