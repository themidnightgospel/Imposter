using System.Collections;
using System.ComponentModel;

namespace Imposter.Abstractions;

/// <summary>Stores accessor and event records without allocating an additional list node.</summary>
/// <typeparam name="T">The invocation record type.</typeparam>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class InvocationHistory<T> : IEnumerable<T>
    where T : InvocationRecord
{
    private InvocationRecord? _head;

    internal T? Head => (T?)Volatile.Read(ref _head);

    /// <summary>Publishes a completed invocation.</summary>
    /// <param name="entry">An unpublished invocation owned by the caller.</param>
    public void Push(T entry)
    {
        InvocationRecord? head;
        do
        {
            head = Volatile.Read(ref _head);
            entry.Next = head;
        } while (Interlocked.CompareExchange(ref _head, entry, head) != head);
    }

    /// <summary>Atomically removes recorded history.</summary>
    public void Clear() => Interlocked.Exchange(ref _head, null);

    /// <summary>Enumerates the history visible when enumeration starts.</summary>
    public Enumerator GetEnumerator() => new(Volatile.Read(ref _head));

    IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>Enumerates a captured history head without allocating.</summary>
    public struct Enumerator : IEnumerator<T>
    {
        private InvocationRecord? _next;
        private T? _current;

        internal Enumerator(InvocationRecord? head)
        {
            _next = head;
            _current = null;
        }

        /// <inheritdoc />
        public T Current => _current!;
        object IEnumerator.Current => Current;

        /// <inheritdoc />
        public bool MoveNext()
        {
            if (_next is null)
                return false;
            _current = (T)_next;
            _next = _next.Next;
            return true;
        }

        /// <inheritdoc />
        public void Dispose() { }

        /// <inheritdoc />
        public void Reset() => throw new NotSupportedException();
    }
}
