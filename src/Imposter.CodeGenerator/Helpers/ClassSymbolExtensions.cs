using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Helpers;

public static class ClassSymbolExtensions
{
    public static IList<IMethodSymbol> GetAllOverridableMethods(this INamedTypeSymbol classSymbol)
    {
        if (!classSymbol.IsClass() || classSymbol.IsSealed())
        {
            return new List<IMethodSymbol>();
        }

        var methods = new List<IMethodSymbol>();
        var visitedTypes = new HashSet<INamedTypeSymbol>(SymbolEqualityComparer.Default);
        var overriddenMembers = new HashSet<IMethodSymbol>(SymbolEqualityComparer.Default);

        CollectOverridableMethodsRecursive(classSymbol, methods, visitedTypes, overriddenMembers);

        return methods;
    }

    public static List<IPropertySymbol> GetAllOverridableProperties(this INamedTypeSymbol classSymbol)
    {
        return GetOverridableProperties(classSymbol);
    }

    public static List<IEventSymbol> GetAllOverridableEvents(this INamedTypeSymbol classSymbol)
    {
        if (!classSymbol.IsClass() || classSymbol.IsSealed())
        {
            return [];
        }

        var events = new List<IEventSymbol>();
        var visitedTypes = new HashSet<INamedTypeSymbol>(SymbolEqualityComparer.Default);
        var overriddenEvents = new HashSet<IEventSymbol>(SymbolEqualityComparer.Default);

        CollectOverridableEventsRecursive(classSymbol, events, visitedTypes, overriddenEvents);

        return events;
    }

    private static List<IPropertySymbol> GetOverridableProperties(INamedTypeSymbol classSymbol)
    {
        if (!classSymbol.IsClass() || classSymbol.IsSealed())
        {
            return [];
        }

        var properties = new List<IPropertySymbol>();
        var visitedTypes = new HashSet<INamedTypeSymbol>(SymbolEqualityComparer.Default);
        var overriddenProperties = new HashSet<IPropertySymbol>(SymbolEqualityComparer.Default);

        CollectOverridablePropertiesRecursive(classSymbol, properties, visitedTypes, overriddenProperties);

        return properties;
    }

    private static void CollectOverridableMethodsRecursive(
        INamedTypeSymbol typeSymbol,
        ICollection<IMethodSymbol> methods,
        HashSet<INamedTypeSymbol> visitedTypes,
        HashSet<IMethodSymbol> overriddenMembers)
    {
        if (typeSymbol is null || !visitedTypes.Add(typeSymbol))
        {
            return;
        }

        foreach (var method in typeSymbol.GetMembers().OfType<IMethodSymbol>())
        {
            if (method.OverriddenMethod is { } overridden)
            {
                var overriddenKey = overridden.OriginalDefinition ?? overridden;
                overriddenMembers.Add(overriddenKey);
            }

            if (!IsOverridableMethod(method))
            {
                continue;
            }

            var methodKey = method.OriginalDefinition ?? method;

            if (overriddenMembers.Contains(methodKey))
            {
                continue;
            }

            methods.Add(method);
        }

        if (typeSymbol.BaseType is { } baseType && baseType.SpecialType != SpecialType.System_Object)
        {
            CollectOverridableMethodsRecursive(baseType, methods, visitedTypes, overriddenMembers);
        }
    }

    private static void CollectOverridablePropertiesRecursive(
        INamedTypeSymbol typeSymbol,
        ICollection<IPropertySymbol> properties,
        HashSet<INamedTypeSymbol> visitedTypes,
        HashSet<IPropertySymbol> overriddenProperties)
    {
        if (typeSymbol is null || !visitedTypes.Add(typeSymbol))
        {
            return;
        }

        foreach (var property in typeSymbol.GetMembers().OfType<IPropertySymbol>().Where(IsOverridableProperty))
        {
            if (property.OverriddenProperty is { } overridden)
            {
                overriddenProperties.Add(overridden);
            }

            if (overriddenProperties.Contains(property))
            {
                continue;
            }

            properties.Add(property);
        }

        if (typeSymbol.BaseType is { } baseType && baseType.SpecialType != SpecialType.System_Object)
        {
            CollectOverridablePropertiesRecursive(baseType, properties, visitedTypes, overriddenProperties);
        }
    }

    private static void CollectOverridableEventsRecursive(
        INamedTypeSymbol typeSymbol,
        ICollection<IEventSymbol> events,
        HashSet<INamedTypeSymbol> visitedTypes,
        HashSet<IEventSymbol> overriddenEvents)
    {
        if (typeSymbol is null || !visitedTypes.Add(typeSymbol))
        {
            return;
        }

        foreach (var @event in typeSymbol.GetMembers().OfType<IEventSymbol>().Where(IsOverridableEvent))
        {
            if (@event.OverriddenEvent is { } overridden)
            {
                overriddenEvents.Add(overridden);
            }

            if (overriddenEvents.Contains(@event))
            {
                continue;
            }

            events.Add(@event);
        }

        if (typeSymbol.BaseType is { } baseType && baseType.SpecialType != SpecialType.System_Object)
        {
            CollectOverridableEventsRecursive(baseType, events, visitedTypes, overriddenEvents);
        }
    }

    private static bool IsOverridableMethod(IMethodSymbol method)
    {
        if (method.MethodKind != MethodKind.Ordinary)
        {
            return false;
        }

        if (method.IsStatic)
        {
            return false;
        }

        if (method.IsSealed || method.DeclaredAccessibility == Accessibility.Private)
        {
            return false;
        }

        return method.IsVirtual || method.IsAbstract;
    }

    private static bool IsOverridableProperty(IPropertySymbol property)
    {
        if (property.IsStatic || property.DeclaredAccessibility == Accessibility.Private || property.IsSealed)
        {
            return false;
        }

        if (property.IsAbstract || property.IsVirtual || property.IsOverride)
        {
            return true;
        }

        return IsOverridableAccessor(property.GetMethod) || IsOverridableAccessor(property.SetMethod);
    }

    private static bool IsOverridableAccessor(IMethodSymbol? accessor)
    {
        if (accessor is null)
        {
            return false;
        }

        if (accessor.IsStatic || accessor.DeclaredAccessibility == Accessibility.Private || accessor.IsSealed)
        {
            return false;
        }

        return accessor.IsAbstract || accessor.IsVirtual || accessor.IsOverride;
    }

    private static bool IsOverridableEvent(IEventSymbol @event)
    {
        if (@event.IsStatic || @event.DeclaredAccessibility == Accessibility.Private || @event.IsSealed)
        {
            return false;
        }

        if (@event.IsAbstract || @event.IsVirtual || @event.IsOverride)
        {
            return true;
        }

        return IsOverridableAccessor(@event.AddMethod) || IsOverridableAccessor(@event.RemoveMethod);
    }

    public static bool IsClass(this INamedTypeSymbol typeSymbol)
    {
        return typeSymbol?.TypeKind == TypeKind.Class;
    }

    public static bool IsSealed(this INamedTypeSymbol typeSymbol)
    {
        return typeSymbol?.IsSealed == true;
    }
}
