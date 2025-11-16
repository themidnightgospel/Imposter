# Indexer Throwing

In Explicit mode or when desired, configure indexer getters to throw. For setters, prefer verification via `Called(Count...)` and callbacks, optionally throwing from callbacks when validation fails.

## Getter throwing

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/IndexerImposter/GetterTests.cs#L341"}
    imposter[Arg<int>.Any(), Arg<string>.Any(), Arg<object>.Any()]
        .Getter()
        .Throws<InvalidOperationException>();

    var service = imposter.Instance();

    // Read throws the configured exception
    Should.Throw<InvalidOperationException>(() => _ = service[3, "err", new object()]);
    ```

## Setter validation with throwing callbacks

!!! example
    ```csharp
    imposter[Arg<int>.Any()].Setter().Callback((key, value)
            => throw new InvalidOperationException("Callback error"));
    Should.Throw<InvalidOperationException>(() => service[42] = 10);
    ```

