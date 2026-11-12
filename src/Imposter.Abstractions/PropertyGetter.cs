using System.ComponentModel;

namespace Imposter.Abstractions;

/// <summary>Implements storage, sequencing and invocation for generated property getters.</summary>
/// <typeparam name="T">The property's value type.</typeparam>
/// <remarks>Public for generated code only and may change with the generator in the same package.</remarks>
[EditorBrowsable(EditorBrowsableState.Never)]
public class PropertyGetter<T> : IImposterVerificationSource
{
    /// <summary>Gets the generated owner used for failure-only diagnostics.</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public IImposter? Owner { get; }

    private ReturnValueSequence<Func<Func<T>?, T>?>? _returnValues;
    private CallbackCollection<Action>? _callbacks;
    private object? _outcomeDescriptions;
    private InvocationCounter _counter;
    private volatile bool _isUsed;
    private volatile bool _isLenient;
    private bool _hasConfiguredCallback;
    private readonly PropertyValue<T> _defaultValue;
    private readonly ImposterMode _mode;
    private readonly string _displayName;
    private bool _hasConfiguredReturn;

    /// <summary>Constructor</summary>
    /// <param name="defaultValue">The value shared with the setter.</param>
    /// <param name="mode">Whether an explicit outcome is required.</param>
    /// <param name="displayName">The property name used in diagnostics.</param>
    protected PropertyGetter(PropertyValue<T> defaultValue, ImposterMode mode, string displayName)
        : this(defaultValue, mode, displayName, null) { }

    /// <summary>Creates getter state associated with its generated owner.</summary>
    /// <param name="defaultValue">The value shared with the setter.</param>
    /// <param name="mode">Whether an explicit outcome is required.</param>
    /// <param name="displayName">The property name used in diagnostics.</param>
    /// <param name="owner">The owning imposter, when available.</param>
    protected PropertyGetter(
        PropertyValue<T> defaultValue,
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

    /// <summary>Appends a constant outcome.</summary>
    /// <param name="value">The value to return.</param>
    protected void Returns(T value) => AddReturnValue(_ => value, "Returns");

    /// <summary>Appends a generated outcome.</summary>
    /// <param name="valueGenerator">The factory to invoke on each read.</param>
    protected void Returns(Func<T> valueGenerator) =>
        AddReturnValue(_ => valueGenerator(), "Returns");

    /// <summary>Appends an exception outcome.</summary>
    /// <param name="exception">The exception to throw.</param>
    protected void Throws(Exception exception) => AddReturnValue(_ => throw exception, "Throws");

    /// <summary>Appends an outcome that constructs an exception on each read.</summary>
    /// <typeparam name="TException">The exception type.</typeparam>
    protected void Throws<TException>()
        where TException : Exception, new() =>
        AddReturnValue(_ => throw new TException(), DiagnosticValue.Throws<TException>());

    /// <summary>Registers a callback without configuring an outcome.</summary>
    /// <param name="callback">The callback to invoke before the outcome.</param>
    protected void Callback(Action callback)
    {
        LazyInitializer.EnsureInitialized(ref _callbacks)!.Add(callback);
        DiagnosticValue.AddOutcome(ref _outcomeDescriptions, "Callback");
        _hasConfiguredCallback = true;
    }

    /// <summary>Verifies the number of reads, including reads whose callbacks or outcomes threw.</summary>
    /// <param name="count">The expected count.</param>
    protected void Called(Count count) => Called(count, null);

    /// <summary>Verifies the number of reads and attaches a reason to a failure.</summary>
    /// <param name="count">The expected count.</param>
    /// <param name="because">An optional reason for the expectation.</param>
    protected void Called(Count count, string? because)
    {
        if (!_counter.Verify(count, out var actual))
            throw new VerificationFailedException(
                _displayName + " (getter)",
                _displayName + " (getter)",
                count,
                actual,
                actual == 0 ? null : actual + " read(s)",
                because,
                actual == 0 && Owner is not null ? DiagnosticValue.OtherInteractions(Owner) : null
            );
    }

    /// <summary>Clears recorded reads and their coverage while preserving configured outcomes and usage.</summary>
    public void ClearInvocations() => _counter.Clear();

    /// <summary>Excludes the getter setup from unused-setup verification.</summary>
    protected void MarkLenient() => _isLenient = true;

    IEnumerable<string> IImposterVerificationSource.UncoveredInteractions()
    {
        var uncovered = _counter.Uncovered;
        if (uncovered > 0)
            yield return _displayName + " (getter): " + uncovered + " uncovered read(s)";
    }

    IEnumerable<string> IImposterVerificationSource.Interactions()
    {
        var count = _counter.Count;
        if (count > 0)
            yield return _displayName + " (getter): " + count + " read(s)";
    }

    IEnumerable<string> IImposterVerificationSource.UnusedSetups()
    {
        if ((_hasConfiguredReturn || _hasConfiguredCallback) && !_isUsed && !_isLenient)
            yield return _displayName
                + " (getter) -> "
                + DiagnosticValue.Outcomes(_outcomeDescriptions);
    }

    /// <summary>Appends an outcome that calls the base getter.</summary>
    public void UseBaseImplementation() =>
        AddReturnValue(
            baseImplementation =>
                baseImplementation is not null ? baseImplementation() : throw MissingImposter(),
            "UseBaseImplementation"
        );

    private void AddReturnValue(Func<Func<T>?, T> valueGenerator, string description)
    {
        _defaultValue.TurnOff();
        LazyInitializer.EnsureInitialized(ref _returnValues)!.Add(valueGenerator);
        DiagnosticValue.AddOutcome(ref _outcomeDescriptions, description);
        _hasConfiguredReturn = true;
    }

    /// <summary>Records a read, invokes callbacks, and returns the current outcome.</summary>
    /// <param name="baseImplementation">The base getter, when available.</param>
    /// <returns>The property's value.</returns>
    public T Get(Func<T>? baseImplementation = null)
    {
        _counter.Record();
        if (_mode == ImposterMode.Explicit && !_hasConfiguredReturn)
            throw MissingImposter();
        if (!_isUsed && (_hasConfiguredReturn || _hasConfiguredCallback))
            _isUsed = true;
        if (_callbacks is not null)
            foreach (var callback in _callbacks)
                callback();
        if (_defaultValue.IsOn)
            return _defaultValue.Read(baseImplementation);
        // An empty sequence can be observed while the first setup is being published. A null fallback
        // returns default directly and avoids looking up a generic cached delegate on every read.
        var outcome = Volatile.Read(ref _returnValues)?.Next(null);
        return outcome is not null ? outcome(baseImplementation) : default!;
    }

    private MissingImposterException MissingImposter() =>
        new(
            _displayName + " (getter)",
            "get " + _displayName,
            _outcomeDescriptions is null
                ? Array.Empty<string>()
                : new[]
                {
                    _displayName + " (getter) -> " + DiagnosticValue.Outcomes(_outcomeDescriptions),
                }
        );
}
