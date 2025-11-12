# Indexer Mocking

Set up getter and setter behaviors for indexers, with argument matchers and base forwarding.

## Getter

```csharp
// Match by predicate
imposter[Arg<int>.Is(k => k > 0)].Getter().Returns(10);
var value = service[123]; // 10
```

## Setter

```csharp
// Observe writes
imposter[Arg<int>.Any()].Setter().Callback((index, v) => { /* side-effects */ });
```

!!! tip "Pro tip"
    Prefer precise key matchers (e.g., `Is(k => k == 123)`) before adding broad `Any()` setups. This helps avoid ambiguity between overlapping indexer setups.

## Base Implementation

```csharp
imposter[Arg<int>.Any()].Getter().UseBaseImplementation();
imposter[Arg<int>.Any()].Setter().UseBaseImplementation();
```

## Tips

- Use `Arg<T>` matchers for index keys, including `Any`, `Is`, and collections.
- See tests under `tests/Imposter.Tests/Features/IndexerImposter/*` for thread-safety and multi-parameter scenarios.
