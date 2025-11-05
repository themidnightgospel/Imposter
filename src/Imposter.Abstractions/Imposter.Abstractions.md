# Module: Imposter.Abstractions

Public API for consumers and the generator. Lives in `src/Imposter.Abstractions` and targets `netstandard2.0`.

## Responsibilities

- Attribute to mark targets for imposter generation.
- Argument matchers used in method/property setups.
- Verification primitives (counts) and exceptions.
- Small helpers to access underlying instances and perform type-safe casts.

## Key Types

- `GenerateImposterAttribute` (`src/Imposter.Abstractions/GenerateImposterAttribute.cs`)
  - Usage: place on interface declaration, class, or assembly.
  - Example: `[GenerateImposter(typeof(IMyService))]`.
  - Property: `PutInTheSameNamespace` (default true) controls generated namespace.

- `Arg<T>` (`src/Imposter.Abstractions/Arg.cs`)
  - Matchers for input parameters in setups:
    - `Arg<T>.Is(predicate)` / `Arg<T>.Is(value)`
    - `Arg<T>.Any()`
    - `Arg<T>.IsDefault()`
    - `Arg<string>.MatchesRegex(string)`

- `OutArg<T>` (`src/Imposter.Abstractions/Arg.cs`)
  - `OutArg<T>.Any()` for matching `out` parameters.

- `Count` (`src/Imposter.Abstractions/Count.cs`)
  - Verification counts: `Exactly(n)`, `AtLeast(n)`, `AtMost(n)`, `Never()`, `Any`, `Once()`.
  - `Matches(int)` helper used by generated verifiers.

- `VerificationFailedException` (`src/Imposter.Abstractions/VerificationFailedException.cs`)
  - Thrown by generated verifiers when counts don’t match.

- `IHaveImposterInstance<T>` and `ImposterExtensions` (`src/Imposter.Abstractions/IHaveImposterInstance.cs`, `ImposterExtensions.cs`)
  - Allows `imposter.Instance()` extension usage.

- `TypeCaster` (`src/Imposter.Abstractions/TypeCaster.cs`)
  - `TryCast<TIn,TOut>(in,out)`; `Cast<TIn,TOut>(in)` for safe conversions.

## How It’s Used

- Include `Imposter.Abstractions` in consumer projects to reference the attribute and argument matching types.
- The generator uses these types at compile time to discover targets and glue behaviour contracts.

## Local Build & Tests

- Build: `dotnet build src/Imposter.Abstractions/Imposter.Abstractions.csproj`
- Tests: see `Imposter_codex/modules/Tests.md` (contains tests referencing abstractions).

