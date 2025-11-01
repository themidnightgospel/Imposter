using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.PropertySetup
{
    public class SetterOnlyPropertyTest
    {
        private readonly IPropertySetupSutImposter _sut = new IPropertySetupSutImposter();
        
        [Fact]
        public void GivenSetterOnlyProperty_WhenImposterAccessed_ShouldHaveGetterApi()
        {
            _sut.LastName.Setter(1).ShouldNotBeNull();
            _sut.LastName.Setter(1).Callback(_ => { }).ShouldNotBeNull();
            _sut.LastName.Setter(1).Called(Count.Never());
        }

    }
}