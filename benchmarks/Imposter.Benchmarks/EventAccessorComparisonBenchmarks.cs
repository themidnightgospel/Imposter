using System;
using BenchmarkDotNet.Attributes;
using Imposter.Abstractions;

namespace Imposter.Benchmarks;

// Each batch releases retained subscription/history state. Construction is included and amortized
// over 1000 accesses identically in both revisions. UnsubscribeAbsent isolates a legal event removal
// that has no subscription-list mutation; it still must retain invocation history.
[MemoryDiagnoser]
public class EventAccessorComparisonBenchmarks
{
    private const int Calls = 1000;
    private static readonly EventHandler Handler = static (_, _) => { };

    [Benchmark(OperationsPerInvoke = Calls)]
    public void Subscribe()
    {
        var instance = new ICallAllocationBenchmarkServiceImposter().Instance();
        for (var index = 0; index < Calls; index++)
            instance.Changed += Handler;
    }

    [Benchmark(OperationsPerInvoke = Calls)]
    public void UnsubscribeAbsent()
    {
        var instance = new ICallAllocationBenchmarkServiceImposter().Instance();
        for (var index = 0; index < Calls; index++)
            instance.Changed -= Handler;
    }
}
