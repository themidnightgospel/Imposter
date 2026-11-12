namespace Imposter.Abstractions;

/// <summary>
/// Matches a <c>ReadOnlySpan&lt;T&gt;</c> parameter. An imposter keeps a copy of each span argument as an array, so
/// the matcher compares that copy. It's a type of its own, apart from <see cref="SpanArg{T}"/>, so a method
/// overloaded on <c>Span&lt;T&gt;</c> and on <c>ReadOnlySpan&lt;T&gt;</c> keeps distinct setups.
/// </summary>
/// <typeparam name="T">The span's element type.</typeparam>
/// <remarks>
/// Example:
/// <code>
/// // int Parse(ReadOnlySpan&lt;char&gt; text);
/// imposter.Parse(ReadOnlySpanArg&lt;char&gt;.Is('4', '2')).Returns(42);
/// imposter.Parse(ReadOnlySpanArg&lt;char&gt;.Is(text =&gt; text.Length &gt; 2)).Returns(3);
/// imposter.Parse(ReadOnlySpanArg&lt;char&gt;.Any()).Called(Count.Once());
/// </code>
/// </remarks>
public sealed class ReadOnlySpanArg<T>
{
    private static readonly ReadOnlySpanArg<T> AnyInstance = new(_ => true, description: "any");

    private readonly Func<T[], bool> _matches;
    private readonly T[]? _expected;
    private readonly string? _description;

    private ReadOnlySpanArg(
        Func<T[], bool> matches,
        T[]? expected = null,
        string? description = null
    )
    {
        _matches = matches;
        _expected = expected;
        _description = description;
    }

    /// <summary>Returns a new matcher with the given diagnostic description.</summary>
    /// <param name="description">Text to display instead of the default description.</param>
    /// <exception cref="ArgumentNullException">The description is null.</exception>
    public ReadOnlySpanArg<T> Describe(string description) =>
        new(
            _matches,
            _expected,
            description ?? throw new ArgumentNullException(nameof(description))
        );

    /// <summary>Describes the matcher without evaluating its predicate or comparer.</summary>
    public override string ToString() =>
        _description ?? (_expected is null ? "<predicate>" : DiagnosticValue.Sequence(_expected));

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
    public static ReadOnlySpanArg<T> Any() => AnyInstance;

    /// <summary>
    /// Matches a span whose elements equal <paramref name="expected"/>, in order.
    /// </summary>
    /// <param name="expected">The elements to compare with; the matcher keeps its own copy.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="expected"/> is <see langword="null"/>.</exception>
    public static ReadOnlySpanArg<T> Is(params T[] expected) =>
        Is(expected, EqualityComparer<T>.Default);

    /// <summary>
    /// Matches a span whose elements equal <paramref name="expected"/>, in order, using <paramref name="comparer"/>.
    /// </summary>
    /// <param name="expected">The elements to compare with; the matcher keeps its own copy.</param>
    /// <param name="comparer">Compares the elements.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="expected"/> or <paramref name="comparer"/> is <see langword="null"/>.
    /// </exception>
    public static ReadOnlySpanArg<T> Is(T[] expected, IEqualityComparer<T> comparer)
    {
        var matcher = SpanElementMatchers.SequenceEqualTo(expected, comparer, out var elements);
        return new(matcher, elements);
    }

    /// <summary>
    /// Matches a span whose elements satisfy <paramref name="predicate"/>.
    /// </summary>
    /// <param name="predicate">Evaluates a copy of the span argument's elements.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="predicate"/> is <see langword="null"/>.</exception>
    public static ReadOnlySpanArg<T> Is(Func<T[], bool> predicate) =>
        new(SpanElementMatchers.Satisfying(predicate));

    /// <summary>
    /// Implicitly converts <see cref="Arg.Any"/> to a matcher for any span.
    /// </summary>
    public static implicit operator ReadOnlySpanArg<T>(AnyArgMarker _) => Any();
}
