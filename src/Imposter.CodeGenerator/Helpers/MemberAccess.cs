using System.Linq;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Helpers;

// Which members of a target the imposter, compiled into the imposter's assembly, may call or override, and which types
// it may use. That assembly has internal access to a member's assembly when it is that assembly or the member's
// assembly grants it InternalsVisibleTo. Without that access, internal and private protected members are inaccessible,
// and a protected internal member is overridden as protected.
internal readonly struct MemberAccess
{
    private readonly IAssemblySymbol _imposterAssembly;

    internal MemberAccess(IAssemblySymbol imposterAssembly) => _imposterAssembly = imposterAssembly;

    internal bool IsAccessible(ISymbol member) =>
        member.DeclaredAccessibility switch
        {
            Accessibility.Private => false,
            Accessibility.Internal or Accessibility.ProtectedAndInternal => HasInternalAccessTo(
                member
            ),
            _ => true,
        };

    // An inaccessible accessor, such as a private one, cannot be overridden or called, so it counts as absent.
    internal IMethodSymbol? AccessibleOrNull(IMethodSymbol? accessor) =>
        accessor is not null && IsAccessible(accessor) ? accessor : null;

    internal Accessibility GetOverrideAccessibility(ISymbol member) =>
        member.DeclaredAccessibility == Accessibility.ProtectedOrInternal
        && !HasInternalAccessTo(member)
            ? Accessibility.Protected
            : member.DeclaredAccessibility;

    // A type the imposter's assembly declares, or one a referenced assembly makes accessible to it.
    internal bool CanUseType(string metadataName)
    {
        var assemblies = _imposterAssembly
            .Modules.SelectMany(module => module.ReferencedAssemblySymbols)
            .Prepend(_imposterAssembly);

        foreach (var assembly in assemblies)
        {
            if (assembly.GetTypeByMetadataName(metadataName) is { } type && IsAccessible(type))
            {
                return true;
            }
        }

        return false;
    }

    private bool HasInternalAccessTo(ISymbol member) =>
        member.ContainingAssembly.GivesAccessTo(_imposterAssembly);
}
