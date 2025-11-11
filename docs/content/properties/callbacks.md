# Property Callbacks

Run side effects when a property is written or read.

## Setter callbacks

```csharp
imposter.Age.Setter(Arg<int>.Any()).Callback(v =>
{
    // observe or react to writes
});
```

## Getter callbacks

```csharp
imposter.Age.Getter().Callback(() =>
{
    // observe reads; combine with Returns/Then
}).Returns(10);
```

Tips
- Use `Arg<T>` to scope callbacks to specific values.
- Combine with `Called(Count.…)` for verification.

