using System;
using System.Linq;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Abstractions.Tests;

public class InvocationHistoryLifecycleTests
{
    [Fact]
    public void GivenEmptyHistory_WhenClearedRepeatedly_ShouldRemainReusable()
    {
        var history = new InvocationHistory<TestInvocation>();
        history.Clear();
        history.Clear();
        history.ShouldBeEmpty();

        var invocation = new TestInvocation(1);
        history.Push(invocation);

        history.ShouldHaveSingleItem().ShouldBeSameAs(invocation);
    }

    [Fact]
    public void GivenCapturedHistory_WhenClearedAndReused_ShouldKeepCapturedRecordsIndependent()
    {
        var history = new InvocationHistory<TestInvocation>();
        var failure = new InvalidOperationException("recorded failure");
        var first = new TestInvocation(1, failure);
        var second = new TestInvocation(2);
        history.Push(first);
        history.Push(second);
        var captured = history.GetEnumerator();

        history.Clear();
        history.ShouldBeEmpty();
        var replacement = new TestInvocation(3);
        history.Push(replacement);

        history.ShouldHaveSingleItem().ShouldBeSameAs(replacement);
        captured.MoveNext().ShouldBeTrue();
        captured.Current.ShouldBeSameAs(second);
        captured.MoveNext().ShouldBeTrue();
        captured.Current.ShouldBeSameAs(first);
        captured.Current.Exception.ShouldBeSameAs(failure);
        captured.MoveNext().ShouldBeFalse();
    }

    [Fact]
    public void GivenConcurrentPublicationAndClear_WhenStartingANewEpoch_ShouldNeverResurrectThePreviousChain()
    {
        var history = new InvocationHistory<TestInvocation>();
        for (var epoch = 0; epoch < 64; epoch++)
        {
            history.Clear();
            var previous = new TestInvocation(epoch * 3 + 1);
            history.Push(previous);
            var captured = history.GetEnumerator();
            var overlapping = new TestInvocation(epoch * 3 + 2);

            Parallel.Invoke(() => history.Push(overlapping), history.Clear);

            // Either publication linearizes last, or Clear removes it. The old chain
            // cannot reappear through a publisher that captured its head before Clear.
            var current = history.ToArray();
            current.Length.ShouldBeLessThanOrEqualTo(1);
            if (current.Length == 1)
                current[0].ShouldBeSameAs(overlapping);
            captured.MoveNext().ShouldBeTrue();
            captured.Current.ShouldBeSameAs(previous);
            captured.MoveNext().ShouldBeFalse();

            var after = new TestInvocation(epoch * 3 + 3);
            history.Push(after);
            history.First().ShouldBeSameAs(after);
            history.ShouldNotContain(previous);
        }
    }

    private sealed class TestInvocation(long sequence, Exception? exception = null)
        : InvocationRecord(sequence, exception)
    {
        public override string Member => "Member";
    }
}
