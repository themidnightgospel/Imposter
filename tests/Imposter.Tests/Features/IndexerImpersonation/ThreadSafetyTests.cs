using System;
using System.Threading;
using Imposter.Abstractions;
using Imposter.Tests.Shared;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.IndexerImpersonation
{
    public class ThreadSafetyTests
    {
        private const int ThreadCount = 200;
        private readonly IIndexerSetupSutImposter _sut =
#if USE_CSHARP14
        IIndexerSetupSut.Imposter();
#else
        new IIndexerSetupSutImposter();
#endif

        [Fact]
        public void GivenConcurrentOperations_WhenMixingGetterAndSetter_ShouldNotThrowExceptions()
        {
            var getter = _sut[Arg<int>.Any()].Getter();
            var setter = _sut[Arg<int>.Any()].Setter();

            var instance = _sut.Instance();
            var threads = new Thread[ThreadCount];
            var startSignal = new ManualResetEventSlim(false);
            var readySignal = new CountdownEvent(ThreadCount);

            for (int i = 0; i < ThreadCount; i++)
            {
                var index = i;
                threads[i] =
                    index % 2 == 0
                        ? new Thread(() =>
                        {
                            readySignal.Signal();
                            startSignal.Wait();

                            instance[index] = index;
                        })
                        : new Thread(() =>
                        {
                            readySignal.Signal();
                            startSignal.Wait();

                            var _ = instance[index];
                        });
            }

            foreach (var thread in threads)
            {
                thread.Start();
            }

            readySignal.Wait();
            startSignal.Set();

            foreach (var thread in threads)
            {
                thread.Join();
            }

            Should.NotThrow(() => getter.Called(Count.Exactly(ThreadCount / 2)));
            Should.NotThrow(() => setter.Called(Count.Exactly(ThreadCount / 2)));
        }

        [Fact]
        public void GivenSequentialGetterReturns_WhenAccessedConcurrently_ShouldProduceUniqueValues()
        {
            var builder = _sut[Arg<int>.Any()].Getter();
            var continuation = builder.Returns(0);

            for (int i = 1; i < ThreadCount; i++)
            {
                continuation = continuation.Then().Returns(i);
            }

            var instance = _sut.Instance();
            var results = new int[ThreadCount];
            var threads = new Thread[ThreadCount];
            var startSignal = new ManualResetEventSlim(false);
            var readySignal = new CountdownEvent(ThreadCount);

            for (int i = 0; i < ThreadCount; i++)
            {
                var index = i;
                threads[i] = new Thread(() =>
                {
                    readySignal.Signal();
                    startSignal.Wait();

                    results[index] = instance[index];
                });
            }

            foreach (var thread in threads)
            {
                thread.Start();
            }

            readySignal.Wait();
            startSignal.Set();

            foreach (var thread in threads)
            {
                thread.Join();
            }

            results.ShouldBeUnique();
            results.ShouldAllBe(x => x >= 0 && x < ThreadCount);
        }

        [Fact]
        public void GivenGetterReturnsSetup_WhenFirstReadsAreConcurrent_ShouldReturnConfiguredValueEveryTime()
        {
            var outcomes = ConcurrentFirstCalls.CountOutcomes(
                () => ConfiguredInstance(it => it[Arg<int>.Any()].Getter().Returns(42)),
                it => it[7]
            );

            outcomes.ShouldBe(ConcurrentFirstCalls.EveryCall("42"));
        }

        [Fact]
        public void GivenGetterReturnsSequence_WhenFirstReadsAreConcurrent_ShouldRepeatLastValueAfterSequence()
        {
            var outcomes = ConcurrentFirstCalls.CountOutcomes(
                () =>
                    ConfiguredInstance(it =>
                        it[Arg<int>.Any()].Getter().Returns(1).Then().Returns(2)
                    ),
                it => it[7]
            );

            outcomes.ShouldBe(
                ConcurrentFirstCalls.FirstCallThenEveryOtherCall("1", "2"),
                ignoreOrder: true
            );
        }

        private static IIndexerSetupSut ConfiguredInstance(Action<IIndexerSetupSutImposter> setup)
        {
            var imposter = new IIndexerSetupSutImposter();
            setup(imposter);
            return imposter.Instance();
        }
    }
}
