using System.ComponentModel;

namespace Imposter.Abstractions;

/// <summary>A recorded property assignment, including a failed assignment.</summary>
/// <typeparam name="T">The stored value type.</typeparam>
[EditorBrowsable(EditorBrowsableState.Never)]
public abstract class PropertySetterInvocation<T> : InvocationRecord
{
    /// <summary>The assigned value.</summary>
    protected internal T StoredValue { get; }

    /// <summary>Creates a completed property assignment record.</summary>
    protected PropertySetterInvocation(T value, long sequence, Exception? exception)
        : base(sequence, exception) => StoredValue = value;

    /// <summary>Formats the assignment without invoking user callbacks.</summary>
    public override string ToString() =>
        "set "
        + Member
        + " = "
        + StoredValue
        + (Exception is null ? string.Empty : " threw " + Exception);
}

/// <summary>Implements callbacks, history and invocation for generated property setters.</summary>
/// <typeparam name="T">The property's value type.</typeparam>
/// <remarks>Public for generated code only and may change with the generator in the same package.</remarks>
[EditorBrowsable(EditorBrowsableState.Never)]
public class PropertySetter<T>
{
    private CallbackCollection<(Arg<T> Criteria, Action<T> Callback)>? _callbacks;
    private InvocationBuffer<T>? _invocationHistory;
    private readonly PropertyValue<T>? _defaultValue;
    private readonly ImposterMode _mode;
    private readonly string _displayName;
    private bool _hasConfiguredSetter;
    private bool _useBaseImplementation;

    /// <summary>Creates the setter state.</summary>
    protected PropertySetter(PropertyValue<T>? defaultValue, ImposterMode mode, string displayName)
    {
        _defaultValue = defaultValue;
        _mode = mode;
        _displayName = displayName;
    }

    /// <summary>Registers a callback for matching assignments.</summary>
    public void Callback(Arg<T> criteria, Action<T> callback)
    {
        LazyInitializer.EnsureInitialized(ref _callbacks)!.Add((criteria, callback));
        _hasConfiguredSetter = true;
    }

    /// <summary>Verifies the number of matching assignments.</summary>
    public void Called(Arg<T> criteria, Count count)
    {
        var snapshot = _invocationHistory?.GetEnumerator() ?? default;
        var cursor = snapshot;
        var actual = 0;
        while (cursor.MoveNext())
            if (criteria.Matches(cursor.Current.Value))
                actual++;
        if (count.Matches(actual))
            return;
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
    public void UseBaseImplementation()
    {
        _hasConfiguredSetter = true;
        _useBaseImplementation = true;
    }

    private MissingImposterException MissingImposter() => new(_displayName + " (setter)");

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
                if (registration.Criteria.Matches(value))
                {
                    registration.Callback(value);
                }
        if (_useBaseImplementation)
        {
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

    /// <summary>Clears recorded interactions while preserving configured behavior.</summary>
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
                    IImposterInvocation record = invocation;
                    result.Add((TInvocation)record);
                }
        return InvocationSnapshots.Order(result, orderBySequence);
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
}
