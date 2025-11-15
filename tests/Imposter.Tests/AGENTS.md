# Imposter.Tests � Coding Agent Instructions

Scope: These conventions apply to `tests/Imposter.Tests` and its subtree.

## Testing Conventions
- Framework: use `xUnit` with `[Fact]` tests only; avoid `[Theory]` unless explicitly required.
- Assertions: use `Shouldly` (`ShouldBe`, `Should.NotThrow`, `Should.Throw<...>`, `Should.ThrowAsync<...>`, etc.).
- Target: `.NET 9.0`, C# `LangVersion` 8.0, `Nullable[AGENTS.md](AGENTS.md)` enabled.
- Namespaces: start with `Imposter.CodeGenerator.Tests` and mirror the folder path, e.g. `Imposter.CodeGenerator.Tests.Features.MethodSetup`.
- Layout: group by feature under `Features/<FeatureName>/`; shared helpers in `Shared/`.
- File/class naming: one public test class per file, class name ends with `Tests` (e.g., `ReturnValueSetupTests`), file name matches the class name.
- Test method names: use `Given_..._When_..._Should_...` e.g. `GivenMethodSetupWithReturnValue_WhenMethodIsInvoked_ShouldReturnSetupValue`.

## SUT Pattern
- Each test class creates a single readonly SUT imposter instance:
  - `private readonly <InterfaceName>Imposter _sut = new <InterfaceName>Imposter();`
- Call the subject via `_sut.Instance()`; configure behavior on `_sut`.

## Setup/Verification APIs
- Returns:
  - `.Returns(value)` or `.Returns(lambda)` � lambdas may capture `ref`, `out`, `in` as needed.
  - Chain via `.Then()` to build sequences; last value repeats after sequence is exhausted.
  - Async: support `.Returns(Task.FromResult(...))` or `.Returns(async () => { ... })` for `Task<T>`.
- Throws:
  - `.Throws<ExceptionType>()`, `.Throws(exceptionInstance)`, or `.Throws(() => new Exception(...))`.
  - Can be chained with `.Then()` alongside `Returns`.
- Callbacks:
  - `.Callback(...)` runs after the result; last matching callback wins for non-generic callbacks, chained callbacks run in order for parameterized forms.
- Argument matching:
  - `Arg<T>.Is(value)` / `Arg<T>.Is(predicate)`, `Arg<T>.Any()`; `OutArg<T>.Any()` for out parameters.
- Verification:
  - Methods: `_sut.Method(...).Called(Count.Exactly(n | AtLeast(n) | AtMost(n) | Once() | Never()))`.
  - Properties: `_sut.Property.Getter().Called(...)`, `_sut.Property.Setter(Arg<T>...).Called(...)`.

## Style & Practices
- Arrange/Act/Assert structure; keep fluent setup readable with line breaks.
- Prefer `[Fact] public async Task ...` for async tests; avoid `async void`.
- Use `Should.ThrowAsync<...>` for async exception assertions.
- Handle nullability explicitly (e.g., `(string?)null`).
- Keep tests deterministic; use tiny delays and `Task.WhenAll` where concurrency is required.
- Do not introduce other frameworks (e.g., Machine.Specifications) even though it�s referenced.
- Place reusable test types in `Shared/` (e.g., `Animals.cs`).

## Examples (from this project)
- `[Fact]` and Shouldly: see `tests/Imposter.Tests/Features/MethodSetup/ReturnValueSetupTests.cs:17`.
- Verification API: see `tests/Imposter.Tests/Features/MethodSetup/InvocationVerificationTests.cs:23`.
- Property verification: see `tests/Imposter.Tests/Features/PropertySetup/VerificationTests.cs:14`.
- Async setup and assertion: see `tests/Imposter.Tests/Features/MethodSetup/ReturnValueSetupTests.cs:640`.

## Important
- When writing code, coverage is critical; ensure all new features and edge cases are tested.
- The language version of this project must remain at C# 9.0 to match the minimum supported language version by the Imposter generator.
- Test must be concise and focused; Avoid asserting multiple behaviors in a single test method. For example, if you're testing protected member imposters, have separate tests for methods, indexers, properties, etc. Instead of putting all of it in one test method.

Notes
- Generated files are emitted to `tests/Imposter.Tests/GeneratedFiles` for inspection and are excluded from compilation.
- Test project references the Imposter source generator as an analyzer; the `_sut` types used in tests are generated.
 - Never modify any `.g.cs` files in `GeneratedFiles/`. Update the generator and regenerate instead.
