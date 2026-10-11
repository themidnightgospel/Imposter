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

        // The arguments (24 bytes), the history entry (40) and its stack node (32), and the enumerator that looks for
        // the setup the arguments match (48).
        [Fact]
        public void GivenMethodWithoutCallbacks_WhenCalled_ShouldOnlyAllocateItsArgumentsHistoryAndSetupLookup()
        {
            _sut.Square(Arg<int>.Any()).Returns(4);
            var instance = _sut.Instance();

            AllocatedBytesPerCall(() => instance.Square(2)).ShouldBeLessThanOrEqualTo(144);
        }

        // The arguments (24 bytes), their history stack node (32), and the enumerator that looks for the getter setup
        // the arguments match (48).
        [Fact]
        public void GivenIndexerGetterWithoutCallbacks_WhenCalled_ShouldOnlyAllocateItsArgumentsHistoryAndSetupLookup()
        {
            _sut[Arg<int>.Any()].Getter().Returns(5);
            var instance = _sut.Instance();

            AllocatedBytesPerCall(() => _ = instance[1]).ShouldBeLessThanOrEqualTo(104);
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
