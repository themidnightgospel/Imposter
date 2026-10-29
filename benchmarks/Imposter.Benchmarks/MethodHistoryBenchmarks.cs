using System;
using BenchmarkDotNet.Attributes;
using Imposter.Abstractions;
using Imposter.Benchmarks;

[assembly: GenerateImposter(typeof(IMethodHistoryBenchmarkService))]

namespace Imposter.Benchmarks;

public interface IMethodHistoryBenchmarkService
{
    int Square(int value);
    T Echo<T>(T value);
    int Sum(ReadOnlySpan<int> values);
    long Read(in HistoryLargeValue value);
}

public readonly struct HistoryLargeValue
{
    public readonly long A,
        B,
        C,
        D,
        E,
        F,
        G,
        H,
        I,
        J,
        K,
        L,
        M,
        N,
        O,
        P;
}

// Each batch owns its history, keeping retained memory bounded. Setup and construction costs are amortized.
[MemoryDiagnoser]
public class MethodHistoryCallBenchmarks
{
    private const int Calls = 1000;
    private readonly int[] _values = { 1, 2, 3, 4 };
    private readonly HistoryLargeValue _largeValue = default;

    [Benchmark(OperationsPerInvoke = Calls)]
    public int NonGeneric()
    {
        var sut = new IMethodHistoryBenchmarkServiceImposter();
        sut.Square(Arg<int>.Any()).Returns(4);
        var instance = sut.Instance();
        var result = 0;
        for (var i = 0; i < Calls; i++)
            result += instance.Square(2);
        return result;
    }

    [Benchmark(OperationsPerInvoke = Calls)]
    public int Generic()
    {
        var sut = new IMethodHistoryBenchmarkServiceImposter();
        sut.Echo<int>(Arg<int>.Any()).Returns(4);
        var instance = sut.Instance();
        var result = 0;
        for (var i = 0; i < Calls; i++)
            result += instance.Echo(2);
        return result;
    }

    [Benchmark(OperationsPerInvoke = Calls)]
    public int Span()
    {
        var sut = new IMethodHistoryBenchmarkServiceImposter();
        sut.Sum(ReadOnlySpanArg<int>.Any()).Returns(10);
        var instance = sut.Instance();
        var result = 0;
        for (var i = 0; i < Calls; i++)
            result += instance.Sum(_values);
        return result;
    }

    [Benchmark(OperationsPerInvoke = Calls)]
    public long LargeInWithEightCandidates()
    {
        var sut = new IMethodHistoryBenchmarkServiceImposter();
        sut.Read(Arg<HistoryLargeValue>.Any()).Returns(1);
        for (var candidate = 1; candidate < 8; candidate++)
        {
            var expected = candidate;
            sut.Read(Arg<HistoryLargeValue>.Is(value => value.A == expected)).Returns(0);
        }
        var instance = sut.Instance();
        long result = 0;
        for (var i = 0; i < Calls; i++)
            result += instance.Read(in _largeValue);
        return result;
    }
}

// Reuse verification builders and Count instances so setup registration is not charged to history traversal.
[MemoryDiagnoser]
public class MethodHistoryVerificationBenchmarks
{
    private Action _nonGeneric = null!;
    private Action _generic = null!;
    private Action _span = null!;

    [Params(1, 1000)]
    public int HistoryLength { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        var sut = new IMethodHistoryBenchmarkServiceImposter();
        var normal = sut.Square(Arg<int>.Any());
        var generic = sut.Echo<int>(Arg<int>.Any());
        var span = sut.Sum(ReadOnlySpanArg<int>.Any());
        normal.Returns(4);
        generic.Returns(4);
        span.Returns(10);
        var instance = sut.Instance();
        var values = new[] { 1, 2, 3, 4 };
        for (var i = 0; i < HistoryLength; i++)
        {
            instance.Square(2);
            instance.Echo(2);
            instance.Sum(values);
        }
        var count = Count.Exactly(HistoryLength);
        _nonGeneric = () => normal.Called(count);
        _generic = () => generic.Called(count);
        _span = () => span.Called(count);
    }

    [Benchmark]
    public void NonGeneric() => _nonGeneric();

    [Benchmark]
    public void Generic() => _generic();

    [Benchmark]
    public void Span() => _span();
}
