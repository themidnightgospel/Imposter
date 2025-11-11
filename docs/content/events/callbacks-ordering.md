# Callbacks & Ordering

Attach callbacks and validate the execution order relative to returns.

```csharp
var stages = new List<string>();

imposter.GetNumber()
    .Returns(() => { stages.Add("return"); return 42; })
    .Callback(() => stages.Add("first"))
    .Callback(() => stages.Add("second"));

// stages: ["return", "first", "second"]
```

Use similar patterns on events to track subscription changes or handler invocations.

