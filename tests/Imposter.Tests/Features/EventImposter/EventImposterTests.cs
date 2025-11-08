using System;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.EventImposter
{
    public class EventImposterTests
    {
        private readonly IEventSetupSutImposter _sut = new IEventSetupSutImposter();

        [Fact]
        public void GivenSubscriber_WhenRaise_ShouldInvokeHandler()
        {
            var invoked = false;
            _sut.Instance().SomethingHappened += (sender, args) => invoked = true;

            _sut.SomethingHappened.Raise(this, EventArgs.Empty);

            invoked.ShouldBeTrue();
        }

        [Fact]
        public async Task GivenAsyncSubscriber_WhenRaiseAsync_ShouldAwaitHandler()
        {
            var invoked = false;
            _sut.Instance().AsyncSomethingHappened += async (sender, args) =>
            {
                await Task.Delay(10);
                invoked = true;
            };

            await _sut.AsyncSomethingHappened.RaiseAsync(this, EventArgs.Empty);

            invoked.ShouldBeTrue();
        }

        [Fact]
        public async Task GivenValueTaskSubscriber_WhenRaiseAsync_ShouldAwaitHandler()
        {
            var invoked = false;
            _sut.Instance().ValueTaskSomethingHappened += async (sender, args) =>
            {
                await Task.Delay(10);
                invoked = true;
            };

            await _sut.ValueTaskSomethingHappened.RaiseAsync(this, EventArgs.Empty);

            invoked.ShouldBeTrue();
        }

        [Fact]
        public async Task GivenCustomAsyncDelegateSubscriber_WhenRaiseAsync_ShouldAwaitHandler()
        {
            var invoked = false;
            _sut.Instance().CustomAsyncSomethingHappened += async (sender, args) =>
            {
                await Task.Delay(10);
                invoked = true;
            };

            await _sut.CustomAsyncSomethingHappened.RaiseAsync(this, EventArgs.Empty);

            invoked.ShouldBeTrue();
        }

        [Fact]
        public void GivenSubscription_WhenVerifyingSubscribedCount_ShouldMatch()
        {
            EventHandler handler = (sender, args) => { };
            _sut.Instance().SomethingHappened += handler;

            Should.NotThrow(() =>
                _sut.SomethingHappened.Subscribed(
                    Arg<EventHandler>.Is(h => h == handler),
                    Count.Exactly(1)));
        }

        [Fact]
        public void GivenRaisedEvent_WhenVerifyingRaisedCriteria_ShouldMatch()
        {
            var sender = new object();
            var args = EventArgs.Empty;

            _sut.SomethingHappened.Raise(sender, args);

            Should.NotThrow(() =>
                _sut.SomethingHappened.Raised(
                    Arg<object?>.Is(s => s == sender),
                    Arg<EventArgs>.Any(),
                    Count.Exactly(1)));
        }

        [Fact]
        public async Task GivenRaisedCustomAsyncDelegateEvent_WhenVerifyingRaisedCriteria_ShouldMatch()
        {
            var sender = new object();
            var args = EventArgs.Empty;

            _sut.Instance().CustomAsyncSomethingHappened += (s, eventArgs) => Task.CompletedTask;

            await _sut.CustomAsyncSomethingHappened.RaiseAsync(sender, args);

            Should.NotThrow(() =>
                _sut.CustomAsyncSomethingHappened.Raised(
                    Arg<object?>.Is(s => s == sender),
                    Arg<EventArgs>.Is(a => a == args),
                    Count.Exactly(1)));
        }

        [Fact]
        public async Task GivenRaisedValueTaskEvent_WhenVerifyingRaisedCriteria_ShouldMatch()
        {
            var sender = new object();
            var args = EventArgs.Empty;

            _sut.Instance().ValueTaskSomethingHappened += (s, eventArgs) => ValueTask.CompletedTask;

            await _sut.ValueTaskSomethingHappened.RaiseAsync(sender, args);

            Should.NotThrow(() =>
                _sut.ValueTaskSomethingHappened.Raised(
                    Arg<object?>.Is(s => s == sender),
                    Arg<EventArgs>.Is(a => a == args),
                    Count.Exactly(1)));
        }
    }
}
