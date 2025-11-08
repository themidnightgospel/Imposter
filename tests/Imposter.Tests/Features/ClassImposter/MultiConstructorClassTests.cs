using System;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.ClassImposter
{
    public class MultiConstructorClassTests
    {
        [Fact]
        public void Imposter_Exposes_Constructors_For_Each_Accessible_Base_Ctor()
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
        public void Imposter_Constructors_Accept_InvocationBehavior()
        {
            var imposter = new MultiConstructorClassImposter(
                10,
                "beta",
                ImposterInvocationBehavior.Explicit);

            imposter.Calculate(Arg<int>.Any()).Returns(42);

            var instance = imposter.Instance();
            instance.Calculate(7).ShouldBe(42);
        }
    }
}
