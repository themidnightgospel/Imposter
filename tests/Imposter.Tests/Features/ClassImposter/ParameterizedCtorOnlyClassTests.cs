using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.ClassImposter.Suts;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.ClassImposter
{
    public class ParameterizedCtorOnlyClassTests
    {
        [Fact]
        public void GivenParameterizedClass_WhenGeneratingImposter_ShouldOverrideAllMembers()
        {
            var imposter = new ParameterizedCtorOnlyClassImposter(10, "alpha");

            imposter.Compute(Arg<int>.Any()).Returns(1337);
            imposter.Name.Getter().Returns("configured");
            var nameSetter = imposter.Name.Setter(Arg<string>.Any());

            imposter[Arg<int>.Any()].Getter().Returns(index => index * 2);
            var indexerSetter = imposter[Arg<int>.Any()].Setter();

            var eventRaised = false;
            var instance = imposter.Instance();
            instance.SomethingHappened += (sender, args) => eventRaised = true;

            instance.Compute(99).ShouldBe(1337);
            instance.Name.ShouldBe("configured");
            instance.Name = "beta";

            instance[5].ShouldBe(10);
            instance[2] = 42;

            Should.NotThrow(() => nameSetter.Called(Count.Exactly(1)));
            Should.NotThrow(() => indexerSetter.Called(Count.Exactly(1)));

            imposter.SomethingHappened.Raise(instance, EventArgs.Empty);
            eventRaised.ShouldBeTrue();
        }
    }
}
