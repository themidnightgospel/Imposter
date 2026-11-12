using System.Collections.Generic;
using System.Reflection;
using Imposter.Abstractions;
using Imposter.Tests.Features.CallAllocations;
using Shouldly;
using Xunit;

[assembly: GenerateImposter(
    typeof(Imposter.Tests.Features.Verification.IInvocationHistoryCollisionTarget)
)]

namespace Imposter.Tests.Features.Verification
{
    public interface IInvocationHistoryCollisionTarget
    {
        int Invocations();
        int Collide(
            int result,
            int exception,
            int member,
            int sequence,
            int next,
            int collideInvocation
        );
    }

    public class InvocationHistoryAcceptanceTests
    {
        [Fact]
        public void GivenCompatibleSpanPredicate_WhenItRetainsAndMutatesArrays_ShouldKeepMatchingAndHistoryIsolated()
        {
            var observed = new List<int[]>();
            var sut = new ISpanAdapterAllocationSutImposter();
            sut.Contains<object>(
                    ReadOnlySpanArg<int>.Is(items =>
                    {
                        observed.Add(items);
                        items[0] += 10;
                        return true;
                    }),
                    Arg<object>.Any()
                )
                .Returns(true);
            var values = new[] { 1, 2, 3, 4 };
            sut.Instance().Contains<string>(values, "value").ShouldBeTrue();
            observed.Count.ShouldBeGreaterThanOrEqualTo(2);
            observed[0].ShouldNotBeSameAs(observed[1]);
            var record = sut.Contains<string>(ReadOnlySpanArg<int>.Any(), Arg<string>.Any())
                .Invocations.ShouldHaveSingleItem();
            foreach (var array in observed)
                array[0] = 99;
            values[0].ShouldBe(1);
            record.Items.Span[0].ShouldBe(1);
            record.Item.ShouldBe("value");
        }

        [Fact]
        public void GivenTargetInvocationsMember_WhenCallingAndInspecting_ShouldPreserveSetupAndAggregateHistory()
        {
            var sut = new IInvocationHistoryCollisionTargetImposter();
            var setup = sut.Invocations();
            setup.Returns(42);
            sut.Instance().Invocations().ShouldBe(42);
            setup.Invocations.ShouldHaveSingleItem().Result.ShouldBe(42);
            ((IImposter)sut)
                .Invocations()
                .ShouldHaveSingleItem()
                .ShouldBeSameAs(setup.Invocations[0]);
        }

        [Fact]
        public void GivenParametersNamedAfterRecordMembers_WhenRecorded_ShouldSuffixCapturedProperties()
        {
            var sut = new IInvocationHistoryCollisionTargetImposter();
            var setup = sut.Collide(1, 2, 3, 4, 5, 6);
            setup.Returns(42);
            sut.Instance().Collide(1, 2, 3, 4, 5, 6).ShouldBe(42);
            var record = setup.Invocations.ShouldHaveSingleItem();
            record.Result_1.ShouldBe(1);
            record.Exception_1.ShouldBe(2);
            record.Member_1.ShouldBe(3);
            record.Sequence_1.ShouldBe(4);
            record.Next_1.ShouldBe(5);
            record.CollideInvocation_1.ShouldBe(6);
            record.Result.ShouldBe(42);
            record.Exception.ShouldBeNull();
            record.Member.ShouldBe("Collide");
            record.Sequence.ShouldBeGreaterThan(0);
        }

        [Fact]
        public void GivenTypedRecord_WhenInspectingPublicApi_ShouldHideMutableCoverage()
        {
            var sut = new IInvocationHistoryCollisionTargetImposter();
            sut.Instance().Invocations();
            var record = sut.Invocations().Invocations.ShouldHaveSingleItem();
            const BindingFlags publicInstance = BindingFlags.Public | BindingFlags.Instance;
            record.GetType().GetProperty("IsVerified", publicInstance).ShouldBeNull();
            record.GetType().GetMethod("MarkVerified", publicInstance).ShouldBeNull();
            var coverage = (IVerifiableInvocation)record;
            coverage.IsVerified.ShouldBeFalse();
            coverage.MarkVerified();
            coverage.IsVerified.ShouldBeTrue();
        }
    }
}
