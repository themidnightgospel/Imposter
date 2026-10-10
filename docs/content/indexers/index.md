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

## Ref struct values

An indexer whose value is another `ref struct` can be impersonated too. The imposter can't keep or match the value, so it only passes it between the indexer and your delegates. Keys work as usual:

- `Getter().Returns` takes only the delegate that gets the keys; there's no `Returns(value)` or `Returns(() => value)`. `Throws`, callbacks, `Then()` sequences and `UseBaseImplementation()` work as usual.

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/IndexerImpersonation/RefStructIndexerTests.cs#L16"}
    // Marker this[int index] { get; set; }, where Marker is a ref struct
    imposter[Arg<int>.Any()].Getter().Returns(index => new Marker(index + 1));

    var position = imposter.Instance()[2].Position; // 3
    ```

- Setter callbacks get the keys and the value, and `Called` counts the sets by their keys.

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/IndexerImpersonation/RefStructIndexerTests.cs#L79"}
    var sets = new List<(int, int)>();
    imposter[Arg<int>.Any()]
        .Setter()
        .Callback((index, value) => sets.Add((index, value.Position)));

    imposter.Instance()[3] = new Marker(4); // sets is { (3, 4) }
    ```

- Without a setup, the getter returns what the base getter returns, for a class indexer that has one, or the default otherwise. Setting the indexer doesn't change what it returns.

A class indexer with a key taken by `in` or `ref readonly` and a getter with a base implementation still reports [IMP009](../diagnostics.md#imp009): its base getter reads the base indexer through a copy of the key, which the value could refer to.
