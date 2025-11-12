# Language Features

Some niceties depend on compiler features. Core functionality works across supported C# versions; static type extensions are available on C# 14+.

## Static Type Extensions (C# 14+)

- Feature: call `IMyService.Imposter()` on the type itself.
- Availability: emitted when the project compiles with C# 14 or later.
- Class targets: overloads mirror the target's constructors.

```xml
<PropertyGroup>
  <LangVersion>latest</LangVersion>
</PropertyGroup>
```

```csharp
// Interfaces
var imp = IMyService.Imposter();

// Classes with multiple constructors
var c0 = MultiCtorClass.Imposter();
var c1 = MultiCtorClass.Imposter(42, "label");
```

## C# 8–13

- Use the generated imposter types directly:

```csharp
var imposter = new IMyServiceImposter();
var service = imposter.Instance();
```

For details, see tests under `tests/Imposter.CodeGenerator.Tests/Features/ClassImposter/ImposterStaticTypeExtensionTests.cs`.
