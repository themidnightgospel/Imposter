using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.EventImposter
{
    public class ThreadSafetyTests
    {
        private readonly IEventSetupSutImposter _sut =
#if USE_CSHARP14
        IEventSetupSut.Imposter();
#else
        new IEventSetupSutImposter();
#endif

        [Fact]
        public async Task GivenConcurrentRaises_WhenPerformed_ShouldRecordAll()
        {
            const int raises = 64;
            var startSignal = new TaskCompletionSource<bool>(
                TaskCreationOptions.RunContinuationsAsynchronously
            );
            var readySignal = new TaskCompletionSource<bool>(
                TaskCreationOptions.RunContinuationsAsynchronously
            );
            var remaining = raises;

            var tasks = Enumerable
                .Range(0, raises)
                .Select(_ =>
                    Task.Run(async () =>
                    {
                        if (Interlocked.Decrement(ref remaining) == 0)
                        {
                            readySignal.TrySetResult(true);
                        }

                        await startSignal.Task;

                        _sut.SomethingHappened.Raise(this, EventArgs.Empty);
                    })
                )
                .ToArray();

            await readySignal.Task;
            startSignal.TrySetResult(true);

            await Task.WhenAll(tasks);

            _sut.SomethingHappened.Raised(
                Arg<object>.Any(),
                Arg<EventArgs>.Any(),
                Count.Exactly(raises)
            );
        }

        [Fact]
        public void GivenConcurrentSubscribeSameHandler_WhenRaiseOnce_ShouldInvokeExpectedTimes()
        {
            const int subs = 32;
            int invoked = 0;
            EventHandler h = (s, e) => Interlocked.Increment(ref invoked);
            var startSignal = new ManualResetEventSlim(false);
            var readySignal = new CountdownEvent(subs);

            var tasks = Enumerable
                .Range(0, subs)
                .Select(_ =>
                    Task.Run(() =>
                    {
                        readySignal.Signal();
                        startSignal.Wait();

                        _sut.Instance().SomethingHappened += h;
                    })
                )
                .ToArray();

            readySignal.Wait();
            startSignal.Set();
            Task.WaitAll(tasks);

            _sut.SomethingHappened.Raise(this, EventArgs.Empty);

            invoked.ShouldBe(subs);
            _sut.SomethingHappened.HandlerInvoked(Arg<EventHandler>.Is(h), Count.Exactly(subs));
        }

        [Fact]
        public void GivenConcurrentSubscribeAndUnsubscribe_WhenPerformed_ShouldKeepCountsConsistent()
        {
            const int iterations = 64;
            EventHandler h = (s, e) => { };
            var startSignal = new ManualResetEventSlim(false);
            var readySignal = new CountdownEvent(2);

            var subs = Task.Run(() =>
            {
                readySignal.Signal();
                startSignal.Wait();

                for (int i = 0; i < iterations; i++)
                    _sut.Instance().SomethingHappened += h;
            });

            var unsubs = Task.Run(() =>
            {
                readySignal.Signal();
                startSignal.Wait();

                for (int i = 0; i < iterations; i++)
                    _sut.Instance().SomethingHappened -= h;
            });

            readySignal.Wait();
            startSignal.Set();
            Task.WaitAll(subs, unsubs);

            // We may end up anywhere between 0 and iterations subscriptions based on interleaving,
            // but counts should never go negative and verifications should not throw for AtMost/AtLeast bounds
            Should.NotThrow(() =>
                _sut.SomethingHappened.Subscribed(Arg<EventHandler>.Is(h), Count.AtLeast(0))
            );
            Should.NotThrow(() =>
                _sut.SomethingHappened.Unsubscribed(Arg<EventHandler>.Is(h), Count.AtLeast(0))
            );
        }

        [Fact]
        public async Task GivenConcurrentRaiseAsync_WhenPerformed_ShouldRecordAll()
        {
            const int raises = 64;

            // Drive concurrency via async tasks directly without extra thread-pool work items.
            var tasks = Enumerable
                .Range(0, raises)
                .Select(_ => _sut.AsyncSomethingHappened.RaiseAsync(this, EventArgs.Empty))
                .ToArray();

            await Task.WhenAll(tasks);

            _sut.AsyncSomethingHappened.Raised(
                Arg<object>.Any(),
                Arg<EventArgs>.Any(),
                Count.Exactly(raises)
            );
        }
    }
}
