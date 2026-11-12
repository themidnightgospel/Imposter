using System;
using System.Threading;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

[assembly: GenerateImposter(typeof(Imposter.Tests.Features.Verification.IVerificationService))]

namespace Imposter.Tests.Features.Verification
{
    public interface IVerificationService
    {
        void Save(int value);
        int Read(int value);
        T Echo<T>(T value);
        void VerifyNoOtherCalls();
    }

    public class VerificationCompletenessTests
    {
        private readonly IVerificationServiceImposter _sut = new();

        [Fact]
        public void GivenRecordedCalls_WhenOnlyOneIsCovered_ShouldReportTheOther()
        {
            _sut.Instance().Save(1);
            _sut.Instance().Save(2);
            _sut.Save(1).Called(Count.Once());
            var failure = Should.Throw<VerificationFailedException>(() =>
                ((IImposter)_sut).VerifyNoOtherCalls()
            );
            failure.Message.ShouldContain("<2>");
            _sut.Save(2).Called(Count.Once());
            ((IImposter)_sut).VerifyNoOtherCalls();
            Should.Throw<VerificationFailedException>(() => _sut.VerifyNoInteractions());
        }

        [Fact]
        public void GivenRecordedCall_WhenCountingReadingOrFailingVerification_ShouldNotCoverIt()
        {
            _sut.Instance().Save(1);
            var builder = _sut.Save(1);
            builder.CallCount().ShouldBe(1);
            builder.Invocations.Count.ShouldBe(1);
            Should.Throw<VerificationFailedException>(() => builder.Called(Count.Twice()));
            _sut.Save(2).Called(Count.Never());
            Should.Throw<VerificationFailedException>(() => ((IImposter)_sut).VerifyNoOtherCalls());
        }

        [Fact]
        public void GivenCallAfterVerification_WhenCheckingCoverage_ShouldRemainUncovered()
        {
            _sut.Instance().Save(1);
            _sut.Save(Arg<int>.Any()).Called(Count.Once());
            _sut.Instance().Save(2);
            Should.Throw<VerificationFailedException>(() => ((IImposter)_sut).VerifyNoOtherCalls());
        }

        [Fact]
        public async Task GivenConcurrentCall_WhenVerificationCapturedHistory_ShouldNotCoverNewCall()
        {
            _sut.Instance().Save(1);
            using var entered = new ManualResetEventSlim();
            using var resume = new ManualResetEventSlim();
            var builder = _sut.Save(
                Arg<int>.Is(value =>
                {
                    entered.Set();
                    resume.Wait(TimeSpan.FromSeconds(10)).ShouldBeTrue();
                    return value > 0;
                })
            );
            var verification = Task.Run(() => builder.Called(Count.Once()));
            entered.Wait(TimeSpan.FromSeconds(10)).ShouldBeTrue();
            _sut.Instance().Save(2);
            resume.Set();
            await verification;
            Should.Throw<VerificationFailedException>(() => ((IImposter)_sut).VerifyNoOtherCalls());
        }

        [Fact]
        public void GivenShadowedAndLenientSetups_WhenVerifyingUsage_ShouldIgnoreOnlyLenient()
        {
            _sut.Read(1).Returns(1);
            _sut.Read(1).Returns(2);
            _sut.Read(2).Returns(3).Lenient();
            _sut.Read(3); // A verification handle is not a configured setup.
            _sut.Instance().Read(1).ShouldBe(2);
            var failure = Should.Throw<VerificationFailedException>(() =>
                _sut.VerifyAllSetupsUsed()
            );
            failure.Message.ShouldContain("Read");
            failure.Message.ShouldNotContain("<2>");
        }

        [Fact]
        public void GivenSequentialThrowingAndGenericSetups_WhenUsed_ShouldCountTheWholeChain()
        {
            _sut.Read(1).Returns(10).Then().Returns(20);
            _sut.Save(1).Throws<InvalidOperationException>();
            _sut.Echo<int>(1).Returns(2);
            _sut.Instance().Read(1).ShouldBe(10);
            Should.Throw<InvalidOperationException>(() => _sut.Instance().Save(1));
            _sut.Instance().Echo(1).ShouldBe(2);
            _sut.VerifyAllSetupsUsed();
            _sut.Echo<int>(1).Called(Count.Once());
            _sut.Save(1).Called(Count.Once());
            _sut.Read(1).Called(Count.Once());
            ((IImposter)_sut).VerifyNoOtherCalls();
        }

        [Fact]
        public void GivenSeveralImposters_WhenVerifyingTogether_ShouldReportEveryFailure()
        {
            var second = new IVerificationServiceImposter();
            _sut.Instance().Save(1);
            second.Instance().Save(2);
            var failure = Should.Throw<VerificationFailedException>(() =>
                Imposters.VerifyNoInteractions(_sut, second)
            );
            failure.Message.ShouldContain("#1");
            failure.Message.ShouldContain("#2");
        }

