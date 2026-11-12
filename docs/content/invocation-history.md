# Invocation history

Generated imposters expose completed interactions as read-only snapshots. A method verifier's
`Invocations` property applies the same argument matchers as `Called`:

!!! example "Inspect typed history"
    === "C# 14"

        ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/InvocationHistory/InvocationHistoryTests.cs#L21"}
        var imposter = IHistoryExample.Imposter();
        var instance = imposter.Instance();
        instance.Hello("Ann", 1);
        instance.Hello("Bob", 2);

        var calls = imposter.Hello(Arg<string>.Any(), Arg<int>.Any()).Invocations;
        calls[0].Name.ShouldBe("Ann");
        calls[0].Times.ShouldBe(1);
        calls[0].Sequence.ShouldBeLessThan(calls[1].Sequence);
        ```

    === "C# 8-13"

        ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/InvocationHistory/InvocationHistoryTests.cs#L23"}
        var imposter = new IHistoryExampleImposter();
        var instance = imposter.Instance();
        instance.Hello("Ann", 1);
        instance.Hello("Bob", 2);

        var calls = imposter.Hello(Arg<string>.Any(), Arg<int>.Any()).Invocations;
        calls[0].Name.ShouldBe("Ann");
        calls[0].Times.ShouldBe(1);
        calls[0].Sequence.ShouldBeLessThan(calls[1].Sequence);
        ```

