using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.PropertyImposter
{
    public class SetterOnlyPropertyTest
    {
        private readonly IPropertySetupSutImposter _sut =
#if USE_CSHARP14
            IPropertySetupSut.Imposter();
#else
            new IPropertySetupSutImposter();
#endif

        [Fact]
        public void GivenSetterOnlyProperty_WhenImposterAccessed_ShouldHaveGetterApi()
        {
            _sut.LastName.Setter(1);
            
            _sut.LastName.Setter(1).ShouldNotBeNull();
            _sut.LastName.Setter(1).Callback(_ => { }).ShouldNotBeNull();
            _sut.LastName.Setter(1).Called(Count.Never());
        }

    }
}
