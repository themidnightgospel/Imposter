using System;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.Verification
{
    public class OrderedVerificationTests
    {
        private readonly IVerificationServiceImposter _sut = new();

        [Fact]
        public void GivenNoSteps_WhenVerifyingOrder_ShouldRejectEmptyExpectation()
        {
            Should.Throw<InvalidOperationException>(() => Imposters.InOrder().Verify());
        }

        [Fact]
        public void GivenNestedCallbackCall_WhenVerifyingOrder_ShouldPlaceCallerBeforeNestedCall()
        {
            var second = new IVerificationServiceImposter();
            _sut.Save(1).Callback(_ => second.Instance().Read(2));
            _sut.Instance().Save(1);
            Should.Throw<VerificationFailedException>(() =>
                Imposters.InOrder().Then(second.Read(2)).Then(_sut.Save(1)).Verify()
            );
            Should.Throw<VerificationFailedException>(() => ((IImposter)_sut).VerifyNoOtherCalls());
            Should.Throw<VerificationFailedException>(() =>
                ((IImposter)second).VerifyNoOtherCalls()
            );
            Imposters.InOrder().Then(_sut.Save(1)).Then(second.Read(2)).Verify();
            Imposters.VerifyNoOtherCalls(_sut, second);
        }

        [Fact]
        public void GivenInterleavedCallsAcrossImposters_WhenOrdered_ShouldSelectOnlyRequiredCalls()
        {
            var second = new IVerificationServiceImposter();
            _sut.Instance().Save(1);
            _sut.Instance().Save(99);
            second.Instance().Read(2);
            _sut.Instance().Save(3);
            Imposters.InOrder().Then(_sut.Save(1)).Then(second.Read(2)).Then(_sut.Save(3)).Verify();
            Should.Throw<VerificationFailedException>(() => ((IImposter)_sut).VerifyNoOtherCalls());
            ((IImposter)second).VerifyNoOtherCalls();
            _sut.Save(99).Called(Count.Once());
            ((IImposter)_sut).VerifyNoOtherCalls();
        }

        [Fact]
        public void GivenRepeatedMatchingCalls_WhenOrdered_ShouldCoverEarliestDistinctSelections()
        {
            _sut.Instance().Save(1);
            _sut.Instance().Save(1);
            _sut.Instance().Save(2);
            Imposters.InOrder().Then(_sut.Save(1)).Then(_sut.Save(2)).Verify();
            Should.Throw<VerificationFailedException>(() => ((IImposter)_sut).VerifyNoOtherCalls());
            Imposters.InOrder().Then(_sut.Save(1)).Then(_sut.Save(1)).Then(_sut.Save(2)).Verify();
            ((IImposter)_sut).VerifyNoOtherCalls();
        }

        [Fact]
        public void GivenMissingLaterStep_WhenOrderedCheckFails_ShouldCoverNothing()
        {
            _sut.Instance().Save(1);
            var failure = Should.Throw<VerificationFailedException>(() =>
                Imposters.InOrder().Then(_sut.Save(1)).Then(_sut.Save(2)).Verify()
            );
            failure.Expected.ShouldContain("-> 2.");
            failure.PerformedInvocations!.ShouldContain("<1>");
            Should.Throw<VerificationFailedException>(() => ((IImposter)_sut).VerifyNoOtherCalls());
        }

        [Fact]
        public void GivenSwappedCalls_WhenOrdered_ShouldFailWithoutChangingBehavior()
        {
            _sut.Instance().Save(2);
            _sut.Instance().Save(1);
            Should.Throw<VerificationFailedException>(() =>
                Imposters.InOrder().Then(_sut.Save(1)).Then(_sut.Save(2)).Verify()
            );
            _sut.Instance().Read(1).ShouldBe(0);
        }

        [Fact]
        public void GivenOneCall_WhenExpectedTwiceInOrder_ShouldFail()
        {
            _sut.Instance().Save(1);
            Should.Throw<VerificationFailedException>(() =>
                Imposters.InOrder().Then(_sut.Save(1)).Then(_sut.Save(1)).Verify()
            );
        }
    }
}
