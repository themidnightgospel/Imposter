using System.ComponentModel;

namespace Imposter.Abstractions;

/// <summary>Counter-only verification for accessors whose values cannot be retained.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class CounterVerification
{
    private InvocationCounter _counter;
    private volatile bool _configured;
    private volatile bool _used;
    private volatile bool _lenient;

    /// <summary>Records an interaction without allocating.</summary>
    public void Record() => _counter.Record();

    /// <summary>Marks the accessor setup as configured.</summary>
    public void Configure() => _configured = true;

    /// <summary>Marks the accessor setup as used.</summary>
    public void Use()
    {
        if (!_used && _configured)
            _used = true;
    }

    /// <summary>Excludes the accessor setup from unused-setup verification.</summary>
    public void Lenient() => _lenient = true;

    /// <summary>Verifies and covers the number of interactions observed by this check.</summary>
    /// <param name="expected">The expected count.</param>
    public void Verify(Count expected)
    {
        if (!_counter.Verify(expected, out var count))
            throw new VerificationFailedException(expected, count);
    }

    /// <summary>Clears history and coverage without affecting configured setup usage.</summary>
    public void Clear() => _counter.Clear();

    /// <summary>Describes recorded interactions.</summary>
    /// <param name="member">The accessor description.</param>
    public IEnumerable<string> Interactions(string member)
    {
        var count = _counter.Count;
        if (count > 0)
            yield return member + ": " + count + " interaction(s)";
    }

    /// <summary>Describes interactions not covered by a successful check.</summary>
    /// <param name="member">The accessor description.</param>
    public IEnumerable<string> UncoveredInteractions(string member)
    {
        var count = _counter.Uncovered;
        if (count > 0)
            yield return member + ": " + count + " uncovered interaction(s)";
    }

    /// <summary>Describes an unused configured setup unless it is lenient.</summary>
    /// <param name="member">The accessor description.</param>
    public IEnumerable<string> UnusedSetups(string member)
    {
        if (_configured && !_used && !_lenient)
            yield return member;
    }
}
