using Imposter.Abstractions;
using Imposter.Tests.Features.ClassImpersonation.Suts;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.ClassImpersonation
{
    public class ConstructorDefaultsClassImposterTests
    {
        private readonly ClassWithConstructorDefaults _instance =
            new ClassWithConstructorDefaultsImposter().Instance();

        [Fact]
        public void GivenNullableDecimalDefault_WhenImposterIsCreatedWithoutArguments_ShouldPassTheDefault()
        {
            _instance.Amount.ShouldBe(1.5m);
        }

        [Fact]
        public void GivenNullableEnumDefault_WhenImposterIsCreatedWithoutArguments_ShouldPassTheDefault()
        {
            _instance.Level.ShouldBe(ConstructorDefaultLevel.High);
        }

        [Fact]
        public void GivenNaNDefault_WhenImposterIsCreatedWithoutArguments_ShouldPassTheDefault()
        {
            double.IsNaN(_instance.Ratio).ShouldBeTrue();
        }

        [Fact]
        public void GivenNegativeInfinityDefault_WhenImposterIsCreatedWithoutArguments_ShouldPassTheDefault()
        {
            _instance.Limit.ShouldBe(float.NegativeInfinity);
        }

        [Fact]
        public void GivenNegativeEnumDefault_WhenImposterIsCreatedWithoutArguments_ShouldPassTheDefault()
        {
            _instance.Floor.ShouldBe(ConstructorDefaultLevel.Below);
        }

        [Fact]
        public void GivenNegativeNullableEnumDefault_WhenImposterIsCreatedWithoutArguments_ShouldPassTheDefault()
        {
            _instance.Ceiling.ShouldBe(ConstructorDefaultLevel.Below);
        }
    }
}
