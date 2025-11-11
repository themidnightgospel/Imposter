# Indexer Setter

Observe writes and verify counts.

```csharp
imposter[Arg<int>.Any()].Setter().Callback((index, value) =>
{
    // record writes
});

service[7] = 42;
imposter[Arg<int>.Is(7)].Setter().Called(Count.Once());
```

