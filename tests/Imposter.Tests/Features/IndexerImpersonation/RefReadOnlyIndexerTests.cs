#if USE_CSHARP14
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.IndexerImpersonation
{
    public class RefReadOnlyIndexerTests
    {
        private readonly IRefReadOnlyIndexerSutImposter _sut = new IRefReadOnlyIndexerSutImposter();

        [Fact]
        public void GivenGetterReturnsDelegate_WhenRead_ShouldPassTheKeyToTheDelegate()
        {
            _sut[Arg<int>.Any()].Getter().Returns(key => key * 2);
            var key = 3;

            _sut.Instance()[in key].ShouldBe(6);
        }

        [Fact]
        public void GivenSetterCallback_WhenWritten_ShouldPassTheKeyAndValue()
        {
            (int Key, int Value)? received = null;
            _sut[Arg<int>.Any()].Setter().Callback((key, value) => received = (key, value));
            var key = 3;

            _sut.Instance()[in key] = 7;

            received.ShouldBe((3, 7));
        }

        [Fact]
        public void GivenClassGetterWithUseBaseImplementation_WhenRead_ShouldPassTheKeyToTheBaseGetter()
        {
            var imposter = new RefReadOnlyIndexerClassImposter();
            imposter[Arg<int>.Any()].Getter().UseBaseImplementation();
            var key = 4;

            imposter.Instance()[in key].ShouldBe(40);
        }

        [Fact]
        public void GivenClassSetterWithUseBaseImplementation_WhenWritten_ShouldPassTheKeyAndValueToTheBaseSetter()
        {
            var imposter = new RefReadOnlyIndexerClassImposter();
            imposter[Arg<int>.Any()].Setter().UseBaseImplementation();
            var instance = imposter.Instance();
            var key = 4;

            instance[in key] = 9;

            instance.LastSet.ShouldBe((4, 9));
        }
    }
}
#endif
