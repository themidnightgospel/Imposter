# Indexer Getter

Set up indexer reads using `Arg<T>` matchers.

```csharp
imposter[Arg<int>.Is(k => k > 0)].Getter().Returns(10);
var x = service[123]; // 10
```

Multi-parameter indexers are supported; match each argument:

```csharp
imposter[Arg<string>.Any(), Arg<int>.Is(i => i > 10)].Getter().Returns(5);
```

