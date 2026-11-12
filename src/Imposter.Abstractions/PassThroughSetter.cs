using System.ComponentModel;

namespace Imposter.Abstractions;

/// <summary>Stores verification state for setters with values that cannot be retained.</summary>
/// <typeparam name="TInvocation">The generated invocation record without a retained value.</typeparam>
[EditorBrowsable(EditorBrowsableState.Never)]
public abstract class PassThroughSetter<TInvocation> : IImposterVerificationSource
    where TInvocation : InvocationRecord
{
    private InvocationHistory<TInvocation>? _history;
    private SetupCollection<SetterSetup>? _setups;
    private readonly ImposterMode _mode;
    private readonly string _member;
    private bool _configured;
    private bool _useBase;

    /// <summary>Creates setter state.</summary>
    protected PassThroughSetter(ImposterMode mode, string member)
        : this(mode, member, null) { }

    /// <summary>Creates setter state associated with its owning imposter.</summary>
    protected PassThroughSetter(ImposterMode mode, string member, IImposter? owner)
    {
        _mode = mode;
        _member = member;
        Owner = owner;
    }

    /// <summary>The owning imposter.</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public IImposter? Owner { get; }

    /// <summary>Registers an outcome on a setup chain.</summary>
    public void Register(SetterSetup setup)
    {
        if (Interlocked.CompareExchange(ref setup.State.Registered, 1, 0) == 0)
            LazyInitializer.EnsureInitialized(ref _setups)!.Push(setup);
        _configured = true;
    }

    /// <summary>Configures unconditional base setter delegation.</summary>
    public void UseBaseImplementation() => UseBaseImplementation(new SetterSetup());

    /// <summary>Configures base setter delegation.</summary>
    public void UseBaseImplementation(SetterSetup setup)
    {
        setup.State.UsesBase = true;
        Register(setup);
        _useBase = true;
    }

    /// <summary>Checks explicit setup requirements.</summary>
    protected void EnsureConfigured()
    {
        if (_mode == ImposterMode.Explicit && !_configured)
            throw new MissingImposterException(_member);
    }

    /// <summary>Marks base setup execution and returns whether to delegate.</summary>
    protected bool ShouldUseBase()
    {
        if (!_useBase)
            return false;
        if (_setups is not null)
            foreach (var setup in _setups)
                if (setup.State.UsesBase)
                    setup.MarkUsed();
        return true;
    }

    /// <summary>Creates the generated member-specific record.</summary>
    protected abstract TInvocation CreateInvocation(long sequence, Exception? exception);

    /// <summary>Records completion, including throwing calls.</summary>
    public void RecordInvocation(long sequence, Exception? exception) =>
        LazyInitializer
            .EnsureInitialized(ref _history)!
            .Push(CreateInvocation(sequence, exception));

    /// <summary>Clears history without modifying setups.</summary>
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

    /// <summary>Selects the earliest assignment after an entry stamp without copying history.</summary>
    /// <param name="sequence">The exclusive lower entry-stamp bound.</param>
    /// <param name="match">The selection, or default when no entry matches.</param>
    public bool TryFindAfter(long sequence, out InvocationMatch match)
    {
        var cursor = _history?.GetEnumerator() ?? default;
        var found = false;
        match = default;
        while (cursor.MoveNext())
        {
            var record = cursor.Current;
            if (record.Sequence > sequence && (!found || record.Sequence < match.Sequence))
            {
                match = InvocationMatch.FromInvocation(record);
                found = true;
            }
        }
        return found;
    }

    /// <summary>Verifies and covers the observed setter calls.</summary>
    public void Called(Count count)
    {
        var head = _history?.Head;
        var actual = 0;
        for (var record = head; record is not null; record = (TInvocation?)record.Next)
            actual++;
        if (!count.Matches(actual))
            throw new VerificationFailedException(count, actual);
        for (var record = head; record is not null; record = (TInvocation?)record.Next)
            ((IVerifiableInvocation)record).MarkVerified();
    }

    /// <inheritdoc/>
    public IEnumerable<string> Interactions()
    {
        if (_history is not null)
            foreach (var record in _history)
                yield return Describe(record);
    }

    /// <inheritdoc/>
    public IEnumerable<string> UncoveredInteractions()
    {
        for (var record = _history?.Head; record is not null; record = (TInvocation?)record.Next)
        {
            if (!((IVerifiableInvocation)record).IsVerified)
                yield return Describe(record);
        }
    }

    /// <inheritdoc/>
    public IEnumerable<string> UnusedSetups()
    {
        if (_setups is null)
            yield break;
        foreach (var setup in _setups)
            if (Volatile.Read(ref setup.State.Used) == 0 && !setup.State.IsLenient)
                yield return "set " + _member + "(value: <not captured>)";
    }

    private static string Describe(TInvocation record) =>
        "set "
        + record.Member
        + "(value: <not captured>)"
        + (record.Exception is null ? string.Empty : " threw " + record.Exception);
}
