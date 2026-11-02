#nullable enable

using System;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Imposter.Abstractions;
using Imposter.Benchmarks;

[assembly: GenerateImposter(typeof(ICallAllocationBenchmarkService))]

namespace Imposter.Benchmarks;

// Reset retained histories between iterations; setup allocations are excluded from measurements.
// Run with IMPOSTER_BENCHMARK_JOB=short and --filter '*CallAllocationBenchmarks*'.
[MemoryDiagnoser]
public class CallAllocationBenchmarks
{
    private const int Calls = 1000;
    private ICallAllocationBenchmarkServiceImposter _imposter = null!;
    private ICallAllocationBenchmarkService _service = null!;

    [IterationSetup]
    public void Setup()
    {
        _imposter = new ICallAllocationBenchmarkServiceImposter();
        _imposter.ReturnTask(Arg<int>.Any()).ReturnsAsync(12345);
        _imposter.ReturnValueTask(Arg<int>.Any()).ReturnsAsync(12345);
        _imposter.ThrowTask(Arg<int>.Any()).ThrowsAsync(new InvalidOperationException());
        _imposter.ThrowValueTask(Arg<int>.Any()).ThrowsAsync(new InvalidOperationException());
        _imposter.Throw(Arg<int>.Any()).Throws(new InvalidOperationException());
        _imposter[Arg<int>.Any()].Getter().Callback(_ => { });
        _service = _imposter.Instance();
        _service.Changed += (_, _) => { };
    }

    [Benchmark(OperationsPerInvoke = Calls)]
    public void ReturnsAsyncTask()
    {
        for (var i = 0; i < Calls; i++)
            _ = _service.ReturnTask(1);
    }

    [Benchmark(OperationsPerInvoke = Calls)]
    public void ReturnsAsyncValueTask()
    {
        for (var i = 0; i < Calls; i++)
            _ = _service.ReturnValueTask(1);
    }

    // Measure generating the faulted outcome, excluding the caller's await/exception dispatch.
    [Benchmark(OperationsPerInvoke = Calls)]
    public void ThrowsAsyncTask()
    {
        for (var i = 0; i < Calls; i++)
            _ = _service.ThrowTask(1);
    }

    [Benchmark(OperationsPerInvoke = Calls)]
    public void ThrowsAsyncValueTask()
    {
        for (var i = 0; i < Calls; i++)
            _ = _service.ThrowValueTask(1);
    }

    [Benchmark(OperationsPerInvoke = Calls)]
    public void ThrowsSynchronously()
    {
        for (var i = 0; i < Calls; i++)
        {
            try
            {
                _service.Throw(1);
            }
            catch (InvalidOperationException) { }
        }
    }

    [Benchmark(OperationsPerInvoke = Calls)]
    public void CallbackOnlyIndexer()
    {
        for (var i = 0; i < Calls; i++)
            _ = _service[1];
    }

    [Benchmark(OperationsPerInvoke = Calls)]
    public void RaiseWithHandler()
    {
        for (var i = 0; i < Calls; i++)
            _imposter.Changed.Raise(null, EventArgs.Empty);
    }

    [Benchmark(OperationsPerInvoke = Calls)]
    public void RaiseAsyncWithoutHandlers()
    {
        for (var i = 0; i < Calls; i++)
            _ = _imposter.ChangedAsync.RaiseAsync(null, EventArgs.Empty);
    }
}

public interface ICallAllocationBenchmarkService
{
    Task<int> ReturnTask(int key);
    ValueTask<int> ReturnValueTask(int key);
    Task<int> ThrowTask(int key);
    ValueTask<int> ThrowValueTask(int key);
    int Throw(int key);
    int this[int key] { get; set; }
    event EventHandler Changed;
    event Func<object?, EventArgs, Task> ChangedAsync;
}
