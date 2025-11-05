namespace Imposter.Abstractions;

/// <summary>
/// Provides static factory methods for creating argument matchers used in Imposter setups.
/// </summary>
/// <typeparam name="T">The type of the argument to match.</typeparam>
public class Arg<T>
{
    private readonly Func<T, bool> _matches;

    private Arg(Func<T, bool> matches)
    {
        _matches = matches;
    }

    /// <summary>
    /// Checks if criteria matches the value
    /// </summary>
    public bool Matches(T value) => _matches(value);

    /// <summary>
    /// Matches an argument based on a custom predicate.
    /// </summary>
    public static Arg<T> Is(Func<T, bool> predicate)
    {
        if (predicate is null) throw new ArgumentNullException(nameof(predicate));
        return new(predicate);
    }

    /// <summary>
    /// Matches an argument that is equal to the provided value.
    /// </summary>
    public static Arg<T> Is(T? value) => Is(value, EqualityComparer<T>.Default);

    /// <summary>
    /// Matches an argument that is equal to the provided value, using a custom comparer.
    /// </summary>
    public static Arg<T> Is(T? value, IEqualityComparer<T> comparer)
    {
        if (comparer is null) throw new ArgumentNullException(nameof(comparer));
        return new(it => comparer.Equals(it, value!));
    }

    /// <summary>
    /// Matches an argument that does not satisfy the provided predicate.
    /// </summary>
    public static Arg<T> IsNot(Func<T, bool> predicate)
    {
        if (predicate is null) throw new ArgumentNullException(nameof(predicate));
        return new(it => !predicate(it));
    }

    /// <summary>
    /// Matches an argument that is not equal to the provided value.
    /// </summary>
    public static Arg<T> IsNot(T? value) => IsNot(value, EqualityComparer<T>.Default);

    /// <summary>
    /// Matches an argument that is not equal to the provided value, using a custom comparer.
    /// </summary>
    public static Arg<T> IsNot(T? value, IEqualityComparer<T> comparer)
    {
        if (comparer is null) throw new ArgumentNullException(nameof(comparer));
        return new(it => !comparer.Equals(it, value!));
    }

    /// <summary>
    /// Matches an argument if it belongs to the provided set of values.
    /// </summary>
    public static Arg<T> IsIn(IEnumerable<T> values) => IsIn(values, EqualityComparer<T>.Default);

    /// <summary>
    /// Matches an argument if it belongs to the provided set of values, using a custom comparer.
    /// </summary>
    public static Arg<T> IsIn(IEnumerable<T> values, IEqualityComparer<T> comparer)
    {
        if (values is null) throw new ArgumentNullException(nameof(values));
        if (comparer is null) throw new ArgumentNullException(nameof(comparer));

        var lookup = new HashSet<T>(values, comparer);
        return new(lookup.Contains);
    }

    /// <summary>
    /// Matches an argument if it does not belong to the provided set of values.
    /// </summary>
    public static Arg<T> IsNotIn(IEnumerable<T> values) => IsNotIn(values, EqualityComparer<T>.Default);

    /// <summary>
    /// Matches an argument if it does not belong to the provided set of values, using a custom comparer.
    /// </summary>
    public static Arg<T> IsNotIn(IEnumerable<T> values, IEqualityComparer<T> comparer)
    {
        if (values is null) throw new ArgumentNullException(nameof(values));
        if (comparer is null) throw new ArgumentNullException(nameof(comparer));

        var lookup = new HashSet<T>(values, comparer);
        return new(it => !lookup.Contains(it));
    }

    /// <summary>
    /// Matches an argument that is the default value for its type (e.g., 0 for int, null for string).
    /// </summary>
    public static Arg<T> IsDefault() => Is(default(T));

    /// <summary>
    /// Matches any value of type T.
    /// </summary>
    public static Arg<T> Any() => _anyInstance;

    private static readonly Arg<T> _anyInstance = new(_ => true);

    /// <summary>
    /// Implicitly converts a value of type T to an Arg{T} that matches that specific value.
    /// </summary>
    /// <param name="value">The value to convert to an argument matcher.</param>
    public static implicit operator Arg<T>(T value) => Is(value);
}

/// <summary>
/// A matcher for 'out' parameters. Since 'out' parameters have no input value, this always matches.
/// </summary>
public class OutArg<T>
{
    private static readonly OutArg<T> Instance = new();

    /// <summary>
    /// Matches any 'out' argument of type T.
    /// </summary>
    public static OutArg<T> Any() => Instance;

    private OutArg()
    {
    }

    /// <summary>
    /// Always returns <c>true</c>; out arguments cannot be constrained and are treated as unconstrained wildcards.
    /// Use the appropriate <c>Arg&lt;T&gt;.Is(...)</c> overload for in/ref arguments when matching specific values is required.
    /// </summary>
    public bool Matches(T value) => true;
}
