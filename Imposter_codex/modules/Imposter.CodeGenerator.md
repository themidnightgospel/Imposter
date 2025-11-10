# Imposter.CodeGenerator – Property Default Behaviour

This note documents the default behaviour for class property imposters when the target property has an initializer.

## Class Property Initializers

When an overridable property on a class has an initializer (e.g. `public virtual int A { get; set; } = 11;`), the imposter’s default property behaviour now reflects that initialized value on first read.

### How it works

- For class targets, when default behaviour is active and no explicit getter setup exists, the getter will:
  - On the first read, if a base getter is available, seed the internal backing field from the base getter value, then return it.
  - On subsequent reads (and sets), continue to behave like an auto-property until explicitly configured.
  - If `UseBaseImplementation` is enabled, calls go directly to the base implementation and throw if base is not available.

### Rationale

This mirrors C# semantics where property initializers are applied in the base class’s initialization path. The imposter should surface the same initialized state by default.

### Tests

See `tests/Imposter.Tests/Features/PropertyImposter/PropertyDefaultBehaviour.cs:27` which verifies the first read returns the initializer value.

