using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.ClassImposter.Suts;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.ClassImposter
{
    public class AbstractClassImposterTests
    {
        [Fact]
        public void GivenAbstractClassMembers_WhenGenerated_ShouldRemainOverridable()
        {
            var imposter = new AbstractTelemetryServiceImposter();

            imposter.Compute(Arg<int>.Any()).Returns(256);
            imposter.Name.Getter().Returns("overridden");
            var nameSetter = imposter.Name.Setter(Arg<string>.Any());

            imposter[Arg<int>.Any()].Getter().Returns(index => index + 5);
            var indexerSetter = imposter[Arg<int>.Any()].Setter();

            var eventRaised = false;
            var instance = imposter.Instance();
            instance.StreamAdvanced += (sender, args) => eventRaised = true;

            instance.Compute(1).ShouldBe(256);
            instance.Name.ShouldBe("overridden");
            instance.Name = "changed";

            instance[10].ShouldBe(15);
            instance[3] = 123;

            Should.NotThrow(() => nameSetter.Called(Count.Exactly(1)));
            Should.NotThrow(() => indexerSetter.Called(Count.Exactly(1)));

            imposter.StreamAdvanced.Raise(instance, EventArgs.Empty);
            eventRaised.ShouldBeTrue();
        }

        [Fact]
        public void GivenAbstractMember_WhenImplicitBehaviorInvokedWithoutSetup_ShouldReturnDefaultValue()
        {
            var imposter = new AbstractTelemetryServiceImposter(ImposterMode.Implicit);
            var instance = imposter.Instance();

            instance.Compute(5).ShouldBe(default);
        }
    }
}
