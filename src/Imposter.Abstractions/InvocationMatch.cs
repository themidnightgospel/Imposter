using System.ComponentModel;

namespace Imposter.Abstractions;

/// <summary>A captured ordered-verification selection without materializing an invocation snapshot.</summary>
/// <remarks>Generated-code support. A selection retains its original history entry across clearing.</remarks>
[EditorBrowsable(EditorBrowsableState.Never)]
public readonly struct InvocationMatch
{
    private readonly IVerifiableInvocation? _invocation;
    private readonly IInvocationCoverage? _coverage;
    private readonly int _index;

    private InvocationMatch(IVerifiableInvocation invocation)
    {
        _invocation = invocation;
        _coverage = null;
        _index = 0;
        Sequence = invocation.Sequence;
    }

    internal InvocationMatch(IInvocationCoverage coverage, int index, long sequence)
    {
        _invocation = null;
        _coverage = coverage;
        _index = index;
        Sequence = sequence;
    }

    /// <summary>The entry stamp captured by the selection.</summary>
    public long Sequence { get; }

    /// <summary>Selects an existing invocation without allocating a wrapper.</summary>
    /// <param name="invocation">The invocation whose coverage will be marked on success.</param>
    public static InvocationMatch FromInvocation(IVerifiableInvocation invocation)
    {
        if (invocation is null)
            throw new ArgumentNullException(nameof(invocation));
        return new InvocationMatch(invocation);
    }

    internal bool IsValid => _invocation is not null || _coverage is not null;

    internal void MarkVerified()
    {
        if (_invocation is not null)
            _invocation.MarkVerified();
        else
            _coverage!.MarkVerified(_index);
    }
}

internal interface IInvocationCoverage
{
    void MarkVerified(int index);
}
