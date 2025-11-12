# Imposter Modes (Properties)

How property getters and setters behave when you didn’t set them up.

## Implicit mode

Getters without setups return default values like `0`, `null`, or `default(T)`.

```csharp
var imposter = new IMyServiceImposter(ImposterMode.Implicit);
var service = imposter.Instance();

// Getter without a setup => default(int) == 0
int age = service.Age; // 0

// Add a getter setup
imposter.Age.Getter().Returns(33);
age = service.Age; // 33

// Setters work without setups; verify writes
service.Age = 10;
imposter.Age.Setter(Arg<int>.Is(10)).Called(Count.Once());
```

## Explicit mode

Getters without setups throw so unintended reads are caught.

```csharp
var imposter = new IMyServiceImposter(ImposterMode.Explicit);
var service = imposter.Instance();

// No getter setup -> throws MissingImposterException
Assert.Throws<MissingImposterException>(() => { var _ = service.Age; });

// Add a getter setup -> read succeeds
imposter.Age.Getter().Returns(10);
var value = service.Age; // 10

// Setters can still be verified
service.Age = 5;
imposter.Age.Setter(Arg<int>.Is(5)).Called(Count.Once());
```

## Calling real code on classes

Forward to the class’s own implementation for overridable properties:

```csharp
imposter.Age.Getter().UseBaseImplementation();
imposter.Age.Setter(Arg<int>.Any()).UseBaseImplementation();
```

See also: `tests/Imposter.Tests/Features/PropertyImposter/PropertyDefaultBehaviour.cs`.
