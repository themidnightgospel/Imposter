# Contributing to Imposter

Thank you for your interest in contributing to Imposter! This guide will help you get started.

## Getting Started

### Prerequisites

- .NET SDKs 8.0 and 10.0
- PowerShell 7+ (for running the full verification script)
- An editor with Roslyn Source Generator support (VS 2022 17.10+, Rider, or VS Code + C# Dev Kit)

### Clone and Build

```bash
git clone https://github.com/themidnightgospel/Imposter.git
cd Imposter
dotnet build Imposter.slnx
```

### Run Tests

Use the CI-style script for full verification:

```bash
pwsh ./build-scripts/build-and-test.ps1
```

Or run tests directly:

```bash
dotnet test Imposter.slnx
```

### Run Benchmarks

```bash
dotnet run -c Release -p benchmarks/Imposter.Benchmarks/Imposter.Benchmarks.csproj
```

## Project Structure

| Path | Purpose |
|------|---------|
| `src/Imposter.Abstractions` | Public runtime API (attributes, matchers, verification) |
| `src/Imposter.CodeGenerator` | Roslyn Incremental Source Generator |
| `tests/Imposter.Tests` | Behaviour tests (xUnit + Shouldly) |
| `tests/Imposter.Abstractions.Tests` | Unit tests for abstractions |
| `tests/Imposter.CodeGenerator.Tests` | Generator-specific compilation tests |
| `benchmarks/Imposter.Benchmarks` | BenchmarkDotNet suite vs Moq, NSubstitute, FakeItEasy, Rocks |

## How to Contribute

### Reporting Bugs

Open a [GitHub issue](https://github.com/themidnightgospel/Imposter/issues) with:

- A minimal interface definition that triggers the problem
- The compiler error or unexpected behaviour you observe
- Your .NET SDK version and target framework

### Suggesting Features

Open an issue describing the use case and expected API surface. Include code examples where possible.

### Submitting Pull Requests

1. Fork the repository and create a branch from `master`.
2. Make your changes following the coding standards below.
3. Add or update tests to cover new behaviour.
4. Run `pwsh ./build-scripts/build-and-test.ps1` and ensure all tests pass.
5. Open a PR against `master` with a clear description of what and why.

### Code Generation Rules

- **Never** use string templates, interpolation, or concatenation to generate code.
- Always use `SyntaxFactory` and `SyntaxFactoryHelper` to construct syntax trees.
- Use builder classes (`ClassDeclarationBuilder`, `MethodDeclarationBuilder`, etc.) over raw `SyntaxFactory` chains.
- Use `WellKnownTypes` and `NameSet` for type/member names — no hardcoded string literals for generated identifiers.

### Metadata-First Design

- Define `readonly struct` metadata that describes the generated structure before writing builders.
- Builders consume metadata to emit syntax — keep them free of structural decisions.
- Organize under `Features/<Area>/Metadata` with corresponding builders.

### Naming

- Types: `PascalCase`
- Interfaces: `I` prefix (`IHaveImposterInstance`)
- Private fields: `_camelCase`
- Constants: `PascalCase`

### Terminology

- Use **"impersonate / impersonation"** instead of "mock / mocking" in code, comments, and docs.
- Generated types are called **"imposters"**.
- Exception: when comparing to other libraries, "mocking" is acceptable for clarity.

### Testing

- Framework: xUnit with `[Fact]` tests and Shouldly assertions.
- Test naming: `Given_<Context>_When_<Action>_Should_<Outcome>`
- Place behaviour tests under `tests/Imposter.Tests/Features/<FeatureName>/`.
- Place generator compilation tests under `tests/Imposter.CodeGenerator.Tests/Issues/Issue<N>/` for bug reproductions.
- Each test class creates a single readonly SUT: `private readonly <Name>Imposter _sut = new <Name>Imposter();`
- Call the subject via `_sut.Instance()`; configure behaviour on `_sut`.

### Generated Files

- Never edit files ending with `.g.cs` — they are generated output.
- Make changes in the generator or metadata, then rebuild to regenerate.
- Inspect generated output at `tests/Imposter.Tests/GeneratedFiles/` after building.

## PR Checklist

- [ ] Changes are scoped and focused (no unrelated formatting diffs)
- [ ] Tests added or updated to cover new behaviour
- [ ] `pwsh ./build-scripts/build-and-test.ps1` passes (or `dotnet test Imposter.slnx`)
- [ ] Release build compiles clean (warnings treated as errors)
- [ ] No string-based code generation introduced
- [ ] Terminology uses "impersonate/imposter" (not "mock")

## Architecture at a Glance

1. Consumer marks an interface with `[GenerateImposter]`
2. The Roslyn generator discovers the attribute and emits an imposter type (e.g., `IMyServiceImposter`)
3. The imposter exposes a fluent API for setup (`.Returns()`, `.Throws()`, `.Callback()`) and verification (`.Called()`)
4. Test code calls `imposter.Instance()` to get a wired implementation of the target interface

Key entrypoints:

- Attribute: `src/Imposter.Abstractions/GenerateImposterAttribute.cs`
- Generator: `src/Imposter.CodeGenerator/CodeGenerator/ImposterGenerator.cs`
- Example test: `tests/Imposter.Tests/Features/MethodImpersonation/ReturnValueSetupTests.cs`

## License

By contributing, you agree that your contributions will be licensed under the [MIT License](LICENSE.txt).
