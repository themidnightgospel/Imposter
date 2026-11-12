using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.PropertyImpersonation;
using Shouldly;
using Xunit;

[assembly: GenerateImposter(typeof(Imposter.Tests.Features.Verification.ISetterHistoryTarget))]

namespace Imposter.Tests.Features.Verification
{
    public interface ISetterHistoryTarget
    {
        int Value { get; set; }
    }

    public class PropertySetterInvocationTests
    {
        private readonly ISetterHistoryTargetImposter _sut = new ISetterHistoryTargetImposter();

        [Fact]
        public void GivenDirectBaseInit_WhenBaseThrows_ShouldRecordExactlyOneCompleteInvocation()
        {
            var sut = new VirtualInitOnlyPropertySutImposter();
            sut.Throwing.Setter(Arg<int>.Any()).UseBaseImplementation();
            var failure = Should.Throw<InvalidOperationException>(() =>
                InitOnlyAccessor.Invoke(sut.Instance(), "Throwing", 42)
            );

            var records = sut.Throwing.Setter(42).Invocations;
            records.Count.ShouldBe(1);
            records[0].Value.ShouldBe(42);
            records[0].Exception.ShouldBeSameAs(failure);
            sut.Throwing.Setter(42).Called(Count.Once());
        }

        [Fact]
        public void GivenAssignments_WhenReadingTypedHistory_ShouldExposeValuesInEntryOrder()
        {
            _sut.Instance().Value = 1;
            _sut.Instance().Value = 2;

            var records = _sut.Value.Setter(Arg<int>.Any()).Invocations;

            records.Count.ShouldBe(2);
            records[0].Value.ShouldBe(1);
            records[1].Value.ShouldBe(2);
            records[0].Member.ShouldBe("Value");
            records[0].Sequence.ShouldBeLessThan(records[1].Sequence);
            records[0].Exception.ShouldBeNull();
            _sut.Value.Setter(2).Invocations.Count.ShouldBe(1);
        }

        [Fact]
        public void GivenThrowingCallback_WhenAssigned_ShouldRecordException()
        {
            var failure = new InvalidOperationException("setter callback failed");
            _sut.Value.Setter(3).Callback(_ => throw failure);

            Should
                .Throw<InvalidOperationException>(() => _sut.Instance().Value = 3)
                .ShouldBeSameAs(failure);

            var invocation = _sut.Value.Setter(3).Invocations[0];
            invocation.Exception.ShouldBeSameAs(failure);
            invocation.Value.ShouldBe(3);
            _sut.Value.Setter(3).Called(Count.Once());
        }

        [Fact]
        public void GivenExplicitModeWithoutSetup_WhenAssigned_ShouldRecordMissingSetupFailure()
        {
            var sut = new ISetterHistoryTargetImposter(ImposterMode.Explicit);
            var failure = Should.Throw<MissingImposterException>(() => sut.Instance().Value = 5);

            sut.Value.Setter(5).Invocations[0].Exception.ShouldBeSameAs(failure);
        }
    }
}
