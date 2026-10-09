using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Helpers;

internal static class InterfaceSymbolExtensions
{
    internal static IReadOnlyCollection<IMethodSymbol> GetAllInterfaceMethods(
        this INamedTypeSymbol interfaceSymbol
    ) =>
        CollectMembers<IMethodSymbol>(
            interfaceSymbol,
            static method =>
                method.MethodKind == MethodKind.Ordinary && !KeepsItsDefaultBody(method)
        );

    internal static IReadOnlyCollection<IPropertySymbol> GetAllInterfaceProperties(
        this INamedTypeSymbol interfaceSymbol
    ) =>
        CollectMembers<IPropertySymbol>(
            interfaceSymbol,
            static property => !KeepsItsDefaultBody(property)
        );

    internal static IReadOnlyCollection<IEventSymbol> GetAllInterfaceEvents(
        this INamedTypeSymbol interfaceSymbol
    ) => Deduplicate(CollectMembers<IEventSymbol>(interfaceSymbol, static _ => true));

    // The interface's members, then those of each interface it inherits, depth first. An interface inherited along
    // several paths is visited once.
    private static List<TMember> CollectMembers<TMember>(
        INamedTypeSymbol interfaceSymbol,
        Func<TMember, bool> isImpersonated
    )
        where TMember : ISymbol
    {
        var members = new List<TMember>();
        var visitedInterfaces = new HashSet<INamedTypeSymbol>(SymbolEqualityComparer.Default);

        Collect(interfaceSymbol);

        return members;

        void Collect(INamedTypeSymbol @interface)
        {
            if (!visitedInterfaces.Add(@interface))
            {
                return;
            }

            members.AddRange(@interface.GetInstanceMembers<TMember>().Where(isImpersonated));

            foreach (var inheritedInterface in @interface.Interfaces)
            {
                Collect(inheritedInterface);
            }
        }
    }

    private static List<IEventSymbol> Deduplicate(IEnumerable<IEventSymbol> events)
    {
        var result = new List<IEventSymbol>();

        foreach (var eventSymbol in events)
        {
            var alreadyAdded = result.Any(existing =>
                string.Equals(existing.Name, eventSymbol.Name, StringComparison.Ordinal)
                && SymbolEqualityComparer.Default.Equals(existing.Type, eventSymbol.Type)
            );

            if (!alreadyAdded)
            {
                result.Add(eventSymbol);
            }
        }

        return result;
    }

    // A static member is called through its interface, never through the imposter's instance, so it isn't impersonated.
    internal static IEnumerable<TMember> GetInstanceMembers<TMember>(
        this INamedTypeSymbol interfaceSymbol
    )
        where TMember : ISymbol =>
        interfaceSymbol.GetMembers().OfType<TMember>().Where(member => !member.IsStatic);

    // The imposter returns by value, so it can't implement a member that returns by reference. One with a default body
    // keeps it, and calls reach that body; an abstract one reports IMP011.
    private static bool KeepsItsDefaultBody(ISymbol member) =>
        member
            is IMethodSymbol { RefKind: not RefKind.None, IsAbstract: false }
                or IPropertySymbol { RefKind: not RefKind.None, IsAbstract: false };
}
