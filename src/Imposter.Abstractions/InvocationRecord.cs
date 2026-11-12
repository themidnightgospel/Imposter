using System.ComponentModel;

namespace Imposter.Abstractions;

/// <summary>Shared storage for generated accessor and event invocation records.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public abstract class InvocationRecord : IVerifiableInvocation
{
    internal InvocationRecord? Next;

    // Entry stamps are positive; the sign bit tracks coverage without growing every record.
    private long _sequenceAndCoverage;

    /// <summary>Initializes an invocation record.</summary>
    /// <param name="sequence">The invocation entry stamp.</param>
    /// <param name="exception">The synchronous exception, if any.</param>
    protected InvocationRecord(long sequence, Exception? exception = null)
    {
        _sequenceAndCoverage = sequence;
        Exception = exception;
    }

    /// <inheritdoc />
    public abstract string Member { get; }

    /// <inheritdoc />
    public long Sequence => Volatile.Read(ref _sequenceAndCoverage) & long.MaxValue;

    /// <inheritdoc />
    public Exception? Exception { get; }

    bool IVerifiableInvocation.IsVerified => IsVerified;

    void IVerifiableInvocation.MarkVerified() => MarkVerified();

    /// <summary>Gets the coverage state for verification infrastructure.</summary>
    protected virtual bool IsVerified => Volatile.Read(ref _sequenceAndCoverage) < 0;

    /// <summary>Marks the record covered for verification infrastructure.</summary>
    protected virtual void MarkVerified()
    {
        if (IsVerified)
            return;
        Volatile.Write(
            ref _sequenceAndCoverage,
            Volatile.Read(ref _sequenceAndCoverage) | long.MinValue
        );
    }
}
