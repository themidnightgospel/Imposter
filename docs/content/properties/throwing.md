# Property Throwing

In Explicit mode or when desired, configure getters to throw.

```csharp
imposter.Age.Getter().Throws<InvalidOperationException>();

// Or use a factory
imposter.Age.Getter().Throws(() => new Exception("boom"));
```

Setter validations typically use `Called(Count.…)`; prefer throws on getter for read-time failures.

