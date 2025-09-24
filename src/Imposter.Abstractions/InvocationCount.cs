namespace Imposter.Abstractions;

/// <summary>
/// TODO Add docs
/// </summary>
public sealed class Count
{
    private readonly int? _exactly;
    private readonly int? _atLeast;
    private readonly int? _atMost;

    private Count(int? exactly, int? atLeast, int? atMost)
    {
        _exactly = exactly;
        _atLeast = atLeast;
        _atMost = atMost;
    }

    public static Count Exactly(int count)
    {
        if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
        return new Count(exactly: count, atLeast: null, atMost: null);
    }

    public static Count AtLeast(int count)
    {
        if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
        return new Count(exactly: null, atLeast: count, atMost: null);
    }

    public static Count AtMost(int count)
    {
        if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
        return new Count(exactly: null, atLeast: null, atMost: count);
    }

    public static Count Never => Exactly(0);

    public static Count Any = new Count(null, null, null);

    public static Count Once = Exactly(1);

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