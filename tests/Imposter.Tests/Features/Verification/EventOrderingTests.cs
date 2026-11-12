using System;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

[assembly: GenerateImposter(typeof(Imposter.Tests.Features.Verification.IEventOrderTarget<>))]

namespace Imposter.Tests.Features.Verification
{
    public delegate void EventOrderHandler<T>(
        T _parent,
        T _criteria,
        T _subscription,
        T InvocationStep
    );

    public interface IEventOrderTarget<T>
    {
        event EventOrderHandler<T> @event;
    }

    public class EventOrderingTests
    {
        private readonly IAccessorVerificationTargetImposter _sut = new();

        [Fact]
        public void GivenHandlesCreatedBeforeCalls_WhenVerifyingAcrossImposters_ShouldCoverOnlyMatchingActions()
        {
            var service = new IVerificationServiceImposter();
            Action<int> handler = _ => { };
            Action<int> other = _ => { };
            var subscription = _sut.Changed.Subscribed(handler);
            var unsubscription = _sut.Changed.Unsubscribed(handler);
            var order = Imposters
                .InOrder()
                .Then(subscription)
                .Then(service.Save(7))
                .Then(unsubscription);

            _sut.Instance().Changed += handler;
            _sut.Instance().Changed += other;
            service.Instance().Save(7);
            _sut.Instance().Changed -= handler;
            order.Verify();

            ((IImposter)service).VerifyNoOtherCalls();
            Should.Throw<VerificationFailedException>(() => _sut.VerifyNoOtherCalls());
            _sut.Changed.Subscribed(other, Count.Once());
            _sut.VerifyNoOtherCalls();
        }

        [Fact]
        public void GivenRepeatedSubscriptionSteps_WhenOnlyOneCallExists_ShouldFailWithoutCoverage()
        {
            Action<int> handler = _ => { };
            var step = _sut.Changed.Subscribed(handler);
            var order = Imposters.InOrder().Then(step).Then(step);
            _sut.Instance().Changed += handler;

            Should.Throw<VerificationFailedException>(() => order.Verify());
            Should.Throw<VerificationFailedException>(() => _sut.VerifyNoOtherCalls());

            _sut.Instance().Changed += handler;
            order.Verify();
            _sut.VerifyNoOtherCalls();
        }

        [Fact]
        public void GivenMissingLaterUnsubscription_WhenOrderedCheckFails_ShouldLeaveSubscriptionUncovered()
        {
            Action<int> handler = _ => { };
            _sut.Instance().Changed += handler;
            var failure = Should.Throw<VerificationFailedException>(() =>
                Imposters
                    .InOrder()
                    .Then(_sut.Changed.Subscribed(handler))
                    .Then(_sut.Changed.Unsubscribed(handler))
                    .Verify()
            );

            failure.Message.ShouldContain("unsubscribed");
            Should.Throw<VerificationFailedException>(() => _sut.VerifyNoOtherCalls());
            _sut.Changed.Subscribed(handler, Count.Once());
            _sut.VerifyNoOtherCalls();
        }

        [Fact]
        public void GivenHandlerMatcher_WhenHandleIsCreated_ShouldEvaluateOnlyDuringVerification()
        {
            Action<int> handler = _ => { };
            Action<int> other = _ => { };
            var evaluations = 0;
            var matcher = Arg<Action<int>>.Is(candidate =>
            {
                evaluations++;
                return candidate == handler;
            });
            var step = _sut.Changed.Unsubscribed(matcher);
            evaluations.ShouldBe(0);
            _sut.Instance().Changed -= other;
            _sut.Instance().Changed -= handler;

            Imposters.InOrder().Then(step).Verify();

            evaluations.ShouldBe(2);
            Should.Throw<VerificationFailedException>(() => _sut.VerifyNoOtherCalls());
            _sut.Changed.Unsubscribed(other, Count.Once());
            _sut.VerifyNoOtherCalls();
        }

        [Fact]
        public void GivenExplicitModeAndThrowingInterceptors_WhenOrdered_ShouldIncludeFailedAddAndRemove()
        {
            var sut = new IAccessorVerificationTargetImposter(ImposterMode.Explicit);
            var addFailure = new InvalidOperationException("add failed");
            var removeFailure = new InvalidOperationException("remove failed");
            Action<int> handler = _ => { };
            sut.Changed.OnSubscribe(_ => throw addFailure).OnUnsubscribe(_ => throw removeFailure);

            Should
                .Throw<InvalidOperationException>(() => sut.Instance().Changed += handler)
                .ShouldBeSameAs(addFailure);
            Should
                .Throw<InvalidOperationException>(() => sut.Instance().Changed -= handler)
                .ShouldBeSameAs(removeFailure);

            Imposters
                .InOrder()
                .Then(sut.Changed.Subscribed(handler))
                .Then(sut.Changed.Unsubscribed(handler))
                .Verify();
            sut.Changed.Subscriptions[0].Exception.ShouldBeSameAs(addFailure);
            sut.Changed.Unsubscriptions[0].Exception.ShouldBeSameAs(removeFailure);
            sut.VerifyNoOtherCalls();
            sut.VerifyAllSetupsUsed();
        }

        [Fact]
        public void GivenEscapedGenericEventAndCollidingDelegateParameterNames_WhenOrdered_ShouldCompileAndMatch()
        {
            var sut = new IEventOrderTargetImposter<string>();
            EventOrderHandler<string> handler = (_, _, _, _) => { };
            var order = Imposters
                .InOrder()
                .Then(sut.@event.Subscribed(handler))
                .Then(sut.@event.Unsubscribed(handler));

            sut.Instance().@event += handler;
            sut.Instance().@event -= handler;

            order.Verify();
            sut.VerifyNoOtherCalls();
        }

        [Fact]
        public void GivenNullHandlerMatcher_WhenCreatingEventStep_ShouldRejectItImmediately()
        {
            Arg<Action<int>> matcher = null!;
            Should.Throw<ArgumentNullException>(() => _sut.Changed.Subscribed(matcher));
            Should.Throw<ArgumentNullException>(() => _sut.Changed.Unsubscribed(matcher));
        }
    }
}
