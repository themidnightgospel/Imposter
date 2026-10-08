using Imposter.Abstractions;
using Imposter.Tests.Features.IndexerImpersonation;
using Shouldly;
using Xunit;

[assembly: GenerateImposter(typeof(ICollidingIndexers))]
[assembly: GenerateImposter(typeof(IHidingIndexer))]

namespace Imposter.Tests.Features.IndexerImpersonation
{
    public interface INumberIndexer
    {
        int this[int key] { get; }
    }

    public interface ITextIndexer
    {
        string this[int key] { get; set; }
    }

    public interface ICollidingIndexers : INumberIndexer, ITextIndexer { }

    public interface IHiddenIndexer
    {
        int this[int key] { get; }
    }

    public interface IHidingIndexer : IHiddenIndexer
    {
        new string this[int key] { get; }
    }

    public class CollidingIndexerTests
    {
        private readonly ICollidingIndexersImposter _sut = new ICollidingIndexersImposter();

        [Fact]
        public void GivenGetterSetupThroughEachView_WhenReadThroughEachInterface_ShouldReturnItsOwnValue()
        {
            _sut.For(default(INumberIndexer))[Arg<int>.Any()].Getter().Returns(1);
            _sut.For(default(ITextIndexer))[Arg<int>.Any()].Getter().Returns("one");
            var instance = _sut.Instance();

            ((INumberIndexer)instance)[0].ShouldBe(1);
            ((ITextIndexer)instance)[0].ShouldBe("one");
        }

        [Fact]
        public void GivenSetterThroughOneInterface_WhenVerifiedThroughItsView_ShouldRecordTheSet()
        {
            ((ITextIndexer)_sut.Instance())[3] = "three";

            Should.NotThrow(() =>
                _sut.For(default(ITextIndexer))[Arg<int>.Is(3)].Setter().Called(Count.Once())
            );
        }

        [Fact]
        public void GivenHiddenIndexer_WhenEachViewIsSetUp_ShouldKeepValuesSeparate()
        {
            var imposter = new IHidingIndexerImposter();
            imposter.For(default(IHiddenIndexer))[Arg<int>.Any()].Getter().Returns(1);
            imposter.For(default(IHidingIndexer))[Arg<int>.Any()].Getter().Returns("one");
            var instance = imposter.Instance();

            ((IHiddenIndexer)instance)[0].ShouldBe(1);
            instance[0].ShouldBe("one");
        }
    }
}
