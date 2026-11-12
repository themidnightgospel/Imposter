using System.ComponentModel;

namespace Imposter.Abstractions;

/// <summary>Stores completed invocations in compact segments, without allocating a record per call.</summary>
/// <typeparam name="T">The captured invocation payload.</typeparam>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class InvocationBuffer<T>
{
    private Storage? _storage;

    /// <summary>Publishes the captured value and completed synchronous outcome.</summary>
    public void Add(T value, long sequence, Exception? exception) =>
        LazyInitializer.EnsureInitialized(ref _storage)!.Add(value, sequence, exception);

    /// <summary>Starts an empty generation without invalidating existing snapshots or coverage handles.</summary>
    public void Clear() => Interlocked.Exchange(ref _storage, null);

    /// <summary>Marks a captured entry covered without materializing a record.</summary>
    /// <param name="entry">The completed interaction selected by a passing verification.</param>
    public static void MarkVerified(Entry entry) => entry.MarkVerified();

    /// <summary>Reads a captured entry's verification coverage.</summary>
    /// <param name="entry">The completed interaction to inspect.</param>
    public static bool IsVerified(Entry entry) => entry.IsVerified;

    /// <summary>Captures the completed-entry chain visible at this point.</summary>
    public Enumerator GetEnumerator()
    {
        var storage = Volatile.Read(ref _storage);
        return new Enumerator(storage, storage is null ? -1 : Volatile.Read(ref storage.Head));
    }

    /// <summary>A stable handle to a published entry and its verification coverage.</summary>
    public readonly struct Entry
    {
        private readonly Segment? _segment;
        private readonly int _index;

        internal Entry(Segment segment, int index)
        {
            _segment = segment;
            _index = index;
        }

        internal bool IsValid => _segment is not null;
        internal int Next => _segment!.Next[_index];

        /// <summary>Gets the captured payload.</summary>
        public T Value => _segment!.Values[_index].Value;

        /// <summary>Gets the synchronous exception.</summary>
        public Exception? Exception => Volatile.Read(ref _segment!.Exceptions)?[_index];

        /// <summary>Gets the globally ordered entry stamp.</summary>
        public long Sequence =>
            Volatile.Read(ref _segment!.Values[_index].SequenceAndCoverage) & long.MaxValue;

        /// <summary>Gets whether a successful verification covered this entry.</summary>
        internal bool IsVerified =>
            Volatile.Read(ref _segment!.Values[_index].SequenceAndCoverage) < 0;

        /// <summary>Marks this entry covered without changing its captured values.</summary>
        internal void MarkVerified()
        {
            if (IsVerified)
                return;
            ref var stamp = ref _segment!.Values[_index].SequenceAndCoverage;
            Volatile.Write(ref stamp, Volatile.Read(ref stamp) | long.MinValue);
        }
    }

    /// <summary>Enumerates one captured publication head without allocations.</summary>
    public struct Enumerator
    {
        private readonly Storage? _storage;
        private int _next;
        private Entry _current;

        internal Enumerator(Storage? storage, int head)
        {
            _storage = storage;
            _next = head;
            _current = default;
        }

        /// <summary>Gets the current completed entry.</summary>
        public readonly Entry Current => _current;

        /// <summary>Advances along the immutable captured publication chain.</summary>
        public bool MoveNext()
        {
            if (_storage is null || _next < 0)
                return false;
            var index = _next;
            _current = _storage.GetEntry(index);
            _next = _current.Next;
            return true;
        }
    }

    // A generation owns all its slots. Clear swaps this entire owner, allowing discarded values to be
    // collected while previously materialized records retain only their containing segment.
    internal sealed class Storage
    {
        private const int SegmentSize = 32;
        private object?[]? _directory;
        private Segment? _firstSegment;
        private Segment? _recentSegment;
        private int _reserved;
        internal int Head = -1;

        internal void Add(T value, long sequence, Exception? exception)
        {
            var reservation = Interlocked.Increment(ref _reserved);
            if (reservation <= 0)
                throw new InvalidOperationException(
                    "The invocation history has exceeded its capacity."
                );
            var index = reservation - 1;
            var segment = SegmentFor(index);
            var offset = index % SegmentSize;
            segment.Values[offset] = new Data(value, sequence);
            if (exception is not null)
                LazyInitializer.EnsureInitialized(
                    ref segment.Exceptions,
                    static () => new Exception?[SegmentSize]
                )![offset] = exception;
            int head;
            do
            {
                head = Volatile.Read(ref Head);
                segment.Next[offset] = head;
            } while (Interlocked.CompareExchange(ref Head, index, head) != head);
        }

        internal Entry GetEntry(int index) => new(SegmentFor(index), index % SegmentSize);

        private Segment SegmentFor(int index)
        {
            var page = index / SegmentSize;
            if (page == 0)
                return LazyInitializer.EnsureInitialized(
                    ref _firstSegment,
                    static () => new Segment(0)
                )!;
            var recent = Volatile.Read(ref _recentSegment);
            if (recent is not null && recent.Page == page)
                return recent;

            // Seven four-bit levels cover the 26 page-index bits of a nonnegative int slot index.
            // Each slot is installed once, so concurrent growth never copies or loses another writer's page.
            var directory = LazyInitializer.EnsureInitialized(
                ref _directory,
                static () => new object?[16]
            )!;
            for (var shift = 24; shift > 0; shift -= 4)
            {
                var slot = (page >> shift) & 15;
                var child = (object?[]?)Volatile.Read(ref directory[slot]);
                if (child is null)
                {
                    var candidate = new object?[16];
                    child =
                        (object?[]?)
                            Interlocked.CompareExchange(ref directory[slot], candidate, null)
                        ?? candidate;
                }
                directory = child;
            }
            var leaf = page & 15;
            var segment = (Segment?)Volatile.Read(ref directory[leaf]);
            if (segment is null)
            {
                var candidate = new Segment(page);
                segment =
                    (Segment?)Interlocked.CompareExchange(ref directory[leaf], candidate, null)
                    ?? candidate;
            }
            Volatile.Write(ref _recentSegment, segment);
            return segment;
        }

        internal static int Capacity => SegmentSize;
    }

    internal sealed class Segment(int page)
    {
        internal readonly int Page = page;
        internal readonly Data[] Values = new Data[Storage.Capacity];
        internal readonly int[] Next = new int[Storage.Capacity];
        internal Exception?[]? Exceptions;
    }

    // Keep links in a separate int array: nesting them in Data would introduce another alignment gap.
    internal struct Data(T value, long sequence)
    {
        internal readonly T Value = value;
        internal long SequenceAndCoverage = sequence;
    }
}
