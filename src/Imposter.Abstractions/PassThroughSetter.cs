using System.ComponentModel;

namespace Imposter.Abstractions;

/// <summary>Stores verification state for setters with values that cannot be retained.</summary>
/// <typeparam name="TInvocation">The generated invocation record without a retained value.</typeparam>
[EditorBrowsable(EditorBrowsableState.Never)]
public abstract class PassThroughSetter<TInvocation>
    where TInvocation : InvocationRecord
{
    private InvocationHistory<TInvocation>? _history;
    private readonly ImposterMode _mode;
    private readonly string _member;
    private bool _configured;
    private bool _useBase;

    /// <summary>Creates setter state.</summary>
    protected PassThroughSetter(ImposterMode mode, string member)
    {
        _mode = mode;
        _member = member;
    }

    /// <summary>Records that a setter callback has been configured.</summary>
    public void Register() => _configured = true;

    /// <summary>Configures delegation to the base setter.</summary>
    public void UseBaseImplementation()
    {
        _configured = true;
        _useBase = true;
    }

    /// <summary>Reports whether the base setter should run.</summary>
    protected bool ShouldUseBase() => _useBase;

    /// <summary>Verifies the number of assignments.</summary>
    public void Called(Count count)
    {
        var actual = 0;
        for (var record = _history?.Head; record is not null; record = (TInvocation?)record.Next)
            actual++;
        if (!count.Matches(actual))
            throw new VerificationFailedException(count, actual);
    }

    /// <summary>Checks explicit setup requirements.</summary>
    protected void EnsureConfigured()
    {
        if (_mode == ImposterMode.Explicit && !_configured)
            throw new MissingImposterException(_member);
    }

    /// <summary>Creates the generated member-specific record.</summary>
    protected abstract TInvocation CreateInvocation(long sequence, Exception? exception);

    /// <summary>Records completion, including throwing calls.</summary>
    public void RecordInvocation(long sequence, Exception? exception) =>
        LazyInitializer
            .EnsureInitialized(ref _history)!
            .Push(CreateInvocation(sequence, exception));

    /// <summary>Clears recorded interactions while preserving configured behavior.</summary>
    public void ClearInvocations() => _history?.Clear();

    /// <summary>Returns a detached ordered snapshot.</summary>
    public IReadOnlyList<IImposterInvocation> Snapshot() => Snapshot<IImposterInvocation>();

    /// <summary>Returns assignments, optionally leaving ordering to an enclosing aggregate.</summary>
    /// <param name="orderBySequence">Whether to order this snapshot by entry stamp.</param>
    public IReadOnlyList<IImposterInvocation> Snapshot(bool orderBySequence) =>
        Snapshot<IImposterInvocation>(orderBySequence);

    /// <summary>Returns a detached ordered snapshot in the requested public record type.</summary>
    public IReadOnlyList<TRecord> Snapshot<TRecord>()
        where TRecord : IImposterInvocation => Snapshot<TRecord>(true);

    private IReadOnlyList<TRecord> Snapshot<TRecord>(bool orderBySequence)
        where TRecord : IImposterInvocation
    {
        var result = new List<TRecord>();
        if (_history is not null)
            foreach (var record in _history)
                result.Add((TRecord)(object)record);
        return InvocationSnapshots.Order(result, orderBySequence);
    }
}
