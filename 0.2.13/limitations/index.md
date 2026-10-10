# Limitations

Imposter keeps the source generator and runtime focused on common impersonation scenarios. This comes with a few intentional limitations.

## Language and tooling

- Minimum supported C# version is 9.0.
- Only Roslyn-based C# projects are supported (no Visual Basic or F#).
- Generated imposters compile for projects targeting .NET Standard 2.0 and later, .NET Framework 4.7.2 and later, and .NET Core or .NET 5 and later. Projects targeting .NET Standard or .NET Framework default to an older C# version, so set `<LangVersion>9.0</LangVersion>` or later (see [IMP003](https://themidnightgospel.github.io/Imposter/0.2.13/diagnostics/#imp003)).

## Class targets

- Only non-sealed classes can be impersonated.
- Only virtual or abstract members can be impersonated on class imposters.
- `UseBaseImplementation()` applies only to non-abstract, virtual class members and is not available for interfaces.
- The imposter creates its instance without setting C# 11 `required` members. A required field or non-virtual property keeps its default value. A virtual or abstract required property is impersonated like any other. Before .NET 7, the project needs its own `SetsRequiredMembersAttribute` (see [IMP010](https://themidnightgospel.github.io/Imposter/0.2.13/diagnostics/#imp010)).

### Virtual calls during construction

If a target constructor calls an intercepted member, the imposter's setup state is not yet available. During this phase, concrete virtual methods, properties, indexers, and event accessors call their base implementation. Exceptions from that implementation propagate normally.

Abstract methods and getters return `default` (including `null` for reference types and `Task`), `out` parameters receive their default value, and `ref` parameters remain unchanged. Abstract void methods, setters, and event accessors do nothing. Constructors must tolerate these default values; for example, they cannot await an abstract method's default `Task` result.

This behavior is the same in implicit and explicit modes. Constructor-time calls do not run setup callbacks or appear in invocation verification. After construction, normal setup, verification, and explicit-mode checks apply.

## Interface targets

