# Subscriptions

Track and verify that handlers are added or removed as expected. Use argument matchers if you need to assert a specific delegate identity.

```csharp
EventHandler h = (s, e) => { };
service.SomethingHappened += h;
service.SomethingHappened -= h;

imposter.SomethingHappened.Subscribed(Arg<EventHandler>.Is(h), Count.Once());
imposter.SomethingHappened.Unsubscribed(Arg<EventHandler>.Is(h), Count.Once());
```

Observe changes as they occur:

```csharp
imposter.SomethingHappened.OnSubscribe(handler => { /* inspect */ });
imposter.SomethingHappened.OnUnsubscribe(handler => { /* inspect */ });
```

## Semantics

- Subscriptions are tracked per handler instance. If you subscribe the same delegate twice, you must unsubscribe twice.
- `Subscribed`/`Unsubscribed` respect matchers. Use `Arg<EventHandler>.Any()` to count all subscribe/remove operations.
- Verification counts follow the same rules as methods; see Verification.

## Tips

- Keep handler references to make unsubscription clear and verifiable.
- For lambdas, assign them to a local variable so you can verify the same instance.
- In class targets, ensure event accessors are virtual to enable interception.
