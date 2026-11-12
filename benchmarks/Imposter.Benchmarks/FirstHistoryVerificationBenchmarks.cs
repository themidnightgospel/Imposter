using System;
using BenchmarkDotNet.Attributes;
using Imposter.Abstractions;

namespace Imposter.Benchmarks;

// Each timed invocation verifies fresh histories exactly once. Construction, setup, and calls occur
// in IterationSetup and are excluded from timing and reported managed allocation. A batch of 1000
// independent histories amortizes harness overhead; do not compare these means to single-check
// microbenchmarks without accounting for the shared delegate-loop overhead.
[MemoryDiagnoser]
public class FirstHistoryVerificationBenchmarks
{
    private const int Histories = 1000;
    private Action[] _checks = null!;

    [Params(1, 1000)]
    public int HistoryLength { get; set; }

    [IterationSetup]
    public void PrepareFreshHistories()
    {
        _checks = new Action[Histories];
        var count = Count.Exactly(HistoryLength);
        for (var index = 0; index < Histories; index++)
        {
            var sut = new IMethodHistoryBenchmarkServiceImposter();
            var verification = sut.Square(Arg<int>.Any());
            verification.Returns(4);
            var instance = sut.Instance();
            for (var call = 0; call < HistoryLength; call++)
                _ = instance.Square(2);
            _checks[index] = () => verification.Called(count);
        }
    }

    [Benchmark(OperationsPerInvoke = Histories)]
    public void FirstVerification()
    {
        for (var index = 0; index < _checks.Length; index++)
            _checks[index]();
    }
}
