# Raising

Raise events to invoke current subscribers.

```csharp
imposter.SomethingHappened.Raise(this, EventArgs.Empty);
```

Async scenarios are supported; see tests under `tests/Imposter.Tests/Features/EventImposter/AsyncTests.cs`.

