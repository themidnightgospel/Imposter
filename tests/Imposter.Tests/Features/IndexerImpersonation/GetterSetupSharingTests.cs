using System.Threading;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.IndexerImpersonation
{
    // Getter builders taken from one indexer builder configure one setup, however many threads configure it.
    public class GetterSetupSharingTests
    {
        private const int ThreadCount = 8;

        [Fact]
        public void Given_TwoGetterBuildersOfOneIndexerBuilder_When_Configuring_Should_ShareOneSetup()
        {
            var imposter = new IIndexerSetupSutImposter();
            var calls = 0;
            var indexer = imposter[Arg<int>.Is(1)];

            indexer.Getter().Returns(10);
            indexer.Getter().Callback(_ => calls++);

            imposter.Instance()[1].ShouldBe(10);
            calls.ShouldBe(1);
        }

        [Fact]
        public void Given_ConcurrentConfiguration_When_Getting_Should_RunEveryCallbackOfTheOneSetup()
        {
            var imposter = new IIndexerSetupSutImposter();
            var indexer = imposter[Arg<int>.Is(1)];
            var calls = 0;
            var threads = new Thread[ThreadCount];
            using var startSignal = new ManualResetEventSlim(false);
            using var readySignal = new CountdownEvent(ThreadCount);

            for (var i = 0; i < ThreadCount; i++)
            {
                threads[i] = new Thread(() =>
                {
                    readySignal.Signal();
                    startSignal.Wait();
                    indexer.Getter().Callback(_ => Interlocked.Increment(ref calls));
                });
                threads[i].Start();
            }

            readySignal.Wait();
            startSignal.Set();
            foreach (var thread in threads)
                thread.Join();

            _ = imposter.Instance()[1];

            calls.ShouldBe(ThreadCount);
        }
    }
}
