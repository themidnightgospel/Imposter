using System;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.EventImpersonation
{
    public class VerificationTests
    {
        private readonly IEventSetupSutImposter _sut =
#if USE_CSHARP14
        IEventSetupSut.Imposter();
#else
        new IEventSetupSutImposter();
#endif

        [Fact]
        public void GivenSubscriptions_WhenVerifyingSubscribedCounts_ShouldMatch()
        {
            EventHandler h = (s, e) => { };
            _sut.Instance().SomethingHappened += h;
            _sut.Instance().SomethingHappened += h;

            _sut.SomethingHappened.Subscribed(Arg<EventHandler>.Is(h), Count.Exactly(2));
            _sut.SomethingHappened.Subscribed(Arg<EventHandler>.Is(h), Count.AtLeast(1));
            _sut.SomethingHappened.Subscribed(Arg<EventHandler>.Is(h), Count.AtMost(2));
        }

        [Fact]
        public void GivenNoUnsubscription_WhenVerifyingUnsubscribedNever_ShouldMatch()
        {
            EventHandler h = (s, e) => { };
            _sut.Instance().SomethingHappened += h;

            _sut.SomethingHappened.Unsubscribed(Arg<EventHandler>.Is(h), Count.Never());
        }

        [Fact]
        public void GivenRaisedMultipleTimes_WhenVerifyingRaisedAtLeast_ShouldMatch()
        {
            _sut.SomethingHappened.Raise(this, EventArgs.Empty)
                .Raise(this, EventArgs.Empty)
                .Raise(this, EventArgs.Empty);

            _sut.SomethingHappened.Raised(
                Arg<object>.Any(),
                Arg<EventArgs>.Any(),
                Count.AtLeast(3)
            );
        }

        [Fact]
        public void GivenMismatchedVerification_WhenVerificationIsPerformed_ShouldThrowVerificationFailedException()
        {
            _sut.SomethingHappened.Raise(this, EventArgs.Empty);

            Should.Throw<VerificationFailedException>(() =>
                _sut.SomethingHappened.Raised(
                    Arg<object>.Any(),
                    Arg<EventArgs>.Any(),
                    Count.Exactly(2)
                )
            );
        }

        [Fact]
        public void GivenHandlerInvocations_WhenVerifyingHandlerInvoked_ShouldMatch()
        {
            int count = 0;
            EventHandler h = (s, e) => count++;
            _sut.Instance().SomethingHappened += h;

            _sut.SomethingHappened.Raise(this, EventArgs.Empty).Raise(this, EventArgs.Empty);

            count.ShouldBe(2);
            _sut.SomethingHappened.HandlerInvoked(Arg<EventHandler>.Is(h), Count.Exactly(2));
        }
    }
}
