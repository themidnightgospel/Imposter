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
