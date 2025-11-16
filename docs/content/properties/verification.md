# Property Verification

## Setter verification

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/PropertyImposter/VerificationTests.cs#L39"}
    service.Age = 33;
    service.Age = 34;

    imposter.Age.Setter(Arg<int>.Any()).Called(Count.AtLeast(2));
    imposter.Age.Setter(Arg<int>.Is(34)).Called(Count.Once());
    ```

## Getter verification

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/PropertyImposter/VerificationTests.cs#L17"}
    var _ = service.Age;
    imposter.Age.Getter().Called(Count.Once());
    ```

## Failures

When verification fails, `VerificationFailedException` is thrown with the message:

```
Invocation was expected to be performed {expectedCount} but instead was performed {actualCount} times.
```

Examples:

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/PropertyImposter/VerificationTests.cs#L26"}
    // Setter expected once for value 34, but no matching write occurred
    imposter.Age.Setter(Arg<int>.Is(34)).Called(Count.Once());
    // throws: "Invocation was expected to be performed exactly 1 time(s) but instead was performed 0 times."

    // Getter expected at least 2 reads, but only 1
    var _ = service.Age;
    imposter.Age.Getter().Called(Count.AtLeast(2));
    // throws: "Invocation was expected to be performed at least 2 time(s) but instead was performed 1 times."
    ```

Tips
- Use `Arg<T>` matchers to target specific values.
- Pair verifications with callbacks when you need to capture payloads.
