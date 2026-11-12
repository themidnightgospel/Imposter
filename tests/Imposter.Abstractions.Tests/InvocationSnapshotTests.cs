using System;
using System.Collections.Generic;
using System.Linq;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Abstractions.Tests;

public class InvocationSnapshotTests
{
    [Fact]
    public void GivenUnorderedHistory_WhenCopied_ShouldReturnAnOrderedDetachedSnapshot()
    {
        var later = new ObservedInvocation(2);
        var earlier = new ObservedInvocation(1);
        var history = new List<IImposterInvocation> { later, earlier };

        var snapshot = InvocationSnapshots.Copy(history);
        history.Clear();

        snapshot.ShouldBe(new IImposterInvocation[] { earlier, later });
        Should.Throw<NotSupportedException>(() => ((IList<IImposterInvocation>)snapshot).Clear());
    }

    [Fact]
    public void GivenDeferredOrdering_WhenMaterializingAndCombining_ShouldReadSequencesOnlyAtFinalOrdering()
    {
        var later = new ObservedInvocation(2);
        var earlier = new ObservedInvocation(1);
        var first = InvocationSnapshots.Copy(new[] { later }, false);
        var second = InvocationSnapshots.Copy(new[] { earlier }, false);

        var combined = InvocationSnapshots.Combine(first, second, false);
        combined.ShouldBe(new IImposterInvocation[] { later, earlier });
        later.SequenceReads.ShouldBe(0);
        earlier.SequenceReads.ShouldBe(0);

        var ordered = InvocationSnapshots.Order(combined.ToList());
        ordered.ShouldBe(new IImposterInvocation[] { earlier, later });
        later.SequenceReads.ShouldBeGreaterThan(0);
        earlier.SequenceReads.ShouldBeGreaterThan(0);
    }

    [Fact]
    public void GivenEventHistory_WhenUsingPublicSnapshot_ShouldKeepOrderingWithoutExposingConstructors()
    {
        var history = new InvocationBuffer<Action>();
        Action earlier = () => { };
        Action later = () => { };
        history.Add(earlier, 1, null);
        history.Add(later, 2, null);

        var snapshot = EventSubscriptionInvocation<Action>.Snapshot(history, "Changed", true);

        snapshot.Select(invocation => invocation.Sequence).ShouldBe(new long[] { 1, 2 });
        snapshot.Select(invocation => invocation.Handler).ShouldBe(new[] { earlier, later });
        typeof(EventSubscriptionInvocation<Action>).GetConstructors().ShouldBeEmpty();
    }

    private sealed class ObservedInvocation(long sequence) : IImposterInvocation
    {
        public int SequenceReads { get; private set; }
        public string Member => "Member";
        public Exception? Exception => null;
        public long Sequence
        {
            get
            {
                SequenceReads++;
                return sequence;
            }
        }
    }
}
