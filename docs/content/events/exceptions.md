# Event Exceptions

Configure failure behavior and test exception paths.

```csharp
// Example: raising when no subscribers is a no-op unless your consumer expects otherwise.
imposter.SomethingHappened.Raise(this, EventArgs.Empty);

// For explicit exception paths, assert via your test framework after Raise or via custom callbacks.
```

See tests under `tests/Imposter.Tests/Features/EventImposter/ExceptionTests.cs`.
