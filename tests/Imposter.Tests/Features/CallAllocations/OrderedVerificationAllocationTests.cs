using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.PropertyImpersonation;
using Imposter.Tests.Features.Verification;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.CallAllocations
{
    public class OrderedVerificationAllocationTests
    {
        public enum StepKind
        {
            Method,
            PropertySetter,
            IndexerAccessors,
            EventAccessors,
            GenericMethod,
            SpanSetter,
            RefStructSetter,
        }

        [Theory]
        [InlineData(StepKind.Method, false)]
        [InlineData(StepKind.PropertySetter, false)]
        [InlineData(StepKind.IndexerAccessors, false)]
        [InlineData(StepKind.EventAccessors, false)]
        [InlineData(StepKind.GenericMethod, false)]
        [InlineData(StepKind.SpanSetter, false)]
        [InlineData(StepKind.RefStructSetter, false)]
        [InlineData(StepKind.Method, true)]
        [InlineData(StepKind.PropertySetter, true)]
        [InlineData(StepKind.IndexerAccessors, true)]
        [InlineData(StepKind.EventAccessors, true)]
        [InlineData(StepKind.GenericMethod, true)]
        [InlineData(StepKind.SpanSetter, true)]
        [InlineData(StepKind.RefStructSetter, true)]
        public void GivenConfiguredOrder_WhenVerifyingSuccessfully_ShouldAllocateNoBytes(
            StepKind kind,
            bool firstVerification
        )
        {
            for (var i = 0; i < 100; i++)
                CreateOrder(kind).Verify();
            const int repetitions = 1000;
            var orders = new InvocationOrder[repetitions];
            for (var i = 0; i < repetitions; i++)
            {
                orders[i] = CreateOrder(kind);
                if (!firstVerification)
                    orders[i].Verify();
            }

            var before = GC.GetAllocatedBytesForCurrentThread();
            for (var i = 0; i < repetitions; i++)
                orders[i].Verify();
            var allocated = GC.GetAllocatedBytesForCurrentThread() - before;
            allocated.ShouldBe(0);
        }

        private static InvocationOrder CreateOrder(StepKind kind)
        {
            if (kind == StepKind.Method)
            {
                var sut = new IVerificationServiceImposter();
                var step = sut.Save(1);
                sut.Instance().Save(1);
                sut.Instance().Save(1);
                return Imposters.InOrder().Then(step).Then(step);
            }
            if (kind == StepKind.GenericMethod)
            {
                var sut = new IVerificationServiceImposter();
                var step = sut.Echo<int>(1);
                sut.Instance().Echo(1);
                sut.Instance().Echo(1);
                return Imposters.InOrder().Then(step).Then(step);
            }
            if (kind == StepKind.SpanSetter)
            {
                var sut = new ISpanPropertySutImposter();
                var step = sut.Buffer.Setter(SpanArg<byte>.Is(1, 2));
                sut.Instance().Buffer = new byte[] { 1, 2 };
                sut.Instance().Buffer = new byte[] { 1, 2 };
                return Imposters.InOrder().Then(step).Then(step);
            }
            if (kind == StepKind.RefStructSetter)
            {
                var sut = new IRefStructPropertySutImposter();
                var step = sut.Current.Setter();
                sut.Instance().Current = new Bookmark(1);
                sut.Instance().Current = new Bookmark(2);
                return Imposters.InOrder().Then(step).Then(step);
            }
            var accessor = new IAccessorVerificationTargetImposter();
            if (kind == StepKind.PropertySetter)
            {
                var step = accessor.Value.Setter(1);
                accessor.Instance().Value = 1;
                accessor.Instance().Value = 1;
                return Imposters.InOrder().Then(step).Then(step);
            }
            if (kind == StepKind.IndexerAccessors)
            {
                var setter = accessor[1].Setter();
                var getter = accessor[1].Getter();
                accessor.Instance()[1] = 2;
                _ = accessor.Instance()[1];
                accessor.Instance()[1] = 3;
                _ = accessor.Instance()[1];
                return Imposters.InOrder().Then(setter).Then(getter).Then(setter).Then(getter);
            }
            Action<int> handler = _ => { };
            var subscribed = accessor.Changed.Subscribed(handler);
            var unsubscribed = accessor.Changed.Unsubscribed(handler);
            accessor.Instance().Changed += handler;
            accessor.Instance().Changed += handler;
            accessor.Instance().Changed -= handler;
            return Imposters.InOrder().Then(subscribed).Then(subscribed).Then(unsubscribed);
        }
    }
}
