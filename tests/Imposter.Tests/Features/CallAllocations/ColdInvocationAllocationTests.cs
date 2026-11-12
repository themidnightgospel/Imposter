using System;
using Imposter.Abstractions;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Imposter.Tests.Features.CallAllocations
{
    public class ColdInvocationAllocationTests
    {
        private const int MeasuredImposters = 100;
        private const int Samples = 5;
        private readonly ITestOutputHelper _output;

        public ColdInvocationAllocationTests(ITestOutputHelper output) => _output = output;

        [Theory]
        [InlineData(false, 1136)]
        [InlineData(true, 1352)]
        public void GivenFreshPropertyImposter_WhenFirstSetterRuns_ShouldBoundConstructionSetupAndFirstSegment(
            bool withCallback,
            long ceiling
        )
        {
            MinimumFreshBatchBytes(() =>
                {
                    var sut = new IPropertyAllocationSutImposter();
                    if (withCallback)
                        sut.Value.Setter(Arg<int>.Any()).Callback(static _ => { });
                    var instance = sut.Instance();
                    instance.Value = 1;
                    GC.KeepAlive(instance);
                })
                .ShouldBeLessThanOrEqualTo(ceiling * MeasuredImposters);
        }

        [Theory]
        [InlineData(false, 1000)]
        [InlineData(true, 1872)]
        public void GivenFreshEventImposter_WhenFirstAccessorsRun_ShouldBoundConstructionAndFirstSegments(
            bool subscribe,
            long ceiling
        )
        {
            EventHandler handler = static (_, _) => { };
            MinimumFreshBatchBytes(() =>
                {
                    var sut = new IEventAllocationSutImposter();
                    var instance = sut.Instance();
                    if (subscribe)
                        instance.Changed += handler;
                    instance.Changed -= handler;
                    GC.KeepAlive(instance);
                })
                .ShouldBeLessThanOrEqualTo(ceiling * MeasuredImposters);
        }

        private long MinimumFreshBatchBytes(Action constructAndCall)
        {
            // Warm generated code and cached delegates on other imposters. Every measured imposter is fresh:
            // its construction, setup, Instance, and first history segments are all included.
            for (var index = 0; index < MeasuredImposters; index++)
                constructAndCall();

            // Repeated batches exclude occasional process/runtime work from the allocation floor.
            // A deterministic per-imposter increase affects every batch and still fails the same ceiling.
            var totals = new long[Samples];
            var minimum = long.MaxValue;
            for (var sample = 0; sample < Samples; sample++)
            {
                var before = GC.GetAllocatedBytesForCurrentThread();
                for (var index = 0; index < MeasuredImposters; index++)
                    constructAndCall();
                totals[sample] = GC.GetAllocatedBytesForCurrentThread() - before;
                minimum = Math.Min(minimum, totals[sample]);
            }

            // Report after all measurement windows; compare totals without rounding bytes per imposter.
            _output.WriteLine(
                $"Fresh batches of {MeasuredImposters} imposters, including first interactions (bytes): {string.Join(", ", totals)}."
            );
            return minimum;
        }
    }
}
