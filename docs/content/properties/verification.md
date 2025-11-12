# Property Verification

Verify reads and writes using `Called` with `Count` helpers.

## Semantics

- Setter verification: `imposter.Property.Setter(matchers...).Called(Count)` counts only writes whose value matches the provided matcher (or any value when using `Any()`).
- Getter verification: `imposter.Property.Getter().Called(Count)` counts reads of the property.
- Count helpers behave the same as for methods: `Once = Exactly(1)`, `Exactly(n)`, `AtLeast(n)`, `AtMost(n)`, `Never()`, `Any`.

## Setter verification

```csharp
service.Age = 33;
service.Age = 34;

imposter.Age.Setter(Arg<int>.Any()).Called(Count.AtLeast(2));
imposter.Age.Setter(Arg<int>.Is(34)).Called(Count.Once());
```

## Getter verification

```csharp
var _ = service.Age;
imposter.Age.Getter().Called(Count.Once());
```

## Failures

When verification fails, `VerificationFailedException` is thrown with the message:

```
Invocation was expected to be performed {expectedCount} but instead was performed {actualCount} times.
```

Examples:

```csharp
// Setter expected once for value 34, but no matching write occurred
imposter.Age.Setter(Arg<int>.Is(34)).Called(Count.Once());
// throws: "Invocation was expected to be performed exactly 1 time(s) but instead was performed 0 times."

// Getter expected at least 2 reads, but only 1
var _ = service.Age;
imposter.Age.Getter().Called(Count.AtLeast(2));
// throws: "Invocation was expected to be performed at least 2 time(s) but instead was performed 1 times."
```

Tips
- Use `Arg<T>` matchers to target specific values.
- Pair verifications with callbacks when you need to capture payloads.
