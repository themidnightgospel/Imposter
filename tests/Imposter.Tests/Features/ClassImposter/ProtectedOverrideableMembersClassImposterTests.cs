using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.ClassImposter.Suts;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.ClassImposter
{
    public class ProtectedOverrideableMembersClassImposterTests
    {
        [Fact]
        public void GivenProtectedOverrideableMembers_WhenConfigured_ThenOverridesRemainAccessible()
        {
            var imposter = new ClassWithProtectedOverrideableMembersImposter();

            imposter.ProtectedVirtualMethod(Arg<int>.Is(5)).Returns(42);

            imposter.ProtectedVirtualProperty.Getter().Returns("overridden");
            var propertySetter = imposter.ProtectedVirtualProperty.Setter(Arg<string>.Any());

            imposter[Arg<int>.Any()].Getter().Returns(index => index * 10);
            var indexerSetter = imposter[Arg<int>.Is(7)].Setter();

            var eventRaised = false;
            var instance = imposter.Instance();
            instance.SubscribeToProtectedEvent((sender, args) => eventRaised = true);

            instance.InvokeProtectedMethod(5).ShouldBe(42);
            instance.ReadProtectedProperty().ShouldBe("overridden");
            instance.WriteProtectedProperty("changed");

            instance.ReadProtectedValue(3).ShouldBe(30);
            instance.WriteProtectedValue(7, 123);

            Should.NotThrow(() => propertySetter.Called(Count.Exactly(1)));
            Should.NotThrow(() => indexerSetter.Called(Count.Exactly(1)));

            imposter.ProtectedVirtualEvent.Raise(instance, EventArgs.Empty);
            eventRaised.ShouldBeTrue();
        }
    }
}
