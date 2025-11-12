# Imposter Modes (Events)

How event subscription and raising behave when you didn't set anything up.

## Implicit mode

Subscribing and unsubscribing just works; you don’t need setups for handlers.

```csharp
var imposter = new IMyServiceImposter(ImposterMode.Implicit);
var service = imposter.Instance();

EventHandler h = (s, e) => { };
service.SomethingHappened += h;
service.SomethingHappened -= h;

// Verify subscribe/unsubscribe
imposter.SomethingHappened.Subscribed(Arg<EventHandler>.Is(h), Count.Once());
imposter.SomethingHappened.Unsubscribed(Arg<EventHandler>.Is(h), Count.Once());

// Raising with no subscribers is a no-op
imposter.SomethingHappened.Raise(this, EventArgs.Empty);
```

## Explicit mode

Subscriptions, unsubscriptions, and raising behave the same — no setup is required for the event pipeline. Explicit mode primarily affects other members (methods/properties) when missing setups are encountered.

```csharp
var imposter = new IMyServiceImposter(ImposterMode.Explicit);
var service = imposter.Instance();

EventHandler h = (s, e) => { };
service.SomethingHappened += h;
imposter.SomethingHappened.Raise(this, EventArgs.Empty);
imposter.SomethingHappened.HandlerInvoked(Arg<EventHandler>.Is(h), Count.Once());
```

## Interceptors

Observe subscriptions and unsubscriptions in both modes:

```csharp
imposter.SomethingHappened.OnSubscribe(handler => { /* inspect */ });
imposter.SomethingHappened.OnUnsubscribe(handler => { /* inspect */ });
```

### Tips

- Choose Implicit for most tests; use Explicit to make unintended calls stand out in strict scenarios.
- Event verification (`Subscribed`, `Unsubscribed`, `HandlerInvoked`) works identically in both modes.

See also: `tests/Imposter.Tests/Features/EventImposter/*`.
