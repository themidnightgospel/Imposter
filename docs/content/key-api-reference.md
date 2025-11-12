# Key API Reference

A compact list of the core types and fluent members you’ll use most. This is not a full API surface — just the names and signatures you need to get productive.

## Attribute

- `GenerateImposterAttribute`
  - Usage: `[assembly: Imposter.Abstractions.GenerateImposter(typeof(TTarget), bool putInTheSameNamespace = true)]`
  - Parameters:
    - `Type type` — target interface or class
    - `bool putInTheSameNamespace = true` - try to place the generated imposter in the target's namespace

!!! tip "Pro tip"
    Add the attribute at the assembly level in a single file to keep generation centralized and easy to audit.

## Argument Matchers

- `Arg<T>`
  - `static Arg<T> Is(Func<T, bool> predicate)`
  - `static Arg<T> Is(T? value)`
  - `static Arg<T> Is(T? value, IEqualityComparer<T> comparer)`
  - `static Arg<T> IsNot(Func<T, bool> predicate)`
  - `static Arg<T> IsNot(T? value)`
  - `static Arg<T> IsNot(T? value, IEqualityComparer<T> comparer)`
  - `static Arg<T> IsIn(IEnumerable<T> values)`
  - `static Arg<T> IsIn(IEnumerable<T> values, IEqualityComparer<T> comparer)`
  - `static Arg<T> IsNotIn(IEnumerable<T> values)`
  - `static Arg<T> IsNotIn(IEnumerable<T> values, IEqualityComparer<T> comparer)`
  - `static Arg<T> IsDefault()`
  - `static Arg<T> Any()`
  - `static implicit operator Arg<T>(T value)`

- `OutArg<T>`
  - `static OutArg<T> Any()` — wildcard for `out` parameters

## Verification Counts

- `Count`
  - `static Count Exactly(int count)`
  - `static Count AtLeast(int count)`
  - `static Count AtMost(int count)`
  - `static Count Never()`
  - `static Count Once()`
  - `static readonly Count Any`

## Imposter Mode

- `ImposterMode`
  - `Implicit` — missing setups return defaults (loose)
  - `Explicit` — missing setups throw `MissingImposterException` (strict)

## Method Setups (generated)

- Typical fluent members on a method setup builder:
  - `Returns(TResult value)`
  - `Returns(Func<TResult> factory)`
  - `Returns(Func<...parameters..., TResult> factory)` — delegate receives in/ref/out/in params
  - `ReturnsAsync(TResult value)`
  - `ReturnsAsync(Func<Task<TResult>> factory)`
  - `Throws(Exception ex)`
  - `Throws<TException>()`
  - `Callback(Action action)` / `Callback(Action<...parameters...> action)`
  - `Then()` — chain next behavior (return/throw/etc.) for subsequent matching calls
  - `Called(Count count)` — verify invocation frequency

## Property Setups (generated)

- Getter: `imposter.Property.Getter()` → supports `Returns(...)`, `Throws(...)`, `Callback(...)`, `Then()`, `Called(Count)`
- Setter: `imposter.Property.Setter(Arg<T> valueMatcher)` → supports `Callback(...)`, `Called(Count)`
- Base implementation: `UseBaseImplementation()` (when applicable for classes)

!!! tip "Pro tip"
    Do not edit generated `.g.cs` files. Change behavior via setups or generator inputs, then rebuild.

## Indexer Setups (generated)

- Indexer access: `imposter[Arg<T1>..., Arg<TN>...]`
  - Getter: `.Getter()` → `Returns(...)`, `Throws(...)`, `Callback(...)`, `Then()`, `Called(Count)`
  - Setter: `.Setter()` / `.Setter(Arg<TValue> valueMatcher)` → `Callback(...)`, `Called(Count)`
  - Base implementation: `UseBaseImplementation()`

## Event Helpers (generated)

- Raise: `imposter.Event.Raise(object? sender, EventArgs args)` (or concrete event args type)
- Verify:
  - `imposter.Event.Subscribed(Arg<Delegate>.Is(handler), Count count)`
  - `imposter.Event.HandlerInvoked(Arg<Delegate>.Is(handler), Count count)`

See also: [Cheat Sheet](cheat-sheet.md) and [Limitations](limitations.md).
