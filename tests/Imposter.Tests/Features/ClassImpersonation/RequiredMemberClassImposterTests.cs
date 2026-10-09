#if USE_CSHARP14
using Imposter.Abstractions;
using Imposter.Tests.Features.ClassImpersonation;
using Shouldly;
using Xunit;

[assembly: GenerateImposter(typeof(RequiredMemberService))]

namespace Imposter.Tests.Features.ClassImpersonation
{
    public class RequiredMemberService
    {
        public required string Name { get; set; }

        public virtual required string Label { get; set; }

        public virtual int Count() => 0;
    }

    public class RequiredMemberClassImposterTests
    {
        private readonly RequiredMemberServiceImposter _sut = new RequiredMemberServiceImposter();

        [Fact]
        public void GivenVirtualRequiredPropertyGetterSetup_WhenRead_ShouldReturnTheSetupValue()
        {
            _sut.Label.Getter().Returns("label");

            _sut.Instance().Label.ShouldBe("label");
        }

        [Fact]
        public void GivenNonVirtualRequiredProperty_WhenRead_ShouldHoldItsDefault()
        {
            _sut.Instance().Name.ShouldBeNull();
        }

        [Fact]
        public void GivenMethodSetup_WhenInvoked_ShouldReturnTheSetupValue()
        {
            _sut.Count().Returns(3);

            _sut.Instance().Count().ShouldBe(3);
        }
    }
}
#endif
