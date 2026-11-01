using System.ComponentModel;

namespace Imposter.Abstractions;

/// <summary>
/// Stores generated imposter setups in newest-first order with allocation-free snapshot enumeration.
/// </summary>
/// <typeparam name="T">The setup type.</typeparam>
/// <remarks>Intended for generated code. Published nodes are immutable, so readers need no locks.</remarks>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class SetupCollection<T>
{
    private Node? _head;

    /// <summary>Adds a setup ahead of all previously published setups.</summary>
    /// <param name="item">The setup to add.</param>
    public void Push(T item)
    {
        Node? head;
        do
        {
            head = Volatile.Read(ref _head);
        } while (Interlocked.CompareExchange(ref _head, new Node(item, head), head) != head);
    }

    /// <summary>Reads the newest setup without removing it.</summary>
    /// <param name="item">The newest setup, or the default value when the collection is empty.</param>
    /// <returns>Whether a setup was found.</returns>
    public bool TryPeek(out T item)
    {
        var head = Volatile.Read(ref _head);
        item = head is null ? default! : head.Item;
        return head is not null;
    }

    /// <summary>Captures the current setups for newest-first enumeration.</summary>
    /// <returns>A value-type enumerator that does not observe subsequent additions.</returns>
    public Enumerator GetEnumerator() => new(this);

    private sealed class Node(T item, Node? next)
    {
        internal readonly T Item = item;
        internal readonly Node? Next = next;
    }

    /// <summary>Enumerates the snapshot captured when it was created without allocating.</summary>
    public struct Enumerator
    {
        private Node? _next;
        private T _current;

        internal Enumerator(SetupCollection<T> collection)
        {
            _next = Volatile.Read(ref collection._head);
            _current = default!;
        }

        /// <summary>Gets the setup at the current position.</summary>
        public T Current => _current;

        /// <summary>Moves to the next setup in the captured snapshot.</summary>
        /// <returns>Whether another setup was found.</returns>
        public bool MoveNext()
        {
            if (_next is null)
            {
                _current = default!;
                return false;
            }

            _current = _next.Item;
            _next = _next.Next;
            return true;
        }
    }
}