        [Theory]
        [InlineData("NoOtherCalls", "No uncovered interactions", "Uncovered interactions:", false)]
        [InlineData("NoOtherCalls", "No uncovered interactions", "Uncovered interactions:", true)]
        [InlineData("NoInteractions", "No interactions", "Interactions:", false)]
        [InlineData("NoInteractions", "No interactions", "Interactions:", true)]
        [InlineData("AllSetupsUsed", "All configured setups used", "Unused setups:", false)]
        [InlineData("AllSetupsUsed", "All configured setups used", "Unused setups:", true)]
        public void GivenPassingAndFailingImposters_WhenVerifyingTogether_ShouldReportOnlyFailure(
            string verification,
            string expected,
            string heading,
            bool multipleFindings
        )
        {
            _sut.Save(1).Callback(_ => { });
            if (verification != "NoInteractions")
            {
                _sut.Instance().Save(1);
                _sut.Save(1).Called(Count.Once());
            }
            var second = new IVerificationServiceImposter();
            second.Save(2).Callback(_ => { });
            if (multipleFindings)
                second.Save(3).Callback(_ => { });
            if (verification != "AllSetupsUsed")
            {
                second.Instance().Save(2);
                if (multipleFindings)
                    second.Instance().Save(3);
            }

            Action verify = verification switch
            {
                "NoOtherCalls" => () => Imposters.VerifyNoOtherCalls(_sut, second),
                "NoInteractions" => () => Imposters.VerifyNoInteractions(_sut, second),
                _ => () => Imposters.VerifyAllSetupsUsed(_sut, second),
            };
            var failure = Should.Throw<VerificationFailedException>(verify);

            failure.PerformedInvocations.ShouldNotBeNull();
            failure.PerformedInvocations.ShouldStartWith(
                typeof(IVerificationServiceImposter).Name + " #2: "
            );
            failure.PerformedInvocations.ShouldContain("Save");
            failure.PerformedInvocations.ShouldContain("<2>");
            var findings = failure.PerformedInvocations.Split(
                new[] { Environment.NewLine },
                StringSplitOptions.None
            );
            findings.Length.ShouldBe(multipleFindings ? 2 : 1);
            foreach (var finding in findings)
                finding.ShouldStartWith(typeof(IVerificationServiceImposter).Name + " #2: ");
            if (multipleFindings)
                failure.PerformedInvocations.ShouldContain("<3>");
            failure.PerformedInvocations.ShouldNotContain("#1");
            failure.PerformedInvocations.ShouldNotContain("<1>");
            failure.Message.ShouldBe(
                $"Expected {expected}{Environment.NewLine}{heading}{Environment.NewLine}{failure.PerformedInvocations}"
            );
            failure.PerformedInvocations.ShouldNotContain("Expected ");
            failure.PerformedInvocations.ShouldNotContain(heading);
        }

        [Fact]
        public void GivenMissingExplicitMethodSetup_WhenVerifying_ShouldCountAndCoverRecordedFailure()
        {
            var imposter = new IVerificationServiceImposter(ImposterMode.Explicit);
            var missing = Should.Throw<MissingImposterException>(() =>
                imposter.Instance().Save(47)
            );
            var calls = imposter.Save(47).Invocations;
            calls.Count.ShouldBe(1);
            calls[0].Exception.ShouldBeSameAs(missing);
            Should.Throw<VerificationFailedException>(() => imposter.VerifyNoInteractions());

            imposter.Save(47).Called(Count.Once());
            ((IImposter)imposter).VerifyNoOtherCalls();
        }

        [Fact]
        public void GivenLenientSetup_WhenInvoked_ShouldStillRecordAndCountItsInteractions()
        {
            _sut.Read(7).Returns(70).Then().Returns(71).Lenient();
            _sut.VerifyAllSetupsUsed();
            _sut.Instance().Read(7).ShouldBe(70);
            var calls = _sut.Read(7).Invocations;
            calls.Count.ShouldBe(1);
            calls[0].Value.ShouldBe(7);
            calls[0].Result.ShouldBe(70);
            Should.Throw<VerificationFailedException>(() => _sut.VerifyNoInteractions());

            _sut.Read(7).Called(Count.Once());
            ((IImposter)_sut).VerifyNoOtherCalls();
        }

        [Fact]
        public void GivenMemberNamedVerifyNoOtherCalls_WhenUsingImposterVerification_ShouldRemainReachable()
        {
            _sut.Instance().VerifyNoOtherCalls();
            _sut.VerifyNoOtherCalls().Called(Count.Once());
            ((IImposter)_sut).VerifyNoOtherCalls();
        }
    }
}
