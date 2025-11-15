using Imposter.Abstractions;
using Imposter.Tests.Features.Docs.Methods.ProtectedMethods;
using Shouldly;
using Xunit;

[assembly: GenerateImposter(typeof(MyService))]

namespace Imposter.Tests.Features.Docs.Methods.ProtectedMethods
{
    public class MyService
    {
        protected virtual int ProtectedAdd(int value) => value * 2;

        public virtual int InvokeProtected(int value) => ProtectedAdd(value);
    }

    public class ProtectedMembersTests
    {
        [Fact]
        public void Methods_ProtectedMembers_SetupAndInvokeViaWrapper()
        {
            var imp = new MyServiceImposter();

            // Arrange the protected method directly on the imposter
            imp.ProtectedAdd(Arg<int>.Is(5)).Returns(42);

            // Forward the public wrapper to the real implementation so it calls the protected member
            imp.InvokeProtected(Arg<int>.Any()).UseBaseImplementation();

            var svc = imp.Instance();

            svc.InvokeProtected(5).ShouldBe(42);

            Should.NotThrow(() => imp.InvokeProtected(Arg<int>.Any()).Called(Count.Once()));
        }
    }
}
