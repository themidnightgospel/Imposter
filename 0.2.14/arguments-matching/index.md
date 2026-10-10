# Arguments Matching

How imposters decide which setup applies for a given call.

Target type used in examples:

Example

```
using Imposter.Abstractions;

[assembly: GenerateImposter(typeof(Imposter.Tests.Docs.ArgumentsMatching.IArgumentMatchingService))]

public interface IArgumentMatchingService
{
    int Add(int a, int b);
    int Increment(int value);
    int Sum(params int[] values);
    string Format(string template, string value);
    int InOnly(in int input);
    int RefOnly(ref int state);
    int OutOnly(out int result);
}
```

## Basics: value vs matcher

Example

```
var imposter = new IArgumentMatchingServiceImposter();
var service = imposter.Instance();

// Implicit equality matchers.
// Equivalent to imposter.Add(Arg<int>.Is(1), Arg<int>.Is(2)).Returns(3);
imposter.Add(1, 2).Returns(3);

// Explicit equality matchers
imposter.Add(Arg<int>.Is(1), Arg<int>.Is(2)).Returns(30);

// Wildcard on first argument, fixed second
imposter.Add(Arg<int>.Any(), 0).Returns(0);

service.Add(1, 2);  // 30
service.Add(10, 0); // 0
service.Add(5, 5);  // 0 (default(int) in Implicit mode)
```

## Untyped wildcard — `Arg.Any`

When every argument should be a wildcard and you don't want to spell out the generic type, use the shorthand `Arg.Any`. It is implicitly converted to `Arg<T>` for each parameter, so it works in any position.

Example

```
var imposter = new IArgumentMatchingServiceImposter();
var service = imposter.Instance();

// Arg.Any is implicitly converted to Arg<T>.Any() for each parameter
imposter.Add(Arg.Any, Arg.Any).Returns(99);

service.Add(1, 2);   // 99
service.Add(100, 0); // 99
```

## Predicates and ranges

Example

```
var imposter = new IArgumentMatchingServiceImposter();
var service = imposter.Instance();

imposter.Increment(Arg<int>.Is(x => x < 0)).Returns(-1);
imposter.Increment(Arg<int>.Is(x => x >= 0 && x <= 10)).Returns(10);
imposter.Increment(Arg<int>.Is(x => x > 10)).Returns(100);

service.Increment(-5);  // -1
service.Increment(3);   // 10
service.Increment(50);  // 100
```

## Negation and defaults

Example

```
var imposter = new IArgumentMatchingServiceImposter();
var service = imposter.Instance();

// Not equal
imposter.Increment(Arg<int>.IsNot(0)).Returns(1);

// Default(T)
imposter.Increment(Arg<int>.IsDefault()).Returns(-1);

service.Increment(0);  // -1 (default)
service.Increment(5);  // 1  (IsNot)
```

## Membership and collections

Example

```
var imposter = new IArgumentMatchingServiceImposter();
var service = imposter.Instance();

// Discrete set
imposter.Increment(Arg<int>.IsIn(new[] { 1, 2, 3 })).Returns(10);
imposter.Increment(Arg<int>.IsNotIn(new[] { 1, 2, 3 })).Returns(99);

service.Increment(2);  // 10
service.Increment(5);  // 99
```

## Ref / out / in parameters

### `in` parameters

Example

```
var imposter = new IArgumentMatchingServiceImposter();
var service = imposter.Instance();

imposter.InOnly(Arg<int>.Is(x => x > 0)).Returns(99);

int input = 5;
service.InOnly(in input); // 99
```

### `ref readonly` parameters

Note

`ref readonly` parameters need C# 12 or later. They match like `in` parameters, and the delegates you pass to a method's `Returns` or `Callback` declare them as `ref readonly`.

Example

```
var imposter = new IRefReadOnlyArgumentMatchingServiceImposter();
var service = imposter.Instance();

imposter.Double(Arg<int>.Is(x => x > 0)).Returns((ref readonly int value) => value * 2);

int input = 5;
service.Double(in input); // 10
```

### `ref` parameters

Example

