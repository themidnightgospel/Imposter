using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Helpers;

internal static class ClassSymbolExtensions
{
    internal static List<IMethodSymbol> GetAllOverridableMethods(
        this INamedTypeSymbol classSymbol
    ) =>
        CollectOverridable<IMethodSymbol>(
            classSymbol,
            IsOverridableMethod,
            static method => method.OverriddenMethod
        );

    internal static List<IPropertySymbol> GetAllOverridableProperties(
        this INamedTypeSymbol classSymbol
    ) =>
        CollectOverridable<IPropertySymbol>(
            classSymbol,
            IsOverridableProperty,
            static property => property.OverriddenProperty
        );

    internal static List<IEventSymbol> GetAllOverridableEvents(this INamedTypeSymbol classSymbol) =>
        CollectOverridable<IEventSymbol>(
            classSymbol,
            IsOverridableEvent,
            static @event => @event.OverriddenEvent
        );

    // Walks the class and its base classes up to object, the most derived first. A member that a more derived class
    // overrides is left out, so each member is listed once, as its most derived declaration.
    private static List<TMember> CollectOverridable<TMember>(
        INamedTypeSymbol classSymbol,
        Func<TMember, bool> isOverridable,
        Func<TMember, TMember?> overriddenMember
    )
        where TMember : class, ISymbol
    {
        var members = new List<TMember>();
        var overriddenMembers = new HashSet<ISymbol>(SymbolEqualityComparer.Default);

        for (
            var type = classSymbol;
            type is { SpecialType: not SpecialType.System_Object };
            type = type.BaseType
        )
        {
            foreach (var member in type.GetMembers().OfType<TMember>())
            {
                if (overriddenMember(member) is { } overridden)
                {
                    overriddenMembers.Add(overridden.OriginalDefinition);
                }

                if (isOverridable(member) && !overriddenMembers.Contains(member.OriginalDefinition))
                {
                    members.Add(member);
                }
            }
        }

        return members;
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

        return method.IsVirtual
            || method.IsAbstract
            || (method.IsOverride && !OverridesObjectMember(method));
    }

    // ToString, Equals and GetHashCode overrides keep their real behaviour, because collections, assertions and
    // string formatting call them implicitly.
    private static bool OverridesObjectMember(IMethodSymbol method)
    {
        var root = method;
        while (root.OverriddenMethod is { } overridden)
        {
            root = overridden;
        }

        return root.ContainingType.SpecialType == SpecialType.System_Object;
    }

    private static bool IsOverridableProperty(IPropertySymbol property)
    {
        if (
            property.IsStatic
            || property.DeclaredAccessibility == Accessibility.Private
            || property.IsSealed
        )
        {
            return false;
        }

        if (property.IsAbstract || property.IsVirtual || property.IsOverride)
        {
            return true;
        }

        return IsOverridableAccessor(property.GetMethod)
            || IsOverridableAccessor(property.SetMethod);
    }

    private static bool IsOverridableAccessor(IMethodSymbol? accessor)
    {
        if (accessor is null)
        {
            return false;
        }

        if (
            accessor.IsStatic
            || accessor.DeclaredAccessibility == Accessibility.Private
            || accessor.IsSealed
        )
        {
            return false;
        }

        return accessor.IsAbstract || accessor.IsVirtual || accessor.IsOverride;
    }

    private static bool IsOverridableEvent(IEventSymbol @event)
    {
        if (
            @event.IsStatic
            || @event.DeclaredAccessibility == Accessibility.Private
            || @event.IsSealed
        )
        {
            return false;
        }

        if (@event.IsAbstract || @event.IsVirtual || @event.IsOverride)
        {
            return true;
        }

        return IsOverridableAccessor(@event.AddMethod)
            || IsOverridableAccessor(@event.RemoveMethod);
    }
}
