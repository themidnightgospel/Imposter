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
imposter.GetNumber().Returns(42);

imposter.Instance().GetNumber(); // returns 42
```

- Return via a delegate (captures inputs):

Example

```
imposter.Increment(Arg<int>.Any()).Returns(v => v + 2);

imposter.Instance().Increment(10); // returns 12;
```

- Return a `Span<T>` or `ReadOnlySpan<T>`: `Returns` takes the array the returned span covers. Every call returns a span over that same array, so writes to a returned `Span<T>` land in it. A delegate passed to `Returns` returns the span itself.

Example

```
// Span<byte> Rent(int size);
var buffer = new byte[4];
imposter.Rent(Arg<int>.Any()).Returns(buffer);

var span = imposter.Instance().Rent(4); // a span over buffer
span[0] = 42; // buffer[0] is now 42
```

- Return another `ref struct`: an imposter can't keep such a value, so `Returns` takes only a delegate, as in `Returns(size => new Slot(size))`, and the invocation history doesn't record the result. Without a setup the method returns `default`. A method's `ref struct` parameters are passed to the delegate the same way (see [Ref struct parameters](https://themidnightgospel.github.io/Imposter/0.2.11/arguments-matching/#ref-struct-parameters)).
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

 // sequence is exhausted, last outcome repeats
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

await imposter.Instance().GetNumberAsync(); // returns 42
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

Use `OutArg<T>.Any()` to match `out` parameters; `Arg<T>` for `ref`, `in` and `ref readonly`. Span parameters have matchers of their own; see [Span parameters](https://themidnightgospel.github.io/Imposter/0.2.11/arguments-matching/#span-parameters).

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

### Overloads that differ only in passing a parameter by reference

`Arg<T>` matches a parameter whether it's passed by value, `in`, `ref` or `ref readonly`, so overloads such as `int Count(int value)` and `int Count(in int value)` can't share a setup method. The overload declared first keeps the name and the others get a numbered one (`Count_1`), on the imposter and in its [setup views](https://themidnightgospel.github.io/Imposter/0.2.11/interface-setup/index.md):

Example

```
imposter.Count(Arg<int>.Any()).Returns(1); // int Count(int value)
imposter.Count_1(Arg<int>.Any()).Returns(2); // int Count(in int value)

var value = 5;
imposter.Instance().Count(value); // 1
imposter.Instance().Count(in value); // 2
```

## Base Implementation (Class Targets)

Info

`UseBaseImplementation()` applies only to non-abstract, virtual class members. It is not available for interfaces. See the dedicated page: [Base Implementation](https://themidnightgospel.github.io/Imposter/0.2.11/base-implementation/index.md).

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
- [Sequential Returns](https://themidnightgospel.github.io/Imposter/0.2.11/methods/sequential-returns/index.md)
- [Throwing Exceptions](https://themidnightgospel.github.io/Imposter/0.2.11/methods/throwing/index.md)
- [Verification](https://themidnightgospel.github.io/Imposter/0.2.11/methods/verification/index.md)
- [Callbacks](https://themidnightgospel.github.io/Imposter/0.2.11/methods/callbacks/index.md)
- [Base Implementation](https://themidnightgospel.github.io/Imposter/0.2.11/base-implementation/index.md)
- [Protected Methods](https://themidnightgospel.github.io/Imposter/0.2.11/methods/protected-members/index.md)
- [Imposter Modes](https://themidnightgospel.github.io/Imposter/0.2.11/implicit-vs-explicit/index.md)

## Concurrency Notes

Sequenced outcomes are consumed in order under concurrency; the implementation avoids races that would otherwise reorder or drop planned outcomes. If a sequence is exhausted, the last outcome is repeated (when applicable) or default behavior applies.

## Troubleshooting

- Repeating `Returns` or `Throws` without `Then()` is invalid.
- In `Explicit` mode, missing setups throw `MissingImposterException`.
- Ensure `OutArg<T>.Any()` is used for `out` parameters; use `Arg<T>` for `ref`/`in`/`ref readonly`.
