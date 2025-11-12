# Imposter

A C# incremental source generator that creates lightweight imposters (mocks/stubs) for your interfaces and overridable class members using a simple attribute: `GenerateImposterAttribute`.

- Fast, allocation‑aware Roslyn generator
- Fluent API for arranging returns, throws, callbacks, and verification
- Supports async, ref/out/in parameters, sequencing, and base forwarding for class targets

Docs: https://themidnightgospel.github.io/Imposter/

## Quick Start

1) Add the NuGet package `Imposter` to your project:

Using CLI:

```bash
dotnet add package Imposter
```

Or via `csproj`:

```xml
<ItemGroup>
  <PackageReference Include="Imposter" Version="0.1.0" />
</ItemGroup>
```

2) Mark your target type and use the generated imposter:

```csharp
using Imposter.Abstractions;

public interface IMyService
{
    int GetNumber();
    Task<int> GetNumberAsync();
}

[assembly: GenerateImposter(typeof(IMyService))]

// Arrange + use
var imposter = new IMyServiceImposter();
var service = imposter.Instance();

imposter.GetNumber().Returns(42);
service.GetNumber(); // 42

imposter.GetNumberAsync().ReturnsAsync(7);
await service.GetNumberAsync(); // 7
```

More examples are available in the docs and in tests under `tests/Imposter.Tests/Features/MethodImposter` and siblings for properties, indexers, and events.

## Building & Testing

- Build solution: `dotnet build Imposter.sln`
- Run tests: `dotnet test Imposter.sln`
- Run benchmarks: `dotnet run -c Release -p benchmarks/Imposter.Benchmarks/Imposter.Benchmarks.csproj`

## Learn More

- Docs site: https://themidnightgospel.github.io/Imposter/
- Repository overview: REPOSITORY_OVERVIEW.md
- Architecture notes: ARCHITECTURE.md
- Local development: LOCAL_DEVELOPMENT.md
- Coding standards: CODING_STANDARDS.md

## License

See LICENSE.txt.