- Static members of an interface aren't impersonated. They're called through the interface, never through the imposter's instance, so the imposter has no setup for them.
- An interface with a static abstract member that has no implementation in it, declared or inherited, can't be impersonated: C# doesn't allow it as a type argument, which its imposter needs. Such a target reports [IMP012](https://themidnightgospel.github.io/Imposter/0.2.13/diagnostics/#imp012) and gets no imposter.

## Ref-like types

- Methods with `Span<T>` or `ReadOnlySpan<T>` parameters, or that return one by value, properties and indexers with span values or keys, and events whose delegate takes a span can be impersonated (see [Span properties](https://themidnightgospel.github.io/Imposter/0.2.13/properties/#span-properties), [Span keys and values](https://themidnightgospel.github.io/Imposter/0.2.13/indexers/#span-keys-and-values) and [Span parameters of events](https://themidnightgospel.github.io/Imposter/0.2.13/events/#span-parameters)). The imposter copies the elements a span argument arrives with, and you match them with `SpanArg<T>` or `ReadOnlySpanArg<T>`, or `OutSpanArg<T>` or `OutReadOnlySpanArg<T>` for an `out` span (see [Span parameters](https://themidnightgospel.github.io/Imposter/0.2.13/arguments-matching/#span-parameters)). `Returns` takes the array a returned span covers (see [Methods](https://themidnightgospel.github.io/Imposter/0.2.13/methods/#setup-return-values)).
- In a generic method, a span whose element type uses one of the method's type parameters passes as a copy. A `Span<T>` argument reaches the delegates you pass to `Returns` and `Callback` as a copy, so their writes don't reach the caller's memory; a `ref` or `out` span the delegates leave reaches the caller as a copy; and a returned `Span<T>` is a copy of the array given to `Returns`, so the caller's writes don't reach that array.
- A `scoped` parameter, or a `params` span, which is scoped implicitly, stays `scoped` in the delegates you pass to `Returns` and `Callback`. C# doesn't infer `scoped` for a lambda that leaves out its parameter types, so when the member returns a span or takes one by `ref` or `out`, a lambda passed to `Returns` declares its parameters, `scoped` included (see [Span parameters](https://themidnightgospel.github.io/Imposter/0.2.13/arguments-matching/#span-parameters)).
- A method's parameter of a custom `ref struct` type isn't matched or recorded: setups and verification leave it out, and only the delegates you pass to `Returns`, `Callback` and `Throws` see it (see [Ref struct parameters](https://themidnightgospel.github.io/Imposter/0.2.13/arguments-matching/#ref-struct-parameters)). So a test can't tell calls apart by that argument.
- A method's custom `ref struct` result comes only from a `Returns` delegate or the base implementation: there's no `Returns(value)`, and the invocation history doesn't record it (see [Methods](https://themidnightgospel.github.io/Imposter/0.2.13/methods/#setup-return-values)).
- A value of a method's type parameter that allows ref structs (`where T : allows ref struct`) passes through like a custom `ref struct` parameter or result, whatever the type argument, and the method's setups apply only to calls with the same type arguments (see [Generics](https://themidnightgospel.github.io/Imposter/0.2.13/generics/#type-parameters-that-allow-ref-structs)). The imposter repeats the anti-constraint, which needs the Roslyn 4.14 compiler or later; an older one reports [IMP009](https://themidnightgospel.github.io/Imposter/0.2.13/diagnostics/#imp009) for such a method. The imposter of a generic target takes only type arguments that aren't ref structs, even for a type parameter of the target that allows them.
- A property of a custom `ref struct` type doesn't keep or match its value: its getter's `Returns` takes only a delegate, its `Setter()` takes no criteria, and without a setup the getter returns the base getter's value or the default, even after a set (see [Ref struct properties](https://themidnightgospel.github.io/Imposter/0.2.13/properties/#ref-struct-properties)).
- An indexer of a custom `ref struct` type doesn't keep or match its value either: its getter's `Returns` takes only the delegate that gets the keys, its setter's `Called` counts the sets by their keys, and without a setup the getter returns the base getter's value or the default (see [Ref struct values](https://themidnightgospel.github.io/Imposter/0.2.13/indexers/#ref-struct-values)).
- An indexer key of a custom `ref struct` type isn't matched or recorded: setups and verification match the other keys only, and an indexer whose keys are all ref structs, or whose other keys are another indexer's, is set up by a method named after it, such as `Indexer()` (see [Ref struct keys](https://themidnightgospel.github.io/Imposter/0.2.13/indexers/#ref-struct-keys)).
- Members that use a ref-like type (`Span<T>`, `ReadOnlySpan<T>` or another `ref struct`) in any other way can't be impersonated, because an imposter can't store or match its values: a span returned by reference, a span property or indexer returned by reference, a span an async event's delegate takes by `ref`, `out` or `ref readonly`, a custom `ref struct` anywhere but a method's parameter or by-value result, the type of a property or indexer returned by value, an indexer key passed by value, or a sync event delegate's parameter, a class indexer's `ref struct` value next to a key taken by `in` or `ref readonly` when its getter has a base implementation, a method's parameter or result of a `ref struct` type that uses the method's type parameters, or a generic method's `ref struct` result next to a `ref`, `in` or `ref readonly` parameter. The two generic method cases don't apply when one of the method's type parameters allows ref structs. A target with such a member reports [IMP009](https://themidnightgospel.github.io/Imposter/0.2.13/diagnostics/#imp009) and gets no imposter.

## Ref returns

- Methods, properties and indexers that return by `ref` or `ref readonly` can't be impersonated, because an imposter returns its results by value. A target with such a member reports [IMP011](https://themidnightgospel.github.io/Imposter/0.2.13/diagnostics/#imp011) and gets no imposter. An interface member of this kind with a default body is left out instead: it has no setup, and calls reach its body.

## Pointer types

- Methods, properties, indexers and events whose signature uses a pointer or function pointer type, such as `int*` or `delegate*<int, void>`, can't be impersonated, because a pointer can't be a type argument. A target with such a member reports [IMP013](https://themidnightgospel.github.io/Imposter/0.2.13/diagnostics/#imp013) and gets no imposter.
- A class imposter has no constructor for a target constructor that takes a pointer. A class whose accessible constructors all take one reports [IMP013](https://themidnightgospel.github.io/Imposter/0.2.13/diagnostics/#imp013).

## Async behavior

- Async methods without setup return `default`, which for `Task` is `null`.
- Sequenced async outcomes are consumed in order; when a sequence is exhausted, the last outcome is repeated (where applicable).

## Generated code

- Generated `.g.cs` files are implementation details and should not be edited directly; customize behavior via setups or by changing generator inputs.

Next steps

- [Getting Started](https://themidnightgospel.github.io/Imposter/0.2.13/index.md)
- [Key API Reference](https://themidnightgospel.github.io/Imposter/0.2.13/key-api-reference/index.md)
- [Base Implementation](https://themidnightgospel.github.io/Imposter/0.2.13/base-implementation/index.md)
- [Cheat Sheet](https://themidnightgospel.github.io/Imposter/0.2.13/cheat-sheet/index.md)