```
var imposter = new IArgumentMatchingServiceImposter();
var service = imposter.Instance();

imposter
    .RefOnly(Arg<int>.Is(x => x >= 0))
    .Returns(
        (ref int state) =>
        {
            state += 10;
            return state;
        }
    );

int state = 1;
service.RefOnly(ref state); // 11, state is now 11
```

### `out` parameters

Note

`out` arguments are not inputs and hence they are always treated as wildcards for matching. Use `OutArg<T>.Any()` to indicate "any `out` value".

Example

```
var imposter = new IArgumentMatchingServiceImposter();
var service = imposter.Instance();

imposter
    .OutOnly(OutArg<int>.Any())
    .Returns(
        (out int value) =>
        {
            value = 42;
            return 1;
        }
    );

int result;
service.OutOnly(out result); // result == 42
```

## Span parameters

An imposter can't keep a `Span<T>` or `ReadOnlySpan<T>`, so when a method is called it copies the elements each span argument arrives with into an array. Match those elements with `SpanArg<T>` for a `Span<T>` parameter and `ReadOnlySpanArg<T>` for a `ReadOnlySpan<T>` parameter. Both work the same way: `Is(params T[] expected)` matches the same elements in the same order, `Is(Func<T[], bool> predicate)` matches when the predicate returns `true` for them, and `Any()` or `Arg.Any` matches any elements. Verification sees the elements as they were when the method was called. The delegates you pass to `Returns` or `Callback` receive the span itself.

Example

```
var imposter = new ISpanArgumentMatchingServiceImposter();
var service = imposter.Instance();

imposter.Parse(ReadOnlySpanArg<char>.Is('4', '2')).Returns(42);
imposter.Parse(ReadOnlySpanArg<char>.Is(text => text.Length > 2)).Returns(-1);

service.Parse("42"); // 42
service.Parse("123"); // -1
```

A span passed by `ref` is matched by the elements it arrives with, and the delegates you pass to `Returns` or `Callback` receive it by reference, so they can write to it or replace it. An `out` span is an output: match it with `OutSpanArg<T>.Any()` or `OutReadOnlySpanArg<T>.Any()`, and assign it in the delegate:

Example

```
// bool TryRead(out ReadOnlySpan<byte> data);
imposter
    .TryRead(OutReadOnlySpanArg<byte>.Any())
    .Returns((out ReadOnlySpan<byte> data) =>
    {
        data = new byte[] { 1, 2 };
        return true;
    });

reader.TryRead(out var data); // true, data holds 1, 2
```

A `scoped` parameter, or a `params` span, which is scoped implicitly, stays `scoped` in the delegates you pass to `Returns` or `Callback`. Like the member itself, a delegate can't return the span it receives. C# doesn't infer `scoped` for a lambda that leaves out its parameter types, so when the member returns a span or takes one by `ref` or `out`, a lambda passed to `Returns` declares its parameters, `scoped` included (otherwise CS8986). `Returns` with an array needs nothing extra.

Example

```
// ReadOnlySpan<char> Trim(scoped ReadOnlySpan<char> text);
imposter
    .Trim(ReadOnlySpanArg<char>.Any())
    .Returns((scoped ReadOnlySpan<char> text) => text.ToString().Trim().ToCharArray());

reader.Trim(" a ".ToCharArray()); // "a"
```

Warning

