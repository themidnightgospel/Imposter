namespace Imposter.Abstractions;

/// <summary>
/// Represents a constraint on how many times an invocation (method/property/indexer/event)
/// is expected to occur.
/// Supports specifying exact, minimum, or maximum counts.
/// </summary>
/// <remarks>
/// Use instances of <see cref="Count"/> with verification helpers such as <c>.Called(Count.Exactly(n))</c>
/// to assert invocation frequency.
///
/// Examples:
/// <code>
/// sut.Do().Called(Count.Once());          // exactly 1 time
/// sut.Do().Called(Count.Never());         // zero times
/// sut.Do().Called(Count.AtLeast(3));      // three or more times
/// sut.Do().Called(Count.AtMost(2));       // two or fewer times
/// sut.Do().Called(Count.Any);             // any number of times
/// </code>
/// </remarks>
public sealed class Count : global::System.IEquatable<Count>
{
    /// <summary>
    /// The exact number of times the action should occur, if specified.
    /// </summary>
    private readonly int? _exactly;

    /// <summary>
    /// The minimum number of times the action should occur, if specified.
    /// </summary>
    private readonly int? _atLeast;

    /// <summary>
    /// The maximum number of times the action should occur, if specified.
    /// </summary>
    private readonly int? _atMost;

    /// <summary>
    /// Initializes a new instance of the <see cref="Count"/> class with the given constraints.
    /// </summary>
    /// <param name="exactly">The exact count, if applicable.</param>
    /// <param name="atLeast">The minimum count, if applicable.</param>
    /// <param name="atMost">The maximum count, if applicable.</param>
    private Count(int? exactly, int? atLeast, int? atMost)
    {
        _exactly = exactly;
        _atLeast = atLeast;
        _atMost = atMost;
    }

