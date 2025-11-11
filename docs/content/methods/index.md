# Method Mocking

Deep dive into arranging and verifying method behavior with Imposter. This covers returns, async, exceptions, callbacks, sequencing, argument matching, ref/out/in parameters, base forwarding, verification, and concurrency notes.

## Overview

Generated imposters expose fluent method builders you can use to:
- Arrange outcomes: `Returns`, `ReturnsAsync`, `Throws`
- Add side effects: `Callback`
- Sequence behaviors: `Then`
- Verify invocations: `Called(Count.*)`

Invocation modes:
- `Implicit` (default): missing setups return default values and do nothing for void methods.
- `Explicit`: missing setups throw `MissingImposterException`.

## Quick Start

```csharp
var imposter = new IMyServiceImposter();
var service = imposter.Instance();

imposter.GetNumber().Returns(42);
service.GetNumber(); // 42

imposter.Increment(Arg<int>.Is(x => x > 0)).Returns(v => v + 2);
imposter.Increment(5).Returns(50);          // exact match (implicit Arg<int>)
imposter.Increment(Arg<int>.Any()).Returns(0);
```

## Arranging Returns

- Return a constant value:
  ```csharp
  imposter.GetNumber().Returns(1);
  ```
- Return via a delegate (captures inputs):
  ```csharp
  imposter.Increment(Arg<int>.Any()).Returns(v => v + 2);
  ```
- Sequence multiple outcomes with `Then()`:
  ```csharp
  imposter.GetNumber()
      .Returns(1)
      .Then().Returns(() => 2)
      .Then().Returns(3);
  ```

Chaining rules:
- Use `Then()` between distinct outcomes. Repeating `Returns` without `Then()` is invalid.
- You may mix `Returns` and `Throws` across sequence steps; separate with `Then()`.

## Async Methods

- Task<T> and ValueTask<T> methods:
  ```csharp
  imposter.GetNumberAsync().ReturnsAsync(42);
  imposter.GetNumberAsync().Returns(() => Task.FromResult(1));
  ```
- Task-returning (no result):
  ```csharp
  imposter.DoWorkAsync().Returns(Task.CompletedTask);
  ```
- Sequencing remains the same:
  ```csharp
  imposter.GetNumberAsync()
      .ReturnsAsync(1)
      .Then().Returns(() => Task.FromResult(2));
  ```

## Exceptions

Arrange exceptions instead of return values:
```csharp
imposter.GetNumber().Throws<InvalidOperationException>();
imposter.GetNumber().Throws(new Exception("boom"));
imposter.GetNumber().Throws(() => new Exception("deferred"));
```

Mix with returns using `Then()`:
```csharp
imposter.GetNumber()
    .Returns(1)
    .Then().Throws<InvalidOperationException>()
    .Then().Returns(2);
```

## Callbacks

Callbacks run after an outcome is produced (or after the default result for missing returns in Implicit mode):
```csharp
var stages = new List<string>();

imposter.GetNumber()
    .Returns(() => { stages.Add("return"); return 42; })
    .Callback(() => stages.Add("first"))
    .Callback(() => stages.Add("second"));
```

Parameterized callbacks capture inputs:
```csharp
imposter.Increment(Arg<int>.Any()).Callback(v => /* observe v */);
```

Sequence-local callbacks:
```csharp
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
```csharp
imposter.Combine(
    Arg<int>.Is(x => x > 0),
    Arg<int>.Is(y => y < 10))
  .Returns(42);
```

## Ref/Out/In Parameters

Use `OutArg<T>.Any()` to match `out` parameters; `Arg<T>` for `ref` and `in`.

Returns and callbacks can specify `out/ref/in` in the delegate signature:
```csharp
imposter.GenericAllRefKind<int, string, double, bool, int>(
        OutArg<int>.Any(),
        Arg<string>.Any(),
        Arg<double>.Any(),
        Arg<bool[]>.Any())
    .Returns((out int o, ref string r, in double d, bool[] args) => { o = 5; return 99; })
    .Callback((out int o, ref string r, in double d, bool[] args) => { o = 5; });
```

## Base Implementation (Class Targets)

Forward to the base (virtual/override) method implementation when available:
```csharp
imposter.VirtualCompute(Arg<int>.Any()).UseBaseImplementation();
```

You can include base forwarding as part of a sequence using `Then()` alongside `Returns`/`Throws`.
If base is unavailable and no setup applies, `MissingImposterException` is thrown in `Explicit` mode.

## Verification

Use `Called(Count.*)` on the method to assert invocation counts:
```csharp
// After calling service methods
imposter.Increment(Arg<int>.Any()).Called(Count.AtLeast(2));
imposter.Increment(2).Called(Count.Once());
```

`Count` options:
- `Exactly(n)`, `AtLeast(n)`, `AtMost(n)`, `Once()`, `Never()`, `Any`

If verification fails, a `VerificationFailedException` is thrown with a clear message.

## Concurrency Notes

Sequenced outcomes are consumed in order under concurrency; the implementation avoids races that would otherwise reorder or drop planned outcomes. If a sequence is exhausted, the last outcome is repeated (when applicable) or default behavior applies.

## Troubleshooting

- Repeating `Returns` or `Throws` without `Then()` is invalid.
- In `Explicit` mode, missing setups throw `MissingImposterException`.
- Ensure `OutArg<T>.Any()` is used for `out` parameters; use `Arg<T>` for `ref`/`in`.


