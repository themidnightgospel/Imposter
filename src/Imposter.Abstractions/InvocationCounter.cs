namespace Imposter.Abstractions;

internal struct InvocationCounter
{
    private int _count;
    private int _covered;
    private object? _gate;

    internal int Count => Volatile.Read(ref _count);

    internal void Record() => Interlocked.Increment(ref _count);

    internal int Uncovered
    {
        get
        {
            lock (Gate)
                return Count - _covered;
        }
    }

    internal bool Verify(Count expected, out int actual)
    {
        lock (Gate)
        {
            actual = Count;
            if (!expected.Matches(actual))
                return false;
            _covered = Math.Max(_covered, actual);
            return true;
        }
    }

    internal void Clear()
    {
        lock (Gate)
        {
            Interlocked.Exchange(ref _count, 0);
            _covered = 0;
        }
    }

    private object Gate => LazyInitializer.EnsureInitialized(ref _gate)!;
}
