# Arguments Matching

How imposters decide which setup applies for a given call.

Target type used in examples:

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/Docs/ArgumentsMatching/ArgumentsMatchingTests.cs#L7"}
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

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/Docs/ArgumentsMatching/ArgumentsMatchingTests.cs#L25"}
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

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/Docs/ArgumentsMatching/ArgumentsMatchingTests.cs#L145"}
    var imposter = new IArgumentMatchingServiceImposter();
    var service = imposter.Instance();

    // Arg.Any is implicitly converted to Arg<T>.Any() for each parameter
    imposter.Add(Arg.Any, Arg.Any).Returns(99);

    service.Add(1, 2);   // 99
    service.Add(100, 0); // 99
    ```

## Predicates and ranges

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/Docs/ArgumentsMatching/ArgumentsMatchingTests.cs#L45"}
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

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/Docs/ArgumentsMatching/ArgumentsMatchingTests.cs#L60"}
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

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/Docs/ArgumentsMatching/ArgumentsMatchingTests.cs#L75"}
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

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/Docs/ArgumentsMatching/ArgumentsMatchingTests.cs#L106"}
    var imposter = new IArgumentMatchingServiceImposter();
    var service = imposter.Instance();

    imposter.InOnly(Arg<int>.Is(x => x > 0)).Returns(99);

    int input = 5;
    service.InOnly(in input); // 99
    ```

### `ref readonly` parameters

!!! note
    `ref readonly` parameters need C# 12 or later. They match like `in` parameters, and the delegates you pass to a method's `Returns` or `Callback` declare them as `ref readonly`.

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/Docs/ArgumentsMatching/ArgumentsMatchingTests.cs#L180"}
    var imposter = new IRefReadOnlyArgumentMatchingServiceImposter();
    var service = imposter.Instance();

    imposter.Double(Arg<int>.Is(x => x > 0)).Returns((ref readonly int value) => value * 2);

    int input = 5;
    service.Double(in input); // 10
    ```

### `ref` parameters

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/Docs/ArgumentsMatching/ArgumentsMatchingTests.cs#L120"}
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

!!! note
    `out` arguments are not inputs and hence they are always treated as wildcards for matching. Use `OutArg<T>.Any()` to indicate "any `out` value".

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/Docs/ArgumentsMatching/ArgumentsMatchingTests.cs#L156"}
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

An imposter can't keep a `Span<T>` or `ReadOnlySpan<T>`, so when a method is called it copies the elements of each span argument, passed by value, `in` or `ref readonly`, into an array. Match those elements with `SpanArg<T>` for a `Span<T>` parameter and `ReadOnlySpanArg<T>` for a `ReadOnlySpan<T>` parameter. Both work the same way: `Is(params T[] expected)` matches the same elements in the same order, `Is(Func<T[], bool> predicate)` matches when the predicate returns `true` for them, and `Any()` or `Arg.Any` matches any elements. Verification sees the elements as they were when the method was called. The delegates you pass to `Returns` or `Callback` receive the span itself.

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/Docs/ArgumentsMatching/ArgumentsMatchingTests.cs#L195"}
    var imposter = new ISpanArgumentMatchingServiceImposter();
    var service = imposter.Instance();

    imposter.Parse(ReadOnlySpanArg<char>.Is('4', '2')).Returns(42);
    imposter.Parse(ReadOnlySpanArg<char>.Is(text => text.Length > 2)).Returns(-1);

    service.Parse("42"); // 42
    service.Parse("123"); // -1
    ```

!!! warning
    Imposters support only spans that a method takes by value, `in` or `ref readonly`, or [returns](methods/index.md#setup-return-values) by value. A span taken by `ref` or `out`, a span returned by reference or by a method with a `scoped` parameter, a span in a property, an indexer or an event's delegate, and any other `ref struct` still report [IMP009](diagnostics.md#imp009). See [Limitations](limitations.md#ref-like-types).

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
