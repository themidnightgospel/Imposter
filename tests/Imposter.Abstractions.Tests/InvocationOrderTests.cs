using System;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Abstractions.Tests;

public class InvocationOrderTests
{
    [Fact]
    public void GivenNullStep_WhenAdding_ShouldRejectItWithoutChangingTheOrder()
    {
        var order = Imposters.InOrder();
        Should.Throw<ArgumentNullException>(() => order.Then(null!)).ParamName.ShouldBe("step");
        var entry = new TestInvocation(1);
        order.Then(new TestStep(entry)).Verify();
        entry.IsVerified.ShouldBeTrue();
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void GivenStepCallback_WhenMutatingOrReenteringOrder_ShouldRejectAndRemainReusable(
        bool reenter
    )
    {
        var order = Imposters.InOrder();
        var entry = new TestInvocation(1);
        var extra = new TestInvocation(2);
        var step = new TestStep(entry);
        step.BeforeSelection = () =>
        {
            if (reenter)
                order.Verify();
            else
                order.Then(new TestStep(extra));
        };
        order.Then(step);

        Should.Throw<InvalidOperationException>(() => order.Verify());
        entry.IsVerified.ShouldBeFalse();
        extra.IsVerified.ShouldBeFalse();

        step.BeforeSelection = null;
        order.Verify();
        entry.IsVerified.ShouldBeTrue();
        extra.IsVerified.ShouldBeFalse();
    }

    [Fact]
    public void GivenDefaultSelection_WhenStepReportsMatch_ShouldRejectWithoutCoveringEarlierSteps()
    {
        var entry = new TestInvocation(1);
        var invalid = new TestStep(null);
        var order = Imposters.InOrder().Then(new TestStep(entry)).Then(invalid);

        Should
            .Throw<InvalidOperationException>(() => order.Verify())
            .Message.ShouldBe("An ordered step returned an invalid selection.");
        entry.IsVerified.ShouldBeFalse();

        var later = new TestInvocation(2);
        invalid.Entry = later;
        order.Verify();
        entry.IsVerified.ShouldBeTrue();
        later.IsVerified.ShouldBeTrue();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void GivenNonIncreasingSelection_WhenVerifying_ShouldRejectWithoutCoveringAnyStep(
        long sequence
    )
    {
        var first = new TestInvocation(1);
        var invalid = new TestInvocation(sequence);
        var order = Imposters.InOrder().Then(new TestStep(first)).Then(new TestStep(invalid));

        Should
            .Throw<InvalidOperationException>(() => order.Verify())
            .Message.ShouldBe("An ordered step returned an invalid selection.");
        first.IsVerified.ShouldBeFalse();
        invalid.IsVerified.ShouldBeFalse();
    }

    [Fact]
    public void GivenOwnerlessStep_WhenNoMatchExists_ShouldReportFailureWithoutCoveringEarlierSteps()
    {
        var first = new TestInvocation(1);
        var missing = new TestStep(null) { Matches = false };
        var order = Imposters.InOrder().Then(new TestStep(first)).Then(missing);

        var failure = Should.Throw<VerificationFailedException>(() => order.Verify());

        failure.Message.ShouldContain("Step 2: owning imposter unavailable.");
        first.IsVerified.ShouldBeFalse();
    }

    private sealed class TestStep(TestInvocation? entry) : IInvocationStep
    {
        public TestInvocation? Entry { get; set; } = entry;
        public Action? BeforeSelection { get; set; }
        public bool Matches { get; set; } = true;
        public IImposter Imposter => null!;
        public string Description => "test step";

        public bool TryFindAfter(long sequence, out InvocationMatch match)
        {
            BeforeSelection?.Invoke();
            match = Entry is null ? default : InvocationMatch.FromInvocation(Entry);
            return Matches;
        }
    }

    private sealed class TestInvocation(long sequence) : IVerifiableInvocation
    {
        public string Member => "test member";
        public long Sequence => sequence;
        public Exception? Exception => null;
        public bool IsVerified { get; private set; }

        public void MarkVerified() => IsVerified = true;
    }
}
