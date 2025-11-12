# Event Mocking

Imposter supports interface and class events with first-class APIs to subscribe/unsubscribe, raise, observe, and verify handler activity. Use this section to configure typical scenarios and understand edge cases like ordering and failures.

## Subscribe/Unsubscribe Verification

```csharp
EventHandler h = (s, e) => { };

service.SomethingHappened += h;
service.SomethingHappened -= h;

imposter.SomethingHappened.Subscribed(Arg<EventHandler>.Is(h), Count.Once());
imposter.SomethingHappened.Unsubscribed(Arg<EventHandler>.Is(h), Count.Once());
```

### Tips

- Prefer verifying subscription intent at the boundaries of your SUT (e.g., UI wire-up) rather than internals.
- Use `Arg<EventHandler>.Any()` when you want to assert subscription activity without caring about the exact delegate.
- For classes, events must be virtual/overridable to be intercepted; see Protected Events and Base Implementation.

## Raising

```csharp
// Raise in-order for current subscribers
imposter.SomethingHappened.Raise(this, EventArgs.Empty);
```

### Semantics

- `Raise(sender, args)` notifies the handlers currently subscribed at the time of the call, in subscription order.
- If no one is subscribed, `Raise` is a no-op.
- Exceptions thrown by a handler bubble up and stop further handlers unless your SUT or test catches them (see Event Exceptions).

!!! tip "Pro tip"
    Ensure the SUT subscribes before raising. Use `Subscribed(...)` to make ordering expectations explicit when it matters.

## Interceptors and Invocation Counts

```csharp
// Observe subscriptions/unsubscriptions
imposter.SomethingHappened.OnSubscribe(handler => { /* inspect */ });
imposter.SomethingHappened.OnUnsubscribe(handler => { /* inspect */ });

// Verify handler invocation count
imposter.SomethingHappened.HandlerInvoked(Arg<EventHandler>.Is(h), Count.Exactly(2));
```

### Additional Tips

- Async scenarios are supported (see `EventImposter/AsyncTests.cs`). If your handlers are async `void` or return `Task`, prefer surfacing errors explicitly in tests.
- Thread-safety and ordering are validated in tests; avoid relying on handler side-effects for ordering-sensitive logic in production code.
- In Explicit mode, event subscription/raising does not require setups; verification still works the same.
