using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.EventImpersonation;
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
            AllocatedBytesPerSetup(
                    () => new IPropertyAllocationSutImposter(),
                    sut => sut.Value.Getter().Returns(1).Then().Returns(2)
                )
                .ShouldBeLessThanOrEqualTo(400);
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

        // The arguments used to find the setup (24 bytes), the arguments the imposter keeps (24), the history entry (40)
        // and its stack node (32). Setups with the call's own type arguments need no adapter or converted arguments.
        // Checking candidate setups must not allocate enumerators, even when the newest setup does not match.
        [Fact]
        public void GivenGenericMethodWithoutCallbacks_WhenCalled_ShouldOnlyAllocateItsArgumentsAndHistory()
        {
            _sut.Echo<string>("match").Returns("result");
            _sut.Echo<string>("miss").Returns("other");
            var instance = _sut.Instance();

            AllocatedBytesPerCall(() => instance.Echo("match")).ShouldBeLessThanOrEqualTo(120);
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

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void GivenFixedAsyncOutcome_WhenCalled_ShouldOnlyAllocateArgumentsAndHistory(
            bool throws
        )
        {
            var sut = new IAsyncAllocationSutImposter();
            if (throws)
            {
                sut.GetAsync(Arg<int>.Any()).ThrowsAsync(new InvalidOperationException("failure"));
                sut.PeekAsync(Arg<int>.Any()).ThrowsAsync(new InvalidOperationException("failure"));
            }
            else
            {
                sut.GetAsync(Arg<int>.Any()).ReturnsAsync(12345);
                sut.PeekAsync(Arg<int>.Any()).ReturnsAsync(12345);
            }
            var instance = sut.Instance();
            // Do not await faults here: exception dispatch is runtime work, not generator allocation.
            AllocatedBytesPerCall(() => _ = instance.GetAsync(1)).ShouldBeLessThanOrEqualTo(96);
            AllocatedBytesPerCall(() => _ = instance.PeekAsync(1)).ShouldBeLessThanOrEqualTo(104);
        }

        [Fact]
        public void GivenByReferenceAsyncOutcome_WhenCalled_ShouldOnlyAllocateArgumentsAndHistory()
        {
            var sut = new IAsyncAllocationSutImposter();
            sut.GetByReferenceAsync(Arg<int>.Any(), OutArg<int>.Any(), Arg<int>.Any())
                .ReturnsAsync(12345);
            var instance = sut.Instance();
            var key = 7;
            var extra = 9;
            AllocatedBytesPerCall(() => _ = instance.GetByReferenceAsync(ref key, out _, in extra))
                .ShouldBeLessThanOrEqualTo(104);
        }

        [Fact]
        public void GivenCallbackOnlyIndexerGetter_WhenCalled_ShouldUseDefaultValueWithoutAllocatingDelegate()
        {
            var callbacks = 0;
            _sut[Arg<int>.Any()].Getter().Callback(_ => callbacks++);
            var instance = _sut.Instance();
            instance[1] = 42;
            AllocatedBytesPerCall(() => _ = instance[1]).ShouldBeLessThanOrEqualTo(56);
            instance[1].ShouldBe(42);
            callbacks.ShouldBe(Calls * 2 + 1);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void GivenAsyncEventWithoutTasks_WhenRaised_ShouldNotAllocatePendingTaskList(
            bool nullCallback
        )
        {
            var sut = new IEventSetupSutImposter();
            if (nullCallback)
            {
                sut.AsyncSomethingHappened.Callback((_, __) => null!);
            }
            // The public API returns Task<IEventSetupBuilder>, so its completed result still allocates.
            // Debug also allocates the compiler's async state machine classes.
#if DEBUG
            const long budget = 328;
#else
            const long budget = 112;
#endif
            AllocatedBytesPerCall(() =>
                    _ = sut.AsyncSomethingHappened.RaiseAsync(null, EventArgs.Empty)
                )
                .ShouldBeLessThanOrEqualTo(budget);
        }

        // Verifying a generic method matches each recorded call with the criteria as they are when the type arguments
        // are the same, so the cost doesn't grow with the history.
        [Fact]
        public void GivenGenericMethodHistory_WhenVerifying_ShouldNotAllocatePerRecordedCall()
        {
            var verification = _sut.Echo<int>(Arg<int>.Any());
            var instance = _sut.Instance();

            instance.Echo(1);
            var withOneCall = AllocatedBytesPerCall(() => verification.Called(Count.AtLeastOnce()));
            for (var i = 0; i < 1000; i++)
            {
                instance.Echo(i);
            }

            AllocatedBytesPerCall(() => verification.Called(Count.AtLeastOnce()))
                .ShouldBe(withOneCall);
        }

        // The arguments (24 bytes), the history entry (40) and its stack node (32): running the callbacks allocates
        // nothing.
        [Fact]
        public void GivenMethodWithCallback_WhenCalled_ShouldOnlyAllocateItsArgumentsAndHistory()
        {
            _sut.Square(Arg<int>.Any()).Returns(4).Callback(_ => { });
            var instance = _sut.Instance();

            AllocatedBytesPerCall(() => instance.Square(2)).ShouldBeLessThanOrEqualTo(96);
        }

        // A setup keeps its builder, group, step and Returns closure, and the steps' sequence. A queue for the steps
        // and one for each step's callbacks took 832 bytes each.
        [Fact]
        public void GivenMethod_WhenSetUp_ShouldNotAllocateQueues()
        {
            AllocatedBytesPerSetup(sut => sut.Square(Arg<int>.Any()).Returns(4))
                .ShouldBeLessThanOrEqualTo(400);
        }

        // Each Then() adds only its step, the step's node in the sequence and its Returns closure.
        [Fact]
        public void GivenMethodSequence_WhenSetUp_ShouldOnlyAllocateEachStep()
        {
            AllocatedBytesPerSetup(sut =>
                    sut.Square(Arg<int>.Any()).Returns(1).Then().Returns(2).Then().Returns(3)
                )
                .ShouldBeLessThanOrEqualTo(736);
        }

        // Like Returns, Throws keeps one closure over its argument. The step wrapped it in a second one, which took
        // 88 bytes more.
        [Fact]
        public void GivenMethodThrowingException_WhenSetUp_ShouldWrapItOnce()
        {
            var exception = new InvalidOperationException();

            AllocatedBytesPerSetup(sut => sut.Square(Arg<int>.Any()).Throws(exception))
                .ShouldBeLessThanOrEqualTo(400);
        }

        // Throws<TException>() captures nothing, so its delegate is cached and the setup allocates no closure.
        [Fact]
        public void GivenMethodThrowingExceptionType_WhenSetUp_ShouldNotAllocateClosures()
        {
            AllocatedBytesPerSetup(sut =>
                    sut.Square(Arg<int>.Any()).Throws<InvalidOperationException>()
                )
                .ShouldBeLessThanOrEqualTo(304);
        }

        private static long AllocatedBytesPerSetup(Action<ICallAllocationSutImposter> setup) =>
            AllocatedBytesPerSetup(() => new ICallAllocationSutImposter(), setup);

        // The setup is warmed up on other imposters first, so static initialization doesn't count. Averaging also
        // spreads the one-off cost of rebuilding a reflection cache that a GC collected.
        private static long AllocatedBytesPerSetup<TImposter>(
            Func<TImposter> createImposter,
            Action<TImposter> setup
        )
        {
            for (var i = 0; i < Calls; i++)
            {
                setup(createImposter());
            }

            var suts = new TImposter[Calls];
            for (var i = 0; i < Calls; i++)
            {
                suts[i] = createImposter();
            }

            var before = GC.GetAllocatedBytesForCurrentThread();
            foreach (var sut in suts)
            {
                setup(sut);
            }

            return (GC.GetAllocatedBytesForCurrentThread() - before) / Calls;
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
