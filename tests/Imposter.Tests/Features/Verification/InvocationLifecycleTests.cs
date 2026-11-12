using System;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

[assembly: GenerateImposter(typeof(Imposter.Tests.Features.Verification.ILifecycleTarget))]
[assembly: GenerateImposter(typeof(Imposter.Tests.Features.Verification.LifecycleClassTarget))]

namespace Imposter.Tests.Features.Verification
{
    public interface ILifecycleTarget
    {
        int Next();
        T Echo<T>(T value);
        void Modify(ref int value, out int result);
        int Value { get; set; }
        int this[int key] { get; set; }
        event Action Changed;
        void Reset();
        void ClearInvocations();
    }

    public class LifecycleClassTarget
    {
        public LifecycleClassTarget(int initial) => Value = initial;

        public virtual int Value { get; set; }
    }

    public partial class InvocationLifecycleTests
    {
        private readonly ILifecycleTargetImposter _sut = new ILifecycleTargetImposter();

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void GivenCapturedMemberSnapshots_WhenClearedOrReset_ShouldKeepRecordsAndSequenceOrder(
            bool reset
        )
        {
            var instance = _sut.Instance();
            instance.Next();
            instance.Value = 5;
            instance[2] = 7;
            instance.Changed += () => { };
            var snapshot = ((IImposter)_sut).Invocations();
            snapshot.Count.ShouldBe(4);
            var lastSequence = snapshot[3].Sequence;

            if (reset)
                ((IImposter)_sut).Reset();
            else
                ((IImposter)_sut).ClearInvocations();

            ((IImposter)_sut).Invocations().ShouldBeEmpty();
            snapshot.Count.ShouldBe(4);
            snapshot[3].Sequence.ShouldBe(lastSequence);
            instance.Next();
            ((IImposter)_sut)
                .Invocations()
                .ShouldHaveSingleItem()
                .Sequence.ShouldBeGreaterThan(lastSequence);
        }

        [Fact]
        public void GivenRefAndOutSetup_WhenClearedAndReset_ShouldPreserveThenDiscardBehavior()
        {
            var setup = _sut.Modify(Arg<int>.Any(), OutArg<int>.Any());
            setup.Callback(
                (ref int value, out int result) =>
                {
                    value += 1;
                    result = value * 2;
                }
            );
            var instance = _sut.Instance();
            var value = 2;
            instance.Modify(ref value, out var result);
            value.ShouldBe(3);
            result.ShouldBe(6);

            ((IImposter)_sut).ClearInvocations();

            setup.Called(Count.Never());
            instance.Modify(ref value, out result);
            value.ShouldBe(4);
            result.ShouldBe(8);
            setup.Called(Count.Once());

            ((IImposter)_sut).Reset();

            instance.Modify(ref value, out result);
            value.ShouldBe(4);
            result.ShouldBe(0);
            setup.Called(Count.Never());
            _sut.Modify(Arg<int>.Any(), OutArg<int>.Any()).Called(Count.Once());
        }

        [Fact]
        public void GivenSequentialSetup_WhenHistoryCleared_ShouldKeepPositionAndExistingBuilder()
        {
            var setup = _sut.Next();
            setup.Returns(1).Then().Returns(2);
            var instance = _sut.Instance();
            instance.Next().ShouldBe(1);

            ((IImposter)_sut).ClearInvocations();

            setup.Called(Count.Never());
            instance.Next().ShouldBe(2);
            setup.Called(Count.Once());
        }

        [Fact]
        public void GivenStoredValuesAndHistory_WhenCleared_ShouldKeepValuesAndDropAccessorHistory()
        {
            var instance = _sut.Instance();
            instance.Value = 4;
            instance[7] = 8;
            _ = instance.Value;
            _ = instance[7];

            ((IImposter)_sut).ClearInvocations();

            _sut.Value.Getter().Called(Count.Never());
            _sut.Value.Setter(Arg<int>.Any()).Called(Count.Never());
            _sut[7].Getter().Called(Count.Never());
            _sut[7].Setter().Called(Count.Never());
            instance.Value.ShouldBe(4);
            instance[7].ShouldBe(8);
        }

        [Fact]
        public void GivenSubscriptions_WhenHistoryCleared_ShouldKeepHandlersAndClearEventHistory()
        {
            var calls = 0;
            Action handler = () => calls++;
            Action removed = () => calls += 100;
            _sut.Instance().Changed += handler;
            _sut.Instance().Changed += removed;
            _sut.Instance().Changed -= removed;
            _sut.Changed.Raise();
            calls.ShouldBe(1);
            _sut.Changed.Unsubscribed(removed, Count.Once());
            _sut.Changed.HandlerInvoked(handler, Count.Once());

            ((IImposter)_sut).ClearInvocations();

            _sut.Changed.Subscribed(Arg<Action>.Any(), Count.Never());
            _sut.Changed.Unsubscribed(Arg<Action>.Any(), Count.Never());
            _sut.Changed.Raised(Count.Never());
            _sut.Changed.HandlerInvoked(Arg<Action>.Any(), Count.Never());
            _sut.Changed.Raise();
            calls.ShouldBe(2);
            _sut.Changed.HandlerInvoked(handler, Count.Once());
        }

