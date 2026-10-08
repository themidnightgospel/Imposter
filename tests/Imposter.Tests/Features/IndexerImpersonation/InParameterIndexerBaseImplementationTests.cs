using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.IndexerImpersonation
{
    public class InParameterIndexerBaseImplementationTests
    {
        private readonly InParameterIndexerClassImposter _sut =
            new InParameterIndexerClassImposter();

        [Fact]
        public void GivenGetterUseBaseImplementation_WhenRead_ShouldPassTheKeyToTheBaseGetter()
        {
            _sut[Arg<int>.Any()].Getter().UseBaseImplementation();

            _sut.Instance()[4].ShouldBe(40);
        }

        [Fact]
        public void GivenSetterUseBaseImplementation_WhenWritten_ShouldPassTheKeyAndValueToTheBaseSetter()
        {
            _sut[Arg<int>.Any()].Setter().UseBaseImplementation();
            var instance = _sut.Instance();

            instance[4] = 9;

            instance.LastSet.ShouldBe((4, 9));
        }

        [Fact]
        public void GivenGetterOnlyIndexerWithUseBaseImplementation_WhenRead_ShouldPassTheKeyToTheBaseGetter()
        {
            var imposter = new InParameterGetterOnlyIndexerClassImposter();
            imposter[Arg<int>.Any()].Getter().UseBaseImplementation();

            imposter.Instance()[4].ShouldBe(5);
        }

        [Fact]
        public void GivenParameterNamedLikeAnotherParametersCopy_WhenReadThroughBase_ShouldPassEachKeyToTheBaseGetter()
        {
            var imposter = new InParameterCopyNameCollisionIndexerClassImposter();
            imposter[Arg<int>.Any(), Arg<int>.Any(), Arg<string>.Any()]
                .Getter()
                .UseBaseImplementation();

            imposter.Instance()[1, 2, "name"].ShouldBe("1:2:name");
        }

        [Fact]
        public void GivenParameterNamedLikeAnotherParametersCopy_WhenWrittenThroughBase_ShouldPassEachKeyAndValueToTheBaseSetter()
        {
            var imposter = new InParameterCopyNameCollisionIndexerClassImposter();
            imposter[Arg<int>.Any(), Arg<int>.Any(), Arg<string>.Any()]
                .Setter()
                .UseBaseImplementation();
            var instance = imposter.Instance();

            instance[1, 2, "name"] = "value";

            instance.LastSet.ShouldBe((1, 2, "name", "value"));
        }
    }
}
