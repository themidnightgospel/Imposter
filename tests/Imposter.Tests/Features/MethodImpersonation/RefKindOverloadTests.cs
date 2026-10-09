using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.MethodImpersonation
{
    public class RefKindOverloadTests
    {
        private readonly IRefKindOverloadSutImposter _sut = new IRefKindOverloadSutImposter();

        [Fact]
        public void GivenSetupsForBothOverloads_WhenTheByValueOverloadIsInvoked_ShouldUseTheFirstSetup()
        {
            _sut.Count(Arg<int>.Any()).Returns(1);
            _sut.Count_1(Arg<int>.Any()).Returns(2);

            _sut.Instance().Count(5).ShouldBe(1);
        }

        [Fact]
        public void GivenSetupsForBothOverloads_WhenTheInOverloadIsInvoked_ShouldUseTheNumberedSetup()
        {
            _sut.Count(Arg<int>.Any()).Returns(1);
            _sut.Count_1(Arg<int>.Any()).Returns(2);
            var value = 5;

            _sut.Instance().Count(in value).ShouldBe(2);
        }

        [Fact]
        public void GivenClassOverloads_WhenTheNumberedSetupIsUsed_ShouldConfigureTheOverloadDeclaredSecond()
        {
            var imposter = new RefKindOverloadClassImposter();
            imposter.Count_1(Arg<int>.Any()).Returns(2);
            var value = 5;

            imposter.Instance().Count(in value).ShouldBe(2);
        }

        [Fact]
        public void GivenOverloadsDeclaredByDifferentInterfaces_WhenTheDerivedViewSetsUpCount_ShouldConfigureItsOwnOverload()
        {
            var imposter = new IDerivedRefKindOverloadSutImposter();
            imposter.For(default(IDerivedRefKindOverloadSut)).Count(Arg<int>.Any()).Returns(2);
            var value = 5;

            imposter.Instance().Count(in value).ShouldBe(2);
        }

        [Fact]
        public void GivenSetupThroughTheView_WhenTheInOverloadIsInvoked_ShouldUseIt()
        {
            _sut.For(default(IRefKindOverloadSut)).Count_1(Arg<int>.Any()).Returns(3);
            var value = 5;

            _sut.Instance().Count(in value).ShouldBe(3);
        }
    }
}
