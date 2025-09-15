using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Imposter.Abstractions;
using Imposters;
using Moq;
using NSubstitute;

BenchmarkRunner.Run<Benchy>();

[GenerateImposter(typeof(ICalculator))]
[MemoryDiagnoser]
public class Benchy
{
    [Benchmark]
    public void Mock()
    {
        var calculatorMock = new Mock<ICalculator>();
        calculatorMock
            .Setup(it => it.Add(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(1);

        calculatorMock.Object.Add(1, 1);
    }

    [Benchmark]
    public void NSub()
    {
        var calculatorMock = Substitute.For<ICalculator>();

        calculatorMock.Add(Arg.Any<int>(), Arg.Any<int>()).Returns(1);

        calculatorMock.Add(1, 1);
    }

    [Benchmark]
    public void Imposter()
    {
        var imposter = new ICalculatorImposter();

        imposter
            .Add(Arg<int>.Any, Arg<int>.Any)
            .Returns((left, right) => left * right);

        imposter.Instance().Add(10, 9);
    }

    public interface ICalculator
    {
        int Add(int left, int right);
    }
}