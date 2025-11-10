# Imposter.CodeGenerator – Property Default Behaviour

## Syntax Construction Guidelines

When authoring or updating Roslyn syntax inside the generator, always prefer the shared helpers in `SyntaxFactoryHelper` for common literals and identifiers. In particular:

- Use `SyntaxFactoryHelper.True` instead of `LiteralExpression(SyntaxKind.TrueLiteralExpression)`.
- Use `SyntaxFactoryHelper.False` instead of `LiteralExpression(SyntaxKind.FalseLiteralExpression)`.
- Use `SyntaxFactoryHelper.Null` instead of `LiteralExpression(SyntaxKind.DefaultLiteralExpression)` or other default/null literal expressions.
- Use `SyntaxFactoryHelper.Var` instead of `IdentifierName("var")`.

Keeping literals and identifier shorthands on the helper surface avoids redundant Roslyn allocations and ensures we emit syntax consistently across features.

This note documents the default behaviour for class property imposters when the target property has an initializer.

## Class Property Initializers

When an overridable property on a class has an initializer (e.g. `public virtual int A { get; set; } = 11;`), the imposter’s default property behaviour now reflects that initialized value on first read.

### How it works

- For class targets, when default behaviour is active and no explicit getter setup exists, the getter will:
  - On the first read, if a base getter is available, seed the internal backing field from the base getter value, then return it.
  - On subsequent reads (and sets), continue to behave like an auto-property until explicitly configured.
  - If `UseBaseImplementation` is enabled, calls go directly to the base implementation and throw if base is not available.

### Rationale

This mirrors C# semantics where property initializers are applied in the base class's initialization path. The imposter should surface the same initialized state by default.

### Tests

See `tests/Imposter.Tests/Features/PropertyImposter/PropertyDefaultBehaviour.cs:27` which verifies the first read returns the initializer value.

## Event `UseBaseImplementation`

Event imposters now expose `.UseBaseImplementation()` on their fluent builder whenever the target event belongs to an overridable class member (i.e., virtual/override events with non-abstract accessors). The builder still tracks and verifies subscriptions, but when the flag is enabled it will:

- Capture the base `add`/`remove` implementations via the event accessors and invoke them whenever handlers are subscribed/unsubscribed.
- Throw a `MissingImposterException` that includes the event display name if the base implementation is requested but unavailable (e.g., the imposter is used outside a class target).

This keeps event behaviour aligned with properties/methods: opting into base execution forwards to the real member first while retaining all imposter diagnostics.

### Tests

- `tests/Imposter.Tests/Features/ClassImposter/ProtectedOverrideableMembersClassImposterTests.cs` (`GivenProtectedEventUseBaseImplementation_WhenSubscribed_ThenBaseEventReceivesHandler`) covers protected event subscriptions.
- `tests/Imposter.Tests/Features/ClassImposter/ProtectedOverrideableMembersClassImposterTests.cs` (`GivenAsyncEventUseBaseImplementation_WhenUnsubscribed_ThenBaseEventIsCleared`) covers async task-based events and unsubscriptions.
