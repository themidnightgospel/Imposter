# Behavior Modes (Methods)

Two Imposter modes determine what happens when a method without a setup is invoked.

## Implicit mode

Methods without a setups are implicitly stubbed and return `default(T)`.

```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/main/tests/Imposter.Tests/Docs/BehaviorModesCodeSnippetsTests.cs#L21"}
var imposter = new IMyServiceImposter(ImposterMode.Implicit);
var service = imposter.Instance();

// Method without a setup => default(int) == 0
int n = service.GetNumber(); // 0

// Add a setup and the call returns your value
imposter.GetNumber().Returns(42);
service.GetNumber(); // 42

// Async methods
imposter.GetNumberAsync().ReturnsAsync(7);
var v = await service.GetNumberAsync(); // 7
```

## Explicit mode

Missing setups throw an exception so unintended calls are caught.

```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/main/tests/Imposter.Tests/Docs/BehaviorModesCodeSnippetsTests.cs#L41"}
var imposter = new IMyServiceImposter(ImposterMode.Explicit);
var service = imposter.Instance();

// No setup -> throws MissingImposterException
Assert.Throws<MissingImposterException>(() => service.GetNumber());

// Add a setup -> call succeeds
imposter.GetNumber().Returns(42);
service.GetNumber(); // 42
```

View more examples on [GitHub](https://github.com/themidnightgospel/Imposter/blob/main/tests/Imposter.Tests/Features/MethodImposter/ExplicitModeTests.cs).
