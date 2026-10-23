using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.PropertyImpersonation
{
    public class PropertyBehaviorTests
    {
        [Fact]
        public void GivenExplicitMode_WhenGetterInvokedWithoutSetup_ShouldThrowMissingImposterException()
        {
            var sut = new IPropertySetupSutImposter(ImposterMode.Explicit);

            var exception = Should.Throw<MissingImposterException>(() => sut.Instance().Age);
            exception.MethodName.ShouldNotBeNull().ShouldContain(nameof(IPropertySetupSut.Age));
        }

        [Fact]
        public void GivenExplicitMode_WhenGetterConfigured_ShouldReturnValue()
        {
            var sut = new IPropertySetupSutImposter(ImposterMode.Explicit);

            sut.Age.Getter().Returns(42);

            sut.Instance().Age.ShouldBe(42);
        }

        [Fact]
        public void GivenExplicitMode_WhenSetterInvokedWithoutSetup_ShouldThrowMissingImposterException()
        {
            var sut = new IPropertySetupSutImposter(ImposterMode.Explicit);

            Should.Throw<MissingImposterException>(() => sut.Instance().LastName = 5);
        }

        [Fact]
        public void GivenExplicitMode_WhenSetterHasNoCallback_ShouldThrowMissingImposterException()
        {
            var sut = new IPropertySetupSutImposter(ImposterMode.Explicit);

            sut.LastName.Setter(Arg<int>.Any());

            Should.Throw<MissingImposterException>(() => sut.Instance().LastName = 5);
        }

        [Fact]
        public void GivenExplicitMode_WhenSetterVerified_ShouldStillThrowMissingImposterException()
        {
            var sut = new IPropertySetupSutImposter(ImposterMode.Explicit);

            sut.LastName.Setter(Arg<int>.Any()).Called(Count.Never());

            Should.Throw<MissingImposterException>(() => sut.Instance().LastName = 5);
        }

        [Fact]
        public void GivenExplicitMode_WhenSetterCallbackConfigured_ShouldAllowSettingValues()
        {
            var sut = new IPropertySetupSutImposter(ImposterMode.Explicit);
            var setValue = 0;

            sut.LastName.Setter(Arg<int>.Any()).Callback(value => setValue = value);

            Should.NotThrow(() => sut.Instance().LastName = 5);
            setValue.ShouldBe(5);
        }
    }
}
