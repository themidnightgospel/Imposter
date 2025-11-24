# Property Throwing

In Explicit mode or when desired, configure getters to throw. For setters, prefer verification via `Called(Count...)` and callbacks.

## Getter throwing

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/PropertyImpersonation/ThrowsTests.cs#L27"}
    imposter.Age.Getter().Throws<InvalidOperationException>();

    var service = imposter.Instance();

    // Read throws the configured exception
    Should.Throw<InvalidOperationException>(() => service.Age);
    ```

## Setter validation with throwing callbacks

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/PropertyImpersonation/ThrowsTests.cs#L65"}
    imposter.Age.Setter(Arg<int>.Any())
        .Callback(_ => throw new InvalidOperationException("Callback error"));

    var service = imposter.Instance();

    // Setter callback throws, but the attempted value is still tracked
    Should.Throw<InvalidOperationException>(() => service.Age = 42);

    imposter.Age.Setter(Arg<int>.Is(v => v == 42)).Called(Count.Exactly(1));
    ```
