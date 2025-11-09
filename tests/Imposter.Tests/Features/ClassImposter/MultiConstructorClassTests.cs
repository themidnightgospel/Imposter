using System;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.ClassImposter
{
    public class MultiConstructorClassTests
    {
        [Fact]
        public void GivenClassWithMultipleConstructors_WhenGeneratingImposter_ThenExposesAllConstructors()
        {
            var defaultCtor = new MultiConstructorClassImposter();
            var parameterCtor = new MultiConstructorClassImposter(5, "alpha");
            var correlationId = Guid.NewGuid();
            var guidCtor = new MultiConstructorClassImposter(correlationId);

            defaultCtor.Instance().CtorSignature.ShouldBe("default");
            parameterCtor.Instance().CtorSignature.ShouldBe("value:5/label:alpha");
            guidCtor.Instance().CtorSignature.ShouldBe($"guid:{correlationId}");
        }

        [Fact]
        public void GivenImposterConstructors_WhenPassingInvocationBehavior_ThenAcceptBehavior()
        {
            var imposter = new MultiConstructorClassImposter(
                10,
                "beta",
                ImposterInvocationBehavior.Explicit);

            imposter.Calculate(Arg<int>.Any()).Returns(42);

            var instance = imposter.Instance();
            instance.Calculate(7).ShouldBe(42);
        }

        [Fact]
        public void GivenParameterlessConstruction_WhenNoBehaviorProvided_ThenDefaultsToImplicit()
        {
            var imposter = new MultiConstructorClassImposter();

            var instance = imposter.Instance();
            instance.CtorSignature.ShouldBe("default");

            Should.NotThrow(() => instance.Calculate(3));
        }

        [Fact]
        public void GivenConstructorWithBehavior_WhenConstructed_ThenPreservesSelection()
        {
            const int value = 8;
            const string label = "core";

            var imposter = new MultiConstructorClassImposter(
                value,
                label,
                ImposterInvocationBehavior.Explicit);

            var instance = imposter.Instance();
            instance.CtorSignature.ShouldBe($"value:{value}/label:{label}");

            Should.Throw<MissingImposterException>(() => instance.Calculate(5));
        }

        [Fact]
        public void GivenGeneratedInstanceMethods_WhenInspected_ThenRemainPublicOverrides()
        {
            var imposter = new MultiConstructorClassImposter();
            imposter.Calculate(Arg<int>.Any()).Returns(88);

            MultiConstructorClass instance = imposter.Instance();

            instance.Calculate(9).ShouldBe(88);
        }
    }
}
