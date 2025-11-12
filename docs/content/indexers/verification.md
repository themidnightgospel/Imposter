# Indexer Verification

Use `Called` with `Count` to verify setter usage; combine getters with callbacks or count reads where applicable.

## Semantics

- `imposter[...].Setter().Called(Count)` counts writes for indexers whose indices (and optional value matcher) satisfy the provided matchers.
- `imposter[...].Getter().Called(Count)` counts reads for indexers whose indices satisfy the matchers.
- Count helpers have identical meaning as for methods/properties.

```csharp
service[1] = 10;
service[2] = 20;

imposter[Arg<int>.Any()].Setter().Called(Count.AtLeast(2));
imposter[Arg<int>.Is(2)].Setter().Called(Count.Once());
```

## Failures

When verification fails, `VerificationFailedException` is thrown with the message:

```
Invocation was expected to be performed {expectedCount} but instead was performed {actualCount} times.
```

Example:

```csharp
// Expected a single write to index 2, but none occurred
imposter[Arg<int>.Is(2)].Setter().Called(Count.Once());
// throws: "Invocation was expected to be performed exactly 1 time(s) but instead was performed 0 times."
```
