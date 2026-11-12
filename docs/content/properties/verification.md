# Property Verification

## Setter verification

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/PropertyImpersonation/VerificationTests.cs#L39"}
    service.Age = 33;
    service.Age = 34;

    imposter.Age.Setter(Arg<int>.Any()).Called(Count.AtLeast(2));
    imposter.Age.Setter(Arg<int>.Is(34)).Called(Count.Once());
    ```

## Getter verification

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/PropertyImpersonation/VerificationTests.cs#L17"}
    var _ = service.Age;
    imposter.Age.Getter().Called(Count.Once());
    ```

## Failures

When verification fails, `VerificationFailedException` is thrown with the message:

```
Expected {memberAndMatchers} {expectedCount}, performed {actualCount} times.
```

Examples:

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/PropertyImpersonation/VerificationTests.cs#L26"}
    // Setter expected once for value 34, but no matching write occurred
    imposter.Age.Setter(Arg<int>.Is(34)).Called(Count.Once());
    // Throws with the member/matcher expectation, actual count and recorded member history.

    // Getter expected at least 2 reads, but only 1
    var _ = service.Age;
    imposter.Age.Getter().Called(Count.AtLeast(2));
    // Throws with the member/matcher expectation, actual count and recorded member history.
    ```

Tips
- Use `Arg<T>` matchers to target specific values.
- Pair verifications with callbacks when you need to capture payloads.

Use `Called(count, because)` to append a reason; `because` accepts null. See [Failure messages](../failure-messages.md) for complete examples, missing-setup diagnostics and other-interaction listings.
