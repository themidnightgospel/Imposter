using System.ComponentModel;

namespace Imposter.Abstractions;

/// <summary>Consumes configured outcomes in order, repeating the last after exhaustion.</summary>
/// <typeparam name="T">The reference type representing an outcome.</typeparam>
/// <remarks>
/// Public for generated code only and may change with the generator in the same package.
/// Neither adding nor consuming takes a lock.
/// </remarks>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class ReturnValueSequence<T>
    where T : class?
{
    // The outcomes form a chain from the last consumed one, which starts as an empty node. Consumed outcomes before
    // it are unreachable, so a long sequence doesn't keep what it has already served.
    private Node _current;
    private Node _tail;

    /// <summary>Constructor</summary>
    public ReturnValueSequence()
    {
        _current = _tail = new Node(default!, hasItem: false);
    }

    /// <summary>Appends an outcome, including after earlier outcomes have been consumed.</summary>
    /// <param name="item">The outcome to append.</param>
    public void Add(T item)
    {
        var node = new Node(item, hasItem: true);
        while (true)
        {
            var tail = Volatile.Read(ref _tail);
            var next = Volatile.Read(ref tail.Next);
            if (next is not null)
            {
                // Another Add linked its node but hasn't moved the tail yet.
                Interlocked.CompareExchange(ref _tail, next, tail);
            }
            else if (Interlocked.CompareExchange(ref tail.Next, node, null) is null)
            {
                Interlocked.CompareExchange(ref _tail, node, tail);
                return;
            }
        }
    }

    /// <summary>Consumes the next outcome or repeats the last consumed outcome.</summary>
    /// <param name="fallback">The outcome to use before any outcome has been consumed.</param>
    /// <returns>The next, last, or fallback outcome.</returns>
    public T Next(T fallback)
    {
        while (true)
        {
            var current = Volatile.Read(ref _current);
            var next = Volatile.Read(ref current.Next);
            if (next is null)
            {
                return current.HasItem ? current.Item : fallback;
            }

            // Only the caller that moves the chain onto a node serves it. One that loses the race retries from the
            // node the winner moved to, so it takes the outcome after it or repeats it.
            if (Interlocked.CompareExchange(ref _current, next, current) == current)
            {
                return next.Item;
            }
        }
    }

    private sealed class Node(T item, bool hasItem)
    {
        internal readonly T Item = item;
        internal readonly bool HasItem = hasItem;
        internal Node? Next;
    }
}
