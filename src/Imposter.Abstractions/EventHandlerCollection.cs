using System.ComponentModel;

namespace Imposter.Abstractions;

/// <summary>Publishes event handler snapshots when subscriptions change.</summary>
/// <typeparam name="T">The event delegate type.</typeparam>
/// <remarks>
/// Public for generated code only and may change with the generator in the same package.
/// Removal follows multicast delegate semantics.
/// </remarks>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class EventHandlerCollection<T>
    where T : Delegate
{
    private State? _state;

    /// <summary>Combines a handler with the current subscriptions.</summary>
    /// <param name="handler">The handler to subscribe.</param>
    public void Combine(T handler) => Update(handler, remove: false);

    /// <summary>Removes the last matching subscription or multicast subsequence.</summary>
    /// <param name="handler">The handler to unsubscribe.</param>
    public void Remove(T handler) => Update(handler, remove: true);

    /// <summary>Captures the handlers without creating an invocation list on the read path.</summary>
    /// <returns>An allocation-free snapshot enumerator.</returns>
    public Enumerator GetEnumerator() =>
        new(Volatile.Read(ref _state)?.Handlers ?? Array.Empty<Delegate>());

    private void Update(T handler, bool remove)
    {
        State? snapshot;
        State? updated;
        do
        {
            snapshot = Volatile.Read(ref _state);
            var combined = remove
                ? Delegate.Remove(snapshot?.Combined, handler)
                : Delegate.Combine(snapshot?.Combined, handler);
            if (ReferenceEquals(combined, snapshot?.Combined))
            {
                return;
            }

            updated = combined is null ? null : new State(combined);
        } while (Interlocked.CompareExchange(ref _state, updated, snapshot) != snapshot);
    }

    private sealed class State(Delegate combined)
    {
        internal readonly Delegate Combined = combined;
        internal readonly Delegate[] Handlers = combined.GetInvocationList();
    }

    /// <summary>Enumerates the captured subscriptions in order.</summary>
    public struct Enumerator
    {
        private readonly Delegate[] _handlers;
        private int _index;

        internal Enumerator(Delegate[] handlers)
        {
            _handlers = handlers;
            _index = -1;
        }

        /// <summary>Gets the current handler.</summary>
        public T Current => (T)_handlers[_index];

        /// <summary>Moves to the next handler.</summary>
        /// <returns>Whether another handler is available.</returns>
        public bool MoveNext() => _handlers is not null && ++_index < _handlers.Length;
    }
}
