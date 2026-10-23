using System.ComponentModel;

namespace Imposter.Abstractions;

/// <summary>Consumes configured outcomes in order, repeating the last after exhaustion.</summary>
/// <typeparam name="T">The reference type representing an outcome.</typeparam>
/// <remarks>
/// Public for generated code only and may change with the generator in the same package.
/// Create this collection lazily on the first setup.
/// </remarks>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class ReturnValueSequence<T>
    where T : class?
{
    private readonly List<T> _items = new();
    private int _index;
    private int _pending;
    private T _last = default!;
    private bool _hasLast;

    /// <summary>Appends an outcome, including after earlier outcomes have been consumed.</summary>
    /// <param name="item">The outcome to append.</param>
    public void Add(T item)
    {
        lock (_items)
        {
            _items.Add(item);
            Volatile.Write(ref _pending, _items.Count - _index);
        }
    }

    /// <summary>Consumes the next outcome or repeats the last consumed outcome.</summary>
    /// <param name="fallback">The outcome to use before any outcome has been consumed.</param>
    /// <returns>The next, last, or fallback outcome.</returns>
    public T Next(T fallback)
    {
        // Publish the last outcome before publishing exhaustion. Repeated reads then need no lock,
        // while a later Add publishes pending work again under the same lock as consumption.
        if (Volatile.Read(ref _pending) == 0)
        {
            return Volatile.Read(ref _hasLast) ? Volatile.Read(ref _last) : fallback;
        }

        lock (_items)
        {
            if (_index < _items.Count)
            {
                Volatile.Write(ref _last, _items[_index]);
                Volatile.Write(ref _hasLast, true);
                _items[_index++] = default!;
                if (_index == _items.Count)
                {
                    _items.Clear();
                    _index = 0;
                }

                Volatile.Write(ref _pending, _items.Count - _index);
            }

            return _hasLast ? _last : fallback;
        }
    }
}
