using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace Imposter.CodeGenerator;

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

        CollectOverridableMethodsRecursive(classSymbol, methods, visitedTypes);

        return methods;
    }

    private static void CollectOverridableMethodsRecursive(
        INamedTypeSymbol typeSymbol,
        List<IMethodSymbol> methods,
        HashSet<INamedTypeSymbol> visitedTypes)
    {
        if (typeSymbol == null || !visitedTypes.Add(typeSymbol))
        {
            return;
        }

        // Get overridable methods from current type
        methods.AddRange(typeSymbol.GetMembers()
            .OfType<IMethodSymbol>()
            .Where(IsOverridableMethod));

        // Process base class
        if (typeSymbol.BaseType != null && typeSymbol.BaseType.SpecialType != SpecialType.System_Object)
        {
            CollectOverridableMethodsRecursive(typeSymbol.BaseType, methods, visitedTypes);
        }

        // Process implemented interfaces (for interface methods that can be explicitly implemented)
        foreach (var implementedInterface in typeSymbol.Interfaces)
        {
            CollectOverridableMethodsRecursive(implementedInterface, methods, visitedTypes);
        }
    }

    private static bool IsOverridableMethod(IMethodSymbol method)
    {
        // Must be an ordinary method (not constructor, destructor, etc.)
        if (method.MethodKind != MethodKind.Ordinary)
        {
            return false;
        }

        // Must be instance method (not static)
        if (method.IsStatic)
        {
            return false;
        }

        // Must be accessible and overridable
        if (method.IsSealed || method.DeclaredAccessibility == Accessibility.Private)
        {
            return false;
        }

        // Check if method is virtual, abstract, or override (and not final)
        return method.IsVirtual || method.IsAbstract || method.IsOverride;
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