Imposters support span parameters, spans a method [returns](https://themidnightgospel.github.io/Imposter/0.2.14/methods/#setup-return-values) by value, [span properties](https://themidnightgospel.github.io/Imposter/0.2.14/properties/#span-properties), span [indexer keys and values](https://themidnightgospel.github.io/Imposter/0.2.14/indexers/#span-keys-and-values), and the spans an [event's delegate](https://themidnightgospel.github.io/Imposter/0.2.14/events/#span-parameters) takes. A span returned by reference, a span an async event's delegate takes by `ref`, `out` or `ref readonly`, and any other `ref struct` except a method's [parameter](#ref-struct-parameters) or [result](https://themidnightgospel.github.io/Imposter/0.2.14/methods/#setup-return-values), a [property's](https://themidnightgospel.github.io/Imposter/0.2.14/properties/#ref-struct-properties) or [indexer's](https://themidnightgospel.github.io/Imposter/0.2.14/indexers/#ref-struct-values) value, an indexer's [key](https://themidnightgospel.github.io/Imposter/0.2.14/indexers/#ref-struct-keys) and a sync event delegate's [parameter](https://themidnightgospel.github.io/Imposter/0.2.14/events/#ref-struct-parameters) still report [IMP009](https://themidnightgospel.github.io/Imposter/0.2.14/diagnostics/#imp009). A value of a method's [type parameter that allows ref structs](https://themidnightgospel.github.io/Imposter/0.2.14/generics/#type-parameters-that-allow-ref-structs) passes through like a ref struct parameter. See [Limitations](https://themidnightgospel.github.io/Imposter/0.2.14/limitations/#ref-like-types).

## Ref struct parameters

An imposter can't keep or match an argument of a custom `ref struct` type, so setups and verification take matchers for the method's other parameters only, and the invocation history leaves the argument out. The delegates you pass to `Returns`, `Callback` and `Throws` receive the argument itself, by reference when the method takes it by `ref` or `out`, and so does the base implementation.

Example

```
// int Read(int id, Cursor cursor);
imposter.Read(Arg<int>.Is(1)).Returns((id, cursor) => cursor.Position);

service.Read(1, new Cursor(5)); // 5
```

Overloads that differ only in their ref struct parameters would get the same setup, so each after the first gets a numbered one (`Read_1`), as overloads that differ only in passing a parameter by reference do. A value of a method's type parameter that allows ref structs passes through the same way (see [Generics](https://themidnightgospel.github.io/Imposter/0.2.14/generics/#type-parameters-that-allow-ref-structs)). A ref struct whose type uses the method's own type parameters still reports [IMP009](https://themidnightgospel.github.io/Imposter/0.2.14/diagnostics/#imp009), unless one of them allows ref structs.

## Arg API reference

- `Arg.Any` — untyped wildcard; implicitly converts to `Arg<T>.Any()` for each parameter type.
- `Arg<T>.Any()` — wildcard that matches any value of `T`.
- `Arg<T>.Is(T value)` / `Arg<T>.Is(T value, IEqualityComparer<T> comparer)` — matches when the argument equals the provided value (optionally using a custom comparer).
- `Arg<T>.Is(Func<T, bool> predicate)` — matches when the predicate returns `true`.
- `Arg<T>.IsNot(T value)` / `Arg<T>.IsNot(T value, IEqualityComparer<T> comparer)` — matches when the argument is not equal to the provided value (with optional comparer).
- `Arg<T>.IsNot(Func<T, bool> predicate)` — matches when the predicate returns `false`.
- `Arg<T>.IsDefault()` — matches `default(T)`.
- `Arg<T>.IsIn(IEnumerable<T> values)` / `Arg<T>.IsIn(IEnumerable<T> values, IEqualityComparer<T> comparer)` — matches when the argument is contained in the supplied set.
- `Arg<T>.IsNotIn(IEnumerable<T> values)` / `Arg<T>.IsNotIn(IEnumerable<T> values, IEqualityComparer<T> comparer)` — matches when the argument is not contained in the supplied set.
- `SpanArg<T>.Any()` / `ReadOnlySpanArg<T>.Any()` — wildcard for a `Span<T>` / `ReadOnlySpan<T>` argument.
- `SpanArg<T>.Is(params T[] expected)` / `SpanArg<T>.Is(T[] expected, IEqualityComparer<T> comparer)`, and the same on `ReadOnlySpanArg<T>` — matches when the span holds the same elements in the same order (optionally using a custom comparer).
- `SpanArg<T>.Is(Func<T[], bool> predicate)`, and the same on `ReadOnlySpanArg<T>` — matches when the predicate returns `true` for the span's elements.
- `OutSpanArg<T>.Any()` / `OutReadOnlySpanArg<T>.Any()` — wildcard for an `out Span<T>` / `out ReadOnlySpan<T>` argument.
