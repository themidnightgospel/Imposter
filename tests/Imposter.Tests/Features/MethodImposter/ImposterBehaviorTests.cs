using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.MethodImposter
{
    public class ImposterBehaviorTests
    {
        [Fact]
        public void GivenExplicitMode_WhenMethodInvokedWithoutSetup_ShouldThrowMissingImposterException()
        {
            var sut = new IMethodSetupFeatureSutImposter(ImposterInvocationBehavior.Explicit);

            var exception = Should.Throw<MissingImposterException>(() => sut.Instance().IntNoParams());
            exception.MethodName.ShouldNotBeNull().ShouldContain(nameof(IMethodSetupFeatureSut.IntNoParams));
        }

        [Fact]
        public void GivenExplicitMode_WhenSetupDoesNotConfigureResult_ShouldThrowMissingImposterException()
        {
            var sut = new IMethodSetupFeatureSutImposter(ImposterInvocationBehavior.Explicit);

            sut.IntNoParams(); // Builder created but no Returns/Throws.

            Should.Throw<MissingImposterException>(() => sut.Instance().IntNoParams());
        }

        [Fact]
        public void GivenExplicitMode_WhenSetupConfigured_ShouldInvokeSuccessfully()
        {
            var sut = new IMethodSetupFeatureSutImposter(ImposterInvocationBehavior.Explicit);

            sut.IntNoParams().Returns(123);

            sut.Instance().IntNoParams().ShouldBe(123);
        }
    }
}