    /// <summary>
    /// Creates a <see cref="Count"/> representing an exact number of occurrences.
    /// </summary>
    /// <param name="count">The exact number of times the action should occur. Must be non-negative.</param>
    /// <returns>A <see cref="Count"/> instance representing the exact number of times.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="count"/> is negative.</exception>
    public static Count Exactly(int count)
    {
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count));
        return new Count(exactly: count, atLeast: null, atMost: null);
    }

    /// <summary>
    /// Creates a <see cref="Count"/> representing an exact number of occurrences.
    /// </summary>
    /// <param name="count">The exact number of times the action should occur. Must be non-negative.</param>
    /// <returns>A <see cref="Count"/> instance representing the exact number of times.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="count"/> is negative.</exception>
    public static Count Times(int count) => Exactly(count);

    /// <summary>
    /// Creates a <see cref="Count"/> representing a minimum number of occurrences.
    /// </summary>
    /// <param name="count">The minimum number of times the action should occur. Must be non-negative.</param>
    /// <returns>A <see cref="Count"/> instance representing the minimum count.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="count"/> is negative.</exception>
    public static Count AtLeast(int count)
    {
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count));
        return new Count(exactly: null, atLeast: count, atMost: null);
    }

    /// <summary>
    /// Gets a <see cref="Count"/> representing at least one occurrence.
    /// </summary>
    public static Count AtLeastOnce() => AtLeast(1);

    /// <summary>
    /// Creates a <see cref="Count"/> representing a maximum number of occurrences.
    /// </summary>
    /// <param name="count">The maximum number of times the action should occur. Must be non-negative.</param>
    /// <returns>A <see cref="Count"/> instance representing the maximum count.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="count"/> is negative.</exception>
    public static Count AtMost(int count)
    {
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count));
        return new Count(exactly: null, atLeast: null, atMost: count);
    }

    /// <summary>
    /// Gets a <see cref="Count"/> representing at most one occurrence.
    /// </summary>
    public static Count AtMostOnce() => AtMost(1);

    /// <summary>
    /// Creates a <see cref="Count"/> representing a minimum and maximum inclusive range.
    /// </summary>
    /// <param name="atLeast">The minimum number of occurrences. Must be non-negative.</param>
    /// <param name="atMost">The maximum number of occurrences. Must be non-negative.</param>
    /// <returns>A <see cref="Count"/> instance representing the inclusive range.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if either parameter is negative or if <paramref name="atLeast"/> is greater than <paramref name="atMost"/>.
    /// </exception>
    public static Count Between(int atLeast, int atMost)
    {
        if (atLeast < 0)
            throw new ArgumentOutOfRangeException(nameof(atLeast));
        if (atMost < 0)
            throw new ArgumentOutOfRangeException(nameof(atMost));
        if (atLeast > atMost)
            throw new ArgumentOutOfRangeException(
                nameof(atLeast),
                "The minimum count cannot be greater than the maximum count."
            );

        return new Count(exactly: null, atLeast: atLeast, atMost: atMost);
    }

    /// <summary>
    /// Gets a <see cref="Count"/> that represents "never" — zero occurrences.
    /// </summary>
    public static Count Never() => Exactly(0);

    /// <summary>
    /// Gets a <see cref="Count"/> that allows any number of occurrences.
    /// </summary>
    /// <remarks>
    /// Semantically equivalent to having no constraint.
    /// </remarks>
    public static readonly Count Any = new Count(null, null, null);

    /// <summary>
    /// Gets a <see cref="Count"/> that represents exactly one occurrence.
    /// </summary>
    public static Count Once() => Exactly(1);

    /// <summary>
    /// Gets a <see cref="Count"/> that represents exactly two occurrences.
    /// </summary>
    public static Count Twice() => Exactly(2);

    /// <summary>
    /// Determines whether the specified actual count matches this constraint.
    /// </summary>
    /// <param name="actualCount">The actual number of occurrences.</param>
    /// <returns><see langword="true"/> if the actual count satisfies this constraint; otherwise, <see langword="false"/>.</returns>
    /// <remarks>
    /// Matching rules:
    /// - If <see cref="Exactly(int)"/> was used, the <paramref name="actualCount"/> must equal the exact value.
    /// - If <see cref="AtLeast(int)"/> is set, <paramref name="actualCount"/> must be greater than or equal to the threshold.
    /// - If <see cref="AtMost(int)"/> is set, <paramref name="actualCount"/> must be less than or equal to the threshold.
    /// - If multiple constraints were combined internally, all must be satisfied.
    /// </remarks>
    public bool Matches(int actualCount)
    {
        if (_exactly.HasValue)
            return actualCount == _exactly.Value;

        if (_atLeast.HasValue && actualCount < _atLeast.Value)
            return false;

        if (_atMost.HasValue && actualCount > _atMost.Value)
            return false;

        return true; // no constraints or all passed
    }

    /// <summary>
    /// Returns a human-readable description of the count constraint.
    /// </summary>
    /// <returns>A string describing the count constraint (e.g., "at least 2 times").</returns>
    public override string ToString()
    {
        if (_exactly.HasValue)
            return $"exactly {_exactly.Value} time(s)";
        if (_atLeast.HasValue && _atMost.HasValue)
            return $"between {_atLeast.Value} and {_atMost.Value} times";
        if (_atLeast.HasValue)
            return $"at least {_atLeast.Value} time(s)";
        if (_atMost.HasValue)
            return $"at most {_atMost.Value} time(s)";
        return "any number of times";
    }

    /// <inheritdoc />
    public override bool Equals(object? obj) => Equals(obj as Count);

    /// <inheritdoc />
    public bool Equals(Count? other)
    {
        if (ReferenceEquals(this, other))
            return true;
        if (ReferenceEquals(other, null))
            return false;

        return _exactly == other._exactly && _atLeast == other._atLeast && _atMost == other._atMost;
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        unchecked
        {
            var hash = 17;
            hash = (hash * 31) + (_exactly?.GetHashCode() ?? 0);
            hash = (hash * 31) + (_atLeast?.GetHashCode() ?? 0);
            hash = (hash * 31) + (_atMost?.GetHashCode() ?? 0);
            return hash;
        }
    }

    /// <summary>
    /// Determines whether the specified counts are equal.
    /// </summary>
    public static bool operator ==(Count? left, Count? right) =>
        left is null ? right is null : left.Equals(right);

    /// <summary>
    /// Determines whether the specified counts are not equal.
    /// </summary>
    public static bool operator !=(Count? left, Count? right) => !(left == right);
}
