# Property Mocking

Configure getters and setters, verify writes, and forward to base implementations for class targets.

## Getter

```csharp
imposter.Age.Getter().Returns(33);
var value = service.Age; // 33

// Sequencing
imposter.Age.Getter().Returns(10).Then().Returns(20);
```

## Setter

```csharp
// Observe writes
imposter.Age.Setter(Arg<int>.Any()).Callback(v => { /* side-effects */ });

// Verify writes
imposter.Age.Setter(Arg<int>.Any()).Called(Count.Exactly(1));
```

## Base Implementation

Forward to the base implementation for overridable class members:

```csharp
imposter.Age.Getter().UseBaseImplementation();
imposter.Age.Setter(Arg<int>.Any()).UseBaseImplementation();
```

## Defaults and Initializers

- Implicit mode: missing getter setups return defaults.
- For class targets with property initializers, the first read mirrors the initialized base value.

## Tips

- Combine `Called(Count.*)` with your setter setups to validate write frequency.
- In Explicit mode, missing setups throw `MissingImposterException`.
- See tests under `tests/Imposter.Tests/Features/PropertyImposter/*` for edge cases and thread-safety.

