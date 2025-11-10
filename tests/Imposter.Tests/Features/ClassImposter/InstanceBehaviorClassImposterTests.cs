using Imposter.Abstractions;
using Imposter.Tests.Features.ClassImposter.Suts;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.ClassImposter
{
    public class InstanceBehaviorClassImposterTests
    {
        [Fact]
        public void GivenClassImposter_WhenRequestingInstanceMultipleTimes_ShouldReturnSameObject()
        {
            var imposter = new ClassWithProtectedOverrideableMembersImposter();

            imposter.ProtectedVirtualMethod(Arg<int>.Is(3)).Returns(42);
            imposter.InvokeProtectedMethod(3).UseBaseImplementation();

            var first = imposter.Instance();
            var second = imposter.Instance();

            ReferenceEquals(first, second).ShouldBeTrue();

            first.InvokeProtectedMethod(3).ShouldBe(42);
            second.InvokeProtectedMethod(3).ShouldBe(42);

            Should.NotThrow(() => imposter.ProtectedVirtualMethod(Arg<int>.Is(3)).Called(Count.Exactly(2)));
        }

        [Fact]
        public void GivenSingleInstance_WhenStateMutated_ShouldBeVisibleAcrossCalls()
        {
            var imposter = new ClassWithProtectedOverrideableMembersImposter();

            imposter.WriteProtectedProperty(Arg<string>.Any()).UseBaseImplementation();
            imposter.ReadProtectedProperty().UseBaseImplementation();

            var initial = imposter.Instance();
            initial.WriteProtectedProperty("updated");

            var subsequent = imposter.Instance();
            subsequent.ReadProtectedProperty().ShouldBe("updated");
        }
    }
}
