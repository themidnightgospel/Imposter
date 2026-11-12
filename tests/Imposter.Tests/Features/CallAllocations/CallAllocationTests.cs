using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.EventImpersonation;
using Imposter.Tests.Features.MethodImpersonation;
using Shouldly;
using Xunit;

[assembly: GenerateImposter(
    typeof(Imposter.Tests.Features.CallAllocations.ISpanAdapterAllocationSut)
)]

namespace Imposter.Tests.Features.CallAllocations
{
    // A call allocates only what the imposter keeps of it: its arguments and its history entry. With no callbacks
    // registered, it allocates nothing to look for them. The budgets are 64-bit object sizes.
    public interface ISpanAdapterAllocationSut
    {
        bool Contains<T>(ReadOnlySpan<int> items, T item);
    }

    // A call allocates only what the imposter keeps of it: its arguments and its history entry. With no callbacks
    // registered, it allocates nothing to look for them. The budgets are 64-bit object sizes.
    public class CallAllocationTests
    {
        [Fact]
        public void GivenGenericSpanCompatibleArrayPredicate_WhenCalled_ShouldKeepPrivateCopiesWithinBudget()
        {
            var sut = new ISpanAdapterAllocationSutImposter();
            sut.Contains<object>(ReadOnlySpanArg<int>.Is(items => items[0] >= 0), Arg<object>.Any())
                .Returns(true);
            var instance = sut.Instance();
            var items = new[] { 1, 2, 3, 4 };
            instance.Contains<string>(items, "value").ShouldBeTrue();
            AllocatedBytesPerCall(() => instance.Contains<string>(items, "value"))
                .ShouldBeLessThanOrEqualTo(248);
        }

        [Fact]
        public void GivenGenericSpanCompatibleSetup_WhenCalled_ShouldKeepAdapterCopiesWithinBudget()
        {
            var sut = new ISpanAdapterAllocationSutImposter();
            sut.Contains<object>(ReadOnlySpanArg<int>.Any(), Arg<object>.Any()).Returns(true);
            var instance = sut.Instance();
            var items = new[] { 1, 2, 3, 4 };
            instance.Contains<string>(items, "value").ShouldBeTrue();
            AllocatedBytesPerCall(() => instance.Contains<string>(items, "value"))
                .ShouldBeLessThanOrEqualTo(208);
        }

        private const int Calls = 100;

        private readonly ICallAllocationSutImposter _sut = new ICallAllocationSutImposter();

        [Theory]
        [InlineData(100)]
        [InlineData(4096)]
        public void GivenEvent_WhenSubscribingAndUnsubscribing_ShouldStayWithinOriginalHistoryBudget(
            int measuredCalls
        )
        {
            var sut = new IEventAllocationSutImposter();
            var instance = sut.Instance();
            EventHandler handler = (_, __) => { };
            for (var i = 0; i < Calls; i++)
            {
                instance.Changed += handler;
                instance.Changed -= handler;
            }

            long subscribedBytes = 0;
            long unsubscribedBytes = 0;
            for (var i = 0; i < measuredCalls; i++)
            {
                var before = GC.GetAllocatedBytesForCurrentThread();
                instance.Changed += handler;
                subscribedBytes += GC.GetAllocatedBytesForCurrentThread() - before;
                before = GC.GetAllocatedBytesForCurrentThread();
                instance.Changed -= handler;
                unsubscribedBytes += GC.GetAllocatedBytesForCurrentThread() - before;
            }

            // Baseline history nodes used 32 B per accessor; subscribing one handler also publishes
            // a 32 B handler state and 32 B invocation-list array. Include all segment/directory growth.
            (subscribedBytes / measuredCalls).ShouldBeLessThanOrEqualTo(96);
            (unsubscribedBytes / measuredCalls).ShouldBeLessThanOrEqualTo(32);
        }

