using System.ComponentModel;

namespace Imposter.Abstractions;

/// <summary>A recorded property assignment, including a failed assignment.</summary>
/// <typeparam name="T">The stored value type.</typeparam>
[EditorBrowsable(EditorBrowsableState.Never)]
public abstract class PropertySetterInvocation<T> : InvocationRecord
{
    private InvocationBuffer<T>.Entry _entry;

    /// <summary>The assigned value.</summary>
    protected internal T StoredValue { get; }

    /// <summary>Creates a completed property assignment record.</summary>
    protected PropertySetterInvocation(T value, long sequence, Exception? exception)
        : base(sequence, exception) => StoredValue = value;

    internal void Bind(InvocationBuffer<T>.Entry entry) => _entry = entry;

    /// <inheritdoc />
    protected override bool IsVerified => _entry.IsValid ? _entry.IsVerified : base.IsVerified;

    /// <inheritdoc />
    protected override void MarkVerified()
    {
        if (_entry.IsValid)
            _entry.MarkVerified();
        else
            base.MarkVerified();
    }

    /// <summary>Formats the assignment without invoking user callbacks.</summary>
    public override string ToString() =>
        "set "
        + Member
        + " = "
        + StoredValue
        + (Exception is null ? string.Empty : " threw " + Exception);
}

/// <summary>Tracks the use and leniency of one property setter builder chain.</summary>
/// <typeparam name="T">The assigned value type.</typeparam>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class PropertySetterSetup<T>
{
    internal readonly Arg<T> Criteria;
    internal SetterSetupState State;

    /// <summary>Creates a setup for assignments matching the criteria.</summary>
    public PropertySetterSetup(Arg<T> criteria) => Criteria = criteria;

    /// <summary>Excludes this setup chain from unused setup verification.</summary>
    public void Lenient() => State.IsLenient = true;

    /// <summary>Marks this setup chain as executed.</summary>
    internal void MarkUsed() => State.MarkUsed();

    internal bool IsUnused => !State.IsLenient && Volatile.Read(ref State.Used) == 0;

    internal string Describe(string member) =>
        "set " + member + "(value: " + MatcherDescription.Format(Criteria) + ")";
}

/// <summary>Implements callbacks, history and invocation for generated property setters.</summary>
/// <typeparam name="T">The property's value type.</typeparam>
/// <remarks>Public for generated code only and may change with the generator in the same package.</remarks>
[EditorBrowsable(EditorBrowsableState.Never)]
public class PropertySetter<T> : IImposterVerificationSource
{
    private CallbackCollection<(PropertySetterSetup<T> Setup, Action<T> Callback)>? _callbacks;
    private InvocationBuffer<T>? _invocationHistory;
    private SetupCollection<PropertySetterSetup<T>>? _setups;
    private readonly PropertyValue<T>? _defaultValue;
    private readonly ImposterMode _mode;
    private readonly string _displayName;
    private bool _hasConfiguredSetter;
    private bool _useBaseImplementation;

    /// <summary>The owning imposter, assigned by generated initialization.</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public IImposter? Owner { get; }

    /// <summary>Creates the setter state.</summary>
    protected PropertySetter(PropertyValue<T>? defaultValue, ImposterMode mode, string displayName)
        : this(defaultValue, mode, displayName, null) { }

    /// <summary>Creates setter state associated with its owning imposter.</summary>
    protected PropertySetter(
        PropertyValue<T>? defaultValue,
        ImposterMode mode,
        string displayName,
        IImposter? owner
    )
    {
        _defaultValue = defaultValue;
        _mode = mode;
        _displayName = displayName;
        Owner = owner;
    }

    /// <summary>Registers a callback for matching assignments.</summary>
    public void Callback(Arg<T> criteria, Action<T> callback) =>
        Callback(new PropertySetterSetup<T>(criteria), callback);

    /// <summary>Registers a callback belonging to one builder chain.</summary>
    public void Callback(PropertySetterSetup<T> setup, Action<T> callback)
    {
        Register(setup);
        LazyInitializer.EnsureInitialized(ref _callbacks)!.Add((setup, callback));
        _hasConfiguredSetter = true;
    }

    private void Register(PropertySetterSetup<T> setup)
    {
        if (Interlocked.CompareExchange(ref setup.State.Registered, 1, 0) == 0)
            LazyInitializer.EnsureInitialized(ref _setups)!.Push(setup);
    }

    /// <summary>Selects the earliest matching assignment without materializing a record.</summary>
    /// <param name="criteria">The assignment matcher.</param>
    /// <param name="sequence">The exclusive lower entry-stamp bound.</param>
    /// <param name="match">The selection, or default when no entry matches.</param>
    public bool TryFindAfter(long sequence, Arg<T> criteria, out InvocationMatch match)
    {
        var cursor = _invocationHistory?.GetEnumerator() ?? default;
        var found = false;
        match = default;
        while (cursor.MoveNext())
        {
            var entry = cursor.Current;
            if (
                entry.Sequence > sequence
                && (!found || entry.Sequence < match.Sequence)
                && criteria.Matches(entry.Value)
            )
            {
                match = entry.Match;
                found = true;
            }
        }
        return found;
    }

    /// <summary>Verifies matching assignments.</summary>
    public void Called(Arg<T> criteria, Count count)
    {
        var snapshot = _invocationHistory?.GetEnumerator() ?? default;
        var cursor = snapshot;
        var actual = 0;
        while (cursor.MoveNext())
        {
            if (criteria.Matches(cursor.Current.Value))
                actual++;
        }
        if (count.Matches(actual))
        {
            cursor = snapshot;
            while (cursor.MoveNext())
                if (criteria.Matches(cursor.Current.Value))
                    cursor.Current.MarkVerified();
            return;
        }
        var performed = new List<string>();
        cursor = snapshot;
        while (cursor.MoveNext())
            performed.Add(Describe(cursor.Current));
        throw new VerificationFailedException(
            count,
            actual,
            string.Join(Environment.NewLine, performed)
        );
    }

