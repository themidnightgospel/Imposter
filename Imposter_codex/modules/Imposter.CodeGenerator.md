## Method Fluent API Continuations

- Invocation group start interfaces now extend the callback interface so callbacks remain available after calling `Then()`.
- This enables scenarios such as `VirtualCompute(Arg<int>.Is(...)).Returns(123).Then().Callback((int value) => { })` and `VirtualAction().Callback(() => { }).Then().UseBaseImplementation()` to compile.
- Parameter callbacks can now consume method arguments in class imposters (e.g., `(int value) => { }`), aligning generator output with fluent API expectations.
