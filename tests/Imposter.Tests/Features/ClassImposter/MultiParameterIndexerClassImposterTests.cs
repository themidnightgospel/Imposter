using Imposter.Abstractions;
using Imposter.Tests.Features.ClassImposter.Suts;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.ClassImposter
{
    public class MultiParameterIndexerClassImposterTests
    {
        [Fact]
        public void GivenMultiParameterIndexer_WhenConfigured_ThenReturnsConfiguredValue()
        {
            var imposter = new ClassWithMultiParameterIndexerImposter();

            imposter[Arg<int>.Is(2), Arg<string>.Is("alpha")].Getter().Returns(88);

            var instance = imposter.Instance();

            instance.ReadValue(2, "alpha").ShouldBe(88);
        }

        [Fact]
        public void GivenMultiParameterIndexer_WhenUsingBaseBehavior_ThenStoresValues()
        {
            var imposter = new ClassWithMultiParameterIndexerImposter();
            var instance = imposter.Instance();

            instance.WriteValue(5, "base", 123);

            instance.ReadValue(5, "base").ShouldBe(123);
        }

        [Fact]
        public void GivenMultiParameterIndexerSetter_WhenInvoked_ThenVerificationSucceeds()
        {
            var imposter = new ClassWithMultiParameterIndexerImposter();
            var setter = imposter[Arg<int>.Is(7), Arg<string>.Is("verify")].Setter();
            var instance = imposter.Instance();

            instance.WriteValue(7, "verify", 77);

            Should.NotThrow(() => setter.Called(Count.Once()));
        }
    }
}
