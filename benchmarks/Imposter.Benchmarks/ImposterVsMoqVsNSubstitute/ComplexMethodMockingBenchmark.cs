using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using Imposter.Abstractions;
using Imposter.Benchmarks.ImposterVsMoqVsNSubstitute;
using Moq;
using NSubstitute;
using Rocks;
using NSubArg = NSubstitute.Arg;
using RocksArg = Rocks.Arg;

[assembly: GenerateImposter(typeof(ComplexMethodMockingBenchmark.IComplexService))]
[assembly: Rock(typeof(ComplexMethodMockingBenchmark.IComplexService), BuildType.Create)]

namespace Imposter.Benchmarks.ImposterVsMoqVsNSubstitute;

[MemoryDiagnoser]
public class ComplexMethodMockingBenchmark
{
    private const string CriticalRoute = "critical-batch";

    [Params(1, 10, 100, 1000)]
    public int Iteration;

    [Benchmark]
    public void Mock()
    {
        for (var i = 0; i < Iteration; i++)
        {
            var severityFloor = i % 5;
            var mock = new Mock<IComplexService>();

            mock.SetupSequence(service =>
                    service.Process(
                        It.Is<string>(route =>
                            route.StartsWith("critical", StringComparison.OrdinalIgnoreCase)
                        ),
                        It.Is<int>(severity => severity > severityFloor),
                        It.Is<OperationContext>(context =>
                            context.Region == "us-east" && context.RetryCount < 3
                        )
                    )
                )
                .Returns("primed")
                .Returns("engaged")
                .Returns("completed");

            ConsumeSequence(mock.Object, severityFloor);
        }
    }

    [Benchmark]
    public void NSub()
    {
        for (var i = 0; i < Iteration; i++)
        {
            var severityFloor = i % 5;
            var substitute = Substitute.For<IComplexService>();

            substitute
                .Process(
                    NSubArg.Is<string>(route =>
                        route.StartsWith("critical", StringComparison.OrdinalIgnoreCase)
                    ),
                    NSubArg.Is<int>(severity => severity > severityFloor),
                    NSubArg.Is<OperationContext>(context =>
                        context.Region == "us-east" && context.RetryCount < 3
                    )
                )
                .Returns("primed", "engaged", "completed");

            ConsumeSequence(substitute, severityFloor);
        }
    }

    [Benchmark]
    public void Rocks()
    {
        for (var i = 0; i < Iteration; i++)
        {
            var severityFloor = i % 5;
            var expectations = new IComplexServiceCreateExpectations();
            var returns = new Queue<string>(["primed", "engaged", "completed"]);

            expectations
                .Setups.Process(
                    RocksArg.Validate<string>(route =>
                        route.StartsWith("critical", StringComparison.OrdinalIgnoreCase)
                    ),
                    RocksArg.Validate<int>(severity => severity > severityFloor),
                    RocksArg.Validate<OperationContext>(context =>
                        context.Region == "us-east" && context.RetryCount < 3
                    )
                )
                .ExpectedCallCount(3)
                .Callback((_, _, _) => returns.Dequeue());

            ConsumeSequence(expectations.Instance(), severityFloor);
        }
    }

    [Benchmark]
    public void Imposter()
    {
        for (var i = 0; i < Iteration; i++)
        {
            var severityFloor = i % 5;
            var imposter = new IComplexServiceImposter();

            imposter
                .Process(
                    Arg<string>.Is(route =>
                        route.StartsWith("critical", StringComparison.OrdinalIgnoreCase)
                    ),
                    Arg<int>.Is(severity => severity > severityFloor),
                    Arg<OperationContext>.Is(context =>
                        context.Region == "us-east" && context.RetryCount < 3
                    )
                )
                .Returns("primed")
                .Then()
                .Returns("engaged")
                .Then()
                .Returns("completed");

            var instance = imposter.Instance();
            ConsumeSequence(instance, severityFloor);
        }
    }

    private static void ConsumeSequence(IComplexService service, int severityFloor)
    {
        var baseSeverity = severityFloor + 1;
        var context = new OperationContext("us-east", 1);

        service.Process(CriticalRoute, baseSeverity, context);
        service.Process(CriticalRoute, baseSeverity + 1, context);
        service.Process(CriticalRoute, baseSeverity + 2, context);
    }

    public interface IComplexService
    {
        string Process(string route, int severity, OperationContext context);
    }

    public readonly record struct OperationContext(string Region, int RetryCount);
}
