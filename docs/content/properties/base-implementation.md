# Property Base Implementation

Forward to the base implementation for overridable class members.

```csharp
imposter.Age.Getter().UseBaseImplementation();
imposter.Age.Setter(Arg<int>.Any()).UseBaseImplementation();
```

Notes
- For class targets with initializers, the first read mirrors the initialized value.
- In Explicit mode, if base is unavailable and no setup matches, `MissingImposterException` is thrown.

