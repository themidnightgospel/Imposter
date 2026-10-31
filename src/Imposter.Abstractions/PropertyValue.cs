using System.ComponentModel;

namespace Imposter.Abstractions;

/// <summary>Shares the default value between a generated property's accessors.</summary>
/// <typeparam name="T">The property's value type.</typeparam>
/// <remarks>Public for generated code only and may change with the generator in the same package.</remarks>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class PropertyValue<T>
{
    private volatile bool _isOn = true;

    // Set once the value is the base getter's or an assigned one, so the base getter seeds it at most once.
    private volatile bool _isSettled;
    private bool _isSeeding;
    private T _value = default!;

    /// <summary>Whether the property still behaves like an auto-property.</summary>
    public bool IsOn => _isOn;

    /// <summary>Stops the auto-property behaviour once an outcome is configured.</summary>
    public void TurnOff() => _isOn = false;

    /// <summary>Returns the value, seeding it from the base getter on the first read when nothing was assigned.</summary>
    /// <param name="baseImplementation">The base getter, when available.</param>
    /// <returns>The property's value.</returns>
    public T Read(Func<T>? baseImplementation)
    {
        if (!_isSettled && baseImplementation is not null)
            Seed(baseImplementation);
        return _value;
    }

    /// <summary>Stores an assigned value, which the base getter no longer replaces.</summary>
    /// <param name="value">The assigned value.</param>
    public void Write(T value)
    {
        if (_isSettled)
        {
            _value = value;
            return;
        }

        // Waits for a seeding read, so the assignment isn't overwritten by the base value.
        lock (this)
        {
            _value = value;
            _isSettled = true;
        }
    }

    // Concurrent first reads wait here for the one that calls the base getter. Only these accessors can reach the
    // instance, so nothing else locks on it.
    private void Seed(Func<T> baseImplementation)
    {
        lock (this)
        {
            // The lock lets the seeding thread in again, so a base getter that reads the property gets the default.
            if (_isSettled || _isSeeding)
                return;
            _isSeeding = true;
            try
            {
                _value = baseImplementation();
            }
            finally
            {
                // A base getter that throws is called only once, like any other first read.
                _isSettled = true;
            }
        }
    }
}
