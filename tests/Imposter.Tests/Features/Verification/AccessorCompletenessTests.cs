using System;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

[assembly: GenerateImposter(
    typeof(Imposter.Tests.Features.Verification.IAccessorVerificationTarget)
)]

namespace Imposter.Tests.Features.Verification
{
    public interface IAccessorVerificationTarget
    {
        int Value { get; set; }
        int this[int key] { get; set; }
        event Action<int> Changed;
    }

    public class AccessorCompletenessTests
    {
        private readonly IAccessorVerificationTargetImposter _sut = new();

        [Fact]
        public void GivenThrowingIndexerSetterChain_WhenFirstCallbackRuns_ShouldMarkWholeChainUsed()
        {
            _sut[1]
                .Setter()
                .Callback((_, _) => throw new InvalidOperationException())
                .Then()
                .Callback((_, _) => throw new NotSupportedException());

            Should.Throw<InvalidOperationException>(() => _sut.Instance()[1] = 2);
            _sut.VerifyAllSetupsUsed();

            // Equal matcher values on a separately created handle form an independent chain.
            _sut[1].Setter().Callback((_, _) => { });
            Should.Throw<VerificationFailedException>(() => _sut.VerifyAllSetupsUsed());
        }

        [Fact]
        public void GivenSegmentedEventHistory_WhenMaterializingMultipleSnapshots_ShouldShareCoverage()
        {
            Action<int> handler = _ => { };
            for (var i = 0; i < 70; i++)
                _sut.Instance().Changed -= handler;
            var snapshot = _sut.Changed.Unsubscriptions;
            snapshot.Count.ShouldBe(70);
            for (var i = 0; i < 70; i++)
                _sut.Instance().Changed -= handler;

            snapshot.Count.ShouldBe(70);
            _sut.Changed.Unsubscribed(handler, Count.Exactly(140));
            foreach (var invocation in snapshot)
                ((IVerifiableInvocation)invocation).IsVerified.ShouldBeTrue();
            foreach (var invocation in _sut.Changed.Unsubscriptions)
                ((IVerifiableInvocation)invocation).IsVerified.ShouldBeTrue();
            _sut.VerifyNoOtherCalls();
        }

        [Fact]
        public void GivenRetainedEventSnapshot_WhenCleared_ShouldKeepCoverageSeparateFromNewHistory()
        {
            Action<int> handler = _ => { };
            _sut.Instance().Changed += handler;
            var old = _sut.Changed.Subscriptions[0];
            _sut.ClearInvocations();
            _sut.Instance().Changed += handler;

            ((IVerifiableInvocation)old).MarkVerified();
            ((IVerifiableInvocation)_sut.Changed.Subscriptions[0]).IsVerified.ShouldBeFalse();
            old.Handler.ShouldBeSameAs(handler);
            Should.Throw<VerificationFailedException>(() => _sut.VerifyNoOtherCalls());
            _sut.Changed.Subscribed(handler, Count.Once());
            _sut.VerifyNoOtherCalls();
        }

        [Fact]
        public void GivenNoSetupsOrCalls_WhenCheckingCompleteness_ShouldPass()
        {
            _sut.VerifyAllSetupsUsed();
            _sut.VerifyNoInteractions();
            _sut.VerifyNoOtherCalls();
        }

        [Fact]
        public void GivenEventCountFailure_WhenFormatting_ShouldNotEvaluateMatchersAgain()
        {
            Action<int> handler = _ => { };
            _sut.Instance().Changed += handler;
            var evaluations = 0;
            var criteria = Arg<Action<int>>
                .Is(_ =>
                {
                    evaluations++;
                    return true;
                })
                .Describe("registered handler");
            var failure = Should.Throw<VerificationFailedException>(() =>
                _sut.Changed.Subscribed(criteria, Count.Twice(), "both listeners must subscribe")
            );
            evaluations.ShouldBe(1);
            failure.PerformedInvocations.ShouldNotBeNull();
            failure.PerformedInvocations.ShouldContain("Changed subscribed");
            failure.Expected.ShouldContain("Changed subscribed");
            failure.Expected.ShouldContain("registered handler");
            failure.Message.ShouldContain("both listeners must subscribe");
            Should.Throw<VerificationFailedException>(() => _sut.VerifyNoOtherCalls());
        }

        [Fact]
        public void GivenPropertyGet_WhenVerified_ShouldCoverCounterAndRetainUsageAfterClear()
        {
            _sut.Value.Getter().Returns(10).Then().Returns(20);
            _sut.Instance().Value.ShouldBe(10);
            _sut.Invocations().Count.ShouldBe(0);
            Should.Throw<VerificationFailedException>(() => _sut.VerifyNoInteractions());
            Should.Throw<VerificationFailedException>(() => _sut.VerifyNoOtherCalls());
            _sut.Value.Getter().Called(Count.Once());
            _sut.VerifyNoOtherCalls();
            _sut.VerifyAllSetupsUsed();
            _sut.ClearInvocations();
            _sut.VerifyAllSetupsUsed();
            _sut.VerifyNoInteractions();
            _sut.Instance().Value.ShouldBe(20);
            Should.Throw<VerificationFailedException>(() => _sut.VerifyNoOtherCalls());
        }

