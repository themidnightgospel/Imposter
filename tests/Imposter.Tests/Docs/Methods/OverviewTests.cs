using System;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

[assembly: GenerateImposter(typeof(Imposter.Tests.Docs.Methods.IQuickStartService))]

namespace Imposter.Tests.Docs.Methods
{
    public interface IQuickStartService
    {
        int GetNumber();
        int Increment(int v);
        Task<int> GetNumberAsync();
        Task DoWorkAsync();
        int Combine(int a, int b);
        int VirtualCompute(int v);
    }

    public class OverviewTests
    {
        [Fact]
        public async Task Methods_Index_Quick_Start_And_Samples()
        {
            var imposter = new IQuickStartServiceImposter();
            var service = imposter.Instance();

            imposter.GetNumber().Returns(42);
            service.GetNumber().ShouldBe(42);

            // Fallback first, then more specific rules
            imposter.Increment(Arg<int>.Any()).Returns(0);
            imposter.Increment(Arg<int>.Is(x => x > 0)).Returns(v => v + 2);
            imposter.Increment(5).Returns(50);

            service.Increment(1).ShouldBe(3);
            service.Increment(5).ShouldBe(50);
            service.Increment(-1).ShouldBe(0);

            // Arranging Returns sequencing
            imposter.GetNumber()
                .Returns(1)
                .Then().Returns(() => 2)
                .Then().Returns(3);
            service.GetNumber();
            service.GetNumber();
            service.GetNumber();

            // Async methods
            imposter.GetNumberAsync().ReturnsAsync(42);
            imposter.GetNumberAsync().Returns(() => Task.FromResult(1));

            imposter.DoWorkAsync().Returns(Task.CompletedTask);

            imposter.GetNumberAsync()
                .ReturnsAsync(1)
                .Then().Returns(() => Task.FromResult(2));

            await service.GetNumberAsync();
            await service.GetNumberAsync();

            // Exceptions and mix with returns
            imposter.GetNumber().Throws<InvalidOperationException>();
            Should.Throw<InvalidOperationException>(() => service.GetNumber());

            imposter.GetNumber().Throws(new System.Exception("boom"));
            var ex = Should.Throw<System.Exception>(() => service.GetNumber());
            ex.Message.ShouldBe("boom");

            imposter.GetNumber().Throws(() => new System.Exception("deferred"));
            var ex2 = Should.Throw<System.Exception>(() => service.GetNumber());
            ex2.Message.ShouldBe("deferred");

            imposter.GetNumber()
                .Returns(1)
                .Then().Throws<System.InvalidOperationException>()
                .Then().Returns(2);
            service.GetNumber();
            Should.Throw<System.InvalidOperationException>(() => service.GetNumber());
            service.GetNumber();

            // Callbacks
            var stages = new System.Collections.Generic.List<string>();
            imposter.GetNumber()
                .Returns(() => { stages.Add("return"); return 42; })
                .Callback(() => stages.Add("first"))
                .Callback(() => stages.Add("second"));
            service.GetNumber();

            imposter.Increment(Arg<int>.Any()).Callback(v => { var _ = v; });

            imposter.Increment(Arg<int>.Any())
                .Returns(_ => 10)
                .Callback(_ => { })
                .Then()
                .Returns(_ => 20)
                .Callback(_ => { });
            service.Increment(1);
            service.Increment(2);

            // Argument matching combine
            imposter.Combine(
                Arg<int>.Is(x => x > 0),
                Arg<int>.Is(y => y < 10))
              .Returns(42);
            service.Combine(1, 2).ShouldBe(42);

            // Verification
            imposter.Increment(Arg<int>.Any()).Called(Count.AtLeast(2));
            imposter.Increment(2).Called(Count.Once());
        }
    }
}
