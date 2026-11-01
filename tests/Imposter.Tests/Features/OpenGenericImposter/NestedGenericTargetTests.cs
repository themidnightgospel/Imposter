using Imposter.Tests.Features.OpenGenericImposter.NestedTargets;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.OpenGenericImposter
{
    public class NestedGenericTargetTests
    {
        [Fact]
        public void Given_DocumentedNestedGenericTarget_When_GetIsCalled_Should_ReturnConfiguredValue()
        {
            var instance = Usage.Create();

            instance.Get().ShouldBe("value");
        }
    }
}
