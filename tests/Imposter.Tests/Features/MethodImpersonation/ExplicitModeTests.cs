using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.MethodImpersonation
{
    public class ExplicitModeTests
    {
        [Fact]
        public void GivenExplicitMode_WhenMethodInvokedWithoutSetup_ShouldThrowMissingImposterException()
        {
            var sut = new IMethodSetupFeatureSutImposter(ImposterMode.Explicit);

            var exception = Should.Throw<MissingImposterException>(() =>
                sut.Instance().IntNoParams()
            );
            exception
                .MethodName.ShouldNotBeNull()
                .ShouldContain(nameof(IMethodSetupFeatureSut.IntNoParams));
        }

        [Fact]
        public void GivenExplicitMode_WhenVoidMethodInvokedWithoutSetup_ShouldThrowMissingImposterException()
        {
            var sut = new IMethodSetupFeatureSutImposter(ImposterMode.Explicit);

            var exception = Should.Throw<MissingImposterException>(() =>
                sut.Instance().VoidNoParams()
            );
            exception
                .MethodName.ShouldNotBeNull()
                .ShouldContain(nameof(IMethodSetupFeatureSut.VoidNoParams));
        }

        [Fact]
        public void GivenExplicitMode_WhenVoidMethodMissingSetup_ShouldExposeFullSignature()
        {
            var sut = new IMethodSetupFeatureSutImposter(ImposterMode.Explicit);

            var exception = Should.Throw<MissingImposterException>(() =>
                sut.Instance().VoidNoParams()
            );
            exception.MethodName.ShouldBe("void IMethodSetupFeatureSut.VoidNoParams()");
            exception.Message.ShouldContain("void IMethodSetupFeatureSut.VoidNoParams()");
        }

        [Fact]
        public void GivenExplicitMode_WhenSetupDoesNotConfigureResult_ShouldThrowMissingImposterException()
        {
            var sut = new IMethodSetupFeatureSutImposter(ImposterMode.Explicit);

            sut.IntNoParams(); // Builder created but no Returns/Throws.

            Should.Throw<MissingImposterException>(() => sut.Instance().IntNoParams());
        }

        [Fact]
        public void GivenExplicitMode_WhenSetupConfigured_ShouldInvokeSuccessfully()
        {
            var sut = new IMethodSetupFeatureSutImposter(ImposterMode.Explicit);

            sut.IntNoParams().Returns(123);

            sut.Instance().IntNoParams().ShouldBe(123);
        }

        [Fact]
        public void GivenExplicitMode_WhenVoidMethodSetupWithoutConfiguration_ShouldThrowMissingImposterException()
        {
            var sut = new IMethodSetupFeatureSutImposter(ImposterMode.Explicit);

            sut.VoidNoParams();

            Should.Throw<MissingImposterException>(() => sut.Instance().VoidNoParams());
        }

        [Fact]
        public void GivenExplicitMode_WhenVoidMethodCallbackConfigured_ShouldInvokeSuccessfully()
        {
            var sut = new IMethodSetupFeatureSutImposter(ImposterMode.Explicit);
            var callbackInvoked = false;

            sut.VoidNoParams().Callback(() => callbackInvoked = true);

            Should.NotThrow(() => sut.Instance().VoidNoParams());
            callbackInvoked.ShouldBeTrue();
        }

        [Fact]
        public void GivenExplicitMode_WhenVoidMethodVerified_ShouldStillThrowMissingImposterException()
        {
            var sut = new IMethodSetupFeatureSutImposter(ImposterMode.Explicit);

            sut.VoidNoParams().Called(Count.Never());

            Should.Throw<MissingImposterException>(() => sut.Instance().VoidNoParams());
        }

        [Fact]
        public void GivenExplicitMode_WhenVoidMethodCallCountRead_ShouldStillThrowMissingImposterException()
        {
            var sut = new IMethodSetupFeatureSutImposter(ImposterMode.Explicit);

            sut.VoidNoParams().CallCount().ShouldBe(0);

            Should.Throw<MissingImposterException>(() => sut.Instance().VoidNoParams());
        }

        [Fact]
        public void GivenExplicitMode_WhenGenericVoidMethodVerified_ShouldStillThrowMissingImposterException()
        {
            var sut = new IMethodSetupFeatureSutImposter(ImposterMode.Explicit);

            sut.GenericSingleParam<int>(Arg<int>.Any()).Called(Count.Never());

            Should.Throw<MissingImposterException>(() => sut.Instance().GenericSingleParam(1));
        }

        [Fact]
        public void GivenExplicitMode_WhenVerifiedBuilderIsConfiguredLater_ShouldUseItsOutcome()
        {
            var sut = new IMethodSetupFeatureSutImposter(ImposterMode.Explicit);
            var callbackInvoked = false;
            var builder = sut.VoidNoParams();

            builder.Called(Count.Never());
            builder.Callback(() => callbackInvoked = true);

            Should.NotThrow(() => sut.Instance().VoidNoParams());
            callbackInvoked.ShouldBeTrue();
        }

        [Fact]
        public void GivenExplicitMode_WhenGenericVoidMethodVerifiedForAnotherTypeArgument_ShouldStillThrowMissingImposterException()
        {
            var sut = new IMethodSetupFeatureSutImposter(ImposterMode.Explicit);

            sut.GenericSingleParam<object>(Arg<object>.Any()).Called(Count.Never());

            Should.Throw<MissingImposterException>(() =>
                sut.Instance().GenericSingleParam("value")
            );
        }

        [Fact]
        public void GivenImplicitMode_WhenGenericVoidMethodVerifiedForAnotherTypeArgument_ShouldRecordCallsUnderTheirOwnTypeArgument()
        {
            var sut = new IMethodSetupFeatureSutImposter(ImposterMode.Implicit);

            sut.GenericSingleParam<object>(Arg<object>.Any()).Called(Count.Never());
            sut.Instance().GenericSingleParam("value");

            sut.GenericSingleParam<string>(Arg<string>.Any()).Called(Count.Once());
            sut.GenericSingleParam<object>(Arg<object>.Any()).Called(Count.Never());
        }
    }
}
