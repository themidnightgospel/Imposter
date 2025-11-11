# Imposter.CodeGenerator: Agents Guide

## Goal
goal of this project is to create mocking framework, alternative to Moq or NSubstitute, with focus on performance and low memory usage which is achieved by generating code at compile time using Roslyn source generators.

This guide applies to the code generation project under `src/Imposter.CodeGenerator`. It sets hard rules and conventions for implementing or modifying generators and feature builders.

## Generated Files Policy

- Never edit generated files directly. Any file ending with `.g.cs` is produced by the source generator and must not be modified by hand. Update generator logic, metadata, or syntax builders and regenerate.

## Non‑String Code Generation Only

- Do not use string templates, string interpolation, concatenation, or `string.Replace` to generate code.
- Always construct syntax via `Microsoft.CodeAnalysis.CSharp.SyntaxFactory` and the helpers in `Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper`.
- Build syntax trees using helper builders (e.g., `ClassDeclarationBuilder`, `MethodDeclarationBuilder`, `NamespaceDeclarationSyntaxBuilder`) and compose nodes — never emit raw strings of C#.
- Normalize and format using Roslyn APIs and our helpers; do not post‑process generated text directly.

## Metadata‑First Design

- Each feature must define Metadata ref structs that describ[AGENTS.md](../../tests/Imposter.Tests/AGENTS.md)e the full structure of what will be generated (classes, interfaces, methods, fields, properties, nested types, etc.).
- Model the approach used by the Property Imposter generator. See:
  - `src/Imposter.CodeGenerator/Features/PropertyImposter/Metadata/ImposterPropertyMetadata.cs:11`
  - `src/Imposter.CodeGenerator/Features/PropertyImposter/Metadata/PropertyImposterBuilderMetadata.cs:9`
  - `src/Imposter.CodeGenerator/Features/PropertyImposter/Metadata/DefaultPropertyBehaviourMetadata.cs:6`
- Metadata should encapsulate:
  - Core symbols and naming: interface/property/method symbols, unique names, display names, type syntax[AGENTS.md](../../tests/Imposter.Tests/AGENTS.md).
  - Structural parts: builder interfaces, concrete builders, default behavior types/fields, invocation history types, delegate types, etc.
  - Reusable field/property/method descriptors via small metadata structs (e.g., `FieldMetadata`).
- Use `readonly ref struct` metadata where appropriate to avoid allocations and to keep symbol access scoped to construction.

## Folder & Type Layout

- Organize per feature under `src/Imposter.CodeGenerator/Features/<FeatureName>/...` with a `Metadata` folder mirroring major components. Example:
  - `Features/PropertyImposter/Metadata/...`
  - `Features/IndexerImposter/Metadata/...`
- Keep “builder” construction code separate from metadata. Builders consume metadata to emit syntax.
- Prefer fine‑grained metadata files (e.g., one struct per cohesive concept) over monolithic meta[AGENTS.md](../../tests/Imposter.Tests/AGENTS.md)data.

## Syntax Construction Rules

- Prefer helpers in `SyntaxFactoryHelper` over raw `SyntaxFactory` when available:
- Use specific helpers: `TypeSyntax`, `ParameterSyntax`, `Property`, `Method`, `Constructor`, `Field`, `Invocation`, `Lambda`, `Trivia`, `Generic`, `Async`, etc.
- Use collection helpers like `ConcurrentQueueSyntaxHelper` / `ConcurrentStackSyntaxHelper` when building concurrent types.
- Use C# collection expressions where appropriate (LangVersion ≥ 12): prefer `[]`, `[item1, item2]`, and spread `[..enumerable]` over `new[] { ... }` or manual materialization when APIs accept arrays or `IEnumerable<T>` (e.g., argument arrays, member arrays, separated lists construction helpers).
- Use `WellKnownTypes` and `MemberNamesHelper` to avoid hardcoding type and member names.
- Do not hand‑roll text for namespaces, using directives, attributes, or modifiers — compose them as syntax nodes.
- Names and types for variables, fields, properties, methods, and types must be derived from metadata or helpers, never hardcoded.
- Prefer using custom builder classes (e.g., `ClassDeclarationBuilder`, `MethodDeclarationBuilder`) to construct complex syntax nodes. Avoid using raw `SyntaxFactory` calls directly in most cases.

## Consistency & Behavior

- Align new features with the patterns established by Property and Method imposters:
  - Default/explicit invocation behavior where applicable.
  - Builder interfaces with `Returns/Throws/Callback/Called/Then` style APIs.
  - Invocation history collections modeled as dedicated types for verification.
- Reuse existing helpers (e.g., `InterfaceDeclarationBuilderFactory`, `ClassDeclarationBuilderFactory`) rather than duplicating logic.
- Generated code should be thread‑safe where applicable, using concurrent collections. Locks or semaphores should be avoided unless absolutely necessary.

## Documentation & Tests

- When modifying or adding features, update `Imposter_codex/modules/Imposter.CodeGenerator.md` with the new behavior and examples.
- Add/update tests under `tests/Imposter.Tests/Features/...` to cover new scenarios and ensure generated output compiles and behaves as intended.

