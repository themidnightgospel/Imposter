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
        public void GivenExplicitMode_WhenVoidMethodInvokedWithoutSetup_ShouldThrowMissingImposterException()
        {
            var sut = new IMethodSetupFeatureSutImposter(ImposterInvocationBehavior.Explicit);

            var exception = Should.Throw<MissingImposterException>(() => sut.Instance().VoidNoParams());
            exception.MethodName.ShouldNotBeNull().ShouldContain(nameof(IMethodSetupFeatureSut.VoidNoParams));
        }

        [Fact]
        public void GivenExplicitMode_WhenVoidMethodMissingSetup_ShouldExposeFullSignature()
        {
            var sut = new IMethodSetupFeatureSutImposter(ImposterInvocationBehavior.Explicit);

            var exception = Should.Throw<MissingImposterException>(() => sut.Instance().VoidNoParams());
            exception.MethodName.ShouldBe("void IMethodSetupFeatureSut.VoidNoParams()");
            exception.Message.ShouldContain("void IMethodSetupFeatureSut.VoidNoParams()");
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

        [Fact]
        public void GivenExplicitMode_WhenVoidMethodCallbackConfigured_ShouldStillRequireResultConfiguration()
        {
            var sut = new IMethodSetupFeatureSutImposter(ImposterInvocationBehavior.Explicit);
            var callbackInvoked = false;

            sut.VoidNoParams().Callback(() => callbackInvoked = true);

            var exception = Should.Throw<MissingImposterException>(() => sut.Instance().VoidNoParams());
            exception.MethodName.ShouldContain(nameof(IMethodSetupFeatureSut.VoidNoParams));
            callbackInvoked.ShouldBeFalse();
        }
    }
}
