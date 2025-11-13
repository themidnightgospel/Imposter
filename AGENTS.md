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

## Terminology

- Use the term "impersonate / impersonation" instead of "mock / mocking" in all docs, comments, commit messages, UI labels, and variable names where feasible. Examples:
  - Say "impersonate a method/property/event" not "mock a method/property/event".
  - Say "only virtual and abstract members of the class can be impersonated" when describing class constraints.
  - Generated types are "imposters"; their behavior is configured via the fluent API.
  - Avoid introducing "mock" nomenclature except when comparing to other libraries.

## Docs Page Structure (Admonitions + Snippets)

When editing/adding documentation pages (MkDocs Material), follow these patterns used on the Getting Started page to keep the site uniform and scannable.

- Admonition types and purposes
  - `!!! note` — succinct callouts about versions or constraints (e.g., minimum supported C#, or “only virtual/abstract members can be impersonated”).
  - `!!! tip "Pro tip"` — practical guidance for local dev (e.g., how to inspect generated files).
  - `!!! warning` — hard limitations or “gotchas” (e.g., “only non‑sealed classes can have imposters”).
  - `!!! example` — wrap primary code examples/definitions shown in the flow (attribute/targets, class examples, etc.).
  - `!!! info "Next steps"` — closing box for onward navigation and quick links.

- Code examples and versions
  - Use tabs for language/version variants: `=== "C# 14"` and `=== "C# 8-13"`.
  - Prefer the C# 14 static extension API in examples where available; include the C# 8–13 form directly below.
  - In tests, gate C# 14 examples under `#if ROSLYN_5_OR_GREATER` so the suite compiles across Roslyn baselines.
  - Wrap all C# code snippets in `!!! example` admonitions for consistent visual framing. When a snippet appears within a list item, indent the admonition under that list level.

- Snippet linking and tests
  - Every C# snippet in docs must map to an executable test in `tests/Imposter.Tests/Docs/<Section>/...`.
  - Add `data-gh-link` to each C# code block, pointing to the exact test line (e.g., ```csharp {data-gh-link="https://github.com/.../tests/Imposter.Tests/Docs/GettingStarted/GettingStartedTests.cs#L42"}```).
  - Use the `master` branch in GitHub links (e.g., `.../blob/master/...`).
  - The site already injects a GitHub icon button for code blocks via `docs/content/scripts/code-links.js` and styles it via `docs/content/styles/overrides.css`. Do not replace this mechanism; just supply the attribute.

- Test file organization (by docs section)
  - Put snippet tests for each docs section under a matching folder, one class per page:
    - Getting Started: `tests/Imposter.Tests/Docs/GettingStarted/GettingStartedTests.cs`
    - Methods: `tests/Imposter.Tests/Docs/Methods/OverviewTests.cs`, `SequentialReturnsTests.cs`, `ThrowingTests.cs`, `VerificationTests.cs`, `CallbacksTests.cs`, `BaseImplementationTests.cs`, `ProtectedMembersTests.cs`, `ImposterModesTests.cs`
    - Properties/Indexers/Events: mirror this pattern (`OverviewTests.cs`, etc.).
  - Namespace should mirror the folder: e.g., `Imposter.Tests.Docs.Methods` (and deeper sub-namespaces if needed to avoid type name collisions).
  - Keep names stable to minimize churn in `data-gh-link` anchors; when editing, try to append rather than insert above existing snippet lines to avoid changing anchors.

- Section layout (recommended)
  - Title + short tagline.
  - `!!! note` with minimum supported C#.
  - Installation block (`bash`).
  - `!!! tip "Pro tip"` for generator file inspection settings.
  - “Generate an Imposter”
    - `!!! example` wrapping the attribute + interface (or class) definition.
    - “After a build…” followed by tabs for `C# 14` and `C# 8-13` usage.
  - For classes
    - `!!! warning` that only non‑sealed classes can have imposters.
    - `!!! example` for the class definition.
    - Tabs for usage, followed by `!!! note` that only virtual/abstract members can be impersonated.
  - Close with `!!! info "Next steps"` linking to Methods/Properties/Indexers/Events, Key API Reference, Compatibility, and Limitations.

- Terminology in docs
  - Use “impersonate/impersonated” consistently (e.g., “impersonate a method”; “members can be impersonated”), not “mock/mocked”.
  - When comparing with other libraries, it is acceptable to reference “mocking” explicitly for clarity.

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
