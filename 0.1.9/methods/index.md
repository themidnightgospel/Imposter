## Creating an imposter

Define the target interface and enable generation:

Example

```
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

## Setup Return values

- Return a constant value:

Example

```
imposter.GetNumber().Returns(1);

imposter.Instance().GetNumber(); // returns 1
```

- Return via a delegate (captures inputs):

Example

```
imposter.Increment(Arg<int>.Any()).Returns(v => v + 2);

imposter.Instance().Increment(10); // returns 12;
```

- Sequence multiple outcomes with `Then()`:

Example

```
imposter
     .Increment(Arg<int>.Any())
     .Returns(v => v + 2)
     .Then()
     .Returns(v => v + 3)
     .Then()
     .Returns(v => v + 4);

 imposter.Instance().Increment(10); // returns 12
 imposter.Instance().Increment(10); // returns 13
 imposter.Instance().Increment(10); // returns 14

 // sequence is exhausetd, last outcome repeats
 imposter.Instance().Increment(10); // returns 14
```

Note

- Use `Then()` to set up sequence
- After a sequence is exhausted, the last outcome repeats

## Async Methods

With async methods, imposter provides some handy methods to simplify setup:

- Task and ValueTask methods:

Example

```
imposter.GetNumberAsync().ReturnsAsync(42);

await imposter.Instance().GetNumberAsync(); // returns 41
```

- Task-returning (no result):

Example

```
imposter.DoWorkAsync().Returns(Task.CompletedTask);
```

- Sequencing remains the same:

Example

```
imposter.GetNumberAsync()
    .ReturnsAsync(1)
    .Then().Returns(() => Task.FromResult(2));
```

Pro tip

Prefer `ReturnsAsync` for Task/ValueTask methods. It reads cleaner and avoids accidental sync-over-async in factories.

Warning

Async methods without setup return `default`, which in case of `Task` is `null`.

## Ref/Out/In Parameters

Use `OutArg<T>.Any()` to match `out` parameters; `Arg<T>` for `ref` and `in`.

Returns and callbacks can specify `out/ref/in` in the delegate signature:

Example

```
imposter.GenericAllRefKind<int, string, double, bool, int>(
        OutArg<int>.Any(),
        Arg<string>.Any(),
        Arg<double>.Any(),
        Arg<bool[]>.Any())
    .Returns((out int o, ref string r, in double d, bool[] args) => { o = 5; return 99; })
    .Callback((out int o, ref string r, in double d, bool[] args) => { o = 5; });
```

## Base Implementation (Class Targets)

Info

`UseBaseImplementation()` applies only to non-abstract, virtual class members. It is not available for interfaces. See the dedicated page: [Base Implementation](https://themidnightgospel.github.io/Imposter/0.1.9/base-implementation/index.md).

## Verification

Use `Called(Count.*)` on the method to assert invocation counts:

Example

```
// After calling service methods
imposter.Increment(Arg<int>.Any()).Called(Count.AtLeast(2));
imposter.Increment(2).Called(Count.Once());
```

See more examples on [GitHub](https://github.com/themidnightgospel/Imposter/tree/main/tests/Imposter.Tests/Features/MethodImpersonation). `Count` options:

- `Exactly(n)`, `AtLeast(n)`, `AtMost(n)`, `Once()`, `Never()`, `Any`

If verification fails, a `VerificationFailedException` is thrown with a clear message.

Pro tip

Place verification at the end of the test. Verify the broad call first (e.g., `Any()`), then the specific one to keep failure messages focused.

Next steps

- Deep dives for methods:
- [Sequential Returns](https://themidnightgospel.github.io/Imposter/0.1.9/methods/sequential-returns/index.md)
- [Throwing Exceptions](https://themidnightgospel.github.io/Imposter/0.1.9/methods/throwing/index.md)
- [Verification](https://themidnightgospel.github.io/Imposter/0.1.9/methods/verification/index.md)
- [Callbacks](https://themidnightgospel.github.io/Imposter/0.1.9/methods/callbacks/index.md)
- [Base Implementation](https://themidnightgospel.github.io/Imposter/0.1.9/base-implementation/index.md)
- [Protected Methods](https://themidnightgospel.github.io/Imposter/0.1.9/methods/protected-members/index.md)
- [Imposter Modes](https://themidnightgospel.github.io/Imposter/0.1.9/implicit-vs-explicit/index.md)

## Concurrency Notes

Sequenced outcomes are consumed in order under concurrency; the implementation avoids races that would otherwise reorder or drop planned outcomes. If a sequence is exhausted, the last outcome is repeated (when applicable) or default behavior applies.

## Troubleshooting

- Repeating `Returns` or `Throws` without `Then()` is invalid.
- In `Explicit` mode, missing setups throw `MissingImposterException`.
- Ensure `OutArg<T>.Any()` is used for `out` parameters; use `Arg<T>` for `ref`/`in`.
