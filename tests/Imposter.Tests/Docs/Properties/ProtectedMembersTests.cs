using Imposter.Abstractions;
using Shouldly;
using Xunit;

[assembly: GenerateImposter(typeof(Imposter.Tests.Docs.Properties.MyService))]

namespace Imposter.Tests.Docs.Properties
{
    public class MyService
    {
        protected virtual int ProtectedAge { get; set; } = 7;

        public virtual int ReadProtected() => ProtectedAge;

        public virtual void WriteProtected(int value) => ProtectedAge = value;
    }

    public class ProtectedMembersTests
    {
        [Fact]
        public void GivenProtectedProperty_WhenAccessedThroughWrappers_ShouldUseTheImposter()
        {
            var imposter = new MyServiceImposter();

            // Arrange getter
            imposter.ProtectedAge.Getter().Returns(33);

            // Forward the public wrappers to the real implementation so they use the protected property
            imposter.ReadProtected().UseBaseImplementation();
            imposter.WriteProtected(Arg<int>.Any()).UseBaseImplementation();

            var service = imposter.Instance();
            service.WriteProtected(10);

            service.ReadProtected().ShouldBe(33);
            imposter.ProtectedAge.Setter(Arg<int>.Is(10)).Called(Count.Once());
        }
    }
}
