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
  - Thrown by generated verifiers when counts don’t match.

- `IHaveImposterInstance<T>` and `ImposterExtensions` (`src/Imposter.Abstractions/IHaveImposterInstance.cs`, `ImposterExtensions.cs`)
  - Allows `imposter.Instance()` extension usage; the interface also serves as a generic constraint for helpers that take any imposter of a target.

- `TypeCaster` (`src/Imposter.Abstractions/TypeCaster.cs`)
  - `TryCast<TIn,TOut>(in,out)`; `Cast<TIn,TOut>(in)` for safe conversions.
  - Generated-code only, like `SpanElementsComparer<T>`, `SetupCollection<T>`, `CallbackCollection<T>`, `EventHandlerCollection<T>`, `ReturnValueSequence<T>` and `AsyncCallbacks`: all carry `[EditorBrowsable(EditorBrowsableState.Never)]` so they stay out of IntelliSense. They are public for generated code only and may change with the generator in the same package.

## How It’s Used

- `IImposter` provides invocation snapshots, `ClearInvocations()` and `Reset()`. Clearing preserves configured behavior and stored values; resetting replaces imposter-owned state while retaining the mode and instance. Existing snapshots remain usable and old setup builders are detached by reset.
- `IImposterInvocation` supplies member identity, a process-wide entry sequence, and synchronous exceptions. Generated method/accessor records provide typed captures. `EventSubscriptionInvocation<THandler>` represents event add/remove accessors. Property getters retain counters and do not enter the ordered history.
- `InvocationHistory<T>` publishes indexer and custom ref-struct setter records through an intrusive list; generated method histories use an equivalent typed node chain. Shared property setters and event accessors use compact `InvocationBuffer<T>` storage and materialize records on inspection. Snapshots materialize and sort on demand; they keep membership after later calls, but ordinary reference arguments are not copied. Span captures expose `ReadOnlyMemory<T>` over their recorded array.
- Generic dispatch records the caller's type arguments and captures entry values before evaluating setup predicates. Its inner method implementations and compatible-type adapters skip recording so one interaction produces exactly one record. Output values are recorded after conversion, and synchronous exceptions still propagate modified ref/out arguments to the caller.
- Generic span calls retain an entry array separate from matcher arrays. Exact-type dispatch and compatible adapters with method-independent span element types can refresh and reuse lookup arrays when every configured span matcher is built-in and does not expose arrays to user predicates. Arbitrary array predicates disable reuse for that imposter. The four-int exact-type fixtures have ceilings of 144 bytes for built-in matchers and 176 bytes for array predicates; compatible-type adapters have ceilings of 208 and 248 bytes respectively. See the invocation-history performance table and CallAllocationTests for measured scope and other costs.

- Include `Imposter.Abstractions` in consumer projects to reference the attribute and argument matching types.
- The generator uses these types at compile time to discover targets and glue behaviour contracts.

## Local Build & Tests

- Build: `dotnet build src/Imposter.Abstractions/Imposter.Abstractions.csproj`
- Tests: see `Imposter_codex/modules/Tests.md` (contains tests referencing abstractions).


## Internal setter history storage


`PropertySetter<T>` appends compact values and sequence stamps to `InvocationBuffer<T>` and materializes public typed records only when inspected. Entries occupy 32-slot segments with separate integer publication links; exception columns are allocated only when a segment contains a failed assignment. Slot reservation, directory growth, and publication are lock-free. A captured enumerator follows the immutable publication chain visible when enumeration starts. Materialized records copy the captured value, sequence, and exception. Span setters isolate the entry array from mutable callbacks and property storage. `PassThroughSetter<TInvocation>` supplies history for custom ref-struct setters without retaining their unstoreable values. Generated init accessors record after direct base assignment, including failures.
