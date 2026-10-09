namespace Imposter.Abstractions;

/// <summary>
/// Matches a <c>Span&lt;T&gt;</c> or <c>ReadOnlySpan&lt;T&gt;</c> parameter. An imposter keeps a copy of each span argument
/// as an array, so the matcher compares that copy.
/// </summary>
/// <typeparam name="T">The span's element type.</typeparam>
/// <remarks>
/// Example:
/// <code>
/// // int Parse(ReadOnlySpan&lt;char&gt; text);
/// imposter.Parse(SpanArg&lt;char&gt;.Is('4', '2')).Returns(42);
/// imposter.Parse(SpanArg&lt;char&gt;.Is(text =&gt; text.Length &gt; 2)).Returns(3);
/// imposter.Parse(SpanArg&lt;char&gt;.Any()).Called(Count.Once());
/// </code>
/// </remarks>
public sealed class SpanArg<T>
{
    private static readonly SpanArg<T> AnyInstance = new(_ => true);

    private readonly Func<T[], bool> _matches;

    private SpanArg(Func<T[], bool> matches)
    {
        _matches = matches;
    }

    /// <summary>
    /// Determines whether the copy of a span argument satisfies this matcher.
    /// </summary>
    /// <param name="elements">The span argument's elements.</param>
    public bool Matches(T[] elements) => _matches(elements);

    /// <summary>
    /// Matches any span.
    /// </summary>
    public static SpanArg<T> Any() => AnyInstance;

    /// <summary>
    /// Matches a span whose elements equal <paramref name="expected"/>, in order.
    /// </summary>
    /// <param name="expected">The elements to compare with; the matcher keeps its own copy.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="expected"/> is <see langword="null"/>.</exception>
    public static SpanArg<T> Is(params T[] expected) => Is(expected, EqualityComparer<T>.Default);

    /// <summary>
    /// Matches a span whose elements equal <paramref name="expected"/>, in order, using <paramref name="comparer"/>.
    /// </summary>
    /// <param name="expected">The elements to compare with; the matcher keeps its own copy.</param>
    /// <param name="comparer">Compares the elements.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="expected"/> or <paramref name="comparer"/> is <see langword="null"/>.
    /// </exception>
    public static SpanArg<T> Is(T[] expected, IEqualityComparer<T> comparer)
    {
        if (expected is null)
            throw new ArgumentNullException(nameof(expected));
        if (comparer is null)
            throw new ArgumentNullException(nameof(comparer));

        var elements = (T[])expected.Clone();
        return new(actual => actual.SequenceEqual(elements, comparer));
    }

    /// <summary>
    /// Matches a span whose elements satisfy <paramref name="predicate"/>.
    /// </summary>
    /// <param name="predicate">Evaluates a copy of the span argument's elements.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="predicate"/> is <see langword="null"/>.</exception>
    public static SpanArg<T> Is(Func<T[], bool> predicate)
    {
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));
        return new(predicate);
    }

    /// <summary>
    /// Implicitly converts <see cref="Arg.Any"/> to a matcher for any span.
    /// </summary>
    public static implicit operator SpanArg<T>(AnyArgMarker _) => Any();
}
