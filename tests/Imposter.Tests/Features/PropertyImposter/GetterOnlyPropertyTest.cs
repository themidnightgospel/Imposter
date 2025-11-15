using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.PropertyImposter
{
    public class GetterOnlyPropertyTest
    {
        private readonly IPropertySetupSutImposter _sut =
#if USE_CSHARP14
        IPropertySetupSut.Imposter();
#else
        new IPropertySetupSutImposter();
#endif

        [Fact]
        public void GivenGetterOnlyProperty_WhenImposterAccessed_ShouldHaveGetterApi()
        {
            _sut.Name.Getter().ShouldNotBeNull();
            _sut.Name.Getter().Callback(() => { }).ShouldNotBeNull();
            _sut.Name.Getter().Called(Count.Never());
        }
    }
}
