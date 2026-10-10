#if USE_CSHARP14
using System;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.IndexerImpersonation
{
    public class ScopedKeyIndexerTests
    {
        private readonly IScopedKeyIndexerSutImposter _sut = new IScopedKeyIndexerSutImposter();

        [Fact]
        public void GivenGetterSetupForScopedKeyElements_WhenRead_ShouldReturnASpanOverItsArray()
        {
            _sut[ReadOnlySpanArg<char>.Is('k')].Getter().Returns(new[] { 'v' });

            _sut.Instance()["k".AsSpan()].ToString().ShouldBe("v");
        }

        [Fact]
        public void GivenValueSetForScopedKey_WhenSetterIsVerifiedForTheKeyElements_ShouldCountIt()
        {
            _sut.Instance()["k".AsSpan()] = "v".AsSpan();

            _sut[ReadOnlySpanArg<char>.Is('k')].Setter().Called(Count.Once());
        }

        [Fact]
        public void GivenGetterSetupForParamsKeyElements_WhenReadWithThem_ShouldReturnASpanOverItsArray()
        {
            _sut[ReadOnlySpanArg<int>.Is(1, 2)].Getter().Returns(new[] { 3 });

            _sut.Instance()[1, 2].ToArray().ShouldBe(new[] { 3 });
        }
    }
}
#endif
