namespace Imposter.Abstractions;

/// <summary>
/// Matches a <c>Span&lt;T&gt;</c> parameter. An imposter keeps a copy of each span argument as an array, so the
/// matcher compares that copy. <see cref="ReadOnlySpanArg{T}"/> matches a <c>ReadOnlySpan&lt;T&gt;</c> parameter.
/// </summary>
/// <typeparam name="T">The span's element type.</typeparam>
/// <remarks>
/// Example:
/// <code>
/// // void Write(Span&lt;byte&gt; buffer);
/// imposter.Write(SpanArg&lt;byte&gt;.Is(1, 2)).Callback(buffer =&gt; buffer[0] = 0);
/// imposter.Write(SpanArg&lt;byte&gt;.Is(buffer =&gt; buffer.Length &gt; 2)).Throws&lt;ArgumentException&gt;();
/// imposter.Write(SpanArg&lt;byte&gt;.Any()).Called(Count.Once());
/// </code>
/// </remarks>
public sealed class SpanArg<T>
{
    private static readonly SpanArg<T> AnyInstance = new(_ => true);

    private readonly Func<T[], bool> _matches;
    private readonly T[]? _expected;

    private SpanArg(Func<T[], bool> matches, T[]? expected = null)
    {
        _matches = matches;
        _expected = expected;
    }

    internal string DescribeMatcher() =>
        ReferenceEquals(this, AnyInstance) ? "any"
        : _expected is null ? "<predicate>"
        : "[" + string.Join(", ", _expected.Select(value => MatcherDescription.Value(value))) + "]";

    /// <summary>
    /// Determines whether the copy of a span argument satisfies this matcher.
    /// </summary>
    /// <param name="elements">The span argument's elements.</param>
    public bool Matches(T[] elements) => _matches(elements);

    /// <summary>Indicates whether matching keeps the captured array private to the generated imposter.</summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public bool CanReuseCapturedArray =>
        _expected is not null || ReferenceEquals(_matches, AnyInstance._matches);

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
        var matcher = SpanElementMatchers.SequenceEqualTo(expected, comparer, out var elements);
        return new(matcher, elements);
    }

    /// <summary>
    /// Matches a span whose elements satisfy <paramref name="predicate"/>.
    /// </summary>
    /// <param name="predicate">Evaluates a copy of the span argument's elements.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="predicate"/> is <see langword="null"/>.</exception>
    public static SpanArg<T> Is(Func<T[], bool> predicate) =>
        new(SpanElementMatchers.Satisfying(predicate));

    /// <summary>
    /// Implicitly converts <see cref="Arg.Any"/> to a matcher for any span.
    /// </summary>
    public static implicit operator SpanArg<T>(AnyArgMarker _) => Any();
}
