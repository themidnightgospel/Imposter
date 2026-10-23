using System;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.CallAllocations
{
    // A call allocates only what the imposter keeps of it: its arguments and its history entry. With no callbacks
    // registered, it allocates nothing to look for them. The budgets are 64-bit object sizes.
    public class CallAllocationTests
    {
        private const int Calls = 100;

        private readonly ICallAllocationSutImposter _sut = new ICallAllocationSutImposter();

        [Fact]
        public void GivenProperty_WhenConstructed_ShouldNotAllocateUnusedCollections()
        {
            AllocatedBytesPerCall(() => GC.KeepAlive(new IPropertyAllocationSutImposter()))
                .ShouldBeLessThanOrEqualTo(280);
        }

        [Fact]
        public void GivenEvent_WhenConstructed_ShouldNotAllocateUnusedCollections()
        {
            AllocatedBytesPerCall(() => GC.KeepAlive(new IEventAllocationSutImposter()))
                .ShouldBeLessThanOrEqualTo(280);
        }

        [Fact]
        public void GivenIndexer_WhenConstructed_ShouldNotAllocateUnusedCollections()
        {
            AllocatedBytesPerCall(() => GC.KeepAlive(new IIndexerAllocationSutImposter()))
                .ShouldBeLessThanOrEqualTo(312);
        }

        [Fact]
        public void GivenPropertyGetterWithCallback_WhenCalled_ShouldNotAllocate()
        {
            _sut.Value.Getter().Returns(3).Callback(() => { });
            var instance = _sut.Instance();

            AllocatedBytesPerCall(() => _ = instance.Value).ShouldBe(0);
        }

        [Fact]
        public void GivenPropertySetterWithCallback_WhenCalled_ShouldOnlyAllocateItsHistory()
        {
            _sut.Value.Setter(Arg<int>.Any()).Callback(_ => { });
            var instance = _sut.Instance();

            AllocatedBytesPerCall(() => instance.Value = 1).ShouldBeLessThanOrEqualTo(32);
        }

        [Fact]
        public void GivenIndexerGetterWithCallback_WhenCalled_ShouldOnlyAllocateItsArgumentsAndHistory()
        {
            _sut[Arg<int>.Any()].Getter().Returns(5).Callback(_ => { });
            var instance = _sut.Instance();

            AllocatedBytesPerCall(() => _ = instance[1]).ShouldBeLessThanOrEqualTo(56);
        }

        [Fact]
        public void GivenIndexerSetterWithCallback_WhenCalled_ShouldOnlyAllocateItsArgumentsAndHistory()
        {
            _sut[Arg<int>.Any()].Setter().Callback((_, __) => { });
            var instance = _sut.Instance();

            AllocatedBytesPerCall(() => instance[1] = 2).ShouldBeLessThanOrEqualTo(64);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void GivenEvent_WhenRaisedAndVerified_ShouldOnlyAllocateItsHistory(bool withCallback)
        {
            var sut = new IEventAllocationSutImposter();
            if (withCallback)
            {
                sut.Changed.Callback((_, __) => { });
            }

            var sender = Arg<object?>.Any();
            var args = Arg<EventArgs>.Any();
            var count = Count.AtLeast(1);
            AllocatedBytesPerCall(() =>
                {
                    sut.Changed.Raise(null, EventArgs.Empty);
                    sut.Changed.Raised(sender, args, count);
                })
                .ShouldBeLessThanOrEqualTo(128);
        }

        [Fact]
        public void GivenSubscribedEvent_WhenRaised_ShouldReuseItsHandlerSnapshot()
        {
            var sut = new IEventAllocationSutImposter();
            sut.Instance().Changed += (_, __) => { };

            AllocatedBytesPerCall(() => sut.Changed.Raise(null, EventArgs.Empty))
                .ShouldBeLessThanOrEqualTo(128);
        }

        [Fact]
        public void GivenProperty_WhenConfiguringFirstReturns_ShouldUseLightweightSequenceStorage()
        {
            var sut = new IPropertyAllocationSutImposter();
            // Warm the delegate caches and sequence code separately from the measured imposter.
            new IPropertyAllocationSutImposter()
                .Value.Getter()
                .Returns(1)
                .Then()
                .Returns(2);
            var before = GC.GetAllocatedBytesForCurrentThread();
            sut.Value.Getter().Returns(1).Then().Returns(2);
            var allocated = GC.GetAllocatedBytesForCurrentThread() - before;

            allocated.ShouldBeLessThanOrEqualTo(400);
        }

        [Fact]
        public void GivenPropertyGetterWithoutCallbacks_WhenCalled_ShouldNotAllocate()
        {
            _sut.Value.Getter().Returns(3);
            var instance = _sut.Instance();

            AllocatedBytesPerCall(() => _ = instance.Value).ShouldBe(0);
        }

        // The setter keeps the value in its history: a 32-byte stack node.
        [Fact]
        public void GivenPropertySetterWithoutCallbacks_WhenCalled_ShouldOnlyAllocateItsHistory()
        {
            var instance = _sut.Instance();

            AllocatedBytesPerCall(() => instance.Value = 1).ShouldBeLessThanOrEqualTo(32);
        }

        // The arguments (24 bytes), the history entry (40) and its stack node (32).
        [Fact]
        public void GivenMethodWithoutCallbacks_WhenCalled_ShouldOnlyAllocateItsArgumentsAndHistory()
        {
            _sut.Square(Arg<int>.Any()).Returns(4);
            var instance = _sut.Instance();

            AllocatedBytesPerCall(() => instance.Square(2)).ShouldBeLessThanOrEqualTo(96);
        }

        // The arguments (24 bytes), the void history entry (32) and its stack node (32).
        [Fact]
        public void GivenVoidMethodWithoutCallbacks_WhenCalled_ShouldOnlyAllocateItsArgumentsAndHistory()
        {
            _sut.Log(Arg<string>.Any());
            var instance = _sut.Instance();

            AllocatedBytesPerCall(() => instance.Log("value")).ShouldBeLessThanOrEqualTo(88);
        }

        // The arguments and history (96 bytes), an argument conversion for invocation (24), and an adapter plus
        // converted matching arguments per candidate (48 each, with two candidates here).
        // Checking candidate setups must not allocate enumerators, even when the newest setup does not match.
        [Fact]
        public void GivenGenericMethodWithoutCallbacks_WhenCalled_ShouldOnlyAllocateItsArgumentsHistoryAndAdapters()
        {
            _sut.Echo<string>("match").Returns("result");
            _sut.Echo<string>("miss").Returns("other");
            var instance = _sut.Instance();

            AllocatedBytesPerCall(() => instance.Echo("match")).ShouldBeLessThanOrEqualTo(216);
        }

        // The arguments (24 bytes) and their history stack node (32).
        [Fact]
        public void GivenIndexerGetterWithoutCallbacks_WhenCalled_ShouldOnlyAllocateItsArgumentsAndHistory()
        {
            _sut[Arg<int>.Any()].Getter().Returns(5);
            var instance = _sut.Instance();

            AllocatedBytesPerCall(() => _ = instance[1]).ShouldBeLessThanOrEqualTo(56);
        }

        // The arguments (24 bytes) and the history stack node that keeps them with the value (40).
        [Fact]
        public void GivenIndexerSetterWithoutCallbacks_WhenCalled_ShouldOnlyAllocateItsArgumentsAndHistory()
        {
            var instance = _sut.Instance();

            AllocatedBytesPerCall(() => instance[1] = 2).ShouldBeLessThanOrEqualTo(64);
        }

        // The call is warmed up first, so static initialization doesn't count.
        private static long AllocatedBytesPerCall(Action call)
        {
            for (var i = 0; i < Calls; i++)
            {
                call();
            }

            var before = GC.GetAllocatedBytesForCurrentThread();
            for (var i = 0; i < Calls; i++)
            {
                call();
            }

            return (GC.GetAllocatedBytesForCurrentThread() - before) / Calls;
        }
    }
}
