
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Imposter;
using Moq;
using Shouldly;

var mock = new CalculatorImposter();
// TODO
// Builder method

// Throw exception
// mock
//     .Behaviour
//     .AddMethodBehaviour
//     .InvocationBehaviours
//     .Add(new AddMethodInvocationBehaviour
//     {
//         ParameterCriteria = parameters => parameters.left > parameters.right,
//         ResultGenerator = parameters => parameters.left * parameters.right
//     });

// var res = mock.Add(8, 2);
// Console.WriteLine(res);

var calculatorMock = new Mock<ICalculator>();
calculatorMock
    .Setup(it => it.Add(It.IsAny<int>(), It.IsAny<int>()))
    .Returns(1);

calculatorMock.Verify(it => it.Add(It.IsAny<int>(), It.IsAny<int>()), Times.Once);

BenchmarkRunner.Run<Benchy>();

Console.ReadKey();

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
    public void Imposter()
    {
        var imposter = new CalculatorImposter();

        imposter
            .Add(ParameterCriteria<int>.IsAny(), ParameterCriteria<int>.IsAny())
            .Returns((left, right) => left * right);

        imposter.ImposterInstance().Add(10, 9).ShouldBe(10 * 9);
    }
}

