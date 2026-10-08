using Imposter.Abstractions;
using Imposter.Tests.Features.ClassImpersonation.Suts;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.ClassImpersonation
{
    public class RestrictedAccessorsClassImposterTests
    {
        private readonly ClassWithRestrictedAccessorsImposter _sut =
            new ClassWithRestrictedAccessorsImposter();

        [Fact]
        public void GivenGetterReturns_WhenPropertyWithPrivateSetterIsRead_ShouldReturnConfiguredValue()
        {
            _sut.PrivateSetter.Getter().Returns(5);

            _sut.Instance().PrivateSetter.ShouldBe(5);
        }

        [Fact]
        public void GivenProtectedSetter_WhenClassSetsIt_ShouldRecordTheSet()
        {
            _sut.Instance().ResetProtectedSetter();

            Should.NotThrow(() => _sut.ProtectedSetter.Setter(Arg<int>.Is(0)).Called(Count.Once()));
        }

        [Fact]
        public void GivenGetterReturns_WhenClassReadsProtectedGetter_ShouldReturnConfiguredValue()
        {
            _sut.ProtectedGetter.Getter().Returns(7);

            _sut.Instance().ReadProtectedGetter().ShouldBe(7);
        }

        [Fact]
        public void GivenGetterReturns_WhenIndexerWithPrivateSetterIsRead_ShouldReturnConfiguredValue()
        {
            _sut[Arg<int>.Any()].Getter().Returns(9);

            _sut.Instance()[1].ShouldBe(9);
        }
    }
}