    /// <summary>Configures assignments to call the base setter.</summary>
    public void UseBaseImplementation() =>
        UseBaseImplementation(new PropertySetterSetup<T>(Arg<T>.Any()));

    /// <summary>Configures base invocation belonging to the supplied builder chain.</summary>
    public void UseBaseImplementation(PropertySetterSetup<T> setup)
    {
        Register(setup);
        setup.State.UsesBase = true;
        _hasConfiguredSetter = true;
        _useBaseImplementation = true;
    }

    /// <summary>Performs an assignment and records its outcome.</summary>
    public void Set(T value, Action<T>? baseImplementation = null)
    {
        var sequence = InvocationSequence.Next();
        var capturedValue = CaptureInvocationValue(value);
        Exception? failure = null;
        try
        {
            if (!RecordAndShouldCallBase(value))
                return;
            if (baseImplementation is null)
                throw MissingImposter();
            baseImplementation(value);
        }
        catch (Exception exception)
        {
            failure = exception;
            throw;
        }
        finally
        {
            RecordInvocation(capturedValue, sequence, failure);
        }
    }

    /// <summary>Runs the assignment behavior; generated init accessors record its complete outcome themselves.</summary>
    protected bool RecordAndShouldCallBase(T value)
    {
        if (_mode == ImposterMode.Explicit && !_hasConfiguredSetter)
            throw MissingImposter();
        if (_callbacks is not null)
            foreach (var registration in _callbacks)
                if (registration.Setup.Criteria.Matches(value))
                {
                    registration.Setup.MarkUsed();
                    registration.Callback(value);
                }
        if (_useBaseImplementation)
        {
            if (_setups is not null)
                foreach (var setup in _setups)
                    if (setup.State.UsesBase)
                        setup.MarkUsed();
            return true;
        }
        if (_defaultValue is { IsOn: true })
            _defaultValue.Write(value);
        return false;
    }

    /// <summary>Records the result of an assignment, including generated direct base init assignments.</summary>
    public void RecordInvocation(T value, long sequence, Exception? exception) =>
        LazyInitializer.EnsureInitialized(ref _invocationHistory)!.Add(value, sequence, exception);

    /// <summary>Captures an entry value; generated span setters override this to isolate mutable storage.</summary>
    public virtual T CaptureInvocationValue(T value) => value;

    /// <summary>Creates the generated property's concrete invocation record.</summary>
    protected virtual PropertySetterInvocation<T> CreateInvocation(
        T value,
        long sequence,
        Exception? exception
    ) => new RuntimeInvocation(_displayName, value, sequence, exception);

    /// <summary>Clears history without changing setups or their usage.</summary>
    public void ClearInvocations() => _invocationHistory?.Clear();

    /// <summary>Returns an entry-ordered snapshot of assignments.</summary>
    public IReadOnlyList<IImposterInvocation> Snapshot() =>
        MatchingInvocations<IImposterInvocation>(Arg<T>.Any());

    /// <summary>Returns assignments, optionally leaving ordering to an enclosing aggregate.</summary>
    /// <param name="orderBySequence">Whether to order this snapshot by entry stamp.</param>
    public IReadOnlyList<IImposterInvocation> Snapshot(bool orderBySequence) =>
        MatchingInvocations<IImposterInvocation>(Arg<T>.Any(), orderBySequence);

    /// <summary>Returns matching assignments as the generated record type.</summary>
    public IReadOnlyList<TInvocation> MatchingInvocations<TInvocation>(Arg<T> criteria)
        where TInvocation : IImposterInvocation => MatchingInvocations<TInvocation>(criteria, true);

    private IReadOnlyList<TInvocation> MatchingInvocations<TInvocation>(
        Arg<T> criteria,
        bool orderBySequence
    )
        where TInvocation : IImposterInvocation
    {
        var result = new List<TInvocation>();
        var history = _invocationHistory;
        if (history is not null)
            foreach (var entry in history)
                if (criteria.Matches(entry.Value))
                {
                    var invocation = CreateInvocation(entry.Value, entry.Sequence, entry.Exception);
                    invocation.Bind(entry);
                    IImposterInvocation record = invocation;
                    result.Add((TInvocation)record);
                }
        return InvocationSnapshots.Order(result, orderBySequence);
    }

    /// <inheritdoc />
    public IEnumerable<string> UncoveredInteractions()
    {
        var history = _invocationHistory;
        if (history is not null)
            foreach (var invocation in history)
                if (!invocation.IsVerified)
                    yield return Describe(invocation);
    }

    /// <inheritdoc />
    public IEnumerable<string> Interactions()
    {
        var history = _invocationHistory;
        if (history is not null)
            foreach (var invocation in history)
                yield return Describe(invocation);
    }

    /// <inheritdoc />
    public IEnumerable<string> UnusedSetups()
    {
        if (_setups is not null)
            foreach (var setup in _setups)
                if (setup.IsUnused)
                    yield return setup.Describe(_displayName);
    }

    private sealed class RuntimeInvocation(
        string member,
        T value,
        long sequence,
        Exception? exception
    ) : PropertySetterInvocation<T>(value, sequence, exception)
    {
        public override string Member => member;
    }

    private string Describe(InvocationBuffer<T>.Entry invocation) =>
        "set "
        + _displayName
        + " = "
        + "<"
        + (invocation.Value?.ToString() ?? "null")
        + ">"
        + (invocation.Exception is null ? string.Empty : " threw " + invocation.Exception);

    private MissingImposterException MissingImposter() => new(_displayName + " (setter)");
}