        [Fact]
        public void GivenIndexerInteractions_WhenOrdered_ShouldCoverOnlySelectedRecords()
        {
            _sut.Instance()[1] = 5;
            _sut.Instance()[1].ShouldBe(5);
            _sut.Instance()[2] = 6;
            _sut[1].Setter().Called(Count.Once());
            _sut[1].Getter().Called(Count.Once());
            Should.Throw<VerificationFailedException>(() => _sut.VerifyNoOtherCalls());
            Imposters.InOrder().Then(_sut[1].Setter()).Then(_sut[1].Getter()).Verify();
            Should.Throw<VerificationFailedException>(() => _sut.VerifyNoOtherCalls());
            _sut[2].Setter().Called(Count.Once());
            _sut.VerifyNoOtherCalls();
        }

        [Fact]
        public void GivenIndexerCountFailure_WhenCheckingCompleteness_ShouldNotCoverAnyCalls()
        {
            _sut.Instance()[1] = 3;
            _ = _sut.Instance()[2];
            Should.Throw<VerificationFailedException>(() =>
                _sut[Arg<int>.Any()].Setter().Called(Count.Twice())
            );
            Should.Throw<VerificationFailedException>(() =>
                _sut[Arg<int>.Any()].Getter().Called(Count.Twice())
            );
            Should.Throw<VerificationFailedException>(() => _sut.VerifyNoOtherCalls());
            _sut[1].Setter().Called(Count.Once());
            _sut[2].Getter().Called(Count.Once());
            _sut.VerifyNoOtherCalls();
        }

        [Fact]
        public void GivenIndexerSetups_WhenExecutedOrLenient_ShouldVerifyUsage()
        {
            _sut[1].Getter().Returns(10).Then().Returns(20);
            _sut[2].Getter().Returns(30).Lenient();
            _sut[3].Setter().Callback((_, _) => { });
            _sut[4].Setter().Callback((_, _) => { }).Lenient();
            _sut.Instance()[1].ShouldBe(10);
            _sut.Instance()[3] = 4;
            _sut.VerifyAllSetupsUsed();
            _sut[5].Setter().Callback((_, _) => { });
            Should
                .Throw<VerificationFailedException>(() => _sut.VerifyAllSetupsUsed())
                .Message.ShouldContain("<5>");
        }

        [Fact]
        public void GivenEventActions_WhenCovered_ShouldIgnoreRaisesAsInteractions()
        {
            Action<int> handler = _ => { };
            _sut.Instance().Changed += handler;
            _sut.Changed.Raise(1);
            _sut.Instance().Changed -= handler;
            _sut.Invocations().Count.ShouldBe(2);
            _sut.Changed.Subscribed(handler, Count.Once());
            Should.Throw<VerificationFailedException>(() => _sut.VerifyNoOtherCalls());
            _sut.Changed.Unsubscribed(handler, Count.Once());
            _sut.VerifyNoOtherCalls();
        }

        [Fact]
        public void GivenEventSetupRegistrations_WhenAddedAfterExecution_ShouldRemainUnused()
        {
            Action<int> handler = _ => { };
            _sut.Changed.OnSubscribe(_ => { }).OnUnsubscribe(_ => { }).Callback(_ => { });
            _sut.Instance().Changed += handler;
            _sut.Instance().Changed -= handler;
            _sut.Changed.Raise(1);
            _sut.VerifyAllSetupsUsed();
            _sut.Changed.Callback(_ => { });
            Should
                .Throw<VerificationFailedException>(() => _sut.VerifyAllSetupsUsed())
                .Message.ShouldContain("Callback");
            _sut.Changed.Lenient();
            _sut.VerifyAllSetupsUsed();
        }

        [Fact]
        public void GivenThrowingEventInterceptor_WhenLaterRegistrationIsNotExecuted_ShouldRemainUnused()
        {
            _sut.Changed.OnSubscribe(_ => throw new InvalidOperationException())
                .OnSubscribe(_ => { });
            Should.Throw<InvalidOperationException>(() => _sut.Instance().Changed += _ => { });
            Should
                .Throw<VerificationFailedException>(() => _sut.VerifyAllSetupsUsed())
                .Message.ShouldContain("OnSubscribe");
            _sut.Changed.Subscribed(Arg<Action<int>>.Any(), Count.Once());
            _sut.VerifyNoOtherCalls();
        }
    }
}
