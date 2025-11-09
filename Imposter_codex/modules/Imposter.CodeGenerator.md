## Method Fluent API Continuations

- Invocation group start interfaces now extend the callback interface so callbacks remain available after calling `Then()`.
- This enables scenarios such as `VirtualCompute(Arg<int>.Is(...)).Returns(123).Then().Callback((int value) => { })` and `VirtualAction().Callback(() => { }).Then().UseBaseImplementation()` to compile.
- Parameter callbacks can now consume method arguments in class imposters (e.g., `(int value) => { }`), aligning generator output with fluent API expectations.

## Event Fluent API Guardrails

- Event imposter entry interfaces (`I<EventName>EventImposterBuilder`) now inherit both setup and verification lane interfaces so callers see the full surface initially, but the first method call constrains the chain to either setup or verification.
- Setup lane interfaces expose `Callback`, `Raise`/`RaiseAsync`, `OnSubscribe`, and `OnUnsubscribe`, each returning the setup builder (async raise returns `Task<I<EventName>EventImposterSetupBuilder>`), ensuring repeated setup-only chaining.
- Verification lane interfaces expose `Subscribed`, `Unsubscribed`, `Raised`, and `HandlerInvoked`, all returning the verification builder to keep verification chains pure.
- The pattern applies equally to synchronous `EventHandler` events and async `Func<object, EventArgs, Task>`/`ValueTask` style events, preserving existing behaviors while preventing setup/verify mixing mid-chain.
