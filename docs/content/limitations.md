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
- The imposter creates its instance without setting C# 11 `required` members. A required field or non-virtual property keeps its default value. A virtual or abstract required property is impersonated like any other. Before .NET 7, the project needs its own `SetsRequiredMembersAttribute` (see [IMP010](diagnostics.md#imp010)).

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

- Methods with `Span<T>` or `ReadOnlySpan<T>` parameters, or that return one by value, can be impersonated. The imposter copies the elements a span argument arrives with, and you match them with `SpanArg<T>` or `ReadOnlySpanArg<T>`, or `OutSpanArg<T>` or `OutReadOnlySpanArg<T>` for an `out` span (see [Span parameters](arguments-matching.md#span-parameters)). `Returns` takes the array a returned span covers (see [Methods](methods/index.md#setup-return-values)).
- In a generic method, a span whose element type uses one of the method's type parameters passes as a copy. A `Span<T>` argument reaches the delegates you pass to `Returns` and `Callback` as a copy, so their writes don't reach the caller's memory; a `ref` or `out` span the delegates leave reaches the caller as a copy; and a returned `Span<T>` is a copy of the array given to `Returns`, so the caller's writes don't reach that array.
- Members that use a ref-like type (`Span<T>`, `ReadOnlySpan<T>` or another `ref struct`) in any other way can't be impersonated, because an imposter can't store or match its values: a span returned by reference, a span a method with a `scoped` parameter returns or takes by `ref` or `out` (a `params` span parameter is scoped implicitly), a span in a property, indexer or event, or a custom `ref struct` anywhere. A target with such a member reports [IMP009](diagnostics.md#imp009) and gets no imposter.

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
