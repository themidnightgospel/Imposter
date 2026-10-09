# Indexer Impersonation

## Creating an imposter

Use the generated imposter type for your indexer-bearing interface or class and obtain the configured instance:

!!! example
    ```csharp
    [assembly: GenerateImposter(typeof(IMyServiceSut))]

    public interface IMyServiceSut
    {
        int this[int key1] { get; set; }
    }
    ```

## Getter

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/IndexerImpersonation/GetterTests.cs#L103"}
    var imposter = IMyServiceSut.Imposter();
    imposter[Arg<int>.Is(k => k > 0)].Getter().Returns(10);

    var service = imposter.Instance();
    var value = service[123]; // 10
    ```

## Setter

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/IndexerImpersonation/SetterTests.cs#L147"}
    // Observe writes
    imposter[Arg<int>.Any()].Setter().Callback((key, value) => 
            { 
                // value is 50
            });

    var service = imposter.Instance();
    service[42] = 50;
    ```

## Span keys and values

An indexer whose keys or value are `Span<T>` or `ReadOnlySpan<T>` can be impersonated. The imposter keeps each span as the array of its elements:

- Match a span key with `SpanArg<T>` or `ReadOnlySpanArg<T>` (see [Span parameters](../arguments-matching.md#span-parameters)). Delegates passed to `Returns` and `Callback` get a copy of the key's elements as an array.

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/IndexerImpersonation/SpanIndexerTests.cs#L15"}
    // int this[ReadOnlySpan<char> key] { get; set; }
    imposter[ReadOnlySpanArg<char>.Is('a', 'b')].Getter().Returns(3);

    var value = imposter.Instance()["ab".AsSpan()]; // 3
    ```

- Without a setup, keys that hold the same elements address the same value.

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/IndexerImpersonation/SpanIndexerTests.cs#L39"}
    imposter.Instance()["ab".AsSpan()] = 5;
    var value = imposter.Instance()[new[] { 'a', 'b' }]; // 5
    ```

- A span value works as it does for [span properties](../properties/index.md#span-properties): `Getter().Returns` takes the array the returned span covers, and the imposter keeps a copy of the elements a span value is set to.
