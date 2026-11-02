using System;
using System.Linq;
using System.Threading;
using Imposter.Abstractions;
using Imposter.Tests.Shared;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.MethodImpersonation
{
    public class ThreadSafetyTests
    {
        private const int ThreadCount = 200;
        private readonly IMethodSetupFeatureSutImposter _sut =
#if USE_CSHARP14
        IMethodSetupFeatureSut.Imposter();
#else
        new IMethodSetupFeatureSutImposter();
#endif

        [Fact]
        public void GivenConcurrentOperations_WhenMixingVoidAndReturnMethods_ShouldNotThrowExceptions()
        {
            DedicatedThreads.RunTogether(
                ThreadCount,
                index =>
                {
                    if (index % 2 == 0)
                    {
                        _sut.Instance().IntNoParams();
                    }
                    else
                    {
                        _sut.Instance().VoidNoParams();
                    }
                }
            );

            Should.NotThrow(() => _sut.IntNoParams().Called(Count.AtLeast(ThreadCount / 2)));
            Should.NotThrow(() => _sut.VoidNoParams().Called(Count.AtLeast(ThreadCount / 2)));
        }

        [Fact]
        public void GivenSequentialReturnsSetup_WhenMethodsInvokedConcurrently_ShouldConsumeUniqueValues()
        {
            var builder = _sut.IntNoParams();
            var continuation = builder.Returns(0);
            for (int i = 1; i < ThreadCount; i++)
            {
                continuation = continuation.Then().Returns(i);
            }

            var results = new int[ThreadCount];
            DedicatedThreads.RunTogether(
                ThreadCount,
                index => results[index] = _sut.Instance().IntNoParams()
            );

            results.ShouldBeUnique();
            results.ShouldAllBe(x => x >= 0 && x < ThreadCount);
        }

        [Fact]
        public void GivenReturnsSetup_WhenFirstCallsAreConcurrent_ShouldReturnConfiguredValueEveryTime()
        {
            var outcomes = ConcurrentFirstCalls.CountOutcomes(
                () => ConfiguredInstance(ImposterMode.Implicit, it => it.IntNoParams().Returns(42)),
                it => it.IntNoParams()
            );

            outcomes.ShouldBe(ConcurrentFirstCalls.EveryCall("42"));
        }

        [Fact]
        public void GivenReturnsSetupInExplicitMode_WhenFirstCallsAreConcurrent_ShouldReturnConfiguredValueEveryTime()
        {
            var outcomes = ConcurrentFirstCalls.CountOutcomes(
                () => ConfiguredInstance(ImposterMode.Explicit, it => it.IntNoParams().Returns(42)),
                it => it.IntNoParams()
            );

            outcomes.ShouldBe(ConcurrentFirstCalls.EveryCall("42"));
        }

        [Fact]
        public void GivenReturnsSequence_WhenFirstCallsAreConcurrent_ShouldRepeatLastValueAfterSequence()
        {
            var outcomes = ConcurrentFirstCalls.CountOutcomes(
                () =>
                    ConfiguredInstance(
                        ImposterMode.Implicit,
                        it => it.IntNoParams().Returns(1).Then().Returns(2)
                    ),
                it => it.IntNoParams()
            );

            outcomes.ShouldBe(
                ConcurrentFirstCalls.FirstCallThenEveryOtherCall("1", "2"),
                ignoreOrder: true
            );
        }

        [Fact]
        public void GivenLongSequence_WhenEightThreadsCallMoreTimesThanItHasSteps_ShouldServeEachOnceThenRepeatTheLast()
        {
            const int steps = 1_000;
            const int threads = 8;
            const int callsPerThread = 250;
            for (var round = 0; round < 50; round++)
            {
                var imposter = new IMethodSetupFeatureSutImposter();
                var continuation = imposter.IntNoParams().Returns(0);
                for (var step = 1; step < steps; step++)
                {
                    continuation = continuation.Then().Returns(step);
                }

                var instance = imposter.Instance();
                var results = new int[threads * callsPerThread];
                using var start = new Barrier(threads);
                var workers = new Thread[threads];
                for (var thread = 0; thread < threads; thread++)
                {
                    var offset = thread * callsPerThread;
                    workers[thread] = new Thread(() =>
                    {
                        start.SignalAndWait();
                        for (var call = 0; call < callsPerThread; call++)
                        {
                            results[offset + call] = instance.IntNoParams();
                        }
                    });
                    workers[thread].Start();
                }

                foreach (var worker in workers)
                {
                    worker.Join();
                }

                results
                    .Where(value => value != steps - 1)
                    .OrderBy(value => value)
                    .ShouldBe(Enumerable.Range(0, steps - 1));
                results.Count(value => value == steps - 1).ShouldBe(results.Length - (steps - 1));
                instance.IntNoParams().ShouldBe(steps - 1);
            }
        }

        private static IMethodSetupFeatureSut ConfiguredInstance(
            ImposterMode mode,
            Action<IMethodSetupFeatureSutImposter> setup
        )
        {
            var imposter = new IMethodSetupFeatureSutImposter(mode);
            setup(imposter);
            return imposter.Instance();
        }
    }
}
