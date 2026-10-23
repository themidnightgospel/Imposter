using System;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Imposter.Tests.Features.MethodImpersonation;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.CallAllocations
{
    public class SetupPublicationTests
    {
        [Theory]
        [InlineData(ImposterMode.Implicit)]
        [InlineData(ImposterMode.Explicit)]
        public void Given_PendingSetups_When_CallingBeforeAndAfterConfiguration_Should_UseConfiguredOutcomes(
            ImposterMode mode
        )
        {
            var imposter = new ICallAllocationSutImposter(mode);
            var instance = imposter.Instance();
            imposter.Square(Arg<int>.Any()).Returns(10);
            imposter.Echo<int>(Arg<int>.Any()).Returns(10);
            imposter[Arg<int>.Any()].Getter().Returns(10);

            var method = imposter.Square(Arg<int>.Is(5));
            var generic = imposter.Echo<int>(Arg<int>.Is(5));
            var indexer = imposter[Arg<int>.Is(5)].Getter();
            indexer.Callback(_ => { });
            for (var i = 0; i < 3; i++)
            {
                instance.Square(5).ShouldBe(10);
                instance.Echo(5).ShouldBe(10);
                instance[5].ShouldBe(10);
            }

            method.Returns(20);
            generic.Returns(20);
            indexer.Returns(20);
            for (var i = 0; i < 3; i++)
            {
                instance.Square(5).ShouldBe(20);
                instance.Echo(5).ShouldBe(20);
                instance[5].ShouldBe(20);
            }
        }

        [Theory]
        [InlineData(ImposterMode.Implicit)]
        [InlineData(ImposterMode.Explicit)]
        public void Given_ConfiguredSetups_When_VerifyingNarrowerCriteria_Should_PreserveOutcomes(
            ImposterMode mode
        )
        {
            var imposter = new ICallAllocationSutImposter(mode);
            var instance = imposter.Instance();
            imposter.Square(Arg<int>.Any()).Returns(10);
            imposter.Echo<int>(Arg<int>.Any()).Returns(10);
            imposter[Arg<int>.Any()].Getter().Returns(10);
            instance.Square(5).ShouldBe(10);
            instance.Echo(5).ShouldBe(10);
            instance[5].ShouldBe(10);

            imposter.Square(Arg<int>.Is(5)).Called(Count.Once());
            imposter.Echo<int>(Arg<int>.Is(5)).Called(Count.Once());
            imposter[Arg<int>.Is(5)].Getter().Called(Count.Once());
            imposter.Square(Arg<int>.Is(5)).CallCount().ShouldBe(1);
            imposter.Echo<int>(Arg<int>.Is(5)).CallCount().ShouldBe(1);

            instance.Square(5).ShouldBe(10);
            instance.Echo(5).ShouldBe(10);
            instance[5].ShouldBe(10);
        }

        [Theory]
        [InlineData(ImposterMode.Implicit)]
        [InlineData(ImposterMode.Explicit)]
        public void Given_VoidCallback_When_AddingBareSetupAndVerifying_Should_KeepCallback(
            ImposterMode mode
        )
        {
            var imposter = new ICallAllocationSutImposter(mode);
            var calls = 0;
            imposter.Log(Arg<string>.Any()).Callback(_ => calls++);
            imposter.Log(Arg<string>.Is("health"));
            imposter.Instance().Log("health");
            calls.ShouldBe(1);
            imposter.Log(Arg<string>.Is("health")).Called(Count.Once());
            imposter.Log(Arg<string>.Is("health")).CallCount().ShouldBe(1);
            imposter.Instance().Log("health");
            calls.ShouldBe(2);
        }

        [Theory]
        [InlineData(ImposterMode.Implicit)]
        [InlineData(ImposterMode.Explicit)]
        public void Given_VoidThrows_When_AddingBareSetup_Should_KeepThrowUntilExplicitNoOpConfigured(
            ImposterMode mode
        )
        {
            var imposter = new ICallAllocationSutImposter(mode);
            imposter.Log(Arg<string>.Any()).Throws<InvalidOperationException>();
            var pending = imposter.Log(Arg<string>.Is("health"));
            Should.Throw<InvalidOperationException>(() => imposter.Instance().Log("health"));
            pending.Callback(_ => { });
            Should.NotThrow(() => imposter.Instance().Log("health"));
            Should.Throw<InvalidOperationException>(() => imposter.Instance().Log("other"));
        }

        [Theory]
        [InlineData(ImposterMode.Implicit)]
        [InlineData(ImposterMode.Explicit)]
        public async Task Given_ParameterlessAndAsyncSetups_When_AddingPendingSetups_Should_PreserveOutcomes(
            ImposterMode mode
        )
        {
            var imposter = new IMethodSetupFeatureSutImposter(mode);
            var instance = imposter.Instance();
            imposter.IntNoParams().Returns(10).Then().Returns(20);
            imposter.GenericReturnType<int>().Returns(30);
            imposter.AsyncTaskIntNoParams().ReturnsAsync(40);
            imposter.AsyncValueTaskIntNoParams().ThrowsAsync(new InvalidOperationException());
            imposter.VoidNoParams().Throws<InvalidOperationException>();

            var pending = imposter.IntNoParams();
            imposter.GenericReturnType<int>().Called(Count.Never());
            imposter.AsyncTaskIntNoParams().CallCount().ShouldBe(0);
            imposter.AsyncValueTaskIntNoParams().Called(Count.Never());
            imposter.VoidNoParams().Called(Count.Never());

            instance.IntNoParams().ShouldBe(10);
            instance.IntNoParams().ShouldBe(20);
            instance.IntNoParams().ShouldBe(20);
            instance.GenericReturnType<int>().ShouldBe(30);
            (await instance.AsyncTaskIntNoParams()).ShouldBe(40);
            await Should.ThrowAsync<InvalidOperationException>(async () =>
                await instance.AsyncValueTaskIntNoParams()
            );
            Should.Throw<InvalidOperationException>(() => instance.VoidNoParams());
            pending.Returns(50);
            instance.IntNoParams().ShouldBe(50);
        }

        [Theory]
        [InlineData(ImposterMode.Implicit)]
        [InlineData(ImposterMode.Explicit)]
        public void Given_GenericVoidSetup_When_VerifyingOrAddingBareSetups_Should_PreserveConfiguredOutcome(
            ImposterMode mode
        )
        {
            var imposter = new IMethodSetupFeatureSutImposter(mode);
            imposter.GenericSingleParam<int>(Arg<int>.Any()).Throws<InvalidOperationException>();
            imposter.GenericSingleParam<int>(Arg<int>.Is(5)).Called(Count.Never());
            var pending = imposter.GenericSingleParam<int>(Arg<int>.Is(5));
            Should.Throw<InvalidOperationException>(() =>
                imposter.Instance().GenericSingleParam(5)
            );
            pending.Callback(_ => { });
            Should.NotThrow(() => imposter.Instance().GenericSingleParam(5));
        }

        [Theory]
        [InlineData(ImposterMode.Implicit)]
        [InlineData(ImposterMode.Explicit)]
        public void Given_BaseImplementation_When_Verifying_Should_PreserveItUntilNewOutcomeConfigured(
            ImposterMode mode
        )
        {
            var imposter = new MethodSetupFeatureClassSutImposter(mode);
            imposter.IntSingleParam(Arg<int>.Any()).UseBaseImplementation();
            imposter.IntSingleParam(Arg<int>.Is(5)).Called(Count.Never());
            imposter.Instance().IntSingleParam(5).ShouldBe(10);
            var pending = imposter.IntSingleParam(Arg<int>.Is(5));
            imposter.Instance().IntSingleParam(5).ShouldBe(10);
            pending.Throws<InvalidOperationException>();
            Should.Throw<InvalidOperationException>(() => imposter.Instance().IntSingleParam(5));
        }

        [Theory]
        [InlineData(ImposterMode.Implicit)]
        [InlineData(ImposterMode.Explicit)]
        public void Given_PropertyAndIndexerSetters_When_CreatingAndVerifyingBuilders_Should_PreserveBehavior(
            ImposterMode mode
        )
        {
            var imposter = new ICallAllocationSutImposter(mode);
            var propertySets = 0;
            var indexerSets = 0;
            imposter.Value.Getter().Returns(10);
            imposter.Value.Setter(Arg<int>.Any()).Callback(_ => propertySets++);
            imposter[Arg<int>.Any()].Setter().Callback((_, _) => indexerSets++);
            imposter.Value.Getter().Called(Count.Never());
            imposter.Value.Setter(Arg<int>.Is(5)).Called(Count.Never());
            imposter[Arg<int>.Is(5)].Setter().Called(Count.Never());
            imposter.Instance().Value.ShouldBe(10);
            imposter.Instance().Value = 5;
            imposter.Instance()[5] = 5;
            propertySets.ShouldBe(1);
            indexerSets.ShouldBe(1);
        }

        [Fact]
        public void Given_ExplicitMode_When_OnlyBareVoidSetupMatches_Should_ThrowMissingImposterException()
        {
            var imposter = new ICallAllocationSutImposter(ImposterMode.Explicit);
            imposter.Log(Arg<string>.Is("health"));
            imposter.Log(Arg<string>.Is("other")).Throws<InvalidOperationException>();
            Should.Throw<MissingImposterException>(() => imposter.Instance().Log("health"));
        }
    }
}
