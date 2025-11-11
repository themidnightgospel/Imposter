# Property Verification

Verify reads and writes using `Called` with `Count` helpers.

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

Tips
- Use `Arg<T>` matchers to target specific values.
- Pair verifications with callbacks when you need to capture payloads.

