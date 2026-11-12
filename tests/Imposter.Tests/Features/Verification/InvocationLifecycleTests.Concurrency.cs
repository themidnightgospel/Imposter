using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.Verification
{
    public partial class InvocationLifecycleTests
    {
        [Fact]
        public async Task GivenOverlappingMethodSetterAndEventCalls_WhenCleared_ShouldKeepEpochBoundariesAndSnapshotsStable()
        {
            var instance = _sut.Instance();
            Action handler = () => { };
            instance.Next();
            instance.Value = 1;
            instance.Changed += handler;
            var previous = ((IImposter)_sut).Invocations();
            var previousSequences = previous.Select(call => call.Sequence).ToArray();
            var epochStart = InvocationSequence.Next();
            using var entered = new CountdownEvent(3);
            using var release = new ManualResetEventSlim();
            var block = 1;
            void WaitForClear()
            {
                if (Volatile.Read(ref block) == 0)
                    return;
                entered.Signal();
                release.Wait(TimeSpan.FromSeconds(15)).ShouldBeTrue();
            }

            var method = _sut.Next();
            method.Returns(7);
            method.Callback(WaitForClear);
            _sut.Value.Setter(Arg<int>.Any()).Callback(_ => WaitForClear());
            _sut.Changed.OnSubscribe(_ => WaitForClear());
            var writers = new[]
            {
                Task.Run(() => instance.Next().ShouldBe(7)),
                Task.Run(() => instance.Value = 42),
                Task.Run(() => instance.Changed += handler),
            };
            long clearStamp;
            try
            {
                entered.Wait(TimeSpan.FromSeconds(15)).ShouldBeTrue();
                clearStamp = InvocationSequence.Next();
                ((IImposter)_sut).ClearInvocations();
                ((IImposter)_sut).Invocations().ShouldBeEmpty();
                previous.Select(call => call.Sequence).ShouldBe(previousSequences);
            }
            finally
            {
                Volatile.Write(ref block, 0);
                release.Set();
            }
            await Task.WhenAll(writers);

            // A call entering before Clear may publish afterward with an earlier stamp.
            // Buffer publications overlapping Clear may instead be omitted by the new generation.
            var overlapping = ((IImposter)_sut).Invocations();
            overlapping.Count.ShouldBeLessThanOrEqualTo(3);
            overlapping
                .Select(call => call.Sequence)
                .Distinct()
                .Count()
                .ShouldBe(overlapping.Count);
            foreach (var call in overlapping)
            {
                call.Sequence.ShouldBeGreaterThan(epochStart);
                call.Sequence.ShouldBeLessThan(clearStamp);
                call.Exception.ShouldBeNull();
            }
            method.Invocations.ShouldAllBe(call => call.Result == 7);
            _sut.Value.Setter(Arg<int>.Any()).Invocations.ShouldAllBe(call => call.Value == 42);
            _sut.Changed.Subscriptions.ShouldAllBe(call => call.Handler == handler);
            var overlappingSequences = overlapping.Select(call => call.Sequence).ToArray();

            ((IImposter)_sut).ClearInvocations();
            ((IImposter)_sut).Invocations().ShouldBeEmpty();
            var freshEpoch = InvocationSequence.Next();
            instance.Next().ShouldBe(7);
            instance.Value = 43;
            instance.Changed += handler;
            var fresh = ((IImposter)_sut).Invocations();
            fresh.Count.ShouldBe(3);
            fresh.ShouldAllBe(call => call.Sequence > freshEpoch);
            method.Called(Count.Once());
            _sut.Value.Setter(43).Called(Count.Once());
            _sut.Changed.Subscribed(handler, Count.Once());
            previous.Select(call => call.Sequence).ShouldBe(previousSequences);
            overlapping.Select(call => call.Sequence).ShouldBe(overlappingSequences);
        }
    }
}
