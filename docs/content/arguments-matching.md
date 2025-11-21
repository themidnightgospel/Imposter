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
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/Docs/ArgumentsMatching/ArgumentsMatchingTests.cs#L143"}
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

## Arg API reference

- `Arg<T>.Any()` — wildcard that matches any value of `T`.
- `Arg<T>.Is(T value)` / `Arg<T>.Is(T value, IEqualityComparer<T> comparer)` — matches when the argument equals the provided value (optionally using a custom comparer).
- `Arg<T>.Is(Func<T, bool> predicate)` — matches when the predicate returns `true`.
- `Arg<T>.IsNot(T value)` / `Arg<T>.IsNot(T value, IEqualityComparer<T> comparer)` — matches when the argument is not equal to the provided value (with optional comparer).
- `Arg<T>.IsNot(Func<T, bool> predicate)` — matches when the predicate returns `false`.
- `Arg<T>.IsDefault()` — matches `default(T)`.
- `Arg<T>.IsIn(IEnumerable<T> values)` / `Arg<T>.IsIn(IEnumerable<T> values, IEqualityComparer<T> comparer)` — matches when the argument is contained in the supplied set.
- `Arg<T>.IsNotIn(IEnumerable<T> values)` / `Arg<T>.IsNotIn(IEnumerable<T> values, IEqualityComparer<T> comparer)` — matches when the argument is not contained in the supplied set.
