#nullable enable

using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Imposter.Abstractions;

namespace Imposter.Benchmarks;

// Each invocation owns at most Calls history nodes. The two recorder benchmarks differ only in
// whether their sequence counter is local to a member or shared process-wide. GeneratedCalls
// measures the complete implementation under the same scheduling and ownership pattern.
[MemoryDiagnoser]
public class InvocationContentionBenchmarks
{
    private const int Calls = 32768;

    [Params(1, 4, 8)]
    public int Workers { get; set; }

    [Params(false, true)]
    public bool SharedImposter { get; set; }

    [Benchmark(Baseline = true, OperationsPerInvoke = Calls)]
    public long PerMemberSequence() => Record(useGlobalSequence: false);

    [Benchmark(OperationsPerInvoke = Calls)]
    public long GlobalSequence() => Record(useGlobalSequence: true);

    [Benchmark(OperationsPerInvoke = Calls)]
    public long GeneratedCalls()
    {
        var instances = new IMethodHistoryBenchmarkService[Workers];
        for (var worker = 0; worker < Workers; worker++)
        {
            if (SharedImposter && worker != 0)
            {
                instances[worker] = instances[0];
                continue;
            }
            var imposter = new IMethodHistoryBenchmarkServiceImposter();
            imposter.Square(Arg<int>.Any()).Returns(4);
            instances[worker] = imposter.Instance();
        }

        using var start = new ManualResetEventSlim();
        var tasks = new Task<long>[Workers];
        var perWorker = Calls / Workers;
        for (var worker = 0; worker < Workers; worker++)
        {
            var instance = instances[worker];
            tasks[worker] = Task.Run(() =>
            {
                start.Wait();
                long total = 0;
                for (var call = 0; call < perWorker; call++)
                    total += instance.Square(call);
                return total;
            });
        }
        start.Set();
        Task.WaitAll(tasks);
        long result = 0;
        foreach (var task in tasks)
            result += task.Result;
        return result;
    }

    private long Record(bool useGlobalSequence)
    {
        var recorders = new Recorder[Workers];
        for (var worker = 0; worker < Workers; worker++)
            recorders[worker] = SharedImposter && worker != 0 ? recorders[0] : new Recorder();

        using var start = new ManualResetEventSlim();
        var tasks = new Task<long>[Workers];
        var perWorker = Calls / Workers;
        for (var worker = 0; worker < Workers; worker++)
        {
            var recorder = recorders[worker];
            tasks[worker] = Task.Run(() =>
            {
                start.Wait();
                long total = 0;
                for (var call = 0; call < perWorker; call++)
                    total += recorder.Record(call, useGlobalSequence);
                return total;
            });
        }
        start.Set();
        Task.WaitAll(tasks);
        long result = 0;
        foreach (var task in tasks)
            result += task.Result;
        return result;
    }

    private sealed class Recorder
    {
        private long _sequence;
        private Entry? _head;

        internal long Record(int value, bool useGlobalSequence)
        {
            var sequence = useGlobalSequence
                ? InvocationSequence.Next()
                : Interlocked.Increment(ref _sequence);
            var entry = new Entry(value, sequence);
            Entry? observed;
            do
            {
                observed = Volatile.Read(ref _head);
                entry.Next = observed;
            } while (Interlocked.CompareExchange(ref _head, entry, observed) != observed);
            return entry.Sequence + entry.Value;
        }
    }

    private sealed class Entry(int value, long sequence)
    {
        internal readonly int Value = value;
        internal readonly long Sequence = sequence;
        internal Entry? Next;
    }
}
