# Subscriptions

Verify that handlers are added or removed as expected.

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

