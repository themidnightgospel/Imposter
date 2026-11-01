using System;
using System.Linq;
using System.Threading;
using Imposter.Abstractions;
using Imposter.Tests.Shared;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.PropertyImpersonation
{
    public class ThreadSafetyTests
    {
        private const int ThreadCount = 200;
        private const int FirstReaderCount = 8;
        private readonly IPropertySetupSutImposter _sut =
#if USE_CSHARP14
        IPropertySetupSut.Imposter();
#else
        new IPropertySetupSutImposter();
#endif

        [Fact]
        public void GivenConcurrentOperations_WhenMixingGettersAndSetters_ShouldNotThrowExceptions()
        {
            DedicatedThreads.RunTogether(
                ThreadCount,
                index =>
                {
                    if (index % 2 == 0)
                    {
                        _sut.Instance().Age = index;
                    }
                    else
                    {
                        _ = _sut.Instance().Age;
                    }
                }
            );

            // Should not throw - just testing for thread safety
            Should.NotThrow(() => _sut.Age.Getter().Called(Count.Exactly(ThreadCount / 2)));
            Should.NotThrow(() =>
                _sut.Age.Setter(Arg<int>.Any()).Called(Count.Exactly(ThreadCount / 2))
            );
        }

        [Fact]
        public void GivenSequentialReturnsSetup_WhenAccessedConcurrently_ShouldWorkCorrectly()
        {
            // Setup many sequential returns
            for (int i = 0; i < ThreadCount; i++)
            {
                _sut.Age.Getter().Returns(i);
            }

            var results = new int[ThreadCount];
            DedicatedThreads.RunTogether(
                ThreadCount,
                index => results[index] = _sut.Instance().Age
            );

            results.ShouldBeUnique();
            results.ShouldAllBe(x => x >= 0 && x < ThreadCount);
        }

        [Fact]
        public void GivenGetterReturnsSetup_WhenFirstReadsAreConcurrent_ShouldReturnConfiguredValueEveryTime()
        {
            var outcomes = ConcurrentFirstCalls.CountOutcomes(
                () => ConfiguredInstance(it => it.Age.Getter().Returns(42)),
                it => it.Age
            );

            outcomes.ShouldBe(ConcurrentFirstCalls.EveryCall("42"));
        }

        [Fact]
        public void GivenGetterReturnsSequence_WhenFirstReadsAreConcurrent_ShouldRepeatLastValueAfterSequence()
        {
            var outcomes = ConcurrentFirstCalls.CountOutcomes(
                () => ConfiguredInstance(it => it.Age.Getter().Returns(1).Then().Returns(2)),
                it => it.Age
            );

            outcomes.ShouldBe(
                ConcurrentFirstCalls.FirstCallThenEveryOtherCall("1", "2"),
                ignoreOrder: true
            );
        }

        // A getter callback runs after the read is counted and before its value is chosen, so the barrier holds every
        // first read there until all of them are counted.
        [Fact]
        public void GivenClassPropertyWithInitializer_WhenFirstReadsAreConcurrent_ShouldReturnBaseValueEveryTime()
        {
            var imposter = new ClassWithInitialValuePropertyImposter();
            using var countedReads = new Barrier(FirstReaderCount);
            imposter.A.Getter().Callback(() => WaitForOtherReads(countedReads));
            var instance = imposter.Instance();
            var values = new int[FirstReaderCount];

            DedicatedThreads.RunTogether(FirstReaderCount, reader => values[reader] = instance.A);

            values.ShouldAllBe(it => it == 11);
        }

        [Fact]
        public void GivenClassSpanPropertyWithInitializer_WhenFirstReadsAreConcurrent_ShouldReturnBaseValueEveryTime()
        {
            var imposter = new SpanPropertyClassImposter();
            using var countedReads = new Barrier(FirstReaderCount);
            imposter.Buffer.Getter().Callback(() => WaitForOtherReads(countedReads));
            var instance = imposter.Instance();
            var buffers = new byte[FirstReaderCount][];

            DedicatedThreads.RunTogether(
                FirstReaderCount,
                reader => buffers[reader] = instance.Buffer.ToArray()
            );

            buffers.ShouldAllBe(it => it.SequenceEqual(new byte[] { 1, 2 }));
        }

        private static IPropertySetupSut ConfiguredInstance(Action<IPropertySetupSutImposter> setup)
        {
            var imposter = new IPropertySetupSutImposter();
            setup(imposter);
            return imposter.Instance();
        }

        // A reader that fails before reaching the barrier would otherwise leave the others waiting for good.
        private static void WaitForOtherReads(Barrier countedReads) =>
            countedReads.SignalAndWait(TimeSpan.FromSeconds(10)).ShouldBeTrue();
    }
}
