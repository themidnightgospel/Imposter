using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.IndexerImpersonation
{
    public class IndexerKeyEqualityTests
    {
        private readonly IValueKeyIndexerSutImposter _sut = new IValueKeyIndexerSutImposter();

        [Fact]
        public void GivenValueStoredWithKey_WhenReadWithDistinctEqualKey_ShouldReturnStoredValue()
        {
            var instance = _sut.Instance();
            instance[new IndexerKey(1)] = 42;

            instance[new IndexerKey(1)].ShouldBe(42);
        }
    }
}
