#if ROSLYN_5_OR_GREATER
using Imposter.Abstractions;
using Imposter.Tests.Shared;
using Shouldly;
using Xunit;

[assembly: GenerateImposter(typeof(IMammal))]

namespace Imposter.Tests.Features.ImposterStaticExtension
{
    public class ImposterStaticExtensionTests
    {
        [Fact]
        public void GivenRoslyn5OrGreated_WhenImposterCalled_ShouldReturnImposter()
        {
            var imposter = IMammal.Imposter();
            imposter.ShouldNotBeNull();
        }
    }
}
#endif
