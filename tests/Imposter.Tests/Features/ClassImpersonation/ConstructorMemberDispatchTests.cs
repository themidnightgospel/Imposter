using System;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.ClassImpersonation
{
    public class ConstructorMemberDispatchTests
    {
        [Theory]
        [InlineData(ImposterMode.Implicit)]
        [InlineData(ImposterMode.Explicit)]
        public void Given_ConstructorCallingConcreteMembers_When_CreatingImposter_Should_UseBaseWithoutRecording(
            ImposterMode mode
        )
        {
            var imposter = new ConstructorVirtualMembersTargetImposter(mode);
            var instance = imposter.Instance();

            instance.PropertyResult.ShouldBe(7);
            instance.IndexerResult.ShouldBe(10);
            instance.RefResult.ShouldBe(4);
            instance.OutResult.ShouldBe("base");
            instance.MethodResult.ShouldBe("base method");
            instance.AsyncResult.ShouldBe(17);
            instance.TouchCount.ShouldBe(1);
            instance.AddCount.ShouldBe(1);
            instance.RemoveCount.ShouldBe(1);
            imposter.Touch().Called(Count.Never());
            imposter.Value.Getter().Called(Count.Never());
            imposter.Value.Setter(7).Called(Count.Never());
            imposter[2].Getter().Called(Count.Never());
            imposter[2].Setter().Called(Count.Never());
            imposter.Changed.Subscribed(Arg<Action>.Any(), Count.Never());
            imposter.Changed.Unsubscribed(Arg<Action>.Any(), Count.Never());
            imposter.Transform<string>(3, OutArg<string>.Any(), "base").Called(Count.Never());
            imposter.ReadAsync().Called(Count.Never());

            imposter.Value.Getter().Returns(42);
            imposter[2].Getter().Returns(43);
            imposter.Touch().Callback(() => { });

            instance.Value.ShouldBe(42);
            instance[2].ShouldBe(43);
            instance.Touch();
            imposter.Touch().Called(Count.Once());
            instance.TouchCount.ShouldBe(1);
        }

        [Theory]
        [InlineData(ImposterMode.Implicit)]
        [InlineData(ImposterMode.Explicit)]
        public void Given_ConstructorCallingAbstractMembers_When_CreatingImposter_Should_UseDefaultsWithoutRecording(
            ImposterMode mode
        )
        {
            var imposter = new ConstructorAbstractMembersTargetImposter(mode);
            var instance = imposter.Instance();

            instance.PropertyResult.ShouldBeNull();
            instance.IndexerResult.ShouldBeNull();
            instance.RefResult.ShouldBe(3);
            instance.OutResult.ShouldBeNull();
            instance.MethodResult.ShouldBeNull();
            instance.TaskResult.ShouldBeNull();
            imposter.Touch().Called(Count.Never());
            imposter.Value.Getter().Called(Count.Never());
            imposter.Value.Setter("ignored").Called(Count.Never());
            imposter[2].Getter().Called(Count.Never());
            imposter[2].Setter().Called(Count.Never());
            imposter.Changed.Subscribed(Arg<Action>.Any(), Count.Never());
            imposter.Changed.Unsubscribed(Arg<Action>.Any(), Count.Never());
            imposter.Transform<string>(3, OutArg<string>.Any(), "ignored").Called(Count.Never());
            imposter.ReadAsync().Called(Count.Never());

            imposter.Value.Getter().Returns("configured");
            imposter[2].Getter().Returns("indexer");
            imposter.Touch().Callback(() => { });

            instance.Value.ShouldBe("configured");
            instance[2].ShouldBe("indexer");
            instance.Touch();
            imposter.Touch().Called(Count.Once());
        }

        [Fact]
        public void Given_ExplicitMode_When_ConstructionCompletes_Should_StillRequireSetups()
        {
            var imposter = new ConstructorVirtualMembersTargetImposter(ImposterMode.Explicit);

            Should.Throw<MissingImposterException>(() => imposter.Instance().Touch());
            Should.Throw<MissingImposterException>(() => imposter.Instance().Value);
            Should.Throw<MissingImposterException>(() => imposter.Instance()[2]);
        }

        [Fact]
        public void Given_ThrowingConcreteMember_When_CalledFromConstructor_Should_PropagateOriginalException()
        {
            Should
                .Throw<InvalidOperationException>(() =>
                    new ConstructorThrowingMemberTargetImposter()
                )
                .Message.ShouldBe("base constructor dispatch");
        }
    }
}
