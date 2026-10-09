using Imposter.CodeGenerator.Helpers;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Models;

/// <summary>
/// A property or indexer accessor the imposter can override or implement.
/// </summary>
internal sealed record PropertyAccessorModel(
    bool IsAbstract,
    bool IsInitOnly,
    Accessibility OverrideAccessibility
)
{
    // An accessor the imposter's assembly cannot access, such as a private one, cannot be overridden, so it is absent.
    internal static PropertyAccessorModel? FromAccessible(
        IMethodSymbol? accessor,
        MemberAccess memberAccess
    ) =>
        memberAccess.AccessibleOrNull(accessor) is { } accessible
            ? new PropertyAccessorModel(
                accessible.IsAbstract,
                accessible.IsInitOnly,
                memberAccess.GetOverrideAccessibility(accessible)
            )
            : null;
}
