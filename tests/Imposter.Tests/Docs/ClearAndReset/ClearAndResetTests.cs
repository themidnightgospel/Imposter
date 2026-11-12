using Imposter.Abstractions;
using Shouldly;
using Xunit;

[assembly: GenerateImposter(typeof(Imposter.Tests.Docs.ClearAndReset.ILifecycleExample))]
[assembly: GenerateImposter(
    typeof(Imposter.Tests.Docs.ClearAndReset.ILifecycleNamedMembersExample)
)]

namespace Imposter.Tests.Docs.ClearAndReset
{
    public interface ILifecycleExample
    {
        int Next();
    }

    public interface ILifecycleNamedMembersExample
    {
        int Reset();
        int ClearInvocations();
    }

    public class ClearAndResetTests
    {
        [Fact]
        public void GivenSequentialSetup_WhenClearingThenResetting_ShouldRetainInstanceAndReplaceBehavior()
        {
#if USE_CSHARP14
            var imposter = ILifecycleExample.Imposter();
#else
            var imposter = new ILifecycleExampleImposter();
#endif
            imposter.Next().Returns(1).Then().Returns(2);
            var instance = imposter.Instance();
            instance.Next().ShouldBe(1);
            var snapshot = imposter.Next().Invocations;

            imposter.ClearInvocations();
            imposter.Next().Called(Count.Never());
            snapshot.Count.ShouldBe(1);
            instance.Next().ShouldBe(2);

            imposter.Reset();
            imposter.Next().Returns(42);
            instance.Next().ShouldBe(42);
            imposter.Instance().ShouldBeSameAs(instance);
        }

        [Fact]
        public void GivenTargetWithLifecycleMemberNames_WhenUsingInterfaceCasts_ShouldApplyLifecycleOperations()
        {
            var imposter = new ILifecycleNamedMembersExampleImposter();
            var instance = imposter.Instance();
            imposter.Reset().Returns(7);
            imposter.ClearInvocations().Returns(11);
            instance.Reset().ShouldBe(7);
            instance.ClearInvocations().ShouldBe(11);

            ((IImposter)imposter).ClearInvocations();
            imposter.Reset().Called(Count.Never());
            imposter.ClearInvocations().Called(Count.Never());
            instance.Reset().ShouldBe(7);
            instance.ClearInvocations().ShouldBe(11);

            ((IImposter)imposter).Reset();
            instance.Reset().ShouldBe(0);
            instance.ClearInvocations().ShouldBe(0);
            imposter.Instance().ShouldBeSameAs(instance);
        }
    }
}
