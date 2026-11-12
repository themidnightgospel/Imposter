using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.Verification;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Docs.FailureMessages
{
    public class FailureMessagesTests
    {
        [Fact]
        public void GivenSpecificationSetups_WhenExplicitCallHasNoMatch_ShouldMatchCompleteSpecifiedMessage()
        {
            var sut = new IGreeterImposter(ImposterMode.Explicit);
            sut.Hello("Ann", Arg<int>.Any()).Throws(new InvalidOperationException());
            sut.Hello(
                    Arg<string>.Any(),
                    Arg<int>.Is(value => value > 10).Describe("greater than 10")
                )
                .Returns("first")
                .Then()
                .Throws<InvalidOperationException>();
            var failure = Should.Throw<MissingImposterException>(() =>
                sut.Instance().Hello("Bob", 2)
            );
            failure.Message.ShouldBe(
                string.Join(
                    Environment.NewLine,
                    new[]
                    {
                        "No setup matched Hello(name: <Bob>, times: <2>).",
                        "Configured setups for Hello:",
                        "  Hello(name: any, times: greater than 10) -> Returns, Throws<InvalidOperationException>",
                        "  Hello(name: <Ann>, times: any) -> Throws",
                    }
                )
            );
        }

        [Fact]
        public void GivenSpecificationOtherInteraction_WhenVerificationFails_ShouldMatchCompleteSpecifiedMessage()
        {
            var sut = new IGreeterImposter();
            sut.Instance().Goodbye("Ann");
            var failure = Should.Throw<VerificationFailedException>(() =>
                sut.Hello(
                        Arg<string>.Any(),
                        Arg<int>.Is(value => value > 10).Describe("greater than 10")
                    )
                    .Called(Count.Once())
            );
            failure.Message.ShouldBe(
                string.Join(
                    Environment.NewLine,
                    new[]
                    {
                        "Expected Hello(name: any, times: greater than 10) exactly 1 time(s), performed 0 times.",
                        "Performed invocations of Hello: none.",
                        "Other interactions on IGreeterImposter:",
                        "  Goodbye(name: <Ann>)",
                    }
                )
            );
        }

        [Fact]
        public void GivenDocumentedMatcher_WhenVerificationFails_ShouldIncludeDescriptionAndReason()
        {
#if USE_CSHARP14
            var imposter = IDiagnosticMessages.Imposter();
#else
            var imposter = new IDiagnosticMessagesImposter();
#endif
            var failure = Should.Throw<VerificationFailedException>(() =>
                imposter
                    .Hello(Arg<string>.Any(), Arg<int>.Is(x => x > 10).Describe("greater than 10"))
                    .Called(Count.Once(), "a greeting should be repeated")
            );
            failure.Expected.ShouldContain("greater than 10");
            failure.Message.ShouldContain("a greeting should be repeated");
        }
    }
}
