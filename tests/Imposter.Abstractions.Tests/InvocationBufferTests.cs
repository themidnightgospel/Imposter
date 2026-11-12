using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Abstractions.Tests;

public class InvocationBufferTests
{
    [Fact]
    public void GivenDefaultOrEmptyEnumerator_WhenAdvanced_ShouldBeEmpty()
    {
        var empty = default(InvocationBuffer<int>.Enumerator);
        empty.MoveNext().ShouldBeFalse();
        new InvocationBuffer<int>().GetEnumerator().MoveNext().ShouldBeFalse();
        var setter = new TestSetter();
        setter.Called(Arg<int>.Any(), Count.Never());
    }

    [Fact]
    public void GivenCapturedHead_WhenAppendingOrClearing_ShouldKeepOnlyCapturedEntries()
    {
        var buffer = new InvocationBuffer<int>();
        buffer.Add(1, 10, null);
        var snapshot = buffer.GetEnumerator();
        var secondPass = snapshot;
        buffer.Add(2, 20, null);
        buffer.Clear();
        buffer.Add(3, 30, null);

        snapshot.MoveNext().ShouldBeTrue();
        snapshot.Current.Value.ShouldBe(1);
        InvocationBuffer<int>.MarkVerified(snapshot.Current);
        snapshot.Current.Sequence.ShouldBe(10);
        snapshot.MoveNext().ShouldBeFalse();
        secondPass.MoveNext().ShouldBeTrue();
        InvocationBuffer<int>.IsVerified(secondPass.Current).ShouldBeTrue();
        var current = buffer.GetEnumerator();
        current.MoveNext().ShouldBeTrue();
        current.Current.Value.ShouldBe(3);
        InvocationBuffer<int>.IsVerified(current.Current).ShouldBeFalse();
    }

    [Fact]
    public void GivenMaterializedSetterRecords_WhenVerified_ShouldUpdateTheirOriginalCoverageOnly()
    {
        var setter = new TestSetter();
        setter.Set(1);
        var old = (IVerifiableInvocation)setter.Snapshot()[0];
        old.IsVerified.ShouldBeFalse();
        setter.Called(Arg<int>.Is(1), Count.Once());
        old.IsVerified.ShouldBeTrue();
        setter.ClearInvocations();
        setter.Set(2);
        old.MarkVerified();
        setter.UncoveredInteractions().ShouldHaveSingleItem();
        var current = (IVerifiableInvocation)setter.Snapshot()[0];
        current.MarkVerified();
        setter.UncoveredInteractions().ShouldBeEmpty();
    }

    [Fact]
    public async Task GivenConcurrentSegmentGrowth_WhenReading_ShouldOnlyExposeCompleteEntries()
    {
        const int calls = 32768;
        var buffer = new InvocationBuffer<int>();
        var failure = new InvalidOperationException("recorded");
        var writer = Task.Run(() =>
            Parallel.For(
                1,
                calls + 1,
                value => buffer.Add(value, value, value % 2 == 0 ? failure : null)
            )
        );
        do
        {
            foreach (var entry in buffer)
            {
                entry.Sequence.ShouldBe(entry.Value);
                entry.Value.ShouldBeGreaterThan(0);
                entry.Exception.ShouldBeSameAs(entry.Value % 2 == 0 ? failure : null);
            }
        } while (!writer.IsCompleted);
        await writer;
        var values = new HashSet<int>();
        foreach (var entry in buffer)
            values.Add(entry.Value).ShouldBeTrue();
        values.Count.ShouldBe(calls);
    }

    [Fact]
    public void GivenConcurrentCoverage_WhenMarked_ShouldKeepSequenceAndPayload()
    {
        var buffer = new InvocationBuffer<int>();
        buffer.Add(42, long.MaxValue - 1, null);
        var cursor = buffer.GetEnumerator();
        cursor.MoveNext().ShouldBeTrue();
        var entry = cursor.Current;
        Parallel.For(0, 128, _ => InvocationBuffer<int>.MarkVerified(entry));
        InvocationBuffer<int>.IsVerified(entry).ShouldBeTrue();
        entry.Sequence.ShouldBe(long.MaxValue - 1);
        entry.Value.ShouldBe(42);
    }

    [Fact]
    public void GivenOldRecordAfterClear_WhenCollecting_ShouldNotRetainUnrelatedLaterSegments()
    {
        var (entry, laterValue) = CreateClearedHistory();
        // Force collection so this test can distinguish unreachable values from retained history.
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
        laterValue.IsAlive.ShouldBeFalse();
        entry.Value.ShouldNotBeNull();
        GC.KeepAlive(entry);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static (
        InvocationBuffer<object>.Entry Entry,
        WeakReference LaterValue
    ) CreateClearedHistory()
    {
        var buffer = new InvocationBuffer<object>();
        buffer.Add(new object(), 1, null);
        var cursor = buffer.GetEnumerator();
        cursor.MoveNext();
        var entry = cursor.Current;
        for (var index = 0; index < 100; index++)
            buffer.Add(new object(), index + 2, null);
        var value = new object();
        var weak = new WeakReference(value);
        buffer.Add(value, 102, null);
        buffer.Clear();
        return (entry, weak);
    }

    private sealed class TestSetter() : PropertySetter<int>(null, ImposterMode.Implicit, "Value");
}
