# Indexer Verification

Use `Called` with `Count` to verify setter usage; combine getters with callbacks or count reads where applicable.

```csharp
service[1] = 10;
service[2] = 20;

imposter[Arg<int>.Any()].Setter().Called(Count.AtLeast(2));
imposter[Arg<int>.Is(2)].Setter().Called(Count.Once());
```