## Review Checklist

- [ ] No string‑based code emission; only Roslyn `SyntaxFactory` and our helpers used.
- [ ] Metadata structs fully describe the generated structure, following the Property Imposter precedent.
- [ ] Builders consume metadata and only emit syntax nodes.
- [ ] Uses `WellKnownTypes`, `SyntaxFactoryHelper`, and naming helpers instead of hardcoded literals.
- [ ] Files and folders placed under the appropriate `Features/<FeatureName>/Metadata` and builder locations.
- [ ] Docs and tests updated.

## Module Overview

- Purpose: Roslyn Incremental Source Generator that emits imposter types for interfaces annotated by `GenerateImposterAttribute`.
- Path/Target: `src/Imposter.CodeGenerator`, `netstandard2.0`, `LangVersion` 13.
- References: `Microsoft.CodeAnalysis.CSharp`, analyzers, and `Imposter.Abstractions`.

### Entrypoint
- `CodeGenerator/ImposterGenerator.cs`
  - Registers pipelines, reports diagnostics, processes `GenerateImposterAttribute` occurrences.
  - Supports interfaces (`TypeKind.Interface`), emits one source per target `{ImposterName}.g.cs`.

### Syntax Providers
- `CodeGenerator/SyntaxProviders/ImposterTargetValuesProvider.cs`: discovers attribute occurrences and produces `GenerateImposterDeclaration`.
- `CodeGenerator/SyntaxProviders/GenerateImposterDeclaration.cs`: symbol-equality record.

### Shared Metadata
- `Features/Shared/ImposterGenerationContext.cs`: bundles current target symbol + derived metadata.
- `Features/Shared/ImposterTargetMetadata.cs`: computes target methods/properties and builds per-member metadata.

### Feature Areas
- `Features/Imposter/*`: top-level imposter class composition and instance wiring.
- `Features/MethodImposter/*`: delegates, argument criteria, invocation setup/history, verification, fluent surface.
- `Features/PropertyImposter/*`: getter/setter builders, default behavior, verification.
- `Features/IndexerImposter/*`: indexer-specific builders and metadata.
- `Features/EventImposter/*`: event subscription/raise surface, verification, interceptors.

### Generated Code Shape
- Produces `IFooImposter` in user namespace or `Imposters.<N>.<IFoo>`, with per-member builders for methods/properties/indexers/events and `.Instance()` implementation.

### Diagnostics & Local Dev
- `IMP001`: generator crash diagnostic including message/stack.
- Build: `dotnet build src/Imposter.CodeGenerator/Imposter.CodeGenerator.csproj`
- Tests: `tests/Imposter.Tests` enable inspection of generated files.

## Builder Conventions

- Metadata-driven surface: do not hardcode generated member names or parameter identifiers/types in builders. For every method/property that is part of the generated public/fluent surface (including explicit interface members), create metadata that exposes:
  - `Name` of the member.
  - `ReturnType` where applicable.
  - One `ParameterMetadata` per parameter and consume via `SyntaxFactoryHelper.ParameterSyntax(ParameterMetadata)`.
- Apply across all features (Method/Property/Indexer/Event) so implementations and interfaces share a single source of truth.
- EventImposter example: `Subscribe`/`Unsubscribe`/`Callback`/`OnSubscribe`/`OnUnsubscribe`/`Subscribed`/`Unsubscribed`/`Raised`/`HandlerInvoked` should be supplied by dedicated metadata (e.g., `SubscribeMethodMetadata.Name` and `SubscribeMethodMetadata.HandlerParameter`) rather than string literals like "Subscribe" or parameter names like "handler". Internal cooperation methods (Subscribe/Unsubscribe) should also be metadata-backed.
- Exceptions: Calls to external/BCL APIs (e.g., `Enqueue`, `AddOrUpdate`) do not require metadata wrappers.
- Placement: Put method metadata under the feature’s `Metadata` folder (use subfolders), mirroring how other features group their method metadata.
- Tests: Verify names and parameters of generated members under `tests/Imposter.Tests/Features/...`.

## Metadata Organization

- Mirror generated code structure: the metadata that describes a type’s members should be owned by that type’s metadata.
  - Class metadata → its fields/properties/methods metadata.
  - Interface metadata → its methods/properties metadata.
  - Core/shared metadata → symbol-derived facts reusable across types.
- EventImposter example:
  - `ImposterEventCoreMetadata`: event-delegate facts (handler type, parameters, async flags).
  - `EventImposterBuilderInterfaceMetadata`: public interface surface (e.g., raise method naming/return type, and per-method metadata).
  - `EventImposterBuilderMetadata`: owns builder-class members (fields and methods). Avoid attaching builder fields/methods under aggregate event metadata.
- Practical rule: if a field/method is emitted on `FooBuilder`, its metadata belongs under `FooBuilderMetadata` (e.g., `FooBuilderFieldsMetadata`, `FooBuilder.<Method>Metadata`). Do not place these under higher-level aggregate metadata.
