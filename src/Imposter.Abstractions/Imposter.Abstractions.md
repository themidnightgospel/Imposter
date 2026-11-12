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

- `Arg` / `AnyArgMarker` (`src/Imposter.Abstractions/Arg.cs`)
  - `Arg.Any` — untyped wildcard property returning `AnyArgMarker`, implicitly converts to `Arg<T>.Any()` for any parameter type.

- `Arg<T>` (`src/Imposter.Abstractions/Arg.cs`)
  - `Describe(string)` returns a new matcher with a diagnostic label. Default descriptions never evaluate predicates or comparers; ordinary matcher allocation sizes are preserved.
  - Matchers for input parameters in setups:
    - `Arg<T>.Is(predicate)` / `Arg<T>.Is(value)`
    - `Arg<T>.Any()`
    - `Arg<T>.IsDefault()`
    - `Arg<string>.MatchesRegex(string)`
    - `implicit operator Arg<T>(AnyArgMarker)` — enables `Arg.Any` shorthand

- `OutArg<T>` (`src/Imposter.Abstractions/Arg.cs`)
  - `OutArg<T>.Any()` for matching `out` parameters.

- `SpanArg<T>` and `ReadOnlySpanArg<T>` (`src/Imposter.Abstractions/SpanArg.cs`, `ReadOnlySpanArg.cs`)
  - Match the copy of a `Span<T>` / `ReadOnlySpan<T>` argument: `Is(elements)`, `Is(elements, comparer)`, `Is(predicate)`, `Any()`.
  - `implicit operator` from `AnyArgMarker` — enables `Arg.Any` shorthand
  - Share their element checks through the internal `SpanElementMatchers`.

- `OutSpanArg<T>` and `OutReadOnlySpanArg<T>` (`src/Imposter.Abstractions/OutSpanArg.cs`, `OutReadOnlySpanArg.cs`)
  - `Any()` for matching `out Span<T>` / `out ReadOnlySpan<T>` parameters.

- `Count` (`src/Imposter.Abstractions/Count.cs`)
  - Verification counts: `Exactly(n)`, `AtLeast(n)`, `AtMost(n)`, `Never()`, `Any`, `Once()`.
  - `Matches(int)` helper used by generated verifiers.

- `VerificationFailedException` (`src/Imposter.Abstractions/VerificationFailedException.cs`)
  - Rich diagnostics expose `Expected`, retain `PerformedInvocations`, and support a verification reason. `MissingImposterException.ConfiguredSetups` is a snapshot of setup descriptions.
  - `DiagnosticValue` formats values safely on failure; throwing `ToString()` implementations use an unprintable-value fallback.
  - Thrown by generated verifiers when counts don’t match.

- `IHaveImposterInstance<T>` and `ImposterExtensions` (`src/Imposter.Abstractions/IHaveImposterInstance.cs`, `ImposterExtensions.cs`)
  - Allows `imposter.Instance()` extension usage; the interface also serves as a generic constraint for helpers that take any imposter of a target.

- `TypeCaster` (`src/Imposter.Abstractions/TypeCaster.cs`)
  - `TryCast<TIn,TOut>(in,out)`; `Cast<TIn,TOut>(in)` for safe conversions.
  - Generated-code only, like `SpanElementsComparer<T>`, `SetupCollection<T>`, `CallbackCollection<T>`, `EventHandlerCollection<T>`, `ReturnValueSequence<T>` and `AsyncCallbacks`: all carry `[EditorBrowsable(EditorBrowsableState.Never)]` so they stay out of IntelliSense. They are public for generated code only and may change with the generator in the same package.

## How It’s Used

- `IImposter` provides invocation snapshots, clear/reset lifecycle operations, and completeness checks. `Imposters` aggregates completeness failures across multiple instances and creates ordered verifications.
- `IImposterInvocation` supplies member identity, a process-wide entry sequence, and synchronous exceptions. Generated method/accessor records provide typed captures. `EventSubscriptionInvocation<THandler>` represents event add/remove accessors. Property getters retain counters and do not enter the ordered history.
- `InvocationHistory<T>` publishes indexer and custom ref-struct setter records through an intrusive list; generated method histories use an equivalent typed node chain. Shared property setters and event accessors use compact `InvocationBuffer<T>` storage and materialize records on inspection. Coverage occupies the sign bit of each stored sequence, with the public sequence masking that bit. Snapshots materialize and sort on demand; they keep membership across clear/reset, but ordinary reference arguments are not copied. Span captures expose `ReadOnlyMemory<T>` over their recorded array.
- Generic dispatch records the caller's type arguments and captures entry values before evaluating setup predicates. Its inner method implementations and compatible-type adapters skip recording so one interaction produces exactly one record. Output values are recorded after conversion, and synchronous exceptions still propagate modified ref/out arguments to the caller.
- Generic span calls retain an entry array separate from matcher arrays. Exact-type dispatch and compatible adapters with fixed span element types can reuse an internal lookup array when every configured span matcher is built-in and does not expose arrays to user predicates; described built-in matchers preserve that safety. Arbitrary array predicates disable reuse for the member until reset, and adapters with generic-dependent span elements retain separate conversion copies. The exact-type four-int fixture has ceilings of 144 bytes with built-in matchers and 176 with an array predicate (128 measured on the original baseline). The fixed-four-int-span/generic-reference adapter fixture has ceilings of 208 bytes with built-in matchers and 248 with an arbitrary array predicate (both measured 200 on the baseline). The predicate paths retain an additional private 40-byte entry array and an eight-byte sequence word. See the invocation-history performance table and CallAllocationTests for the exact fixture scope.
- `IInvocationStep` is the common method/accessor handle consumed by `InvocationOrder`. Ordered verification greedily selects distinct invocation entries and applies coverage only after every step succeeds.
- Successful count verification covers matching records from a captured history in two deterministic-matcher passes; reading/counting records never changes coverage. Counter-only getters track coverage without allocating on access.
- `IImposterVerificationSource`, `IVerifiableInvocation`, `ImposterVerification`, `CounterVerification`, and `SetupUsage` support generated completeness checks and setup usage tracking.

- Include `Imposter.Abstractions` in consumer projects to reference the attribute and argument matching types.
- The generator uses these types at compile time to discover targets and glue behaviour contracts.

## Local Build & Tests

- Build: `dotnet build src/Imposter.Abstractions/Imposter.Abstractions.csproj`
- Tests: see `Imposter_codex/modules/Tests.md` (contains tests referencing abstractions).


## Lifecycle and setter state

ClearInvocations clears history and coverage while preserving configured outcomes, their position and usage, stored property/indexer values, and subscriptions. Reset clears old histories and replaces generated state objects while retaining the instance and mode. Retained builders are detached; already-returned snapshot lists keep their records.

PropertySetter<T> appends compact values and sequence/coverage stamps to `InvocationBuffer<T>` and materializes public typed records only when inspected. Entries occupy 32-slot segments with separate integer publication links; exception columns are allocated only when a segment contains a failed assignment. Slot reservation, directory/segment growth, and publication are lock-free: a fixed-radix directory installs each child once with compare-and-exchange. Verification copies one captured-head enumerator for its two passes. Clear swaps the storage generation, and retained public records keep only their containing segment while their coverage handles remain live. Span setters adapt span matchers once when a builder is created and isolate the captured entry array from mutable callbacks/property storage, exposing it as read-only memory in the named generated record. PassThroughSetter<TInvocation> provides equivalent history, coverage, and setup usage for custom ref-struct setters while omitting the unretainable value. Generated init accessors record after direct base assignment so a throwing base initializer is represented exactly once.
