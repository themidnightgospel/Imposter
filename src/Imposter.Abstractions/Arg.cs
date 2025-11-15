namespace Imposter.Abstractions;

/// <summary>
/// Provides strongly-typed argument matchers used when configuring method, property, indexer
/// and event setups with Imposter.
/// </summary>
/// <typeparam name="T">The type of the argument to match.</typeparam>
/// <remarks>
/// Matchers created via <see cref="Arg{T}"/> are consumed by generated imposters to determine
/// whether a particular invocation matches a configured setup.
///
/// Examples:
/// <code>
/// // Match by predicate
/// imposter.Do(Arg&lt;int&gt;.Is(x =&gt; x &gt; 10)).Returns(42);
///
/// // Match by equality
/// imposter.Do(Arg&lt;int&gt;.Is(5)).Called(Count.Once());
/// // Or
/// imposter.Do(5).Called(Count.Once());
///
/// // Negated match
/// imposter.Do(Arg&lt;string&gt;.IsNot("admin"));
///
/// // Set membership
/// imposter.Do(Arg&lt;int&gt;.IsIn(new[] { 1, 2, 3 }));
///
/// // Wildcard
/// imposter.Do(Arg&lt;Guid&gt;.Any());
/// </code>
/// </remarks>
public class Arg<T>
{
    private readonly Func<T, bool> _matches;

    private Arg(Func<T, bool> matches)
    {
        _matches = matches;
    }

    /// <summary>
    /// Determines whether the provided value satisfies this matcher.
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <returns><see langword="true"/> if the value matches; otherwise <see langword="false"/>.</returns>
    public bool Matches(T value) => _matches(value);

