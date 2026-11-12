using System;
using System.Reflection;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

[assembly: GenerateImposter(
    typeof(Imposter.Tests.Features.Verification.INullableAwaitableHistoryTarget)
)]

namespace Imposter.Tests.Features.Verification
{
    public interface INullableAwaitableHistoryTarget
    {
        Task? Plain();
        Task<int>? Generic();
        Task<int> Required();
        ValueTask Value();
        ValueTask<int> GenericValue();
        int this[int key] { get; set; }
    }

    public class NullableAwaitableHistoryTests
    {
        [Fact]
        public void GivenNullableTasks_WhenReturningNull_ShouldKeepNullWithoutSynchronousFailure()
        {
            var sut = new INullableAwaitableHistoryTargetImposter();
            Task? nullTask = null;
            Task<int>? nullGenericTask = null;
            sut.Plain().Returns(nullTask);
            sut.Generic().Returns(nullGenericTask);
            sut.Instance().Plain().ShouldBeNull();
            sut.Instance().Generic().ShouldBeNull();
            sut.Plain().Invocations[0].Result.ShouldBeNull();
            sut.Generic().Invocations[0].Result.ShouldBeNull();
            sut.Plain().Invocations[0].Exception.ShouldBeNull();
            sut.Generic().Invocations[0].Exception.ShouldBeNull();
        }

        [Fact]
        public void GivenNullableTasks_WhenThrowingSynchronously_ShouldKeepExceptionAndNullResult()
        {
            var sut = new INullableAwaitableHistoryTargetImposter();
            var failure = new InvalidOperationException("synchronous");
            sut.Plain().Throws(failure);
            sut.Generic().Throws(failure);
            Should
                .Throw<InvalidOperationException>(() =>
                {
                    _ = sut.Instance().Plain();
                })
                .ShouldBeSameAs(failure);
            Should
                .Throw<InvalidOperationException>(() =>
                {
                    _ = sut.Instance().Generic();
                })
                .ShouldBeSameAs(failure);
            sut.Plain().Invocations[0].Result.ShouldBeNull();
            sut.Generic().Invocations[0].Result.ShouldBeNull();
            sut.Plain().Invocations[0].Exception.ShouldBeSameAs(failure);
            sut.Generic().Invocations[0].Exception.ShouldBeSameAs(failure);
        }

        [Fact]
        public void GivenValueTasks_WhenReturningDefault_ShouldExposePresentNullableResults()
        {
            var sut = new INullableAwaitableHistoryTargetImposter();
            sut.Value().Returns(default(ValueTask));
            sut.GenericValue().Returns(default(ValueTask<int>));
            sut.Instance().Value().GetAwaiter().GetResult();
            sut.Instance().GenericValue().GetAwaiter().GetResult().ShouldBe(0);
            sut.Value().Invocations[0].Result.HasValue.ShouldBeTrue();
            sut.GenericValue().Invocations[0].Result.HasValue.ShouldBeTrue();
            sut.Value().Invocations[0].Result.GetValueOrDefault().ShouldBe(default(ValueTask));
            sut.GenericValue()
                .Invocations[0]
                .Result.GetValueOrDefault()
                .ShouldBe(default(ValueTask<int>));
        }

        [Fact]
        public void GivenValueTasks_WhenThrowingSynchronously_ShouldExposeAbsentNullableResults()
        {
            var sut = new INullableAwaitableHistoryTargetImposter();
            var failure = new InvalidOperationException("synchronous");
            sut.Value().Throws(failure);
            sut.GenericValue().Throws(failure);
            Should
                .Throw<InvalidOperationException>(() => sut.Instance().Value())
                .ShouldBeSameAs(failure);
            Should
                .Throw<InvalidOperationException>(() => sut.Instance().GenericValue())
                .ShouldBeSameAs(failure);
            sut.Value().Invocations[0].Result.HasValue.ShouldBeFalse();
            sut.GenericValue().Invocations[0].Result.HasValue.ShouldBeFalse();
            sut.Value().Invocations[0].Exception.ShouldBeSameAs(failure);
            sut.GenericValue().Invocations[0].Exception.ShouldBeSameAs(failure);
        }

        [Fact]
        public void GivenNonNullableTaskSignature_WhenReadingHistory_ShouldExposeNullableResultAnnotation()
        {
            var sut = new INullableAwaitableHistoryTargetImposter();
            var task = Task.FromResult(7);
            sut.Required().Returns(task);
            sut.Instance().Required().ShouldBeSameAs(task);
            var invocation = sut.Required().Invocations[0];
            invocation.Result.ShouldBeSameAs(task);
            var resultProperty = invocation.GetType().GetProperty("Result")!;
            new NullabilityInfoContext()
                .Create(resultProperty)
                .ReadState.ShouldBe(NullabilityState.Nullable);
        }

        [Fact]
        public void GivenValueTypeIndexer_WhenReturningDefaultOrThrowing_ShouldDistinguishPresentAndAbsentResults()
        {
            var sut = new INullableAwaitableHistoryTargetImposter();
            var failure = new InvalidOperationException("getter failed");
            sut[1].Getter().Returns(0);
            sut[2].Getter().Throws(failure);
            sut.Instance()[1].ShouldBe(0);
            Should
                .Throw<InvalidOperationException>(() =>
                {
                    _ = sut.Instance()[2];
                })
                .ShouldBeSameAs(failure);
            sut[1].Getter().Invocations[0].Result.HasValue.ShouldBeTrue();
            sut[1].Getter().Invocations[0].Result.GetValueOrDefault().ShouldBe(0);
            sut[2].Getter().Invocations[0].Result.HasValue.ShouldBeFalse();
            sut[2].Getter().Invocations[0].Exception.ShouldBeSameAs(failure);
            sut[2].Getter().Invocations[0].ToString().ShouldContain("threw");
            sut[2].Getter().Invocations[0].ToString().ShouldNotContain(" => ");
            sut[3].Setter().Callback((_, _) => throw failure);
            Should
                .Throw<InvalidOperationException>(() => sut.Instance()[3] = 7)
                .ShouldBeSameAs(failure);
            sut[3].Setter().Invocations[0].ToString().ShouldContain(" = <7> threw ");
        }
    }
}
