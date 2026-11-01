using System;
using System.Threading;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.CallAllocations
{
    public class CollectionLifecycleTests
    {
        private readonly ICallAllocationSutImposter _sut = new ICallAllocationSutImposter();

        [Fact]
        public void GivenUntouchedMembers_WhenVerifying_ShouldTreatMissingHistoriesAsEmpty()
        {
            var events = new IEventAllocationSutImposter();
            _sut.Value.Getter().Called(Count.Never());
            _sut.Value.Setter(Arg<int>.Any()).Called(Count.Never());
            _sut[Arg<int>.Any()].Getter().Called(Count.Never());
            _sut[Arg<int>.Any()].Setter().Called(Count.Never());
            events.Changed.Raised(Arg<object?>.Any(), Arg<EventArgs>.Any(), Count.Never());
            events.Changed.Subscribed(Arg<EventHandler>.Any(), Count.Never());
            events.Changed.Unsubscribed(Arg<EventHandler>.Any(), Count.Never());
            events.Changed.HandlerInvoked(Arg<EventHandler>.Any(), Count.Never());

            Action[] verifications =
            {
                () => _sut.Value.Setter(Arg<int>.Any()).Called(Count.Once()),
                () => _sut[Arg<int>.Any()].Getter().Called(Count.Once()),
                () => _sut[Arg<int>.Any()].Setter().Called(Count.Once()),
                () => events.Changed.Raised(Arg<object?>.Any(), Arg<EventArgs>.Any(), Count.Once()),
                () => events.Changed.Subscribed(Arg<EventHandler>.Any(), Count.Once()),
                () => events.Changed.Unsubscribed(Arg<EventHandler>.Any(), Count.Once()),
                () => events.Changed.HandlerInvoked(Arg<EventHandler>.Any(), Count.Once()),
            };
            foreach (var verify in verifications)
                Should
                    .Throw<VerificationFailedException>(verify)
                    .PerformedInvocations.ShouldBeEmpty();
        }

        [Fact]
        public void GivenConsumedPropertyReturns_WhenAddingMore_ShouldConsumeTheNewSequence()
        {
            var instance = _sut.Instance();
            _sut.Value.Getter().Returns(1).Then().Returns(2);
            instance.Value.ShouldBe(1);
            instance.Value.ShouldBe(2);
            instance.Value.ShouldBe(2);

            _sut.Value.Getter().Returns(3).Then().Returns(4);
            instance.Value.ShouldBe(3);
            instance.Value.ShouldBe(4);
            instance.Value.ShouldBe(4);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public async Task GivenCallbackSnapshot_WhenAnotherThreadRegisters_ShouldApplyItOnTheNextCall(
            int member
        )
        {
            var instance = _sut.Instance();
            var events = new IEventAllocationSutImposter();
            var eventInstance = events.Instance();
            EventHandler handler = (_, __) => { };
            Action<Action> register;
            Action invoke;
            switch (member)
            {
                case 0:
                    register = callback => _sut.Value.Getter().Callback(callback);
                    invoke = () => _ = instance.Value;
                    break;
                case 1:
                    register = callback =>
                        _sut.Value.Setter(Arg<int>.Any()).Callback(_ => callback());
                    invoke = () => instance.Value = 1;
                    break;
                case 2:
                    var getter = _sut[Arg<int>.Any()].Getter();
                    register = callback => getter.Callback(_ => callback());
                    invoke = () => _ = instance[1];
                    break;
                case 3:
                    register = callback =>
                        _sut[Arg<int>.Any()].Setter().Callback((_, __) => callback());
                    invoke = () => instance[1] = 1;
                    break;
                case 4:
                    register = callback => events.Changed.Callback((_, __) => callback());
                    invoke = () => events.Changed.Raise(null, EventArgs.Empty);
                    break;
                case 5:
                    register = callback => events.Changed.OnSubscribe(_ => callback());
                    invoke = () => eventInstance.Changed += handler;
                    break;
                default:
                    register = callback => events.Changed.OnUnsubscribe(_ => callback());
                    invoke = () => eventInstance.Changed -= handler;
                    break;
            }

            using var entered = new ManualResetEventSlim();
            using var release = new ManualResetEventSlim();
            var blockFirstCall = 1;
            register(() =>
            {
                if (Interlocked.Exchange(ref blockFirstCall, 0) == 1)
                {
                    entered.Set();
                    release.Wait(TimeSpan.FromSeconds(10)).ShouldBeTrue();
                }
            });

            var addedCallbackCalls = 0;
            var calling = Task.Factory.StartNew(
                invoke,
                CancellationToken.None,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default
            );
            try
            {
                entered.Wait(TimeSpan.FromSeconds(10)).ShouldBeTrue();
                register(() => addedCallbackCalls++);
            }
            finally
            {
                release.Set();
                await calling;
            }

            addedCallbackCalls.ShouldBe(0);
            invoke();
            addedCallbackCalls.ShouldBe(1);
        }

        [Fact]
        public void GivenEventHistory_WhenVerificationFails_ShouldDescribeRaisesInOriginalOrder()
        {
            var events = new IEventAllocationSutImposter();
            events.Changed.Raise("first", EventArgs.Empty).Raise("second", EventArgs.Empty);

            var exception = Should.Throw<VerificationFailedException>(() =>
                events.Changed.Raised(Arg<object?>.Any(), Arg<EventArgs>.Any(), Count.Once())
            );
            var description = exception.PerformedInvocations!;
            description
                .IndexOf("first", StringComparison.Ordinal)
                .ShouldBeLessThan(description.IndexOf("second", StringComparison.Ordinal));
        }
    }
}
