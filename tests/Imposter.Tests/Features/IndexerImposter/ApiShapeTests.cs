using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.IndexerImposter
{
    public class ApiShapeTests
    {
        private readonly IIndexerSetupSutImposter _sut = new IIndexerSetupSutImposter();

        [Fact]
        public void GivenIndexerBuilder_WhenChaining_ThenSupported()
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
        public void GivenImposter_WhenAccessingIndexerBuilder_ThenExposesBuilder()
        {
            _sut[Imposter.Abstractions.Arg<int>.Any(), Imposter.Abstractions.Arg<string>.Any(), Imposter.Abstractions.Arg<object>.Any()]
                .ShouldNotBeNull();
            _sut.Instance().ShouldNotBeNull();
        }
    }
}
