# Base Implementation (Events)

Forwarding to a base event implementation applies only when the target class exposes an overridable event pattern that supports forwarding.

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

See also: `tests/Imposter.Tests/Features/ClassImposter/AsyncEventsClassImposterTests.cs` and `tests/Imposter.Tests/Features/EventImposter/*`.

