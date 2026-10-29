#nullable enable

using BenchmarkDotNet.Attributes;
using Imposter.Abstractions;
using Imposter.Benchmarks;

[assembly: GenerateImposter(typeof(IPropertyRuntimeBenchmarkService))]

namespace Imposter.Benchmarks;

[MemoryDiagnoser]
public class PropertyRuntimeBenchmarks
{
    private const int Calls = 1000;
    private IPropertyRuntimeBenchmarkService _default = null!;
    private IPropertyRuntimeBenchmarkService _configured = null!;
    private IPropertyRuntimeBenchmarkService _withCallback = null!;

    [GlobalSetup]
    public void Setup()
    {
        _default = new IPropertyRuntimeBenchmarkServiceImposter().Instance();
        var configured = new IPropertyRuntimeBenchmarkServiceImposter();
        configured.Number.Getter().Returns(42);
        configured.Text.Getter().Returns("value");
        _configured = configured.Instance();
        var withCallback = new IPropertyRuntimeBenchmarkServiceImposter();
        withCallback.Number.Getter().Returns(42).Callback(() => { });
        _withCallback = withCallback.Instance();
    }

    [Benchmark]
    public int DefaultGetter() => _default.Number;

    [Benchmark]
    public int ConfiguredGetter() => _configured.Number;

    [Benchmark]
    public string ConfiguredReferenceGetter() => _configured.Text;

    [Benchmark]
    public int GetterWithCallback() => _withCallback.Number;

    [Benchmark(OperationsPerInvoke = Calls)]
    public void Setter()
    {
        // Release retained history after each batch so the benchmark can run long, steady-state iterations.
        // Construction is included and amortized over Calls assignments, identically in baseline and after runs.
        var service = new IPropertyRuntimeBenchmarkServiceImposter().Instance();
        for (var value = 0; value < Calls; value++)
            service.Number = value;
    }

    [Benchmark(OperationsPerInvoke = Calls)]
    public void SetterWithCallback()
    {
        var imposter = new IPropertyRuntimeBenchmarkServiceImposter();
        imposter.Number.Setter(Arg<int>.Any()).Callback(_ => { });
        var service = imposter.Instance();
        for (var value = 0; value < Calls; value++)
            service.Number = value;
    }
}

public interface IPropertyRuntimeBenchmarkService
{
    int Number { get; set; }
    string Text { get; set; }
}
