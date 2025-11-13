# Verification

Verify that methods were invoked the expected number of times with specific arguments. Use `Called(Count.*)` on the method builder.

Target type used in examples:

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/main/tests/Imposter.Tests/Docs/Methods/VerificationTests.cs#L5"}
    using Imposter.Abstractions;

    [assembly: GenerateImposter(typeof(Imposter.Tests.Docs.Methods.IVerifyService))]

    public interface IVerifyService
    {
        void Increment(int v);
        int Combine(int a, int b);
    }
    ```

## Semantics

- `Called(Count)` counts only invocations that match the builder’s argument matchers.
- `Count.Once()` is an alias for `Count.Exactly(1)`.
- `Count.Exactly(n)` requires the matching call count to equal `n`.
- `Count.AtLeast(n)` requires the count to be `>= n`.
- `Count.AtMost(n)` requires the count to be `<= n`.
- `Count.Never()` requires zero matching calls.
- `Count.Any` imposes no constraint and always succeeds.

## Basic counts

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/main/tests/Imposter.Tests/Docs/Methods/VerificationTests.cs#L26"}
    service.Increment(1);
    service.Increment(2);

    imposter.Increment(Arg<int>.Any()).Called(Count.AtLeast(2));
    imposter.Increment(2).Called(Count.Once());
    ```

## Count helpers

- `Count.Exactly(n)` — exactly n times
- `Count.AtLeast(n)` — n or more times
- `Count.AtMost(n)` — n or fewer times
- `Count.Once()` — exactly once
- `Count.Never()` — zero times
- `Count.Any` — any number of times

## Matching arguments

Verification respects the same argument matching rules used for arrangements:

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/main/tests/Imposter.Tests/Docs/Methods/VerificationTests.cs#L41"}
    // See more examples in repo tests
    imposter.Increment(Arg<int>.Is(x => x > 10)).Called(Count.Exactly(3));
    imposter.Combine(Arg<int>.Is(x => x > 0), Arg<int>.Is(y => y < 10)).Called(Count.Once());
    ```

## Failures

When verification fails, `VerificationFailedException` is thrown. Message format:

```
Invocation was expected to be performed {expectedCount} but instead was performed {actualCount} times.
```

Examples:

```csharp
// Expected at least 2, only 1 occurred
service.Increment(1);
imposter.Increment(Arg<int>.Any()).Called(Count.AtLeast(2));
// throws VerificationFailedException with:
// "Invocation was expected to be performed at least 2 time(s) but instead was performed 1 times."

// Expected exactly 3, got 2
service.Increment(10);
service.Increment(11);
imposter.Increment(Arg<int>.Is(x => x > 5)).Called(Count.Exactly(3));
// throws VerificationFailedException with:
// "Invocation was expected to be performed exactly 3 time(s) but instead was performed 2 times."
```

## Tips

- Prefer verifying behavior at the boundary of your SUT rather than implementation details.
- Verification can be chained after prior setups via `Then()`, but is typically run after exercising your SUT.


