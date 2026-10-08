using System.IO;
using Imposter.Abstractions;
using Imposter.Tests.Features.ClassImpersonation.Suts;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.ClassImpersonation
{
    public class ConstrainedGenericMethodsClassImposterTests
    {
        private readonly ClassWithConstrainedGenericMethodsImposter _sut =
            new ClassWithConstrainedGenericMethodsImposter();

        [Fact]
        public void GivenReturnsSetup_WhenMethodWithInterfaceConstraintIsCalled_ShouldReturnConfiguredValue()
        {
            using var configured = new MemoryStream();
            using var argument = new MemoryStream();
            _sut.Echo<MemoryStream>(Arg<MemoryStream>.Any()).Returns(configured);

            _sut.Instance().Echo(argument).ShouldBeSameAs(configured);
        }

        [Fact]
        public void GivenUseBaseImplementation_WhenMethodWithConstructorConstraintIsCalled_ShouldCallBase()
        {
            using var argument = new MemoryStream();
            _sut.Describe<MemoryStream>(Arg<MemoryStream>.Any()).UseBaseImplementation();

            _sut.Instance().Describe(argument).ShouldBe("base:MemoryStream");
        }
    }
}
