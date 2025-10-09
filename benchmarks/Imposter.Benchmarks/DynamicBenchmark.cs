using BenchmarkDotNet.Attributes;

namespace Imposter.Benchmarks;

public interface IParent
{
}

public interface IInterface : IParent
{
}

public class Implementaton : IInterface
{
    public Implementaton()
    {
    }
}

public class ImposterMethod<TIn, TOut>
    where TOut : new()
{
    public TOut Invoke(TIn input, int age)
    {
        return new TOut();
    }
}

[MemoryDiagnoser]
public class DynamicBenchmark
{
    static ImposterMethod<IParent, Implementaton> imposter = new();

    [Benchmark]
    public void WithoutDynamic()
    {
        imposter.Invoke(new Implementaton(), 22);
    }
    
    [Benchmark]
    public void WithDynamic()
    {
        dynamic dynamicImposter = imposter;
        dynamicImposter.Invoke(new Implementaton(), 22);
    }
}