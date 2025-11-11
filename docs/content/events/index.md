# Event Mocking

Verify subscriptions, raise events, and observe handlers with interceptors.

## Subscribe/Unsubscribe Verification

```csharp
EventHandler h = (s, e) => { };

service.SomethingHappened += h;
service.SomethingHappened -= h;

imposter.SomethingHappened.Subscribed(Arg<EventHandler>.Is(h), Count.Once());
imposter.SomethingHappened.Unsubscribed(Arg<EventHandler>.Is(h), Count.Once());
```

## Raising

```csharp
// Raise in-order for current subscribers
imposter.SomethingHappened.Raise(this, EventArgs.Empty);
```

## Interceptors and Invocation Counts

```csharp
// Observe subscriptions/unsubscriptions
imposter.SomethingHappened.OnSubscribe(handler => { /* inspect */ });
imposter.SomethingHappened.OnUnsubscribe(handler => { /* inspect */ });

// Verify handler invocation count
imposter.SomethingHappened.HandlerInvoked(Arg<EventHandler>.Is(h), Count.Exactly(2));
```

## Tips

- Async scenarios are supported (see tests under `tests/Imposter.Tests/Features/EventImposter/*`).
- Thread-safety and ordering are validated in the tests.
- In Explicit mode, missing setups may throw `MissingImposterException` when unsupported operations are invoked.

