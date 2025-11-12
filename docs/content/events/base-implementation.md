# Base Implementation (Events)

Forwarding to a base event implementation applies only when the target class exposes an overridable event pattern that supports forwarding. Use this when your SUT relies on base accessors or when you want to exercise the real add/remove behavior.

```csharp
// When supported on the target member
imposter.SomethingHappened.UseBaseImplementation();

// Subscriptions and raising flow through the class’s event accessors
EventHandler h = (s, e) => { };
service.SomethingHappened += h;  // forwards to class add accessor
imposter.SomethingHappened.Raise(this, EventArgs.Empty); // invokes handlers maintained by base
```

Notes
- Class targets only; interfaces do not have base event implementations.
- Availability depends on whether the event accessors are overridable.
- Works in both modes; verification (`Subscribed`, `Unsubscribed`, `HandlerInvoked`) still applies.
- When enabled, subscribe/unsubscribe and raises flow through the target's accessors, preserving any custom logic in the base class (e.g., validation, deduplication, logging).

See also: `tests/Imposter.Tests/Features/ClassImposter/AsyncEventsClassImposterTests.cs` and `tests/Imposter.Tests/Features/EventImposter/*`.
