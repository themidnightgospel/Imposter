## Method Fluent API Continuations

- Invocation group start interfaces now extend the callback interface so callbacks remain available after calling `Then()`.
- This enables scenarios such as `VirtualCompute(Arg<int>.Is(...)).Returns(123).Then().Callback((int value) => { })` and `VirtualAction().Callback(() => { }).Then().UseBaseImplementation()` to compile.
- Parameter callbacks can now consume method arguments in class imposters (e.g., `(int value) => { }`), aligning generator output with fluent API expectations.

## Event Fluent API Guardrails

- Event imposter entry interfaces (`I<EventName>EventImposterBuilder`) now inherit both setup and verification lane interfaces so callers see the full surface initially, but the first method call constrains the chain to either setup or verification.
- Setup lane interfaces expose `Callback`, `Raise`/`RaiseAsync`, `OnSubscribe`, and `OnUnsubscribe`, each returning the setup builder (async raise returns `Task<I<EventName>EventImposterSetupBuilder>`), ensuring repeated setup-only chaining.
- Verification lane interfaces expose `Subscribed`, `Unsubscribed`, `Raised`, and `HandlerInvoked`, all returning the verification builder to keep verification chains pure.
- The pattern applies equally to synchronous `EventHandler` events and async `Func<object, EventArgs, Task>`/`ValueTask` style events, preserving existing behaviors while preventing setup/verify mixing mid-chain.

## Imposter Target Guardrails

### Valid imposter targets

`[GenerateImposter]` can target interfaces or extensible (non-sealed) classes only. Targeting sealed classes, structs, records, or other symbol kinds will produce `IMP002`. For classes, ensure the type remains inheritable so the generated imposter can derive from it.

### Accessible constructors

Class imposters must invoke a constructor defined on the target type. At least one constructor must be `public`, `internal`, or `protected` so the generated type can access it. A minimal compliant definition looks like:

```csharp
public class ServiceDependency
{
    protected ServiceDependency()
    {
    }
}
```

This pattern allows the generated imposter (which lives in the same assembly) to chain into the protected constructor without reflection.

## C# language version support

Imposter requires the consuming project to compile with C# 8.0 or newer because the generator outputs nullable-aware code and relies on default interface methods. Set `<LangVersion>` to `latest` (or any value >= `8.0`) in the project file:

```xml
<PropertyGroup>
  <LangVersion>latest</LangVersion>
</PropertyGroup>
```

When the compilation language is not C#, or the `LangVersion` falls below 8.0, the generator reports `IMP001`/`IMP003` with the current and required versions.
