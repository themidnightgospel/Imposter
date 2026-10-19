using Imposter.Abstractions;
using Imposter.Tests.Shared;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.CallAllocations
{
    public class SetupLookupTests
    {
        [Fact]
        public void GivenOverlappingSetups_WhenCalling_ShouldUseNewestMatchingSetup()
        {
            var imposter = new ICallAllocationSutImposter();
            imposter.Square(Arg<int>.Any()).Returns(10);
            imposter.Square(Arg<int>.Is(value => value == 2)).Returns(20);
            imposter.Square(Arg<int>.Is(value => value == 3)).Returns(30);
            imposter.Echo<int>(Arg<int>.Any()).Returns(10);
            imposter.Echo<int>(Arg<int>.Is(value => value == 2)).Returns(20);
            imposter.Echo<string>(Arg<string>.Any()).Returns("different type");
            imposter[Arg<int>.Any()].Getter().Returns(10);
            imposter[Arg<int>.Is(value => value == 2)].Getter().Returns(20);
            imposter[Arg<int>.Is(value => value == 3)].Getter().Returns(30);

            var instance = imposter.Instance();
            instance.Square(1).ShouldBe(10);
            instance.Square(2).ShouldBe(20);
            instance.Square(3).ShouldBe(30);
            instance.Echo(1).ShouldBe(10);
            instance.Echo(2).ShouldBe(20);
            instance.Echo("value").ShouldBe("different type");
            instance[1].ShouldBe(10);
            instance[2].ShouldBe(20);
            instance[3].ShouldBe(30);
        }

        [Theory]
        [InlineData(ImposterMode.Implicit)]
        [InlineData(ImposterMode.Explicit)]
        public void GivenConcurrentSetupAddition_WhenCalling_ShouldObserveOldOrNewResults(
            ImposterMode mode
        )
        {
            var imposter = new ICallAllocationSutImposter(mode);
            imposter.Square(Arg<int>.Any()).Returns(0);
            imposter.Echo<int>(Arg<int>.Any()).Returns(0);
            imposter[Arg<int>.Any()].Getter().Returns(0);
            var instance = imposter.Instance();

            // Worker 0 adds setups while worker 1 calls.
            DedicatedThreads.RunTogether(
                2,
                worker =>
                {
                    if (worker == 0)
                    {
                        for (var i = 0; i < 100; i++)
                        {
                            // Each predicate has a distinct closure so indexer setups also create distinct lookup entries.
                            var iteration = i;
                            imposter
                                .Square(Arg<int>.Is(value => value == 1 && iteration >= 0))
                                .Returns(1);
                            imposter
                                .Echo<int>(Arg<int>.Is(value => value == 1 && iteration >= 0))
                                .Returns(1);
                            imposter[Arg<int>.Is(value => value == 1 && iteration >= 0)]
                                .Getter()
                                .Returns(1);
                        }
                    }
                    else
                    {
                        for (var i = 0; i < 1000; i++)
                        {
                            instance.Square(1).ShouldBeInRange(0, 1);
                            instance.Echo(1).ShouldBeInRange(0, 1);
                            instance[1].ShouldBeInRange(0, 1);
                        }
                    }
                }
            );

            instance.Square(1).ShouldBe(1);
            instance.Echo(1).ShouldBe(1);
            instance[1].ShouldBe(1);
        }
    }
}
