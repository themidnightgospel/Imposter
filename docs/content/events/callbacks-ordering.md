# Callbacks & Ordering

Attach callbacks and validate execution order. Method callbacks run after the configured `Returns`, allowing you to observe post-return side-effects.

```csharp
var stages = new List<string>();

imposter.GetNumber()
    .Returns(() => { stages.Add("return"); return 42; })
    .Callback(() => stages.Add("first"))
    .Callback(() => stages.Add("second"));

// stages: ["return", "first", "second"]
```

## Events

Use similar patterns on events to track subscription changes or handler invocations. Interceptor hooks fire when the action occurs:

```csharp
var subs = new List<Delegate>();
imposter.SomethingHappened.OnSubscribe(h => subs.Add(h));
imposter.SomethingHappened.OnUnsubscribe(h => subs.Remove(h));

service.SomethingHappened += (s,e) => {};
service.SomethingHappened -= (s,e) => {};

// subs reflects current handler list
```

Raising calls handlers in subscription order.
