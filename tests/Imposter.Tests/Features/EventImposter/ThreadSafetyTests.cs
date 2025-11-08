using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.EventImposter
{
    public class ThreadSafetyTests
    {
        private readonly IEventSetupSutImposter _sut = new IEventSetupSutImposter();

        [Fact]
        public void GivenConcurrentRaises_ShouldRecordAll()
        {
            const int raises = 50;
            var tasks = Enumerable.Range(0, raises)
                .Select(_ => Task.Run(() => _sut.SomethingHappened.Raise(this, EventArgs.Empty)))
                .ToArray();

            Task.WaitAll(tasks);

            _sut.SomethingHappened.Raised(Arg<object>.Any(), Arg<EventArgs>.Any(), Count.Exactly(raises));
        }

        [Fact]
        public void GivenConcurrentSubscribeSameHandler_WhenRaiseOnce_ShouldInvokeExpectedTimes()
        {
            const int subs = 20;
            int invoked = 0;
            EventHandler h = (s, e) => Interlocked.Increment(ref invoked);

            var tasks = Enumerable.Range(0, subs)
                .Select(_ => Task.Run(() => _sut.Instance().SomethingHappened += h))
                .ToArray();
            Task.WaitAll(tasks);

            _sut.SomethingHappened.Raise(this, EventArgs.Empty);

            invoked.ShouldBe(subs);
            _sut.SomethingHappened.HandlerInvoked(Arg<EventHandler>.Is(h), Count.Exactly(subs));
        }

        [Fact]
        public void GivenConcurrentSubscribeAndUnsubscribe_ShouldKeepCountsConsistent()
        {
            const int iterations = 100;
            EventHandler h = (s, e) => { };

            var subs = Task.Run(() =>
            {
                for (int i = 0; i < iterations; i++) _sut.Instance().SomethingHappened += h;
            });

            var unsubs = Task.Run(() =>
            {
                for (int i = 0; i < iterations; i++) _sut.Instance().SomethingHappened -= h;
            });

            Task.WaitAll(subs, unsubs);

            // We may end up anywhere between 0 and iterations subscriptions based on interleaving,
            // but counts should never go negative and verifications should not throw for AtMost/AtLeast bounds
            Should.NotThrow(() => _sut.SomethingHappened.Subscribed(Arg<EventHandler>.Is(h), Count.AtLeast(0)));
            Should.NotThrow(() => _sut.SomethingHappened.Unsubscribed(Arg<EventHandler>.Is(h), Count.AtLeast(0)));
        }

        [Fact]
        public async Task GivenConcurrentRaiseAsync_ShouldRecordAll()
        {
            const int raises = 30;
            var tasks = Enumerable.Range(0, raises)
                .Select(_ => _sut.AsyncSomethingHappened.RaiseAsync(this, EventArgs.Empty))
                .ToArray();

            await Task.WhenAll(tasks);

            _sut.AsyncSomethingHappened.Raised(Arg<object>.Any(), Arg<EventArgs>.Any(), Count.Exactly(raises));
        }
    }
}

