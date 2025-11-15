using System;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

[assembly: GenerateImposter(typeof(Imposter.Tests.Docs.Methods.IThrowService))]

namespace Imposter.Tests.Docs.Methods
{
    public interface IThrowService
    {
        int GetNumber();
        Task<int> GetNumberAsync();
    }

    public class ThrowingTests
    {
        [Fact]
        public void Methods_Throwing_Ways_To_Throw()
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
        public void Methods_Throwing_Sequence_With_Returns()
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
        public async Task Methods_Throwing_Async()
        {
            var imposter = new IThrowServiceImposter();
            var service = imposter.Instance();

            imposter.GetNumberAsync().Throws<TimeoutException>();
            await Should.ThrowAsync<TimeoutException>(async () => await service.GetNumberAsync());
        }
    }
}
