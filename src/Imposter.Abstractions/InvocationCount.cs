namespace Imposter.Abstractions;

/// <summary>
/// Represents a constraint on how many times an event or action should occur.
/// Supports specifying exact, minimum, or maximum counts.
/// </summary>
public sealed class Count
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
        if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
        return new Count(exactly: count, atLeast: null, atMost: null);
    }

    /// <summary>
    /// Creates a <see cref="Count"/> representing a minimum number of occurrences.
    /// </summary>
    /// <param name="count">The minimum number of times the action should occur. Must be non-negative.</param>
    /// <returns>A <see cref="Count"/> instance representing the minimum count.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="count"/> is negative.</exception>
    public static Count AtLeast(int count)
    {
        if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
        return new Count(exactly: null, atLeast: count, atMost: null);
    }

    /// <summary>
    /// Creates a <see cref="Count"/> representing a maximum number of occurrences.
    /// </summary>
    /// <param name="count">The maximum number of times the action should occur. Must be non-negative.</param>
    /// <returns>A <see cref="Count"/> instance representing the maximum count.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="count"/> is negative.</exception>
    public static Count AtMost(int count)
    {
        if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
        return new Count(exactly: null, atLeast: null, atMost: count);
    }

    /// <summary>
    /// Gets a <see cref="Count"/> that represents "never" — zero occurrences.
    /// </summary>
    public static Count Never => Exactly(0);

    /// <summary>
    /// Gets a <see cref="Count"/> that allows any number of occurrences.
    /// </summary>
    public static Count Any = new Count(null, null, null);

    /// <summary>
    /// Gets a <see cref="Count"/> that represents exactly one occurrence.
    /// </summary>
    public static Count Once = Exactly(1);

    /// <summary>
    /// Determines whether the specified actual count matches this constraint.
    /// </summary>
    /// <param name="actualCount">The actual number of occurrences.</param>
    /// <returns><see langword="true"/> if the actual count satisfies this constraint; otherwise, <see langword="false"/>.</returns>
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
}
