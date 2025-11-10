# Tests

## Running Tests

- Run all tests: `dotnet test Imposter.sln`

## Roslyn Compatibility

- The `Imposter.CompatibilityTests` project runs the generator against multiple Roslyn versions to catch breaking API changes between Microsoft.CodeAnalysis v4 and v5.
- Per-version runs:
  - `dotnet test tests/Imposter.CompatibilityTests/Imposter.CompatibilityTests.csproj -c Roslyn4`
  - `dotnet test tests/Imposter.CompatibilityTests/Imposter.CompatibilityTests.csproj -c Roslyn5`

In CI, create a build matrix over `configuration: [Roslyn4, Roslyn5]` to exercise both.

