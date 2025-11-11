# Imposter.Tests � Coding Agent Instructions

Scope: These conventions apply to `tests/Imposter.CodeGenerator.Tests` and its subtree.

## Testing Conventions
- Framework: use `xUnit` with `[Fact]` tests only; avoid `[Theory]` unless explicitly required.
- Assertions: use `Shouldly` (`ShouldBe`, `Should.NotThrow`, `Should.Throw<...>`, `Should.ThrowAsync<...>`, etc.).
- Test method names: use `Given_..._When_..._Should_...` e.g. `GivenMethodSetupWithReturnValue_WhenMethodIsInvoked_ShouldReturnSetupValue`.
- Positive compilation scenarios should use the `_ShouldCompile` suffix (retire `_ShouldCompileWithoutDiagnostics` going forward).
- File layout: place new test classes under `tests/Imposter.CodeGenerator.Tests/Features/<FeatureName>/...` (e.g. `Features/FluentApi/PropertyGetterFluentApiTests.cs`) so scenarios stay grouped by feature.
- The language version for the tests `CSharpParseOptions.LanguageVersion` should be `8.0` unless test targets the feature that is specifically available for higher versions, like static type extension `Imposter()` which is generated for c# 14.
-  use /*lang=csharp*/ in front for c# code strings to make it more readable, so like this /*lang=csharp*/"""using System;""";
- Reuse shared compiler error code constants from `WellKnownCsCompilerErrorCodes` instead of hard-coding strings like `"CS1061"`.
- Tests should be testing the behaviour instead of implmentation. For example, if we want to test that .Then method is not available on the fluent api, we should test that code that uses .Then method at the top level does not compile, instead of inspecting the generatod code and searching for strings in it.
- When checking for errors, be as specific as possible, for example when checking if the code snippet compilation failed, check specific diagnostic error at a specific line instead of checking that diagnostic errors is present.
- One test one assert most of the time, unless there is a good reason not to.

## Generated Files
- Do not edit `.g.cs` files. Any generated outputs under `GeneratedFiles/` are for inspection only and must not be manually modified. Update the generator and regenerate instead.
