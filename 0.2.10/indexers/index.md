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

- Match a span key with `SpanArg<T>` or `ReadOnlySpanArg<T>` (see [Span parameters](https://themidnightgospel.github.io/Imposter/0.2.10/arguments-matching/#span-parameters)). Delegates passed to `Returns` and `Callback` get a copy of the key's elements as an array.

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

- A span value works as it does for [span properties](https://themidnightgospel.github.io/Imposter/0.2.10/properties/#span-properties): `Getter().Returns` takes the array the returned span covers, and the imposter keeps a copy of the elements a span value is set to.

## Ref struct values

An indexer whose value is another `ref struct` can be impersonated too. The imposter can't keep or match the value, so it only passes it between the indexer and your delegates. Keys work as usual:

- `Getter().Returns` takes only the delegate that gets the keys; there's no `Returns(value)` or `Returns(() => value)`. `Throws`, callbacks, `Then()` sequences and `UseBaseImplementation()` work as usual.

Example

```
// Marker this[int index] { get; set; }, where Marker is a ref struct
imposter[Arg<int>.Any()].Getter().Returns(index => new Marker(index + 1));

var position = imposter.Instance()[2].Position; // 3
```

- Setter callbacks get the keys and the value, and `Called` counts the sets by their keys.

Example

```
var sets = new List<(int, int)>();
imposter[Arg<int>.Any()]
    .Setter()
    .Callback((index, value) => sets.Add((index, value.Position)));

imposter.Instance()[3] = new Marker(4); // sets is { (3, 4) }
```

- Without a setup, the getter returns what the base getter returns, for a class indexer that has one, or the default otherwise. Setting the indexer doesn't change what it returns.

A class indexer with a key taken by `in` or `ref readonly` and a getter with a base implementation still reports [IMP009](https://themidnightgospel.github.io/Imposter/0.2.10/diagnostics/#imp009): its base getter reads the base indexer through a copy of the key, which the value could refer to.

## Ref struct keys

A key of another `ref struct` type can't be kept or matched either, so setups and verification match the other keys only, as a method's [ref struct parameters](https://themidnightgospel.github.io/Imposter/0.2.10/arguments-matching/#ref-struct-parameters) are. The delegates passed to `Returns`, `Callback` and `Throws`, and the base implementation, still get the key:

Example

```
// int this[int row, Locator locator] { get; set; }, where Locator is a ref struct
imposter[Arg<int>.Any()].Getter().Returns((row, locator) => row + locator.Offset);

var value = imposter.Instance()[2, new Locator(3)]; // 5
```

The default behaviour keeps a value set by the other keys, whatever the ref struct key.

An indexer whose keys are all ref structs has no keys left to match, and two indexers whose other keys are the same would share one setup indexer. Such an indexer is set up by a method named after it instead: `Indexer()`, or `Indexer_1()` and so on when the target has more than one indexer. Its interface's [setup view](https://themidnightgospel.github.io/Imposter/0.2.10/interface-setup/index.md) declares the same method.

Example

```
// string this[Locator locator] { get; set; }, the target's second indexer
imposter.Indexer_1().Getter().Returns(locator => locator.Offset.ToString());

var value = imposter.Instance()[new Locator(8)]; // "8"
```

A ref struct key taken by `in` still reports [IMP009](https://themidnightgospel.github.io/Imposter/0.2.10/diagnostics/#imp009).
