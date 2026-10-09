# Indexer Impersonation

## Creating an imposter

Use the generated imposter type for your indexer-bearing interface or class and obtain the configured instance:

Example

```
[assembly: GenerateImposter(typeof(IMyServiceSut))]

public interface IMyServiceSut
{
    int this[int key1] { get; set; }
}
```

## Getter

Example

```
var imposter = IMyServiceSut.Imposter();
imposter[Arg<int>.Is(k => k > 0)].Getter().Returns(10);

var service = imposter.Instance();
var value = service[123]; // 10
```

## Setter

Example

```
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

- Match a span key with `SpanArg<T>` or `ReadOnlySpanArg<T>` (see [Span parameters](https://themidnightgospel.github.io/Imposter/0.2.7/arguments-matching/#span-parameters)). Delegates passed to `Returns` and `Callback` get a copy of the key's elements as an array.

Example

```
// int this[ReadOnlySpan<char> key] { get; set; }
imposter[ReadOnlySpanArg<char>.Is('a', 'b')].Getter().Returns(3);

var value = imposter.Instance()["ab".AsSpan()]; // 3
```

- Without a setup, keys that hold the same elements address the same value.

Example

```
imposter.Instance()["ab".AsSpan()] = 5;
var value = imposter.Instance()[new[] { 'a', 'b' }]; // 5
```

- A span value works as it does for [span properties](https://themidnightgospel.github.io/Imposter/0.2.7/properties/#span-properties): `Getter().Returns` takes the array the returned span covers, and the imposter keeps a copy of the elements a span value is set to.
