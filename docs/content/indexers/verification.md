# Indexer Verification

## Setter verification

!!! example
    ```csharp
    service[1] = 10;
    service[2] = 20;

    imposter[Arg<int>.Any()].Setter().Called(Count.AtLeast(2));
    imposter[Arg<int>.Is(2)].Setter().Called(Count.Once());
    ```

## Getter verification

!!! example
    ```csharp
    var _ = service[1];
    imposter[Arg<int>.Any()].Getter().Called(Count.Once());
    ```

## Failures

When verification fails, `VerificationFailedException` is thrown with the message:

!!! example
    ```
    Invocation was expected to be performed {expectedCount} but instead was performed {actualCount} times.
    ```

Examples:

!!! example
    ```csharp
    // Setter expected once for index 2, but no matching write occurred
    imposter[Arg<int>.Is(2)].Setter().Called(Count.Once());
    // throws: "Invocation was expected to be performed exactly 1 time(s) but instead was performed 0 times."

    // Getter expected at least 2 reads, but only 1
    var _ = service[1];
    imposter[Arg<int>.Any()].Getter().Called(Count.AtLeast(2));
    // throws: "Invocation was expected to be performed at least 2 time(s) but instead was performed 1 times."
    ```
