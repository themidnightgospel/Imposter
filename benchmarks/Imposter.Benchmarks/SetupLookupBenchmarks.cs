using BenchmarkDotNet.Attributes;
using Imposter.Abstractions;
using Imposter.Benchmarks;

[assembly: GenerateImposter(typeof(ISetupLookupBenchmarkService))]

namespace Imposter.Benchmarks;

// Recreate the imposter between iterations so retained invocation histories cannot grow without bound.
// Report allocations per call, excluding the setup performed by IterationSetup.
[MemoryDiagnoser]
public class SetupLookupBenchmarks
{
    private const int Calls = 1000;
    private ISetupLookupBenchmarkService _service = null!;

    [Params(1, 8)]
    public int Setups { get; set; }

    [IterationSetup]
    public void Setup()
    {
        var imposter = new ISetupLookupBenchmarkServiceImposter();
        for (var i = 0; i < Setups; i++)
        {
            imposter.Square(i).Returns(i * i);
            imposter.Log(i.ToString());
            imposter.Echo<string>(i.ToString()).Returns("value");
            imposter[i].Getter().Returns(i);
        }

        _service = imposter.Instance();
    }

    [Benchmark(OperationsPerInvoke = Calls)]
    public void Method()
    {
        for (var i = 0; i < Calls; i++)
        {
            _service.Square(0);
        }
    }

    [Benchmark(OperationsPerInvoke = Calls)]
    public void VoidMethod()
    {
        for (var i = 0; i < Calls; i++)
        {
            _service.Log("0");
        }
    }

    [Benchmark(OperationsPerInvoke = Calls)]
    public void GenericMethod()
    {
        for (var i = 0; i < Calls; i++)
        {
            _service.Echo("0");
        }
    }

    [Benchmark(OperationsPerInvoke = Calls)]
    public void IndexerGetter()
    {
        for (var i = 0; i < Calls; i++)
        {
            _ = _service[0];
        }
    }
}

public interface ISetupLookupBenchmarkService
{
    int Square(int value);
    void Log(string message);
    T Echo<T>(T value);
    int this[int key] { get; }
}
