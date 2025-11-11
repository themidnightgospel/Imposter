# Event Verification

Verify handler subscriptions and invocations.

```csharp
EventHandler h = (s, e) => { };
service.SomethingHappened += h;
imposter.SomethingHappened.Subscribed(Arg<EventHandler>.Is(h), Count.Once());

imposter.SomethingHappened.Raise(this, EventArgs.Empty);
imposter.SomethingHappened.HandlerInvoked(Arg<EventHandler>.Is(h), Count.Once());
```

