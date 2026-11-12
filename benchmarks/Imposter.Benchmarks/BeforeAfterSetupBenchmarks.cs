using BenchmarkDotNet.Attributes;
using Imposter.Abstractions;

namespace Imposter.Benchmarks;

[MemoryDiagnoser]
public class BeforeAfterSetupBenchmarks
{
    [Benchmark]
    public object ConstructorOnly() => new IMethodHistoryBenchmarkServiceImposter();

    [Benchmark]
    public object MethodSetup()
    {
        var sut = new IMethodHistoryBenchmarkServiceImposter();
        sut.Square(Arg<int>.Any()).Returns(4);
        return sut;
    }

    [Benchmark]
    public object GenericSetup()
    {
        var sut = new IMethodHistoryBenchmarkServiceImposter();
        sut.Echo<int>(Arg<int>.Any()).Returns(4);
        return sut;
    }

    [Benchmark]
    public object SetterSetup()
    {
        var sut = new IPropertyRuntimeBenchmarkServiceImposter();
        sut.Number.Setter(Arg<int>.Any()).Callback(static _ => { });
        return sut;
    }
}
