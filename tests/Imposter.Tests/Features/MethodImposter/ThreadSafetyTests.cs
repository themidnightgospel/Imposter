using System.Threading;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.MethodImposter
{
    public class ThreadSafetyTests
    {
        private const int ThreadCount = 100;
        private readonly IMethodSetupFeatureSutImposter _sut = new IMethodSetupFeatureSutImposter();

        [Fact]
        public void GivenConcurrentOperations_WhenMixingVoidAndReturnMethods_ShouldNotThrowExceptions()
        {
            var threads = new Thread[ThreadCount];

            for (int i = 0; i < ThreadCount; i++)
            {
                var index = i;
                threads[i] = index % 2 == 0
                    ? new Thread(() => _sut.Instance().IntNoParams())
                    : new Thread(() => _sut.Instance().VoidNoParams());
            }

            foreach (var thread in threads)
            {
                thread.Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

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
            var threads = new Thread[ThreadCount];

            for (int i = 0; i < ThreadCount; i++)
            {
                var index = i;
                threads[i] = new Thread(() => results[index] = _sut.Instance().IntNoParams());
            }

            foreach (var thread in threads)
            {
                thread.Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            results.ShouldBeUnique();
            results.ShouldAllBe(x => x >= 0 && x < ThreadCount);
        }
    }
}
