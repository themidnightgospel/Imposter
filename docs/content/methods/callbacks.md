# Method Callbacks

Callbacks run after the arranged result (or after the default result in Implicit mode when no `Returns` is provided).

Target type used in examples:

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/Methods/CallbacksTests.cs#L7"}
    using Imposter.Abstractions;

    [assembly: GenerateImposter(typeof(Imposter.Tests.Docs.Methods.ICallbackService))]

    public interface ICallbackService
    {
        int GetNumber();
        int Increment(int v);
        int GenericAllRefKind(out int o, ref string r, in double d, bool[] args);
    }
    ```

## Ordering relative to results

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/Methods/CallbacksTests.cs#L29"}
    var stages = new List<string>();

    imposter.GetNumber()
        .Returns(() => { stages.Add("return"); return 42; })
        .Callback(() => stages.Add("first"))
        .Callback(() => stages.Add("second"));

    service.GetNumber();
    // stages == ["return", "first", "second"]
    ```

## Parameterized callbacks

Capture input arguments in a callback:

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/Methods/CallbacksTests.cs#L44"}
    imposter.Increment(Arg<int>.Any()).Callback(v => Logger.Log($"value: {v}"));
    ```

## Ref/out/in with callbacks

Callbacks can work with `out`, `ref`, and `in` parameters. Use `OutArg<T>.Any()` to match `out` parameters in the setup signature.

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/Methods/CallbacksTests.cs#L56"}
    imposter.GenericAllRefKind<int, string, double, bool, int>(
            OutArg<int>.Any(),
            Arg<string>.Any(),
            Arg<double>.Any(),
            Arg<bool[]>.Any())
        .Returns((out int o, ref string r, in double d, bool[] args) => { o = 5; return 99; })
        .Callback((out int o, ref string r, in double d, bool[] args) => { o = 5; /* side-effects */ });
    ```

## Sequence-local callbacks

Callbacks are scoped to the sequence step they’re defined on. Use `Then()` to create a new step with independent callbacks.

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/Methods/CallbacksTests.cs#L72"}
    var seen = new List<string>();

    imposter.Increment(Arg<int>.Any())
        .Returns(_ => 10)
        .Callback(_ => seen.Add("first"))
        .Then()
        .Returns(_ => 20)
        .Callback(_ => seen.Add("second"));

    service.Increment(1); // adds "first"
    service.Increment(2); // adds "second"
    ```

## Callbacks that throw

If a callback throws, the exception is propagated to the caller after the result has been produced.

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/Methods/CallbacksTests.cs#L99"}
    // See more examples in repo tests
    imposter.GetNumber()
        .Returns(1)
        .Callback(() => throw new InvalidOperationException("boom"));

    // service.GetNumber() returns 1, then throws InvalidOperationException
    ```

## Tips

- Prefer small, deterministic callbacks; reserve complex state changes for your SUT or helpers.
- When mixing `Returns` and `Callback`, callbacks always run last for that sequence step.


