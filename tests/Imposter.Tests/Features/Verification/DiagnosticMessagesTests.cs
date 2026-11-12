using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.Verification;
using Shouldly;
using Xunit;

[assembly: GenerateImposter(typeof(IDiagnosticMessages))]
[assembly: GenerateImposter(typeof(Imposter.Tests.Features.Verification.IGreeter))]

namespace Imposter.Tests.Features.Verification
{
    public interface IGreeter
    {
        string Hello(string name, int times);
        void Goodbye(string name);
    }

    public interface IDiagnosticMessages
    {
        string Hello(string name, int times);
        void Inspect(object value);
        void Goodbye(string name);
        T Echo<T>(T value);
        int this[string key] { get; set; }
    }

    public class DiagnosticMessagesTests
    {
        private readonly IDiagnosticMessagesImposter _sut = new IDiagnosticMessagesImposter();

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
        public void GivenGenericSetups_WhenNoArgumentOrTypeMatches_ShouldListRetainedSetups()
        {
            var sut = new IDiagnosticMessagesImposter(ImposterMode.Explicit);
            sut.Echo<string>("configured").Returns("result");
            sut.Echo<int>(1).Returns(2);
            sut.Instance().Echo("configured").ShouldBe("result");

            var argumentFailure = Should.Throw<MissingImposterException>(() =>
                sut.Instance().Echo("missing")
            );
            argumentFailure.ConfiguredSetups.ShouldContain("Echo(value: <configured>) -> Returns");
            argumentFailure.ConfiguredSetups.ShouldContain("Echo(value: <1>) -> Returns");
            var typeFailure = Should.Throw<MissingImposterException>(() =>
                sut.Instance().Echo(true)
            );
            typeFailure.ConfiguredSetups.ShouldContain("Echo(value: <configured>) -> Returns");
            typeFailure.ConfiguredSetups.ShouldContain("Echo(value: <1>) -> Returns");
        }

        [Fact]
        public void GivenUnconfiguredIndexerSetter_WhenExplicitCallFails_ShouldDescribeActualArguments()
        {
            var sut = new IDiagnosticMessagesImposter(ImposterMode.Explicit);
            var exception = Should.Throw<MissingImposterException>(() =>
                sut.Instance()["missing"] = 42
            );
            exception.Message.ShouldContain("[<missing>] = <42>");
            exception.ConfiguredSetups.ShouldBeEmpty();
        }

        [Fact]
        public void GivenIndexerSetup_WhenAnotherKeyIsRead_ShouldDescribeConfiguredMatcherAndOutcome()
        {
            var sut = new IDiagnosticMessagesImposter(ImposterMode.Explicit);
            sut[Arg<string>.Is("known").Describe("known key")].Getter().Returns(42);
            var exception = Should.Throw<MissingImposterException>(() => sut.Instance()["missing"]);
            exception.Message.ShouldContain("known key");
            exception.Message.ShouldContain("Returns");
            exception.Message.ShouldContain("[<missing>]");
        }

        [Fact]
        public void GivenOtherCalls_WhenIndexerVerificationFails_ShouldDescribeReasonAndOtherInteractions()
        {
            _sut.Instance().Goodbye("Ann");
            var exception = Should.Throw<VerificationFailedException>(() =>
                _sut["missing"].Getter().Called(Count.Once(), "the value should be read")
            );
            exception.Expected.ShouldContain("key: <missing>");
            exception.Message.ShouldContain("Because: the value should be read");
            exception.Message.ShouldContain("Goodbye(name: <Ann>)");
        }

        [Fact]
        public void GivenOtherRecordedCalls_WhenMemberHasNoCalls_ShouldListOtherInteractionsInOrder()
        {
            _sut.Instance().Goodbye("Ann");
            _sut.Instance().Inspect("later");
            var exception = Should.Throw<VerificationFailedException>(() =>
                _sut.Hello(Arg<string>.Any(), Arg<int>.Any()).Called(Count.Once())
            );
            exception.Message.ShouldContain("Performed invocations of Hello: none.");
            exception.Message.ShouldContain("Other interactions on IDiagnosticMessagesImposter:");
            exception
                .Message.IndexOf("Goodbye(name: <Ann>)", StringComparison.Ordinal)
                .ShouldBeLessThan(
                    exception.Message.IndexOf("Inspect(value: <later>)", StringComparison.Ordinal)
                );
        }

