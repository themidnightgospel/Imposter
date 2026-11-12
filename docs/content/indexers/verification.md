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
    Expected {memberAndMatchers} {expectedCount}, performed {actualCount} times.
    ```

Examples:

!!! example
    ```csharp
    // Setter expected once for index 2, but no matching write occurred
    imposter[Arg<int>.Is(2)].Setter().Called(Count.Once());
    // Throws with the member/matcher expectation, actual count and recorded member history.

    // Getter expected at least 2 reads, but only 1
    var _ = service[1];
    imposter[Arg<int>.Any()].Getter().Called(Count.AtLeast(2));
    // Throws with the member/matcher expectation, actual count and recorded member history.
    ```

Use `Called(count, because)` to append a reason; `because` accepts null. See [Failure messages](../failure-messages.md) for complete examples, missing-setup diagnostics and other-interaction listings.
