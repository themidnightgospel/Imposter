using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.PropertyImposter
{
    public class PropertyBehaviorTests
    {
        [Fact]
        public void GivenExplicitMode_WhenGetterInvokedWithoutSetup_ShouldThrowMissingImposterException()
        {
            var sut = new IPropertySetupSutImposter(ImposterInvocationBehavior.Explicit);

            var exception = Should.Throw<MissingImposterException>(() => sut.Instance().Age);
            exception.MethodName.ShouldNotBeNull().ShouldContain(nameof(IPropertySetupSut.Age));
        }

        [Fact]
        public void GivenExplicitMode_WhenGetterConfigured_ShouldReturnValue()
        {
            var sut = new IPropertySetupSutImposter(ImposterInvocationBehavior.Explicit);

            sut.Age.Getter().Returns(42);

            sut.Instance().Age.ShouldBe(42);
        }

        [Fact]
        public void GivenExplicitMode_WhenSetterInvokedWithoutSetup_ShouldThrowMissingImposterException()
        {
            var sut = new IPropertySetupSutImposter(ImposterInvocationBehavior.Explicit);

            Should.Throw<MissingImposterException>(() => sut.Instance().LastName = 5);
        }

        [Fact]
        public void GivenExplicitMode_WhenSetterConfigured_ShouldAllowSettingValues()
        {
            var sut = new IPropertySetupSutImposter(ImposterInvocationBehavior.Explicit);

            sut.LastName.Setter(Arg<int>.Any());

            Should.NotThrow(() => sut.Instance().LastName = 5);
        }
    }
}
