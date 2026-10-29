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

    [Fact]
    public void GivenEightThreads_WhenCallingMoreTimesThanOutcomes_ShouldServeEachOnceThenRepeatTheLast()
    {
        const int count = 1_000;
        const int threads = 8;
        const int callsPerThread = 250;
        for (var round = 0; round < 50; round++)
        {
            var sequence = new ReturnValueSequence<string>();
            for (var i = 0; i < count; i++)
                sequence.Add(i.ToString());

            var results = new string[threads][];
            using var start = new Barrier(threads);
            Parallel.For(
                0,
                threads,
                new ParallelOptions { MaxDegreeOfParallelism = threads },
                thread =>
                {
                    var served = new string[callsPerThread];
                    start.SignalAndWait();
                    for (var call = 0; call < callsPerThread; call++)
                        served[call] = sequence.Next("fallback");
                    results[thread] = served;
                }
            );

            var all = results.SelectMany(served => served).ToArray();
            all.Where(value => value != "999")
                .Select(int.Parse)
                .OrderBy(value => value)
                .ShouldBe(Enumerable.Range(0, count - 1));
            all.Count(value => value == "999").ShouldBe(threads * callsPerThread - (count - 1));
            sequence.Next("fallback").ShouldBe("999");
        }
    }

    [Fact]
    public void GivenConcurrentAdders_WhenConsumingAfterwards_ShouldKeepEachAdderOrder()
    {
        const int adders = 4;
        const int perAdder = 250;
        var sequence = new ReturnValueSequence<Tuple<int, int>?>();
        using var start = new Barrier(adders);

        Parallel.For(
            0,
            adders,
            new ParallelOptions { MaxDegreeOfParallelism = adders },
            adder =>
            {
                start.SignalAndWait();
                for (var i = 0; i < perAdder; i++)
                    sequence.Add(Tuple.Create(adder, i));
            }
        );

        var served = Enumerable
            .Range(0, adders * perAdder)
            .Select(_ => sequence.Next(null).ShouldNotBeNull())
            .ToArray();
        foreach (var adder in Enumerable.Range(0, adders))
            served
                .Where(item => item.Item1 == adder)
                .Select(item => item.Item2)
                .ShouldBe(Enumerable.Range(0, perAdder));
        sequence.Next(null).ShouldBeSameAs(served[^1]);
    }
}
