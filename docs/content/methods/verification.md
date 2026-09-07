# Verification

Verify that methods were invoked the expected number of times with specific arguments. Use `Called(Count.*)` on the method builder.

Target type used in examples:

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/Docs/Methods/Verification/VerificationTests.cs#L5"}
    using Imposter.Abstractions;

    [assembly: GenerateImposter(typeof(Imposter.Tests.Docs.Methods.IVerifyService))]

    public interface IVerifyService
    {
        void Increment(int v);
        int Combine(int a, int b);
    }
    ```

## Basic counts

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/Docs/Methods/Verification/VerificationTests.cs#L26"}
    service.Increment(1);
    service.Increment(2);

    imposter.Increment(Arg<int>.Any()).Called(Count.AtLeast(2));
    imposter.Increment(2).Called(Count.Once());
    ```

## Matching arguments

Verification respects the same argument matching rules used for arrangements:

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/Docs/Methods/Verification/VerificationTests.cs#L41"}
    // See more examples in repo tests
    imposter.Increment(Arg<int>.Is(x => x > 10)).Called(Count.Exactly(3));
    imposter.Combine(Arg<int>.Is(x => x > 0), Arg<int>.Is(y => y < 10)).Called(Count.Once());
    ```

## Checking for additional calls

Use `imposter.Method(...).CallCount()` to read the number of recorded calls matching its arguments and generic type arguments. `CallCount()` is part of the existing method verification interface alongside `Called(Count.*)` and returns zero if no calls match.

Retain the method builder when configuring behavior and use it for subsequent count checks. Selecting a method on the imposter creates a setup, just as it does for `Called`; selecting it again can replace previously configured behavior. Calling `CallCount()` on the retained builder does not register another setup or clear history. Each count check scans the current recorded calls.

Save the count before the next action, then compare it with the new count:

!!! example
    ```csharp
    var increment = imposter.Increment(Arg<int>.Any());
    service.Increment(1);
    var previousCount = increment.CallCount();

    service.Increment(2);

    var additionalCalls = increment.CallCount() - previousCount;
    additionalCalls.ShouldBe(1);
    increment.Called(Count.Exactly(2));
    ```

For a method with a return value, retain the builder before configuring it: `var combine = imposter.Combine(1, 2); combine.Returns(3);`. You can then read `combine.CallCount()` between calls without changing its behavior.

`CallCount()` uses the same matching rules as `Called(Count.*)`. It counts calls to the selected method, not property, indexer, or event interactions. Await asynchronous work before checking its recorded calls, as you would with `Called`.

## Failures

When verification fails, `VerificationFailedException` is thrown. The message includes both the expected/actual counts and, when available, a textual list of performed invocations:

```
Invocation was expected to be performed {expectedCount} but instead was performed {actualCount} times.
Performed invocations:
{invocation1}
{invocation2}
...
```

Use this list to quickly see which calls were actually made and why the verification did not match your expectations.
