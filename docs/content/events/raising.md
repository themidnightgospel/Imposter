# Raising

Raise events to invoke current subscribers. You can raise both `EventHandler` and `EventHandler<T>`-style events.

```csharp
// Classic EventHandler
imposter.SomethingHappened.Raise(this, EventArgs.Empty);

// Generic EventHandler<T>
imposter.DataReceived.Raise(this, new DataEventArgs(payload));
```

## Semantics

- Notifies handlers in the order they subscribed.
- If there are no subscribers, the call is a no-op.
- Exceptions thrown by a handler will propagate and stop further handlers unless caught upstream.
- Works regardless of ImposterMode (Implicit/Explicit) — setups are not required for events.

## Patterns

- Verify a specific handler was invoked a certain number of times:

```csharp
EventHandler h = (s, e) => { };
service.SomethingHappened += h;

imposter.SomethingHappened.Raise(this, EventArgs.Empty);
imposter.SomethingHappened.HandlerInvoked(Arg<EventHandler>.Is(h), Count.Once());
```

- Verify any handler was invoked (count total invocations):

```csharp
imposter.SomethingHappened.Raise(this, EventArgs.Empty);
imposter.SomethingHappened.HandlerInvoked(Arg<EventHandler>.Any(), Count.Exactly(1));
```

## Async

If handlers are async (returning `Task`), the event shape is still `void` at the CLR level. Prefer surfacing completion and failures through your API under test, and assert via your test framework. See `EventImposter/AsyncTests.cs` for guidance.
