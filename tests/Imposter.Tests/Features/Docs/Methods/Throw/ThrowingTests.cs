using System;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Imposter.Tests.Features.Docs.Methods.Throw;
using Shouldly;
using Xunit;

[assembly: GenerateImposter(typeof(IThrowService))]

namespace Imposter.Tests.Features.Docs.Methods.Throw
{
    public interface IThrowService
    {
        int GetNumber();
        Task<int> GetNumberAsync();
    }

    public class ThrowingTests
    {
        [Fact]
        public void GivenThrowingSetups_WhenConfigured_ShouldSupportMultipleWaysToThrow()
        {
            var imposter = new IThrowServiceImposter();
            var service = imposter.Instance();

            // Generic type
            imposter.GetNumber().Throws<InvalidOperationException>();
            Should.Throw<InvalidOperationException>(() => service.GetNumber());

            // Specific instance
            imposter.GetNumber().Throws(new Exception("boom"));
            var ex = Should.Throw<Exception>(() => service.GetNumber());
            ex.Message.ShouldBe("boom");

            // Factory delegate
            imposter.GetNumber().Throws(() => new Exception("deferred"));
            var ex2 = Should.Throw<Exception>(() => service.GetNumber());
            ex2.Message.ShouldBe("deferred");
        }

        [Fact]
        public void GivenThrowingSetups_WhenSequencedWithReturns_ShouldRespectSequence()
        {
            var imposter = new IThrowServiceImposter();
            var service = imposter.Instance();

            imposter
                .GetNumber()
                .Returns(1)
                .Then()
                .Throws<InvalidOperationException>()
                .Then()
                .Returns(2);

            service.GetNumber().ShouldBe(1);
            Should.Throw<InvalidOperationException>(() => service.GetNumber());
            service.GetNumber().ShouldBe(2);
        }

        [Fact]
        public async Task GivenThrowingSetups_WhenUsingAsyncMethods_ShouldPropagateExceptions()
        {
            var imposter = new IThrowServiceImposter();
            var service = imposter.Instance();

            imposter.GetNumberAsync().Throws<TimeoutException>();
            await Should.ThrowAsync<TimeoutException>(async () => await service.GetNumberAsync());
        }
    }
}
