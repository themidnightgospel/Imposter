using System.Threading;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.PropertyImpersonation
{
    public class ThreadSafetyTests
    {
        private const int ThreadCount = 200;
        private readonly IPropertySetupSutImposter _sut =
#if USE_CSHARP14
        IPropertySetupSut.Imposter();
#else
        new IPropertySetupSutImposter();
#endif

        [Fact]
        public void GivenConcurrentOperations_WhenMixingGettersAndSetters_ShouldNotThrowExceptions()
        {
            var threads = new Thread[ThreadCount];
            var startSignal = new ManualResetEventSlim(false);
            var readySignal = new CountdownEvent(threads.Length);

            // Mix of getters and setters
            for (int i = 0; i < ThreadCount; i++)
            {
                var index = i;
                if (index % 2 == 0)
                {
                    threads[i] = new Thread(() =>
                    {
                        readySignal.Signal();
                        startSignal.Wait();

                        _sut.Instance().Age = index;
                    });
                }
                else
                {
                    threads[i] = new Thread(() =>
                    {
                        readySignal.Signal();
                        startSignal.Wait();

                        var _ = _sut.Instance().Age;
                    });
                }
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
            var threads = new Thread[ThreadCount];
            var startSignal = new ManualResetEventSlim(false);
            var readySignal = new CountdownEvent(threads.Length);

            for (int i = 0; i < ThreadCount; i++)
            {
                var index = i;
                threads[i] = new Thread(() =>
                {
                    readySignal.Signal();
                    startSignal.Wait();

                    results[index] = _sut.Instance().Age;
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

            // All results should be unique values from 0-99 (in some order)
            results.ShouldBeUnique();
            results.ShouldAllBe(x => x >= 0 && x < ThreadCount);
        }
    }
}
