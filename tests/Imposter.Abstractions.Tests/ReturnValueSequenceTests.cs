using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Abstractions.Tests;

public class ReturnValueSequenceTests
{
    [Fact]
    public void GivenExhaustedSequence_WhenAddingMoreOutcomes_ShouldConsumeThemInOrder()
    {
        var sequence = new ReturnValueSequence<string?>();
        sequence.Next("fallback").ShouldBe("fallback");
        sequence.Add("first");
        sequence.Add(null);
        sequence.Next("fallback").ShouldBe("first");
        sequence.Next("fallback").ShouldBeNull();
        sequence.Next("fallback").ShouldBeNull();
        sequence.Add("next");
        sequence.Add("last");
        sequence.Next("fallback").ShouldBe("next");
        sequence.Next("fallback").ShouldBe("last");
        sequence.Next("fallback").ShouldBe("last");
    }

    [Fact]
    public void GivenConcurrentReaders_WhenConsumingSequence_ShouldConsumeEachOutcomeOnce()
    {
        var sequence = new ReturnValueSequence<Func<int>>();
        const int count = 200;
        for (var i = 0; i < count; i++)
        {
            var value = i;
            sequence.Add(() => value);
        }

        var results = new int[count];
        Parallel.For(0, count, i => results[i] = sequence.Next(() => -1)());

        results.OrderBy(value => value).ShouldBe(Enumerable.Range(0, count));
        sequence.Next(() => -1)().ShouldBe(count - 1);
        sequence.Add(() => count);
        sequence.Next(() => -1)().ShouldBe(count);
    }

    [Fact]
    public void GivenExhaustedSequence_WhenAppendingDuringReads_ShouldConsumeEveryNewOutcome()
    {
        var sequence = new ReturnValueSequence<string>();
        const int count = 200;
        var last = "initial";
        sequence.Add(last);
        sequence.Next("fallback").ShouldBe(last);

        Parallel.Invoke(
            () =>
            {
                for (var i = 0; i < count; i++)
                    sequence.Add(i.ToString());
            },
            () =>
            {
                for (var i = 0; i < count; i++)
                {
                    var next = last;
                    SpinWait
                        .SpinUntil(
                            () => (next = sequence.Next("fallback")) != last,
                            TimeSpan.FromSeconds(10)
                        )
                        .ShouldBeTrue();
                    next.ShouldBe(i.ToString());
                    last = next;
                }
            }
        );
        sequence.Next("fallback").ShouldBe((count - 1).ToString());
    }
}
