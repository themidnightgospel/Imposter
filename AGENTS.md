# Imposter: Agents Guide

Welcome to Imposter. This guide orients any coding agent to the repository, modules, and workflows, and links to all core docs you’ll need to work effectively.

- Project overview: `Imposter` is a C# source generator that produces “imposters” (mocks/stubs) for target interfaces marked with `GenerateImposterAttribute`. It ships two projects:
  - `src/Imposter.Abstractions` — public API used by consumers at runtime and at compile time for attribute-based generation.
  - `src/Imposter.CodeGenerator` — Roslyn Incremental Source Generator that emits imposter types.
- Supporting projects:
  - `tests` — unit/integration tests for both components and behaviours.
  - `benchmarks/Imposter.Benchmarks` — BenchmarkDotNet suite vs Moq and NSubstitute.
  - `playground` — scratchpad and ideation experiments.

Start here for a high-level map: `Imposter_codex/ARCHITECTURE.md`

## Quick Links

- Architecture: `Imposter_codex/ARCHITECTURE.md`
- Tech stack: `Imposter_codex/TECH_STACK_AND_DEPENDENCIES.md`
- Usage guide: `Imposter_codex/USAGE.md`
- Local setup & run: `Imposter_codex/LOCAL_DEVELOPMENT.md`
- Coding standards: `Imposter_codex/CODING_STANDARDS.md`
- Module docs:
  - Abstractions: `Imposter_codex/modules/Imposter.Abstractions.md`
  - Code Generator: `Imposter_codex/modules/Imposter.CodeGenerator.md`
  - Tests: `Imposter_codex/modules/Tests.md`
  - Benchmarks: `Imposter_codex/modules/Benchmarks.md`
  - Playground: `Imposter_codex/modules/Playground.md`

## Typical Tasks

- Build solution: `dotnet build Imposter.sln`
- Run tests: `dotnet test Imposter.sln`
- Run benchmarks: `dotnet run -c Release -p benchmarks/Imposter.Benchmarks/Imposter.Benchmarks.csproj`

## Conventions for Agents

- Follow `Imposter_codex/CODING_STANDARDS.md` for naming, structure, and generator patterns.
- When emitting parameters inside generator code, use `SyntaxFactoryHelper.ParameterSyntax` (with the static helper import) instead of `SyntaxFactory.Parameter(...).WithType(...)` to avoid redundant Roslyn allocations and keep builders consistent.
- Prefer `SyntaxFactoryHelper.Call` and `.New` helpers (with the helper import) over raw `InvocationExpression`/`ObjectCreationExpression` calls so our syntax construction stays uniform and terse.
- Declare locals via `SyntaxFactoryHelper.LocalVariableDeclarationSyntax` rather than raw `LocalDeclarationStatement` to keep all builders on the shared helper surface.
- When touching generator code, document or update the relevant feature doc in `Imposter_codex/modules/Imposter.CodeGenerator.md` and architecture notes.
- Add/update examples in tests to verify new scenarios. Prefer adding tests under `tests/Imposter.Tests/Features/...` mirroring feature area.
- Keep public-facing docs in sync with behaviour (XML docs in abstractions + module docs here).
- Do not edit generated source files directly. Any file ending with `.g.cs` is generated output and must never be manually modified. Make changes in the generator or source templates instead, and regenerate.

## Where To Look First

- Attribute entrypoint: `src/Imposter.Abstractions/GenerateImposterAttribute.cs`
- Generator entrypoint: `src/Imposter.CodeGenerator/CodeGenerator/ImposterGenerator.cs`
- Example usage in tests: `tests/Imposter.Tests/Features/MethodSetup/IMethodSetupFeatureSut.cs`
 
## Important
- Use serena as much as possible
- For the test outputs, like .log files you create during work, use TEMP directory.

## review
- when i ask you to review the code, use this as a base guideline: Evaluate its architecture and design quality, focusing on whether it follows SOLID principles, Clean Code practices, KISS, YAGNI, DRY. ssess the code’s readability, maintainability, extensibility, and testability. Check for proper naming conventions, cohesion, coupling, and separation of concerns. Identify potential code smells, violations of design principles, or anti-patterns. Suggest specific and actionable improvements to make the code cleaner, more robust, and easier to evolve. Finally, summarize your evaluation with an overall design quality rating (e.g., Excellent / Good / Needs Improvement / Poor) and key recommendations for refactoring or redesign. During review, do not write any code.

If something is unclear or missing, augment the relevant doc in `Imposter_codex` and link it here.

At the end of each task, in addition, print what was the prompt in summary 
