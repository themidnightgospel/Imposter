using Imposter.Abstractions;
using Imposter.Tests.Features.ClassImpersonation.Suts;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.ClassImpersonation
{
    public class OverriddenMethodClassImposterTests
    {
        private readonly ClassWithOverriddenMethodImposter _sut =
            new ClassWithOverriddenMethodImposter();

        [Fact]
        public void GivenOverriddenMethodWithReturnSetup_WhenInvoked_ShouldReturnConfiguredValue()
        {
            _sut.Get().Returns(42);

            _sut.Instance().Get().ShouldBe(42);
        }

        [Fact]
        public void GivenOverriddenMethodInvokedOnce_WhenVerified_ShouldMatchSingleCall()
        {
            _sut.Instance().Get();

            Should.NotThrow(() => _sut.Get().Called(Count.Once()));
        }

        [Fact]
        public void GivenOverriddenMethodUsingBaseImplementation_WhenInvoked_ShouldRunTheOverride()
        {
            _sut.Get().UseBaseImplementation();

            _sut.Instance().Get().ShouldBe(2);
        }

        [Fact]
        public void GivenUnconfiguredOverriddenMethodInImplicitMode_WhenInvoked_ShouldReturnDefault()
        {
            _sut.Instance().Get().ShouldBe(0);
        }

        [Fact]
        public void GivenUnconfiguredOverriddenMethodInExplicitMode_WhenInvoked_ShouldThrowMissingImposterException()
        {
            var imposter = new ClassWithOverriddenMethodImposter(ImposterMode.Explicit);

            Should.Throw<MissingImposterException>(() => imposter.Instance().Get());
        }

        [Fact]
        public void GivenInheritedOverriddenMethodWithReturnSetup_WhenInvoked_ShouldReturnConfiguredValue()
        {
            var imposter = new ClassInheritingOverriddenMethodImposter();
            imposter.Get().Returns(42);

            imposter.Instance().Get().ShouldBe(42);
        }

        [Fact]
        public void GivenUnconfiguredInheritedOverriddenMethodInExplicitMode_WhenInvoked_ShouldThrowMissingImposterException()
        {
            var imposter = new ClassInheritingOverriddenMethodImposter(ImposterMode.Explicit);

            Should.Throw<MissingImposterException>(() => imposter.Instance().Get());
        }

        [Fact]
        public void GivenToStringOverrideInExplicitMode_WhenInvoked_ShouldRunTheOverride()
        {
            var instance = new ClassWithObjectMemberOverridesImposter(
                ImposterMode.Explicit
            ).Instance();

            instance.ToString().ShouldBe(ClassWithObjectMemberOverrides.Text);
        }

        [Fact]
        public void GivenEqualsOverrideInExplicitMode_WhenComparedWithItself_ShouldRunTheOverride()
        {
            var instance = new ClassWithObjectMemberOverridesImposter(
                ImposterMode.Explicit
            ).Instance();

            instance.Equals(instance).ShouldBeTrue();
        }

        [Fact]
        public void GivenGetHashCodeOverrideInExplicitMode_WhenInvoked_ShouldRunTheOverride()
        {
            var instance = new ClassWithObjectMemberOverridesImposter(
                ImposterMode.Explicit
            ).Instance();

            instance.GetHashCode().ShouldBe(ClassWithObjectMemberOverrides.HashCode);
        }

        [Fact]
        public void GivenAbstractOverrideWithReturnSetup_WhenInvoked_ShouldReturnConfiguredValue()
        {
            var imposter = new ClassWithAbstractOverrideImposter();
            imposter.Get().Returns(42);

            imposter.Instance().Get().ShouldBe(42);
        }
    }
}
