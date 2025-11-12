using BenchmarkDotNet.Attributes;
using Imposter.Abstractions;
using Imposter.Benchmarks.ImposterVsMoqVsNSubstitute;
using Moq;
using NSubstitute;

[assembly: GenerateImposter(typeof(SimpleMethodMockingBenchmarks.ICalculator))]

namespace Imposter.Benchmarks.ImposterVsMoqVsNSubstitute;

[MemoryDiagnoser]
public class SimpleMethodMockingBenchmarks
{
    [Params(1, 10, 100, 1000)]
    public int Iteration;

    [Benchmark]
    public void Mock()
    {
        var calculatorMock = new Mock<ICalculator>();

        for (var i = 0; i < Iteration; i++)
        {
            var input = i;
            calculatorMock
                .Setup(it => it.Square(input))
                .Returns(input * input);
        }

        var mockObject = calculatorMock.Object;

        for (var i = 0; i < Iteration; i++)
        {
            mockObject.Square(i);
        }
    }

    [Benchmark]
    public void NSub()
    {
        var calculatorMock = Substitute.For<ICalculator>();

        for (var i = 0; i < Iteration; i++)
        {
            var input = i;
            calculatorMock.Square(input).Returns(input * input);
        }

        for (var i = 0; i < Iteration; i++)
        {
            calculatorMock.Square(i);
        }
    }

    [Benchmark]
    public void Imposter()
    {
        var imposter = new ICalculatorImposter();

        for (var i = 0; i < Iteration; i++)
        {
            var input = i;
            imposter.Square(input).Returns(input * input);
        }

        var imposterInstance = imposter.Instance();
        for (var i = 0; i < Iteration; i++)
        {
            imposterInstance.Square(i);
        }
    }

    public interface ICalculator
    {
        int Square(int input);
    }
}