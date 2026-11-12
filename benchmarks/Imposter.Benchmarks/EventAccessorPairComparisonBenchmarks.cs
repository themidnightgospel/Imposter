using System;
using BenchmarkDotNet.Attributes;
using Imposter.Abstractions;

namespace Imposter.Benchmarks;

// Each reported operation is one add+remove pair, keeping exactly zero or one handler registered.
// Construction is amortized across 1000 pairs; retained history is released after each batch.
[MemoryDiagnoser]
public class EventAccessorPairComparisonBenchmarks
{
    private const int Pairs = 1000;
    private static readonly EventHandler Handler = static (_, _) => { };

    [Benchmark(OperationsPerInvoke = Pairs)]
    public void AddRemovePair()
    {
        var instance = new ICallAllocationBenchmarkServiceImposter().Instance();
        for (var index = 0; index < Pairs; index++)
        {
            instance.Changed += Handler;
            instance.Changed -= Handler;
        }
    }
}
