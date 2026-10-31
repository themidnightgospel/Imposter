using System.ComponentModel;

namespace Imposter.Abstractions;

/// <summary>Implements storage, sequencing and invocation for generated property getters.</summary>
/// <typeparam name="T">The property's value type.</typeparam>
/// <remarks>Public for generated code only and may change with the generator in the same package.</remarks>
[EditorBrowsable(EditorBrowsableState.Never)]
public class PropertyGetter<T>
{
    private ReturnValueSequence<Func<Func<T>?, T>?>? _returnValues;
    private CallbackCollection<Action>? _callbacks;
    private int _invocationCount;
    private readonly PropertyValue<T> _defaultValue;
    private readonly ImposterMode _mode;
    private readonly string _displayName;
    private bool _hasConfiguredReturn;

    /// <summary>Initializes a generated getter.</summary>
    /// <param name="defaultValue">The value shared with the setter.</param>
    /// <param name="mode">Whether an explicit outcome is required.</param>
    /// <param name="displayName">The property name used in diagnostics.</param>
    protected PropertyGetter(PropertyValue<T> defaultValue, ImposterMode mode, string displayName)
    {
        _defaultValue = defaultValue;
        _mode = mode;
        _displayName = displayName;
    }

    private void AddReturnValue(Func<Func<T>?, T> valueGenerator)
    {
        _defaultValue.TurnOff();
        LazyInitializer.EnsureInitialized(ref _returnValues)!.Add(valueGenerator);
        _hasConfiguredReturn = true;
    }

    /// <summary>Appends a constant outcome.</summary>
    /// <param name="value">The value to return.</param>
    protected void Returns(T value) => AddReturnValue(_ => value);

    /// <summary>Appends a generated outcome.</summary>
    /// <param name="valueGenerator">The factory to invoke on each read.</param>
    protected void Returns(Func<T> valueGenerator) => AddReturnValue(_ => valueGenerator());

    /// <summary>Appends an exception outcome.</summary>
    /// <param name="exception">The exception to throw.</param>
    protected void Throws(Exception exception) => AddReturnValue(_ => throw exception);

    /// <summary>Appends an outcome that constructs an exception on each read.</summary>
    /// <typeparam name="TException">The exception type.</typeparam>
    protected void Throws<TException>()
        where TException : Exception, new() => AddReturnValue(_ => throw new TException());

    /// <summary>Registers a callback without configuring an outcome.</summary>
    /// <param name="callback">The callback to invoke before the outcome.</param>
    protected void Callback(Action callback) =>
        LazyInitializer.EnsureInitialized(ref _callbacks)!.Add(callback);

    /// <summary>Verifies the number of reads, including reads whose callbacks or outcomes threw.</summary>
    /// <param name="count">The expected count.</param>
    protected void Called(Count count)
    {
        var actual = Volatile.Read(ref _invocationCount);
        if (!count.Matches(actual))
            throw new VerificationFailedException(count, actual);
    }

    /// <summary>Appends an outcome that calls the base getter.</summary>
    public void EnableBaseImplementation() =>
        AddReturnValue(baseImplementation =>
            baseImplementation is not null
                ? baseImplementation()
                : throw new MissingImposterException(_displayName + " (getter)")
        );

    /// <summary>Records a read, invokes callbacks, and returns the current outcome.</summary>
    /// <param name="baseImplementation">The base getter, when available.</param>
    /// <returns>The property's value.</returns>
    public T Get(Func<T>? baseImplementation = null)
    {
        if (_mode == ImposterMode.Explicit && !_hasConfiguredReturn)
            throw new MissingImposterException(_displayName + " (getter)");
        Interlocked.Increment(ref _invocationCount);
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
}