        [Fact]
        public void GivenMemberCalledWithDifferentArguments_WhenVerificationFails_ShouldStayScopedToMember()
        {
            _sut.Instance().Goodbye("Ann");
            _sut.Instance().Hello("Ann", 1);
            var exception = Should.Throw<VerificationFailedException>(() =>
                _sut.Hello("Bob", 2).Called(Count.Once())
            );
            exception.Message.ShouldNotContain("Other interactions");
            exception.Message.ShouldNotContain("Goodbye");
        }

        [Fact]
        public void GivenDescribedMatcher_WhenVerificationFails_ShouldExplainExpectationAndReason()
        {
            _sut.Instance().Hello("Ann", 2);
            var exception = Should.Throw<VerificationFailedException>(() =>
                _sut.Hello(Arg<string>.Any(), Arg<int>.Is(x => x > 10).Describe("greater than 10"))
                    .Called(Count.Once(), "a greeting should be repeated")
            );
            exception.Expected.ShouldBe("Hello(name: any, times: greater than 10)");
            exception.Message.ShouldStartWith(
                "Expected Hello(name: any, times: greater than 10) exactly 1 time(s), performed 0 times."
            );
            exception.Message.ShouldContain("Because: a greeting should be repeated");
            exception.PerformedInvocations!.ShouldContain("name: <Ann>, times: <2>");
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void GivenConsumedOutcomeSequence_WhenNoSetupMatches_ShouldDescribeOriginalSequence(
            bool consumeAll
        )
        {
            var sut = new IDiagnosticMessagesImposter(ImposterMode.Explicit);
            sut.Hello("Ann", Arg<int>.Any())
                .Returns("first")
                .Then()
                .Throws<InvalidOperationException>();
            sut.Instance().Hello("Ann", 1).ShouldBe("first");
            if (consumeAll)
                Should.Throw<InvalidOperationException>(() => sut.Instance().Hello("Ann", 1));
            var exception = Should.Throw<MissingImposterException>(() =>
                sut.Instance().Hello("Bob", 2)
            );
            exception.Message.ShouldContain("No setup matched Hello(name: <Bob>, times: <2>).");
            exception.ConfiguredSetups.ShouldContain(
                "Hello(name: <Ann>, times: any) -> Returns, Throws<InvalidOperationException>"
            );
        }

        [Fact]
        public void GivenBareVerificationBuilder_WhenMissingSetup_ShouldNotListItAsConfigured()
        {
            var sut = new IDiagnosticMessagesImposter(ImposterMode.Explicit);
            sut.Hello("Ann", 1);
            var exception = Should.Throw<MissingImposterException>(() =>
                sut.Instance().Hello("Bob", 2)
            );
            exception.ConfiguredSetups.ShouldBeEmpty();
        }

        [Fact]
        public void GivenThrowingToString_WhenVerificationFails_ShouldPreserveVerificationException()
        {
            var value = new Unprintable();
            _sut.Instance().Inspect(value);
            var exception = Should.Throw<VerificationFailedException>(() =>
                _sut.Inspect(value).Called(Count.Twice())
            );
            exception.Message.ShouldContain("<unprintable Unprintable>");
        }

        [Fact]
        public void GivenThrowingToString_WhenExplicitModeRejectsCall_ShouldPreserveMissingException()
        {
            var sut = new IDiagnosticMessagesImposter(ImposterMode.Explicit);
            var exception = Should.Throw<MissingImposterException>(() =>
                sut.Instance().Inspect(new Unprintable())
            );
            exception.Message.ShouldContain("<unprintable Unprintable>");
        }

        [Fact]
        public void GivenConfiguredFactories_WhenDiagnosticsAreFormatted_ShouldNotExecuteThem()
        {
            var sut = new IDiagnosticMessagesImposter(ImposterMode.Explicit);
            var factoryCalls = 0;
            var callbackCalls = 0;
            sut.Hello("Ann", 1)
                .Returns(
                    (_, __) =>
                    {
                        factoryCalls++;
                        return "hello";
                    }
                )
                .Callback((name, times) => callbackCalls++);
            var exception = Should.Throw<MissingImposterException>(() =>
                sut.Instance().Hello("Bob", 2)
            );
            exception.ConfiguredSetups.ShouldContain(
                "Hello(name: <Ann>, times: <1>) -> Returns, Callback"
            );
            factoryCalls.ShouldBe(0);
            callbackCalls.ShouldBe(0);
        }

        private sealed class Unprintable
        {
            public override string ToString() =>
                throw new InvalidOperationException("format failed");
        }
    }
}
