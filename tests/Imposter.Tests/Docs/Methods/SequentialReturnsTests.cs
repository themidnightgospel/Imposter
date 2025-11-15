using System;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

[assembly: GenerateImposter(typeof(Imposter.Tests.Docs.Methods.ISeqService))]

namespace Imposter.Tests.Docs.Methods
{
    public interface ISeqService
    {
        int GetNumber();
        Task<int> GetNumberAsync();
    }

    public class SequentialReturnsTests
    {
        [Fact]
        public void Methods_Sequential_Basic()
        {
            var imposter = new ISeqServiceImposter();
            var service = imposter.Instance();

            imposter.GetNumber().Returns(1).Then().Returns(2).Then().Returns(3);

            service.GetNumber().ShouldBe(1);
            service.GetNumber().ShouldBe(2);
            service.GetNumber().ShouldBe(3);
        }

        [Fact]
        public void Methods_Sequential_MixingDelegates()
        {
            var imposter = new ISeqServiceImposter();
            var service = imposter.Instance();

            imposter.GetNumber().Returns(() => 1).Then().Returns(() => 2);

            service.GetNumber().ShouldBe(1);
            service.GetNumber().ShouldBe(2);
        }

        [Fact]
        public async Task Methods_Sequential_Async()
        {
            var imposter = new ISeqServiceImposter();
            var service = imposter.Instance();

            imposter.GetNumberAsync().ReturnsAsync(1).Then().Returns(() => Task.FromResult(2));

            (await service.GetNumberAsync()).ShouldBe(1);
            (await service.GetNumberAsync()).ShouldBe(2);
        }

        [Fact]
        public void Methods_Sequential_Interleave_Exceptions()
        {
            var imposter = new ISeqServiceImposter();
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
    }
}
