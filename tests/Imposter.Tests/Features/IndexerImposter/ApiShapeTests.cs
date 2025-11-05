using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.IndexerImposter
{
    public class ApiShapeTests
    {
        private readonly IIndexerSetupSutImposter _sut = new IIndexerSetupSutImposter();

        [Fact]
        public void Builder_Chaining_Supported()
        {
            var builder = _sut
                [Imposter.Abstractions.Arg<int>.Any(), Imposter.Abstractions.Arg<string>.Any(), Imposter.Abstractions.Arg<object>.Any()]
                .Getter()
                .Returns(1)
                .Then()
                .Throws<System.InvalidOperationException>()
                .Then()
                .Callback((a, b, c) => { })
                .Then();

            builder.ShouldNotBeNull();
        }

        [Fact]
        public void Exposes_Indexer_Builder_On_Imposter()
        {
            _sut[Imposter.Abstractions.Arg<int>.Any(), Imposter.Abstractions.Arg<string>.Any(), Imposter.Abstractions.Arg<object>.Any()]
                .ShouldNotBeNull();
            _sut.Instance().ShouldNotBeNull();
        }
    }
}
