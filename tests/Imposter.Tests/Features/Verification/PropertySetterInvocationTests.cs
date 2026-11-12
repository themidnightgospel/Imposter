using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.PropertyImpersonation;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.Verification
{
    public class PropertySetterInvocationTests
    {
        private readonly ILifecycleTargetImposter _sut = new ILifecycleTargetImposter();

        [Fact]
        public void GivenDirectBaseInit_WhenBaseThrows_ShouldRecordExactlyOneCompleteInvocation()
        {
            var sut = new VirtualInitOnlyPropertySutImposter();
            sut.Throwing.Setter(Arg<int>.Any()).UseBaseImplementation();
            var failure = Should.Throw<InvalidOperationException>(() =>
                InitOnlyAccessor.Invoke(sut.Instance(), "Throwing", 42)
            );

            var records = sut.Throwing.Setter(42).Invocations;
            records.Count.ShouldBe(1);
            records[0].Value.ShouldBe(42);
            records[0].Exception.ShouldBeSameAs(failure);
            sut.VerifyAllSetupsUsed();
            sut.Throwing.Setter(42).Called(Count.Once());
            sut.VerifyNoOtherCalls();
        }

        [Fact]
        public void GivenAssignments_WhenReadingTypedHistory_ShouldExposeValuesInEntryOrder()
        {
            _sut.Instance().Value = 1;
            _sut.Instance().Value = 2;

            var records = _sut.Value.Setter(Arg<int>.Any()).Invocations;

            records.Count.ShouldBe(2);
            records[0].Value.ShouldBe(1);
            records[1].Value.ShouldBe(2);
            records[0].Member.ShouldBe("Value");
            records[0].Sequence.ShouldBeLessThan(records[1].Sequence);
            records[0].Exception.ShouldBeNull();
            _sut.Value.Setter(2).Invocations.Count.ShouldBe(1);
            ((IImposter)_sut).ClearInvocations();
            records.Count.ShouldBe(2);
            records[0].Value.ShouldBe(1);
        }

        [Fact]
        public void GivenThrowingCallback_WhenAssigned_ShouldRecordExceptionAndUseSetup()
        {
            var failure = new InvalidOperationException("setter callback failed");
            _sut.Value.Setter(3).Callback(_ => throw failure);

            Should
                .Throw<InvalidOperationException>(() => _sut.Instance().Value = 3)
                .ShouldBeSameAs(failure);

            var invocation = _sut.Value.Setter(3).Invocations[0];
            invocation.Exception.ShouldBeSameAs(failure);
            invocation.Value.ShouldBe(3);
            _sut.VerifyAllSetupsUsed();
            Should.Throw<VerificationFailedException>(() => _sut.VerifyNoOtherCalls());
            _sut.Value.Setter(3).Called(Count.Once());
            _sut.VerifyNoOtherCalls();
        }

        [Fact]
        public void GivenMultipleCallbackSetups_WhenAssigned_ShouldMarkEveryExecutedChainUsed()
        {
            var sum = 0;
            _sut.Value.Setter(Arg<int>.Any()).Callback(value => sum += value);
            _sut.Value.Setter(4).Callback(value => sum += value * 2).Then().Callback(_ => sum++);
            _sut.Value.Setter(99).Callback(_ => throw new InvalidOperationException()).Lenient();

            _sut.Instance().Value = 4;

            sum.ShouldBe(13);
            _sut.VerifyAllSetupsUsed();
            ((IImposter)_sut).ClearInvocations();
            _sut.VerifyAllSetupsUsed();
        }

        [Fact]
        public void GivenSetterCalls_WhenVerified_ShouldCoverOnlyMatchingAssignments()
        {
            _sut.Instance().Value = 1;
            _sut.Instance().Value = 1;
            _sut.Instance().Value = 2;

            _sut.Value.Setter(2).Called(Count.Once());

            Should.Throw<VerificationFailedException>(() => _sut.VerifyNoOtherCalls());
            _sut.Value.Setter(1).Called(Count.Exactly(2));
            _sut.VerifyNoOtherCalls();
        }

        [Fact]
        public void GivenExplicitModeWithoutSetup_WhenAssigned_ShouldRecordMissingSetupFailure()
        {
            var sut = new ILifecycleTargetImposter(ImposterMode.Explicit);
            var failure = Should.Throw<MissingImposterException>(() => sut.Instance().Value = 5);

            sut.Value.Setter(5).Invocations[0].Exception.ShouldBeSameAs(failure);
            Should.Throw<VerificationFailedException>(() => sut.VerifyNoInteractions());
        }

        [Fact]
        public void GivenFailedVerification_WhenCheckingCoverage_ShouldLeaveCallsUncovered()
        {
            _sut.Instance().Value = 7;
            var failure = Should.Throw<VerificationFailedException>(() =>
                _sut.Value.Setter(Arg<int>.Any()).Called(Count.Twice())
            );
            failure.PerformedInvocations.ShouldNotBeNull();
            failure.PerformedInvocations.ShouldContain("<7>");
            Should.Throw<VerificationFailedException>(() => _sut.VerifyNoOtherCalls());
        }
    }
}
