using System;
using System.Collections.Generic;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.EventImposter
{
    public class SubscriptionTests
    {
        private readonly IEventSetupSutImposter _sut =
#if USE_CSHARP14
            IEventSetupSut.Imposter();
#else
            new IEventSetupSutImposter();
#endif

        [Fact]
        public void GivenMultipleSubscribers_WhenRaise_ShouldInvokeInSubscriptionOrder()
        {
            var order = new List<string>();
            EventHandler h1 = (s, e) => order.Add("h1");
            EventHandler h2 = (s, e) => order.Add("h2");

            _sut.Instance().SomethingHappened += h1;
            _sut.Instance().SomethingHappened += h2;

            _sut.SomethingHappened.Raise(this, EventArgs.Empty);

            order.ShouldBe(new[] { "h1", "h2" }, ignoreOrder: false);
        }

        [Fact]
        public void GivenDuplicateSubscriptions_WhenRaise_ShouldInvokeDuplicateTimes()
        {
            int count = 0;
            EventHandler h = (s, e) => count++;

            _sut.Instance().SomethingHappened += h;
            _sut.Instance().SomethingHappened += h; // duplicate

            _sut.SomethingHappened.Raise(this, EventArgs.Empty);

            count.ShouldBe(2);
            _sut.SomethingHappened.HandlerInvoked(Arg<EventHandler>.Is(h), Count.Exactly(2));
        }

        [Fact]
        public void GivenDuplicateSubscriptions_WhenUnsubscribeOnce_ShouldDecreaseInvocationCount()
        {
            int count = 0;
            EventHandler h = (s, e) => count++;

            _sut.Instance().SomethingHappened += h;
            _sut.Instance().SomethingHappened += h;
            _sut.Instance().SomethingHappened -= h; // remove one

            _sut.SomethingHappened.Raise(this, EventArgs.Empty);

            count.ShouldBe(1);
            _sut.SomethingHappened.HandlerInvoked(Arg<EventHandler>.Is(h), Count.Once());
        }

        [Fact]
        public void GivenOnSubscribeInterceptor_WhenSubscribing_ShouldInvokeInterceptor()
        {
            var intercepted = new List<EventHandler>();
            _sut.SomethingHappened.OnSubscribe(h => intercepted.Add(h));

            EventHandler h1 = (s, e) => { };
            EventHandler h2 = (s, e) => { };
            _sut.Instance().SomethingHappened += h1;
            _sut.Instance().SomethingHappened += h2;

            intercepted.ShouldBe(new[] { h1, h2 });
            _sut.SomethingHappened.Subscribed(Arg<EventHandler>.Is(h1), Count.Once());
            _sut.SomethingHappened.Subscribed(Arg<EventHandler>.Is(h2), Count.Once());
        }

        [Fact]
        public void GivenOnUnsubscribeInterceptor_WhenUnsubscribing_ShouldInvokeInterceptor()
        {
            var intercepted = new List<EventHandler>();
            _sut.SomethingHappened.OnUnsubscribe(h => intercepted.Add(h));

            EventHandler h1 = (s, e) => { };
            EventHandler h2 = (s, e) => { };
            _sut.Instance().SomethingHappened += h1;
            _sut.Instance().SomethingHappened += h2;
            _sut.Instance().SomethingHappened -= h1;
            _sut.Instance().SomethingHappened -= h2;

            intercepted.ShouldBe(new[] { h1, h2 });
            _sut.SomethingHappened.Unsubscribed(Arg<EventHandler>.Is(h1), Count.Once());
            _sut.SomethingHappened.Unsubscribed(Arg<EventHandler>.Is(h2), Count.Once());
        }
    }
}
