# Behavior Modes: Loose vs Strict (Implicit vs Explicit)

How Imposter behaves when you call something you didn’t set up.

## Loose mode (Implicit)

Missing setups return type defaults like 0, null, or default structs.

```csharp
var imposter = new IMyServiceImposter(ImposterInvocationBehavior.Implicit);
var service = imposter.Instance();

// Method without a setup => default(int) == 0
int n = service.GetNumber(); // 0

// Add a setup and the call returns your value
imposter.GetNumber().Returns(42);
service.GetNumber(); // 42

// Property without a setup => default(int) == 0
int age = service.Age; // 0
imposter.Age.Getter().Returns(33);
age = service.Age; // 33

// Raising an event without subscribers is a no-op
imposter.SomethingHappened.Raise(this, EventArgs.Empty); // no error
```

## Strict mode (Explicit)

Missing setups throw an error so unintended calls are caught.

```csharp
var imposter = new IMyServiceImposter(ImposterInvocationBehavior.Explicit);
var service = imposter.Instance();

// No setup -> throws MissingImposterException
Assert.Throws<MissingImposterException>(() => service.GetNumber());

// Add a setup -> call succeeds
imposter.GetNumber().Returns(42);
service.GetNumber(); // 42

// Property without a setup -> throws
Assert.Throws<MissingImposterException>(() => { var _ = service.Age; });
imposter.Age.Getter().Returns(10);
var age = service.Age; // 10
```

Call the real implementation for overridable class members:

```csharp
// Still in Explicit mode
imposter.DoWork(Arg<int>.Any()).UseBaseImplementation();
service.DoWork(123); // calls the class’s own implementation
```

## How to choose

Start with Loose (Implicit) for convenience, switch to Strict (Explicit) when you want tests to fail on any unplanned call.

```csharp
// C# 14+
var strict = IMyService.Imposter(ImposterInvocationBehavior.Explicit);
var loose  = IMyService.Imposter(); // default is Implicit

// C# 8–13
var strict2 = new IMyServiceImposter(ImposterInvocationBehavior.Explicit);
var loose2  = new IMyServiceImposter();
```

See tests for more examples:
`tests/Imposter.Tests/Features/MethodImposter/ExplicitModeTests.cs`,
`tests/Imposter.Tests/Features/PropertyImposter/PropertyDefaultBehaviour.cs`,
`tests/Imposter.Tests/Features/ClassImposter/InstanceBehaviorClassImposterTests.cs`.