        [Fact]
        public void GivenProperty_WhenConstructed_ShouldNotAllocateUnusedCollections()
        {
            // No later verification/diagnostic state is included in the history foundation.
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

        // Compact segmented setter history retains values and sequence/coverage; failure columns stay lazy.
        [Fact]
        public void GivenPropertySetterWithoutCallbacks_WhenCalled_ShouldOnlyAllocateItsHistory()
        {
            var instance = _sut.Instance();

            AllocatedBytesPerCall(() => instance.Value = 1).ShouldBeLessThanOrEqualTo(32);
        }

        // One 56-byte entry keeps the argument, result, exception, sequence/coverage word and next link.
        [Fact]
        public void GivenMethodWithoutCallbacks_WhenCalled_ShouldOnlyAllocateItsArgumentsAndHistory()
        {
            _sut.Square(Arg<int>.Any()).Returns(4);
            var instance = _sut.Instance();

            AllocatedBytesPerCall(() => instance.Square(2)).ShouldBeLessThanOrEqualTo(56);
        }

        // One 48-byte entry keeps the argument, exception, sequence/coverage word and next link.
        [Fact]
        public void GivenVoidMethodWithoutCallbacks_WhenCalled_ShouldOnlyAllocateItsArgumentsAndHistory()
        {
            _sut.Log(Arg<string>.Any());
            var instance = _sut.Instance();

            AllocatedBytesPerCall(() => instance.Log("value")).ShouldBeLessThanOrEqualTo(48);
        }

        // The 56-byte entry keeps the argument, result, exception, sequence/coverage word and next link.
        // Setups with the call's own type arguments need no adapter or converted arguments.
        // Checking candidate setups must not allocate enumerators, even when the newest setup does not match.
        [Fact]
        public void GivenGenericMethodWithoutCallbacks_WhenCalled_ShouldOnlyAllocateItsArgumentsAndHistory()
        {
            _sut.Echo<string>("match").Returns("result");
            _sut.Echo<string>("miss").Returns("other");
            var instance = _sut.Instance();

            AllocatedBytesPerCall(() => instance.Echo("match")).ShouldBeLessThanOrEqualTo(56);
        }

        // Safe matchers keep their arrays private: lookup and invocation matching reuse one
        // snapshot, while history retains a separate entry copy. Arbitrary predicates keep isolation.
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void GivenGenericMethodWithSpan_WhenCalledWithItsOwnTypeArgument_ShouldNotCopyTheSpanForAnAdapter(
            int matcherKind
        )
        {
            var sut = new ISpanParameterSutImposter();
            var matcher = matcherKind switch
            {
                0 => ReadOnlySpanArg<int>.Any(),
                _ => ReadOnlySpanArg<int>.Is(1, 2, 3, 4),
            };
            sut.Contains<int>(matcher, Arg<int>.Any()).Returns(true);
            var instance = sut.Instance();
            var items = new[] { 1, 2, 3, 4 };

            AllocatedBytesPerCall(() => instance.Contains<int>(items, 2))
                .ShouldBeLessThanOrEqualTo(144);
        }

        [Fact]
        public void GivenGenericSpanPredicate_WhenCalled_ShouldKeepSeparateCapturedArraysWithinBudget()
        {
            var sut = new ISpanParameterSutImposter();
            sut.Contains<int>(ReadOnlySpanArg<int>.Is(values => values.Length == 4), Arg<int>.Any())
                .Returns(true);
            var instance = sut.Instance();
            var items = new[] { 1, 2, 3, 4 };

            AllocatedBytesPerCall(() => instance.Contains<int>(items, 2))
                .ShouldBeLessThanOrEqualTo(176);
        }

        // One node supplies captured keys for matching and retains result, exception and sequence.
        [Fact]
        public void GivenIndexerGetterWithoutCallbacks_WhenCalled_ShouldOnlyAllocateItsArgumentsAndHistory()
        {
            _sut[Arg<int>.Any()].Getter().Returns(5);
            var instance = _sut.Instance();

            AllocatedBytesPerCall(() => _ = instance[1]).ShouldBeLessThanOrEqualTo(56);
        }

        // One node supplies captured keys for matching and retains assigned value, exception and sequence.
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
            AllocatedBytesPerCall(() => _ = instance.GetAsync(1)).ShouldBeLessThanOrEqualTo(48);
            AllocatedBytesPerCall(() => _ = instance.PeekAsync(1)).ShouldBeLessThanOrEqualTo(64);
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
            // A Task result and a synchronous exception share one slot, leaving room for the
            // final out value and entry stamp within the original record plus eight bytes.
            AllocatedBytesPerCall(() => _ = instance.GetByReferenceAsync(ref key, out _, in extra))
                .ShouldBeLessThanOrEqualTo(56);
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
            var count = Count.AtLeastOnce();
            var withOneCall = AllocatedBytesPerCall(() => verification.Called(count));
            withOneCall.ShouldBe(0);
            for (var i = 0; i < 1000; i++)
            {
                instance.Echo(i);
            }

            AllocatedBytesPerCall(() => verification.Called(count)).ShouldBe(withOneCall);
        }

        [Fact]
        public void GivenRecordedMemberInteractions_WhenVerifyingCachedHandles_ShouldNotAllocate()
        {
            var instance = _sut.Instance();
            instance.Square(1);
            instance.Value = 1;
            _ = instance.Value;
            instance[1] = 1;
            _ = instance[1];
            var method = _sut.Square(Arg<int>.Any());
            var propertyGetter = _sut.Value.Getter();
            var propertySetter = _sut.Value.Setter(Arg<int>.Any());
            var indexerGetter = _sut[Arg<int>.Any()].Getter();
            var indexerSetter = _sut[Arg<int>.Any()].Setter();
            var count = Count.Once();

            // Warmed count checks allocate no lists, closures, or per-record objects.
            AllocatedBytesPerCall(() =>
                {
                    method.Called(count);
                    propertyGetter.Called(count);
                    propertySetter.Called(count);
                    indexerGetter.Called(count);
                    indexerSetter.Called(count);
                })
                .ShouldBe(0);
        }

        [Fact]
        public void GivenEventSubscriptions_WhenVerifyingCachedCriteria_ShouldNotAllocate()
        {
            var sut = new IEventAllocationSutImposter();
            EventHandler handler = (_, __) => { };
            sut.Instance().Changed += handler;
            sut.Instance().Changed -= handler;
            var criteria = Arg<EventHandler>.Any();
            var count = Count.Once();

            AllocatedBytesPerCall(() =>
                {
                    sut.Changed.Subscribed(criteria, count);
                    sut.Changed.Unsubscribed(criteria, count);
                })
                .ShouldBe(0);
        }

        // The 56-byte record is the only call allocation; invoking callbacks adds no objects.
        [Fact]
        public void GivenMethodWithCallback_WhenCalled_ShouldOnlyAllocateItsArgumentsAndHistory()
        {
            _sut.Square(Arg<int>.Any()).Returns(4).Callback(_ => { });
            var instance = _sut.Instance();

            AllocatedBytesPerCall(() => instance.Square(2)).ShouldBeLessThanOrEqualTo(56);
        }

        // A setup keeps its builder, group, step and Returns closure, and the steps' sequence. A queue for the steps
        // and one for each step's callbacks took 832 bytes each.
        [Fact]
        public void GivenMethod_WhenSetUp_ShouldNotAllocateQueues()
        {
            AllocatedBytesPerSetup(sut => sut.Square(Arg<int>.Any()).Returns(4))
                .ShouldBeLessThanOrEqualTo(400);
        }

        // Then() adds only its outcome step, sequence node and Returns closure.
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
