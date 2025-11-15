# Troubleshooting

Common issues and quick fixes.

## "Type does not contain definition `Imposter'"

- Cause: static type extensions are generated only when compiling with C# 14 or later.
- Fix: set `<LangVersion>latest</LangVersion>` (or `14.0`) in your project file, or use the generated imposter type directly (e.g., `new IMyServiceImposter()`).

```xml
<PropertyGroup>
  <LangVersion>latest</LangVersion>
  <!-- C# 14+ enables static type extensions; core functionality works on C# 9–13. -->
</PropertyGroup>
```

## `MissingImposterException`

- Cause: Explicit mode with no matching setup.
- Fix: add a setup for the invoked member or switch to Implicit mode.

```csharp
// Add a setup
imposter.DoWork(Arg<int>.Any()).Returns(0);

// Or choose Implicit behavior
var imposter = new IMyServiceImposter(ImposterMode.Implicit);
```

## Generator not running / no generated files

- Ensure the generator package is referenced and included as an analyzer (default with NuGet is fine).
- Verify the attribute usage is at assembly scope and the project builds successfully.
- In test projects, consider enabling emitted generated files for inspection.

```xml
<PropertyGroup>
  <EmitCompilerGeneratedFiles>true</EmitCompilerGeneratedFiles>
  <CompilerGeneratedFilesOutputPath>GeneratedFiles</CompilerGeneratedFilesOutputPath>
  <!-- For local inspection only; do not ship generated sources. -->
</PropertyGroup>
```

## Ref/Out/In signatures do not match

- Cause: delegate signature must match the target method's ref/out/in modifiers exactly.
- Fix: ensure the types and modifiers align; use `OutArg<T>.Any()` for out parameters in setups.

## Ambiguous argument matching

- Cause: overlapping `Arg<T>` matchers cause multiple setups to match.
- Fix: choose more specific matchers or order your setups so the intended one is evaluated first.

## Performance under concurrency

- Imposters are designed and tested for thread-safety. If you observe contention, review setup patterns and prefer precomputed delegates over heavy per-call work.

If issues persist, consult the tests under `tests/Imposter.Tests/Features/*` for working patterns.
