using System.Threading;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Imposter.Tests.Features.CallAllocations;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.IndexerImpersonation
{
    // Getter builders taken from one indexer builder configure one setup, however many threads configure it.
    public class GetterSetupSharingTests
    {
        [Fact]
        public void Given_TwoGetterBuildersOfOneIndexerBuilder_When_Configuring_Should_ShareOneSetup()
        {
            var imposter = new ICallAllocationSutImposter();
            var calls = 0;
            var indexer = imposter[Arg<int>.Is(1)];

            indexer.Getter().Returns(10);
            indexer.Getter().Callback(_ => calls++);

            imposter.Instance()[1].ShouldBe(10);
            calls.ShouldBe(1);
        }

        [Fact]
        public async Task Given_ConcurrentConfiguration_When_Getting_Should_RunEveryCallbackOfTheOneSetup()
        {
            const int threads = 8;
            var imposter = new ICallAllocationSutImposter();
            var indexer = imposter[Arg<int>.Is(1)];
            var calls = 0;
            using var start = new Barrier(threads);

            var configurations = new Task[threads];
            for (var i = 0; i < threads; i++)
            {
                configurations[i] = Task.Run(() =>
                {
                    start.SignalAndWait();
                    indexer.Getter().Callback(_ => Interlocked.Increment(ref calls));
                });
            }

            await Task.WhenAll(configurations);
            _ = imposter.Instance()[1];

            calls.ShouldBe(threads);
        }
    }
}
