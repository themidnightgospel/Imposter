using System.ComponentModel;

namespace Imposter.Abstractions;

/// <summary>Shared storage for generated accessor and event invocation records.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public abstract class InvocationRecord : IImposterInvocation
{
    internal InvocationRecord? Next;

    private readonly long _sequence;

    /// <summary>Initializes an invocation record.</summary>
    /// <param name="sequence">The invocation entry stamp.</param>
    /// <param name="exception">The synchronous exception, if any.</param>
    protected InvocationRecord(long sequence, Exception? exception = null)
    {
        _sequence = sequence;
        Exception = exception;
    }

    /// <inheritdoc />
    public abstract string Member { get; }

    /// <inheritdoc />
    public long Sequence => _sequence;

    /// <inheritdoc />
    public Exception? Exception { get; }
}
