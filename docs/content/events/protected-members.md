# Protected Events

Configure protected virtual events on class targets and interact through public wrappers.

## Subscribe/Raise via wrapper

```csharp
[assembly: GenerateImposter(typeof(MyService))]

public class MyService
{
    protected virtual event EventHandler? ProtectedChanged;

    public virtual void Subscribe(EventHandler h) => ProtectedChanged += h;
    public virtual void Unsubscribe(EventHandler h) => ProtectedChanged -= h;
}

var imp = new MyServiceImposter();
var svc = imp.Instance();

// Forward wrapper methods to base accessors
imp.Subscribe(Arg<EventHandler>.Any()).UseBaseImplementation();
imp.Unsubscribe(Arg<EventHandler>.Any()).UseBaseImplementation();

// Subscribe through the wrapper, then raise protected event
var called = false;
svc.Subscribe((s, e) => called = true);
imp.ProtectedChanged.Raise(svc, EventArgs.Empty);
// called == true
```

Notes
- Class targets only; event accessors must be overridable.
- Wrapper methods must be `virtual` to be configurable on the imposter.
- Verify via `Subscribed`, `Unsubscribed`, and `HandlerInvoked` on the event builder.
- Consider exposing a protected virtual `OnXxx(EventArgs e)` method in your design. Your SUT can call it directly; tests can verify `HandlerInvoked` without relying on wrapper indirection.
