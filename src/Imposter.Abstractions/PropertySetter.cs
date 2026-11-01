using System.Collections.Concurrent;
using System.ComponentModel;

namespace Imposter.Abstractions;

/// <summary>Implements callbacks, history and invocation for generated property setters.</summary>
/// <typeparam name="T">The property's value type.</typeparam>
/// <remarks>Public for generated code only and may change with the generator in the same package.</remarks>
[EditorBrowsable(EditorBrowsableState.Never)]
public class PropertySetter<T>
{
    private CallbackCollection<(Arg<T> Criteria, Action<T> Callback)>? _callbacks;
    private ConcurrentStack<T>? _invocationHistory;
    private readonly PropertyValue<T>? _defaultValue;
    private readonly ImposterMode _mode;
    private readonly string _displayName;
    private bool _hasConfiguredSetter;
    private bool _useBaseImplementation;

    /// <summary>Constructor</summary>
    /// <param name="defaultValue">The value shared with the getter, if present.</param>
    /// <param name="mode">Whether an explicit setup is required.</param>
    /// <param name="displayName">The property name used in diagnostics.</param>
    protected PropertySetter(PropertyValue<T>? defaultValue, ImposterMode mode, string displayName)
    {
        _defaultValue = defaultValue;
        _mode = mode;
        _displayName = displayName;
    }

    /// <summary>Registers a callback for matching assignments.</summary>
    /// <param name="criteria">The values to match.</param>
    /// <param name="callback">The callback to invoke.</param>
    public void Callback(Arg<T> criteria, Action<T> callback)
    {
        LazyInitializer.EnsureInitialized(ref _callbacks)!.Add((criteria, callback));
        _hasConfiguredSetter = true;
    }

    /// <summary>Verifies matching assignments, formatting history only on failure.</summary>
    /// <param name="criteria">The values to match.</param>
    /// <param name="count">The expected count.</param>
    public void Called(Arg<T> criteria, Count count)
    {
        var history = _invocationHistory;
        var actual = history is not null ? history.Count(criteria.Matches) : 0;
        if (count.Matches(actual))
            return;
        var performed = new List<string>();
        if (history is not null)
            foreach (var value in history)
                performed.Add("set " + _displayName + " = <" + (value?.ToString() ?? "null") + ">");
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

    /// <summary>Records an assignment and invokes callbacks before updating the property.</summary>
    /// <param name="value">The assigned value.</param>
    /// <param name="baseImplementation">The base setter, when available.</param>
    public void Set(T value, Action<T>? baseImplementation = null)
    {
        if (!RecordAndShouldCallBase(value))
            return;
        if (baseImplementation is null)
            throw MissingImposter();
        baseImplementation(value);
    }

    /// <summary>Records an assignment, invokes callbacks, and updates enabled storage unless the base accessor is configured.</summary>
    /// <param name="value">The assigned value.</param>
    /// <returns>Whether the caller should invoke the base set or init accessor.</returns>
    protected bool RecordAndShouldCallBase(T value)
    {
        if (_mode == ImposterMode.Explicit && !_hasConfiguredSetter)
            throw MissingImposter();
        LazyInitializer.EnsureInitialized(ref _invocationHistory)!.Push(value);
        if (_callbacks is not null)
            foreach (var registration in _callbacks)
                if (registration.Criteria.Matches(value))
                    registration.Callback(value);
        if (_useBaseImplementation)
            return true;
        if (_defaultValue is { IsOn: true })
            _defaultValue.Write(value);
        return false;
    }

    private MissingImposterException MissingImposter() => new(_displayName + " (setter)");
}
