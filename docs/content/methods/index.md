Generated imposters expose fluent method builders you can use to:

- Arrange outcomes: `Returns`, `ReturnsAsync`, `Throws`
- Add side effects: `Callback`
- Sequence behaviors: `Then`
- Verify invocations: `Called(Count.*)`

Invocation modes:

- `Implicit` (default): missing setups return default values and do nothing for void methods.
- `Explicit`: missing setups throw `MissingImposterException`.

!!! tip "Pro tip"
    Use `Explicit` mode for strict unit tests to fail fast on unintended calls. Keep `Implicit` for exploratory tests where defaults are acceptable.

!!! info "Imposter Modes"
    See a deeper dive with examples in [Imposter Modes](explicit-vs-implicit.md).

## Quick Start

Define the target interface and enable generation:

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/Methods/OverviewTests.cs#L7"}
    using Imposter.Abstractions;

    [assembly: GenerateImposter(typeof(Imposter.Tests.Docs.Methods.IQuickStartService))]

    public interface IQuickStartService
    {
        int GetNumber();
        int Increment(int v);
        System.Threading.Tasks.Task<int> GetNumberAsync();
        System.Threading.Tasks.Task DoWorkAsync();
        int Combine(int a, int b);
        int VirtualCompute(int v);
    }
    ```

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/Methods/OverviewTests.cs#L26"}
    var imposter = new IQuickStartServiceImposter();
    var service = imposter.Instance();

    imposter.GetNumber().Returns(42);
    service.GetNumber(); // 42

    imposter.Increment(Arg<int>.Any()).Returns(0);
    imposter.Increment(Arg<int>.Is(x => x > 0)).Returns(v => v + 2);
    imposter.Increment(5).Returns(50);          // exact match (implicit Arg<int>)
    ```

## Arranging Returns

 - Return a constant value:
  
!!! example
       ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/Methods/OverviewTests.cs#L43"}
       imposter.GetNumber().Returns(1);
       ```
 - Return via a delegate (captures inputs):
  
!!! example
       ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/Methods/OverviewTests.cs#L34"}
       imposter.Increment(Arg<int>.Any()).Returns(v => v + 2);
       ```
 - Sequence multiple outcomes with `Then()`:
  
!!! example
       ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/Methods/OverviewTests.cs#L42"}
       imposter.GetNumber()
           .Returns(1)
           .Then().Returns(() => 2)
           .Then().Returns(3);
       ```

!!! note
    - Use `Then()` to set up sequence.
    - After a sequence is exhausted, the last outcome repeats (when applicable).

## Async Methods

 - Task<T> and ValueTask<T> methods:
!!! example
       ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/Methods/OverviewTests.cs#L51"}
       imposter.GetNumberAsync().ReturnsAsync(42);
       imposter.GetNumberAsync().Returns(() => Task.FromResult(1));
       ```
 - Task-returning (no result):
!!! example
       ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/Methods/OverviewTests.cs#L54"}
       imposter.DoWorkAsync().Returns(Task.CompletedTask);
       ```
 - Sequencing remains the same:
!!! example
       ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/Methods/OverviewTests.cs#L56"}
       imposter.GetNumberAsync()
           .ReturnsAsync(1)
           .Then().Returns(() => Task.FromResult(2));
       ```

!!! tip "Pro tip"
    Prefer `ReturnsAsync` for Task/ValueTask methods. It reads cleaner and avoids accidental sync-over-async in factories.

## Exceptions

Arrange exceptions instead of return values:
!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/Methods/OverviewTests.cs#L64"}
    imposter.GetNumber().Throws<InvalidOperationException>();
    imposter.GetNumber().Throws(new Exception("boom"));
    imposter.GetNumber().Throws(() => new Exception("deferred"));
    ```

Mix with returns using `Then()`:
!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/Methods/OverviewTests.cs#L75"}
    imposter.GetNumber()
        .Returns(1)
        .Then().Throws<InvalidOperationException>()
        .Then().Returns(2);
    ```

## Callbacks

