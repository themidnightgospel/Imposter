# Limitations

Imposter generates compile-time imposters. It does not alter existing types at runtime, so certain members and scenarios are out of scope.

## What You Can Target

- Interfaces: fully supported.
- Classes: supported, but only overridable (virtual/abstract) members can be intercepted.

## Not Supported / Out of Scope

- Non-virtual members on concrete classes
  - Non-virtual instance methods, properties, indexers cannot be intercepted on non-abstract classes.

!!! tip "Pro tip"
    Wrap non-virtual members behind an interface or adapter. Generate an imposter for the adapter to keep tests fast and explicit.

- Static and extension methods
  - Static methods/properties/fields and static classes are not mockable.
  - Extension methods are static calls; you cannot intercept the extension itself. Mock the underlying instance member instead.

- Sealed classes without virtuals
  - Sealed types provide no override surface. If a sealed class exposes no virtual members, it cannot be intercepted.

- Private members
  - Private members are not targetable. Imposters work against accessible members only (public/protected; `internal` when generated within the same assembly).

- Non-virtual events on classes
  - Events declared as non-virtual on classes cannot be intercepted; interface events are supported.

## Language Version Considerations

- Static type extensions (e.g., `IMyService.Imposter()`) require C# 14+. On C# 9–13, use the generated `IMyServiceImposter` type directly.

See also: [Troubleshooting](troubleshooting.md)
