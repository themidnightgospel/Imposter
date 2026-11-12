using System;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Abstractions.Tests;

public class DiagnosticExceptionTests
{
    [Fact]
    public void GivenLegacyConstructorWithoutHistory_WhenCreated_ShouldKeepHistoryNullAndExactCountText()
    {
        var exception = new VerificationFailedException(Count.Once(), 0);
        exception.PerformedInvocations.ShouldBeNull();
        exception.Message.ShouldBe(
            "Invocation was expected to be performed exactly 1 time(s) but instead was performed 0 times."
        );
    }

    [Fact]
    public void GivenLegacyCountConstructor_WhenFormatting_ShouldPreserveExactOriginalMessage()
    {
        var exception = new VerificationFailedException(Count.Once(), 0, "Recorded()");
        exception.Message.ShouldBe(
            "Invocation was expected to be performed exactly 1 time(s) but instead was performed 0 times."
                + Environment.NewLine
                + "Performed invocations:"
                + Environment.NewLine
                + "Recorded()"
        );
        exception.Expected.ShouldBe(Count.Once().ToString());
    }

    [Fact]
    public void GivenPreviouslyPublishedVerificationSignatures_WhenInspectingMetadata_ShouldRetainExactOverloads()
    {
        typeof(CounterVerification).GetMethod("Verify", new[] { typeof(Count) }).ShouldNotBeNull();
        typeof(PassThroughSetter<InvocationRecord>)
            .GetMethod("Called", new[] { typeof(Count) })
            .ShouldNotBeNull();
    }

    [Fact]
    public void GivenSetupDescriptions_WhenCallerMutatesInput_ShouldKeepExceptionSnapshot()
    {
        var setups = new[] { "Hello(name: any) -> Returns" };
        var exception = new MissingImposterException("Hello", "Hello(name: <Ann>)", setups);
        setups[0] = "changed";
        exception.ConfiguredSetups[0].ShouldBe("Hello(name: any) -> Returns");
        exception.Message.ShouldContain("No setup matched Hello(name: <Ann>).");
        exception.MethodName.ShouldBe("Hello");
    }

    [Fact]
    public void GivenNoRecordedCalls_WhenVerificationFails_ShouldExplainExpectationAndReason()
    {
        var exception = new VerificationFailedException(
            "Hello",
            "Hello(name: any)",
            Count.Once(),
            0,
            string.Empty,
            "a greeting is required",
            "Other interactions on Greeter:\n  Goodbye()"
        );
        exception.Expected.ShouldBe("Hello(name: any)");
        exception.Message.ShouldStartWith(
            "Expected Hello(name: any) exactly 1 time(s), performed 0 times."
        );
        exception.Message.ShouldContain("Performed invocations of Hello: none.");
        exception.Message.ShouldContain("Other interactions on Greeter:");
        exception.Message.ShouldEndWith("Because: a greeting is required");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void GivenNullOrEmptyReason_WhenVerificationFails_ShouldOmitBecauseLine(string? because)
    {
        var exception = new VerificationFailedException(
            "Hello",
            "Hello(name: any)",
            Count.Once(),
            0,
            string.Empty,
            because
        );
        exception.Message.ShouldBe(
            "Expected Hello(name: any) exactly 1 time(s), performed 0 times."
                + Environment.NewLine
                + "Performed invocations of Hello: none."
        );
    }

    [Fact]
    public void GivenStoredOutcomeDescriptions_WhenFormatted_ShouldKeepAllConfiguredSteps()
    {
        object? descriptions = null;
        DiagnosticValue.AddOutcome(ref descriptions, "Returns");
        DiagnosticValue.AddOutcome(ref descriptions, "Throws<InvalidOperationException>");
        DiagnosticValue.AddOutcome(ref descriptions, "Callback");
        DiagnosticValue
            .Outcomes(descriptions)
            .ShouldBe("Returns, Throws<InvalidOperationException>, Callback");
        DiagnosticValue.Outcomes(null).ShouldBe(string.Empty);
    }
}