    /// <summary>
    /// Matches an argument based on a custom predicate.
    /// </summary>
    /// <param name="predicate">The predicate that evaluates candidate values.</param>
    /// <returns>An <see cref="Arg{T}"/> that matches when <paramref name="predicate"/> returns true.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="predicate"/> is <see langword="null"/>.</exception>
    public static Arg<T> Is(Func<T, bool> predicate)
    {
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));
        return new(predicate);
    }

    /// <summary>
    /// Matches an argument that is equal to the provided value.
    /// </summary>
    /// <param name="value">The value to compare against.</param>
    /// <returns>An <see cref="Arg{T}"/> that matches equal values.</returns>
    public static Arg<T> Is(T? value) => Is(value, EqualityComparer<T>.Default);

    /// <summary>
    /// Matches an argument that is equal to the provided value, using a custom comparer.
    /// </summary>
    /// <param name="value">The value to compare against.</param>
    /// <param name="comparer">The equality comparer used to determine equality.</param>
    /// <returns>An <see cref="Arg{T}"/> that matches equal values.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="comparer"/> is <see langword="null"/>.</exception>
    public static Arg<T> Is(T? value, IEqualityComparer<T> comparer)
    {
        if (comparer is null)
            throw new ArgumentNullException(nameof(comparer));
        return new(it => comparer.Equals(it, value!));
    }

    /// <summary>
    /// Matches an argument that does not satisfy the provided predicate.
    /// </summary>
    /// <param name="predicate">The predicate that evaluates candidate values.</param>
    /// <returns>An <see cref="Arg{T}"/> that matches when <paramref name="predicate"/> returns false.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="predicate"/> is <see langword="null"/>.</exception>
    public static Arg<T> IsNot(Func<T, bool> predicate)
    {
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));
        return new(it => !predicate(it));
    }

    /// <summary>
    /// Matches an argument that is not equal to the provided value.
    /// </summary>
    /// <param name="value">The value to compare against.</param>
    /// <returns>An <see cref="Arg{T}"/> that matches non-equal values.</returns>
    public static Arg<T> IsNot(T? value) => IsNot(value, EqualityComparer<T>.Default);

    /// <summary>
    /// Matches an argument that is not equal to the provided value, using a custom comparer.
    /// </summary>
    /// <param name="value">The value to compare against.</param>
    /// <param name="comparer">The equality comparer used to determine inequality.</param>
    /// <returns>An <see cref="Arg{T}"/> that matches non-equal values.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="comparer"/> is <see langword="null"/>.</exception>
    public static Arg<T> IsNot(T? value, IEqualityComparer<T> comparer)
    {
        if (comparer is null)
            throw new ArgumentNullException(nameof(comparer));
        return new(it => !comparer.Equals(it, value!));
    }

    /// <summary>
    /// Matches an argument if it belongs to the provided set of values.
    /// </summary>
    /// <param name="values">The collection that defines the allowed set.</param>
    /// <returns>An <see cref="Arg{T}"/> that matches when the value appears in <paramref name="values"/>.</returns>
    public static Arg<T> IsIn(IEnumerable<T> values) => IsIn(values, EqualityComparer<T>.Default);

    /// <summary>
    /// Matches an argument if it belongs to the provided set of values, using a custom comparer.
    /// </summary>
    /// <param name="values">The collection that defines the allowed set.</param>
    /// <param name="comparer">The equality comparer used to test membership.</param>
    /// <returns>An <see cref="Arg{T}"/> that matches when the value appears in <paramref name="values"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="values"/> or <paramref name="comparer"/> is <see langword="null"/>.</exception>
    public static Arg<T> IsIn(IEnumerable<T> values, IEqualityComparer<T> comparer)
    {
        if (values is null)
            throw new ArgumentNullException(nameof(values));
        if (comparer is null)
            throw new ArgumentNullException(nameof(comparer));

        var lookup = new HashSet<T>(values, comparer);
        return new(lookup.Contains);
    }

    /// <summary>
    /// Matches an argument if it does not belong to the provided set of values.
    /// </summary>
    /// <param name="values">The collection that defines the disallowed set.</param>
    /// <returns>An <see cref="Arg{T}"/> that matches when the value does not appear in <paramref name="values"/>.</returns>
    public static Arg<T> IsNotIn(IEnumerable<T> values) =>
        IsNotIn(values, EqualityComparer<T>.Default);

    /// <summary>
    /// Matches an argument if it does not belong to the provided set of values, using a custom comparer.
    /// </summary>
    /// <param name="values">The collection that defines the disallowed set.</param>
    /// <param name="comparer">The equality comparer used to test membership.</param>
    /// <returns>An <see cref="Arg{T}"/> that matches when the value does not appear in <paramref name="values"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="values"/> or <paramref name="comparer"/> is <see langword="null"/>.</exception>
    public static Arg<T> IsNotIn(IEnumerable<T> values, IEqualityComparer<T> comparer)
    {
        if (values is null)
            throw new ArgumentNullException(nameof(values));
        if (comparer is null)
            throw new ArgumentNullException(nameof(comparer));

        var lookup = new HashSet<T>(values, comparer);
        return new(it => !lookup.Contains(it));
    }

    /// <summary>
    /// Matches an argument that is the default value for its type (for example, 0 for <see cref="int"/>,
    /// <see langword="null"/> for reference types).
    /// </summary>
    public static Arg<T> IsDefault() => Is(default(T));

    /// <summary>
    /// Matches any value of type <typeparamref name="T"/>.
    /// </summary>
    public static Arg<T> Any() => _anyInstance;

    private static readonly Arg<T> _anyInstance = new(_ => true);

    /// <summary>
    /// Implicitly converts a value of type <typeparamref name="T"/> to an <see cref="Arg{T}"/>
    /// that matches that specific value.
    /// </summary>
    /// <param name="value">The value to convert to an argument matcher.</param>
    /// <returns>An <see cref="Arg{T}"/> that matches <paramref name="value"/>.</returns>
    public static implicit operator Arg<T>(T value) => Is(value);
}

/// <summary>
/// A matcher for <c>out</c> parameters. Since <c>out</c> parameters have no input value, this matcher always succeeds.
/// </summary>
/// <remarks>
/// Use <see cref="OutArg{T}.Any"/> when verifying or configuring calls that have <c>out</c> parameters.
/// For input and <c>ref</c> parameters use <see cref="Arg{T}"/> instead.
///
/// Example:
/// <code>
/// bool TryGet(out int value);
/// imposter.TryGet(OutArg&lt;int&gt;.Any()).Returns(true);
/// </code>
/// </remarks>
public class OutArg<T>
{
    private static readonly OutArg<T> Instance = new();

    /// <summary>
    /// Matches any <c>out</c> argument of type <typeparamref name="T"/>.
    /// </summary>
    public static OutArg<T> Any() => Instance;

    private OutArg() { }

    /// <summary>
    /// Always returns <see langword="true"/>. Out arguments cannot be constrained and are treated as wildcards.
    /// Use <see cref="Arg{T}"/> (for example, <c>Arg&lt;T&gt;.Is(...)</c>) for in/ref arguments when matching specific values is required.
    /// </summary>
    /// <param name="value">Ignored.</param>
    /// <returns>Always <see langword="true"/>.</returns>
    public bool Matches(T value) => true;
}
