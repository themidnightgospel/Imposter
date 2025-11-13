using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

[assembly: GenerateImposter(typeof(Imposter.Tests.Docs.Methods.IMyService))]

namespace Imposter.Tests.Docs.Methods
{
    public interface IMyService
    {
        int GetNumber();
        Task<int> GetNumberAsync();
    }

    public class ImposterModesTests
    {
        [Fact]
        public async Task Methods_BehaviorModes_Implicit()
        {
            var imposter = new IMyServiceImposter(ImposterMode.Implicit);
            var service = imposter.Instance();

            // Method without a setup => default(int) == 0
            int n = service.GetNumber(); // 0
            n.ShouldBe(0);

            // Add a setup and the call returns your value
            imposter.GetNumber().Returns(42);
            service.GetNumber().ShouldBe(42);

            // Async methods
            imposter.GetNumberAsync().ReturnsAsync(7);
            var v = await service.GetNumberAsync(); // 7
            v.ShouldBe(7);
        }

        [Fact]
        public void Methods_BehaviorModes_Explicit()
        {
            var imposter = new IMyServiceImposter(ImposterMode.Explicit);
            var service = imposter.Instance();

            // No setup -> throws MissingImposterException
            Should.Throw<MissingImposterException>(() => service.GetNumber());

            // Add a setup -> call succeeds
            imposter.GetNumber().Returns(42);
            service.GetNumber().ShouldBe(42);
        }
    }
}
