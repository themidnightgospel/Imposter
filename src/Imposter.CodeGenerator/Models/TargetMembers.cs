using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Models;

/// <summary>
/// The members of a target its imposter impersonates, collected once for both validating and modelling the target. Only
/// the members the imposter's assembly can override or implement are included.
/// </summary>
internal sealed class TargetMembers
{
    private TargetMembers(
        IReadOnlyCollection<IMethodSymbol> methods,
        IReadOnlyCollection<IPropertySymbol> properties,
        IReadOnlyCollection<IEventSymbol> events
    )
    {
        Methods = methods;
        Properties = properties;
        Events = events;
    }

    internal IReadOnlyCollection<IMethodSymbol> Methods { get; }

    internal IReadOnlyCollection<IPropertySymbol> Properties { get; }

    internal IReadOnlyCollection<IEventSymbol> Events { get; }

    /// <summary>
    /// The methods, then the properties, then the events.
    /// </summary>
    internal IEnumerable<ISymbol> All => Methods.Concat<ISymbol>(Properties).Concat(Events);

    internal static TargetMembers Of(INamedTypeSymbol target, MemberAccess memberAccess) =>
        new(
            GetMethods(target, memberAccess),
            GetProperties(target, memberAccess),
            GetEvents(target, memberAccess)
        );

    private static IReadOnlyCollection<IMethodSymbol> GetMethods(
        INamedTypeSymbol target,
        MemberAccess memberAccess
    ) =>
        target.TypeKind switch
        {
            TypeKind.Interface => target.GetAllInterfaceMethods(),
            TypeKind.Class => target
                .GetAllOverridableMethods()
                .Where(memberAccess.IsAccessible)
                .ToArray(),
            _ => [],
        };

    private static IReadOnlyCollection<IPropertySymbol> GetProperties(
        INamedTypeSymbol target,
        MemberAccess memberAccess
    ) =>
        target.TypeKind switch
        {
            TypeKind.Interface => target.GetAllInterfaceProperties(),
            TypeKind.Class => target
                .GetAllOverridableProperties()
                .Where(memberAccess.IsAccessible)
                .ToArray(),
            _ => [],
        };

    // Unlike methods and properties, events need a known delegate signature to generate their raising API.
    // Leave events without one out, even when another generator might supply the delegate later. Keep other
    // members with unresolved types: qualified references can bind once all generators' output is compiled.
    private static IEventSymbol[] GetEvents(INamedTypeSymbol target, MemberAccess memberAccess) =>
        target.TypeKind switch
        {
            TypeKind.Interface => target.GetAllInterfaceEvents().Where(HasDelegateType).ToArray(),
            TypeKind.Class => target
                .GetAllOverridableEvents()
                .Where(memberAccess.IsAccessible)
                .Where(HasDelegateType)
                .ToArray(),
            _ => [],
        };

    private static bool HasDelegateType(IEventSymbol @event) =>
        @event.Type is INamedTypeSymbol { DelegateInvokeMethod: not null };
}
