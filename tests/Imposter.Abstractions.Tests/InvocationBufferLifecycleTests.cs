using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Abstractions.Tests;

public class InvocationBufferLifecycleTests
{
    [Fact]
    public void GivenGrowingBuffer_WhenPublicationOverlapsClear_ShouldKeepCapturedGenerationStable()
    {
        var history = new InvocationBuffer<int>();
        var failure = new InvalidOperationException("recorded failure");
        for (var value = 1; value <= 64; value++)
            history.Add(value, value, value % 2 == 0 ? failure : null);
        var captured = history.GetEnumerator();

        Parallel.Invoke(
            () => Parallel.For(1000, 1256, value => history.Add(value, value, failure)),
            history.Clear
        );

        var remaining = new HashSet<long>();
        foreach (var entry in history)
        {
            // An overlapping writer may publish into a detached generation. Do not
            // require every racing write to survive, but any visible entry is complete.
            entry.Value.ShouldBeInRange(1000, 1255);
            entry.Sequence.ShouldBe(entry.Value);
            entry.Exception.ShouldBeSameAs(failure);
            remaining.Add(entry.Sequence).ShouldBeTrue();
        }
        for (var expected = 64; expected >= 1; expected--)
        {
            captured.MoveNext().ShouldBeTrue();
            captured.Current.Value.ShouldBe(expected);
            captured.Current.Sequence.ShouldBe(expected);
            captured.Current.Exception.ShouldBeSameAs(expected % 2 == 0 ? failure : null);
        }
        captured.MoveNext().ShouldBeFalse();

        history.Clear();
        history.Add(2000, 2000, null);
        var fresh = history.GetEnumerator();
        fresh.MoveNext().ShouldBeTrue();
        fresh.Current.Value.ShouldBe(2000);
        fresh.MoveNext().ShouldBeFalse();
    }

    [Fact]
    public void GivenCapturedBuffer_WhenClearedAndReused_ShouldKeepCapturedEntriesIndependent()
    {
        var history = new InvocationBuffer<int>();
        history.Add(10, 1, null);
        var beforeClear = history.GetEnumerator();

        history.Clear();
        history.GetEnumerator().MoveNext().ShouldBeFalse();
        history.Add(20, 2, null);

        beforeClear.MoveNext().ShouldBeTrue();
        beforeClear.Current.Value.ShouldBe(10);
        beforeClear.Current.Sequence.ShouldBe(1);
        beforeClear.MoveNext().ShouldBeFalse();
        var afterClear = history.GetEnumerator();
        afterClear.MoveNext().ShouldBeTrue();
        afterClear.Current.Value.ShouldBe(20);
        afterClear.Current.Sequence.ShouldBe(2);
        afterClear.MoveNext().ShouldBeFalse();
    }
}
