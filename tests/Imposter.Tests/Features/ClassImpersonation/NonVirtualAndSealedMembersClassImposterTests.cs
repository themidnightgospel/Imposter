using System.Reflection;
using Imposter.Abstractions;
using Imposter.Tests.Features.ClassImpersonation.Suts;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.ClassImpersonation
{
    public class NonVirtualAndSealedMembersClassImposterTests
    {
        private static readonly BindingFlags PublicMembers =
            BindingFlags.Instance | BindingFlags.Public;

        [Fact]
        public void GivenNonVirtualMembers_WhenBuildingImposter_ShouldNotAllowConfigurationAndShouldRunBaseBehavior()
        {
            AssertNoBuilderMember(nameof(ClassWithNonVirtualAndSealedMembers.NonVirtualMethod));
            AssertNoBuilderMember(nameof(ClassWithNonVirtualAndSealedMembers.NonVirtualProperty));

            var imposter = new ClassWithNonVirtualAndSealedMembersImposter();
            var instance = imposter.Instance();

            instance.NonVirtualMethod(4).ShouldBe(8);
            instance.NonVirtualMethodCallCount.ShouldBe(1);

            instance.NonVirtualProperty.ShouldBe("non-virtual-default");

            instance.NonVirtualProperty = "changed";
            instance.NonVirtualProperty.ShouldBe("changed");
            instance.NonVirtualPropertySetCount.ShouldBe(1);
        }

        [Fact]
        public void GivenSealedOverrides_WhenBuildingImposter_ShouldNotAllowConfigurationAndShouldRunBaseBehavior()
        {
            AssertNoBuilderMember(nameof(ClassWithNonVirtualAndSealedMembers.VirtualMethod));

            var imposter = new ClassWithNonVirtualAndSealedMembersImposter();
            var instance = imposter.Instance();

            instance.VirtualMethod(2).ShouldBe(10);
            instance.SealedOverrideInvocationCount.ShouldBe(1);
        }

        private static void AssertNoBuilderMember(string memberName)
        {
            var members = typeof(ClassWithNonVirtualAndSealedMembersImposter).GetMember(
                memberName,
                PublicMembers
            );
            members.ShouldBeEmpty($"Imposter builder unexpectedly exposes '{memberName}'.");
        }
    }
}
