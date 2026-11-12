namespace Imposter.Abstractions;

/// <summary>
/// What <see cref="Arg.Any"/> returns before it converts to a matcher of the parameter's type.
/// </summary>
public struct AnyArgMarker { }

/// <summary>
/// Provides untyped general argument matcher used when configuring method, property, indexer
/// and event setups with Imposter.
/// </summary>
public static class Arg
{
    /// <summary>
    /// Matches any argument
    /// </summary>
    public static AnyArgMarker Any => new();
}

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
    private enum MatcherKind : byte
    {
        Any,
        Equality,
        Predicate,
    }

    private readonly MatcherKind _kind;
    private readonly T? _value;
    private readonly IEqualityComparer<T>? _comparer;
    private readonly Func<T, bool>? _predicate;
    private readonly bool _negated;

    private Arg(
        MatcherKind kind,
        T? value = default,
        IEqualityComparer<T>? comparer = null,
        Func<T, bool>? predicate = null,
        bool negated = false
    )
    {
        _kind = kind;
        _value = value;
        _comparer = comparer;
        _predicate = predicate;
        _negated = negated;
    }

    /// <summary>Returns a new matcher with a diagnostic description, preserving this matcher's behavior.</summary>
    /// <param name="description">Text to display instead of the default description.</param>
    /// <exception cref="ArgumentNullException">The description is null.</exception>
    public Arg<T> Describe(string description) =>
        new DescribedArg(this, description ?? throw new ArgumentNullException(nameof(description)));

    /// <summary>Describes this matcher without evaluating its predicate or comparer.</summary>
    public override string ToString()
    {
        var description = _kind switch
        {
            MatcherKind.Any => "any",
            MatcherKind.Equality => DiagnosticValue.Format(_value),
            _ => "<predicate>",
        };
        return _negated ? "not " + description : description;
    }

    // Only explicitly described and membership matchers need additional diagnostic storage.
    // Ordinary matchers keep their allocation size and their matching fast path.
    private sealed class DescribedArg : Arg<T>
    {
        private readonly string _description;

        internal DescribedArg(Arg<T> original, string description)
            : base(
                original._kind,
                original._value,
                original._comparer,
                original._predicate,
                original._negated
            )
        {
            _description = description;
        }

        public override string ToString() => _description;
    }

    private sealed class MembershipArg : Arg<T>
    {
        private readonly T[] _values;

        internal MembershipArg(HashSet<T> lookup, T[] values, bool negated)
            : base(MatcherKind.Predicate, predicate: lookup.Contains, negated: negated)
        {
            _values = values;
        }

        public override string ToString() =>
            (_negated ? "not in " : "in ") + DiagnosticValue.Sequence(_values);
    }

    /// <summary>
    /// Determines whether the provided value satisfies this matcher.
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <returns><see langword="true"/> if the value matches; otherwise <see langword="false"/>.</returns>
    public bool Matches(T value) =>
        (
            _kind switch
            {
                MatcherKind.Any => true,
                // Keep Default at the call site so the JIT can specialize value-type equality.
                // Custom comparers receive the actual argument first.
                MatcherKind.Equality => _comparer is null
                    ? EqualityComparer<T>.Default.Equals(value, _value!)
                    : _comparer.Equals(value, _value!),
                MatcherKind.Predicate => _predicate!(value),
                _ => throw new InvalidOperationException("Unknown argument matcher kind."),
            }
        ) != _negated;

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
        return new(MatcherKind.Predicate, predicate: predicate);
    }

    /// <summary>
    /// Matches an argument that is equal to the provided value.
    /// </summary>
    /// <param name="value">The value to compare against.</param>
    /// <returns>An <see cref="Arg{T}"/> that matches equal values.</returns>
    public static Arg<T> Is(T? value) => new(MatcherKind.Equality, value);

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
        return new(MatcherKind.Equality, value, comparer);
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
        return new(MatcherKind.Predicate, predicate: predicate, negated: true);
    }

    /// <summary>
    /// Matches an argument that is not equal to the provided value.
    /// </summary>
    /// <param name="value">The value to compare against.</param>
    /// <returns>An <see cref="Arg{T}"/> that matches non-equal values.</returns>
    public static Arg<T> IsNot(T? value) => new(MatcherKind.Equality, value, negated: true);

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
        return new(MatcherKind.Equality, value, comparer, negated: true);
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

        return Membership(values, comparer, false);
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

        return Membership(values, comparer, true);
    }

    private static Arg<T> Membership(
        IEnumerable<T> values,
        IEqualityComparer<T> comparer,
        bool negated
    )
    {
        var lookup = new HashSet<T>(comparer);
        var orderedValues = values.Where(lookup.Add).ToArray();
        return new MembershipArg(lookup, orderedValues, negated);
    }

    /// <summary>
    /// Matches an argument that is the default value for its type (for example, 0 for <see cref="int"/>,
    /// <see langword="null"/> for reference types).
    /// </summary>
    public static Arg<T> IsDefault() => Is(default(T));

    /// <summary>
    /// Matches any value of type <typeparamref name="T"/>.
    /// </summary>
    public static Arg<T> Any() => AnyInstance;

    private static readonly Arg<T> AnyInstance = new(MatcherKind.Any);

    /// <summary>
    /// Implicitly converts a value of type <typeparamref name="T"/> to an <see cref="Arg{T}"/>
    /// that matches that specific value.
    /// </summary>
    /// <param name="value">The value to convert to an argument matcher.</param>
    /// <returns>An <see cref="Arg{T}"/> that matches <paramref name="value"/>.</returns>
    public static implicit operator Arg<T>(T value) => Is(value);

    /// <summary>
    /// Converts <see cref="Arg.Any"/> to a matcher for any value.
    /// </summary>
    public static implicit operator Arg<T>(AnyArgMarker _) => Any();
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
    /// <param name="_">Ignored.</param>
    /// <returns>Always <see langword="true"/>.</returns>
    public bool Matches(T _) => true;
}