        [Fact]
        public void GivenConfiguredImposter_WhenReset_ShouldPreserveInstanceAndDetachOldBuilders()
        {
            var instance = _sut.Instance();
            var oldMethod = _sut.Next();
            var oldGetter = _sut.Value.Getter();
            var oldIndexer = _sut[7].Getter();
            oldMethod.Returns(1);
            oldGetter.Returns(2);
            oldIndexer.Returns(3);
            instance.Next().ShouldBe(1);
            var oldSnapshot = oldMethod.Invocations;

            ((IImposter)_sut).Reset();

            _sut.Instance().ShouldBeSameAs(instance);
            oldMethod.Invocations.Count.ShouldBe(0);
            oldSnapshot.Count.ShouldBe(1);
            oldMethod.Called(Count.Never());
            oldMethod.Returns(10);
            oldGetter.Returns(20);
            oldIndexer.Returns(30);
            instance.Next().ShouldBe(0);
            instance.Value.ShouldBe(0);
            instance[7].ShouldBe(0);
            _sut.Next().Returns(42);
            instance.Next().ShouldBe(42);
        }

        [Fact]
        public void GivenStoredValuesAndSubscribers_WhenReset_ShouldClearOwnedState()
        {
            var instance = _sut.Instance();
            instance.Value = 1;
            instance[7] = 2;
            var calls = 0;
            instance.Changed += () => calls++;

            ((IImposter)_sut).Reset();

            _sut.Value.Setter(Arg<int>.Any()).Called(Count.Never());
            _sut[7].Setter().Called(Count.Never());
            _sut.Changed.Subscribed(Arg<Action>.Any(), Count.Never());
            instance.Value.ShouldBe(0);
            instance[7].ShouldBe(0);
            _sut.Changed.Raise();
            calls.ShouldBe(0);
        }

        [Fact]
        public void GivenExplicitMode_WhenReset_ShouldKeepModeAndForgetSetups()
        {
            var sut = new ILifecycleTargetImposter(ImposterMode.Explicit);
            sut.Next().Returns(1);
            var instance = sut.Instance();
            instance.Next().ShouldBe(1);
            ((IImposter)sut).ClearInvocations();
            instance.Next().ShouldBe(1);

            ((IImposter)sut).Reset();

            Should.Throw<MissingImposterException>(() => instance.Next());
            Should.Throw<MissingImposterException>(() => _ = instance.Value);
        }

        [Fact]
        public void GivenGenericMethodHistory_WhenClearedAndReset_ShouldHandleEveryTypeArgument()
        {
            _sut.Echo(Arg<int>.Any()).Returns(3);
            _sut.Echo(Arg<string>.Any()).Returns("configured");
            var instance = _sut.Instance();
            instance.Echo(1).ShouldBe(3);
            instance.Echo("input").ShouldBe("configured");
            ((IImposter)_sut).ClearInvocations();
            _sut.Echo(Arg<int>.Any()).Called(Count.Never());
            _sut.Echo(Arg<string>.Any()).Called(Count.Never());
            instance.Echo(1).ShouldBe(3);
            ((IImposter)_sut).Reset();
            instance.Echo(1).ShouldBe(0);
            instance.Echo("input").ShouldBeNull();
        }

        [Fact]
        public void GivenTargetLifecycleNames_WhenCalled_ShouldKeepTargetMembersReachable()
        {
            _sut.Instance().Reset();
            _sut.Instance().ClearInvocations();
            _sut.Reset().Called(Count.Once());
            _sut.ClearInvocations().Called(Count.Once());
            ((IImposter)_sut).ClearInvocations();
            _sut.Reset().Called(Count.Never());
            _sut.ClearInvocations().Called(Count.Never());
        }

        [Fact]
        public void GivenClassBaseState_WhenReset_ShouldReseedFromCurrentBaseWithoutReplacingInstance()
        {
            var sut = new LifecycleClassTargetImposter(11);
            var instance = sut.Instance();
            instance.Value.ShouldBe(11);
            sut.Value.Setter(Arg<int>.Any()).UseBaseImplementation();
            instance.Value = 23;
            sut.Value.Getter().Returns(99);
            instance.Value.ShouldBe(99);

            sut.Reset();

            sut.Instance().ShouldBeSameAs(instance);
            instance.Value.ShouldBe(23);
        }

        [Fact]
        public void GivenConcurrentMethodCalls_WhenRepeatedlyClearingInStressSmoke_ShouldPreserveSetupAndReusableHistory()
        {
            _sut.Next().Returns(7);
            var instance = _sut.Instance();
            Parallel.For(
                0,
                500,
                iteration =>
                {
                    if (iteration % 3 == 0)
                        ((IImposter)_sut).ClearInvocations();
                    else
                        instance.Next().ShouldBe(7);
                }
            );

            ((IImposter)_sut).ClearInvocations();
            _sut.Next().Called(Count.Never());
            instance.Next().ShouldBe(7);
            _sut.Next().Called(Count.Once());
        }
    }
}
