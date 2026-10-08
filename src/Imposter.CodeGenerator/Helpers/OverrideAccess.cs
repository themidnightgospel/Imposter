using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Helpers;

// What the assembly that compiles the imposter may override. It has internal access to a member's assembly when it
// is that assembly or the member's assembly grants it InternalsVisibleTo. Without that access, internal and private
// protected members cannot be overridden, and a protected internal member is overridden as protected.
internal readonly struct OverrideAccess
{
    private readonly IAssemblySymbol _imposterAssembly;

    internal OverrideAccess(IAssemblySymbol imposterAssembly) =>
        _imposterAssembly = imposterAssembly;

    internal bool CanOverride(ISymbol member) =>
        member.DeclaredAccessibility
            is not (Accessibility.Internal or Accessibility.ProtectedAndInternal)
        || HasInternalAccessTo(member);

    internal Accessibility GetOverrideAccessibility(ISymbol member) =>
        member.DeclaredAccessibility == Accessibility.ProtectedOrInternal
        && !HasInternalAccessTo(member)
            ? Accessibility.Protected
            : member.DeclaredAccessibility;

    private bool HasInternalAccessTo(ISymbol member) =>
        member.ContainingAssembly.GivesAccessTo(_imposterAssembly);
}