The examples use [Shouldly](https://shouldly.readthedocs.io/) assertions and an `IHistoryExample` interface with `string Hello(string name, int times)`.

Each generated method record has properties for captured arguments, `Result` when the return
value can be stored, and `Member`, `Sequence`, and `Exception`. Generic methods expose records
for the caller's type arguments. A generic value-type result is `default(T)` when the call throws; check `Exception` to distinguish failure from a successful default result. Captured parameter properties that conflict with record members (`Result`, `Exception`, `Member`, `Sequence`, `Next`) or the record class name receive a `_1` suffix. `Called`, `CallCount`, and typed snapshots consistently use
those actual type arguments, including when a compatible setup with other type arguments
provides the outcome. Earlier versions could count an adapted call under its setup's type
arguments instead; verification should now name the type arguments used by the caller.
`ref` and `in` arguments retain their entry values; `out`
arguments retain their values when the call returns or throws. If an exception occurs before
an `out` argument is assigned, its recorded value is the type's default.

Property setters expose `imposter.Property.Setter(matcher).Invocations`. Indexer getters and
setters expose `imposter[keyMatcher].Getter().Invocations` and
`imposter[keyMatcher].Setter().Invocations`, including captured keys and a `Result` or `Value`
when the value can be stored. Custom ref structs cannot be retained in a heap record; their
values are omitted. Span arguments and values are copied and exposed as `ReadOnlyMemory<T>`.
Span setter records also isolate their entry values from setter callbacks and subsequent writes through a property getter.
Ordinary reference arguments and return values retain their identity; arbitrary objects are
not deep-cloned. Read-only memory does not provide isolation from deliberate low-level access
to its underlying array.

For a combined view, use the `Invocations()` extension from `Imposter.Abstractions`:

!!! example "Inspect the same records through the common interface"
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/InvocationHistory/InvocationHistoryTests.cs#L34"}
    IReadOnlyList<IImposterInvocation> history = imposter.Invocations();
    history.Count.ShouldBe(2);
    history[0].Member.ShouldBe("Hello");
    history[0].ShouldBeSameAs(calls[0]);
    ```

!!! tip "A target member named Invocations keeps its setup name"
    Use `((IImposter)imposter).Invocations()` for the aggregate view when a target has its own `Invocations` member.

The combined view includes methods, property setters, indexer getters and setters, and event
subscriptions and unsubscriptions. Event records are `EventSubscriptionInvocation<THandler>`
and expose `Handler` and `IsSubscription`. `imposter.Event.Subscriptions` and
`imposter.Event.Unsubscriptions` provide typed snapshots for the two event accessors.
Event add/remove history stores handler values and entry stamps in compact segments; typed
record objects are materialized only when requesting snapshots. Repeated snapshots preserve captured values but do not promise the same record object identity for buffered property setters and events.
Property getters use counters and do not appear in
history. Raising events from test code and invoking their handlers
do not add history records.

Sequence numbers are assigned when an interaction enters and are shared across imposter
instances. Nested calls therefore sort by entry, even when the inner call completes first.
Only completed interactions appear in snapshots. Calls returning `Task` or `ValueTask` complete
for recording purposes when the return value is obtained: the record keeps that value and
does not await it or capture exceptions from its later asynchronous completion.

A throwing interaction is recorded after the callback's `finally` blocks finish, so its
record includes final `out` values. Verification from inside that callback's `finally` block
does not yet see the current call. Earlier versions could expose a failed call before those
blocks ran; code relying on that timing should verify after the exception reaches the caller.

Snapshots are ordered by sequence and keep their membership after later calls. Reading history does not consume configured outcomes. Lifecycle APIs and snapshot behavior after clear/reset are deferred to #520. Concurrent calls safely publish complete records. A snapshot taken
while calls are running is a view of the records observed during collection, rather than an
atomic transaction across all members; take it after the calls finish when asserting a full
history. Snapshot creation allocates its list on demand.

!!! note "Entry order and completion"
    Sequence numbers describe call entry, while snapshots contain completed interactions. Awaiting a returned task is outside the recorded synchronous interaction.

## Performance costs

History metadata have a measurable cost. Each recorded interaction acquires a global sequence number with `Interlocked.Increment`. Property getters remain counter-only and allocate no history records. Property setters and event accessors store captured values in compact segments; their public record objects are created when history is inspected. Indexer accessors retain captured keys in the invocation record, and stored indexer values use independent value-type keys so default-value storage does not retain unrelated invocation results.

The following figures describe the specific x64 Release fixtures in [CallAllocationTests](https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/CallAllocations/CallAllocationTests.cs), after warm-up. These limits apply to the standalone history implementation; construction and setup retain their original ceilings. Both columns list regression ceilings, not universal costs for every argument or return type; the earlier event figures are measured baseline allocations. Segment creation allocates in batches, including on the first interaction, so setter/event ceilings apply to the average over the test's call batch rather than each individual interaction. Inspecting snapshots allocates separately.

| Fixture | Previous limit | Current limit | Retained information |
| --- | ---: | ---: | --- |
| Method returning `int` or a generic reference result | 48 | 56 per call | Entry sequence in the existing argument/result/exception record |
| Method returning a cached `Task<int>` | 48 | 48 per call | Task and synchronous exception share one outcome slot; they cannot be the same object |
| Void method with one reference argument | 40 | 48 per call | Entry sequence in the existing record |
| Cached `ValueTask<int>` method | 56 | 64 per call | Entry sequence in the existing value-task result record |
| `ref`/`out`/`in` method returning a cached task | 48 | 56 per call | Entry sequence and final `out` value, with a shared Task/exception outcome slot |
| `int` property setter | 32 | 32 per assignment, averaged | Segmented values and sequence words; exception storage is created only when needed |
| `int` indexer getter with one `int` key | 56 | 56 per read | Captured keys plus result, exception and sequence in one object |
| `int` indexer setter with one `int` key | 64 | 64 per assignment | Captured keys plus value, exception and sequence in one object |
| Event subscribe/remove pair, one cached handler | 96 / 32 | 96 / 32, averaged | Compact action history plus the unchanged active-handler state on subscription |
| Exact-type generic span method with four `int` elements and built-in matchers | 136 | 144 per call | Isolated entry history plus one refreshed lookup/matching copy |
| Exact-type generic span method with an array predicate | 128 measured | 176 per call | A separate 40-byte entry copy and eight-byte sequence preserve predicate isolation |
| Compatible-type span adapter with built-in matchers | 200 measured | 208 per call | Entry sequence; safe lookup/matching arrays are reused |
| Compatible-type span adapter with an array predicate | 200 measured | 248 per call | A separate entry copy protects history from arrays retained or mutated by predicates |
| Imposter containing one `int` property | 280 | 280 per construction | Existing accessor state; history and callback storage remain lazy |
| Method setup containing three sequential returns | 736 | 736 per setup | Existing outcome steps and closures |

Generic span calls retain a separate caller-entry snapshot. Exact-type dispatch and compatible adapters whose span element types do not depend on a method type parameter can refresh and reuse lookup arrays for built-in matchers that never expose arrays to user predicates. Registering an arbitrary array predicate disables reuse for that member. These predicates need separate lookup and invocation-matching copies because user code can retain or mutate an evaluation array. Each four-element array occupies 40 bytes in this fixture. Callback execution does not allocate additional per-call wrapper objects.

The same tests pin property getter access to **0 bytes**, and successful `Called`, `Subscribed` and `Unsubscribed` checks with cached handles, matchers and `Count` instances to **0 bytes after warm-up**. Reading invocation snapshots, producing failure messages, and configuring new setups can allocate. Successful count verification counts matches in one pass and does not mutate recorded interactions.

!!! note "First-interaction allocations"
    A fresh setter or event history allocates its initial segment on the first interaction. The warm averages above do not describe that first call. [ColdInvocationAllocationTests](https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/CallAllocations/ColdInvocationAllocationTests.cs) includes construction, optional setup, `Instance()` and the first interaction in each measured sample. Its x64 Release ceilings are 1,136 bytes for a property assignment, 1,352 with a callback, 1,000 for an event removal and 1,872 for a subscribe/remove pair. These are regression limits for the stated fixtures, not per-call costs for every imposter.

The global sequence can introduce contention across independent imposters. [InvocationContentionBenchmarks](https://github.com/themidnightgospel/Imposter/blob/master/benchmarks/Imposter.Benchmarks/InvocationContentionBenchmarks.cs) compares local and global sequence counters with identical record publication, and separately measures generated calls with 1, 4 and 8 workers sharing one imposter or using independent imposters. Each batch retains a bounded number of records. Run the benchmark on representative hardware when concurrent-call throughput matters; the generated-call comparison includes setup and lookup work in addition to sequence publication.

See [Invocation sequence contention](invocation-contention.md) for reproducible benchmarks and first-interaction allocation limits.

!!! info "Next steps"
    Read [Verification](verification.md) to check invocation counts, or [Methods](methods/index.md) to configure method behavior.
