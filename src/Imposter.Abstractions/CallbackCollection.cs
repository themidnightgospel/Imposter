using System.ComponentModel;

namespace Imposter.Abstractions;

/// <summary>Stores callbacks in registration order with allocation-free snapshot enumeration.</summary>
/// <typeparam name="T">The callback or registration type.</typeparam>
/// <remarks>
/// Public for generated code only and may change with the generator in the same package.
/// Published arrays are immutable.
/// </remarks>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class CallbackCollection<T>
{
    private T[] _items = Array.Empty<T>();

    /// <summary>Appends a registration without changing snapshots already being read.</summary>
    /// <param name="item">The registration to append.</param>
    public void Add(T item)
    {
        T[] snapshot;
        T[] updated;
        do
        {
            snapshot = Volatile.Read(ref _items);
            updated = new T[snapshot.Length + 1];
            Array.Copy(snapshot, updated, snapshot.Length);
            updated[snapshot.Length] = item;
        } while (Interlocked.CompareExchange(ref _items, updated, snapshot) != snapshot);
    }

    /// <summary>Captures the registrations in their original order.</summary>
    /// <returns>An enumerator that does not observe subsequent additions.</returns>
    public Enumerator GetEnumerator() => new(Volatile.Read(ref _items));

    /// <summary>Enumerates a captured array without allocating.</summary>
    public struct Enumerator
    {
        private readonly T[] _items;
        private int _index;

        internal Enumerator(T[] items)
        {
            _items = items;
            _index = -1;
        }

        /// <summary>Gets the current registration.</summary>
        public T Current => _items[_index];

        /// <summary>Moves to the next registration.</summary>
        /// <returns>Whether another registration is available.</returns>
        public bool MoveNext() => _items is not null && ++_index < _items.Length;
    }
}