Callbacks run after an outcome is produced (or after the default result for missing returns in Implicit mode):
!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/Methods/OverviewTests.cs#L84"}
    var stages = new List<string>();

    imposter.GetNumber()
        .Returns(() => { stages.Add("return"); return 42; })
        .Callback(() => stages.Add("first"))
        .Callback(() => stages.Add("second"));
    ```

Parameterized callbacks capture inputs:
!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/Methods/OverviewTests.cs#L91"}
    imposter.Increment(Arg<int>.Any()).Callback(v => /* observe v */);
    ```

Sequence-local callbacks:
!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/Methods/OverviewTests.cs#L93"}
    imposter.Increment(Arg<int>.Any())
        .Returns(_ => 10)
        .Callback(_ => Log("first"))
        .Then()
        .Returns(_ => 20)
        .Callback(_ => Log("second"));
    ```

## Argument Matching

Use precise values or matchers per parameter:
- Exact value (implicit `Arg<T>`): `imposter.Increment(5)`
- Any value: `Arg<T>.Any()`
- Predicate: `Arg<T>.Is(x => x > 10)` / negation: `Arg<T>.IsNot(...)`
- Membership: `Arg<T>.IsIn(collection)` / `IsNotIn(collection)`
- Default: `Arg<T>.IsDefault()`

Combine for multi-parameter methods:
!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/Methods/OverviewTests.cs#L103"}
    imposter.Combine(
        Arg<int>.Is(x => x > 0),
        Arg<int>.Is(y => y < 10))
      .Returns(42);
    ```

!!! tip "Pro tip"
    Start with precise matchers (e.g., `Is(...)`) before adding broad `Any()` fallbacks. This reduces ambiguity between overlapping setups.

## Ref/Out/In Parameters

Use `OutArg<T>.Any()` to match `out` parameters; `Arg<T>` for `ref` and `in`.

Returns and callbacks can specify `out/ref/in` in the delegate signature:
!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/Methods/CallbacksTests.cs#L56"}
    imposter.GenericAllRefKind<int, string, double, bool, int>(
            OutArg<int>.Any(),
            Arg<string>.Any(),
            Arg<double>.Any(),
            Arg<bool[]>.Any())
        .Returns((out int o, ref string r, in double d, bool[] args) => { o = 5; return 99; })
        .Callback((out int o, ref string r, in double d, bool[] args) => { o = 5; });
    ```

## Base Implementation (Class Targets)

!!! info
    `UseBaseImplementation()` applies only to non-abstract, virtual class members. It is not available for interfaces. See the dedicated page: [Base Implementation](base-implementation.md).

## Verification

Use `Called(Count.*)` on the method to assert invocation counts:
!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/Methods/VerificationTests.cs#L26"}
    // After calling service methods
    imposter.Increment(Arg<int>.Any()).Called(Count.AtLeast(2));
    imposter.Increment(2).Called(Count.Once());
    ```
See more examples on [GitHub](https://github.com/themidnightgospel/Imposter/tree/main/tests/Imposter.Tests/Features/MethodImposter).
`Count` options:
- `Exactly(n)`, `AtLeast(n)`, `AtMost(n)`, `Once()`, `Never()`, `Any`

If verification fails, a `VerificationFailedException` is thrown with a clear message.

!!! tip "Pro tip"
    Place verification at the end of the test. Verify the broad call first (e.g., `Any()`), then the specific one to keep failure messages focused.

!!! info "Next steps"
    - Deep dives for methods:
      - [Sequential Returns](sequential-returns.md)
      - [Throwing Exceptions](throwing.md)
      - [Verification](verification.md)
      - [Callbacks](callbacks.md)
      - [Base Implementation](base-implementation.md)
      - [Protected Methods](protected-members.md)
      - [Imposter Modes](explicit-vs-implicit.md)

## Concurrency Notes

Sequenced outcomes are consumed in order under concurrency; the implementation avoids races that would otherwise reorder or drop planned outcomes. If a sequence is exhausted, the last outcome is repeated (when applicable) or default behavior applies.

## Troubleshooting

- Repeating `Returns` or `Throws` without `Then()` is invalid.
- In `Explicit` mode, missing setups throw `MissingImposterException`.
- Ensure `OutArg<T>.Any()` is used for `out` parameters; use `Arg<T>` for `ref`/`in`.
