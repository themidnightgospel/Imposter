using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.Docs.GettingStarted;
using Shouldly;
using Xunit;

[assembly: GenerateImposter(typeof(IMyService))]
[assembly: GenerateImposter(typeof(BaseService))]

namespace Imposter.Tests.Features.Docs.GettingStarted
{
    public interface IMyService
    {
        int GetNumber();
        int Increment(int value);
        event EventHandler SomethingHappened;
        int this[int key] { get; set; }
    }

    public abstract class BaseService
    {
        public virtual int GetNumber() => 0;
    }

    public class GettingStartedTests
    {
        [Fact]
        public void Interface_Generation_And_Usage()
        {
            // C# 9-13 usage
            var imposter = new IMyServiceImposter(); // default: Implicit behavior
            var service = imposter.Instance(); // user-facing instance

            // Mock a method
            imposter.GetNumber().Returns(42);
            service.GetNumber().ShouldBe(42);

            // Parameterized with a delegate
            imposter.Increment(Arg<int>.Any()).Returns(v => v + 2);

            // Match a specific value directly (implicit Arg<int> conversion)
            imposter.Increment(20).Returns(200);
            service.Increment(5).ShouldBe(7);
            service.Increment(20).ShouldBe(200);
        }

        [Fact]
        public void Class_Generation_And_Usage()
        {
            // C# 9-13 usage
            var imposter = new BaseServiceImposter();
            var service = imposter.Instance();

            // Default implicit behavior returns the base implementation result
            service.GetNumber().ShouldBe(0);

            // Arrange an override
            imposter.GetNumber().Returns(10);
            service.GetNumber().ShouldBe(10);
        }

#if ROSLYN_5_OR_GREATER
        [Fact]
        public void Interface_And_Class_Usage_CSharp14()
        {
            var imposter = IMyService.Imposter();
            var service = imposter.Instance();

            var classImposter = BaseService.Imposter();
            var classService = classImposter.Instance();

            // Default base implementation should be callable
            classService.GetNumber().ShouldBe(0);
        }
#endif
    }
}
