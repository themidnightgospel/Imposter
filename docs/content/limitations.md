# Limitations

Imposter keeps the source generator and runtime focused on common impersonation scenarios. This comes with a few intentional limitations.

## Language and tooling

- Minimum supported C# version is 9.0.
- Only Roslyn-based C# projects are supported (no Visual Basic or F#).
- Generated imposters compile for projects targeting .NET Standard 2.0 and later, .NET Framework 4.7.2 and later, and .NET Core or .NET 5 and later. Projects targeting .NET Standard or .NET Framework default to an older C# version, so set `<LangVersion>9.0</LangVersion>` or later (see [IMP003](diagnostics.md#imp003)).

## Class targets

- Only non-sealed classes can be impersonated.
- Only virtual or abstract members can be impersonated on class imposters.
- `UseBaseImplementation()` applies only to non-abstract, virtual class members and is not available for interfaces.

### Virtual calls during construction

If a target constructor calls an intercepted member, the imposter's setup state is not yet available.
During this phase, concrete virtual methods, properties, indexers, and event accessors call their base
implementation. Exceptions from that implementation propagate normally.

Abstract methods and getters return `default` (including `null` for reference types and `Task`),
`out` parameters receive their default value, and `ref` parameters remain unchanged. Abstract void
methods, setters, and event accessors do nothing. Constructors must tolerate these default values;
for example, they cannot await an abstract method's default `Task` result.

This behavior is the same in implicit and explicit modes. Constructor-time calls do not run setup
callbacks or appear in invocation verification. After construction, normal setup, verification, and
explicit-mode checks apply.

## Ref-like types

- Methods that take a `Span<T>` or `ReadOnlySpan<T>` by value can be impersonated: the imposter copies the span's elements, and you match them with `SpanArg<T>` or `ReadOnlySpanArg<T>` (see [Span parameters](arguments-matching.md#span-parameters)).
- In a generic method, a `Span<T>` argument whose element type uses one of the method's type parameters reaches the delegates you pass to `Returns` and `Callback` as a copy, so writes to it don't reach the caller's memory.
- Members that use a ref-like type (`Span<T>`, `ReadOnlySpan<T>` or another `ref struct`) in any other way can't be impersonated, because an imposter can't store or match its values: a span passed by `ref`, `out`, `in` or `ref readonly`, a span return type, a span in a property, indexer or event, or a custom `ref struct` anywhere. A target with such a member reports [IMP009](diagnostics.md#imp009) and gets no imposter.

## Async behavior

- Async methods without setup return `default`, which for `Task` is `null`.
- Sequenced async outcomes are consumed in order; when a sequence is exhausted, the last outcome is repeated (where applicable).

## Generated code

- Generated `.g.cs` files are implementation details and should not be edited directly; customize behavior via setups or by changing generator inputs.

!!! info "Next steps"
    - [Getting Started](index.md)
    - [Key API Reference](key-api-reference.md)
    - [Base Implementation](base-implementation.md)
    - [Cheat Sheet](cheat-sheet.md)
