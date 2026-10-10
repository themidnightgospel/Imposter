using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.MethodImpersonation
{
    // Shouldly's extension methods can't take a dynamic, so the tests cast what members return.
    public class DynamicMemberTests
    {
        private readonly IDynamicMemberSutImposter _sut = new IDynamicMemberSutImposter();

        [Fact]
        public void GivenReturnsDelegate_WhenInvoked_ShouldReturnItsDynamicResult()
        {
            _sut.Echo(Arg<dynamic>.Any()).Returns(value => value + 1);

            ((int)_sut.Instance().Echo(1)).ShouldBe(2);
        }

        [Fact]
        public void GivenInvocations_WhenVerified_ShouldMatchTheirDynamicArguments()
        {
            _sut.Instance().Echo("first");
            _sut.Instance().Echo("second");

            _sut.Echo(Arg<dynamic>.Is("first")).Called(Count.Once());
        }

        [Fact]
        public void GivenGenericMethodCallback_WhenInvoked_ShouldPassItTheDynamicArgument()
        {
            object? seen = null;
            _sut.Use<int>(Arg<int>.Any(), Arg<dynamic>.Any())
                .Callback((first, second) => seen = second);

            _sut.Instance().Use(1, "second");

            seen.ShouldBe("second");
        }

        [Fact]
        public void GivenBaseImplementation_WhenInvoked_ShouldCallTheBaseWithTheDynamicArgument()
        {
            var sut = new DynamicMemberClassImposter();
            sut.Describe(Arg<dynamic>.Any()).UseBaseImplementation();

            ((string)sut.Instance().Describe(1)).ShouldBe("base 1");
        }

        [Fact]
        public void GivenNoSetup_WhenDynamicPropertyIsSet_ShouldReturnTheValue()
        {
            var sut = new DynamicMemberClassImposter();

            sut.Instance().Value = 5;

            ((int)sut.Instance().Value!).ShouldBe(5);
        }

        [Fact]
        public void GivenIndexerBaseImplementation_WhenInvoked_ShouldCallTheBaseWithTheDynamicKey()
        {
            var sut = new DynamicMemberClassImposter();
            sut[Arg<dynamic>.Any()].Getter().UseBaseImplementation();

            ((string)sut.Instance()[2]).ShouldBe("key 2");
        }

        [Fact]
        public void GivenIndexerReturnsDelegate_WhenInvoked_ShouldPassItTheDynamicKey()
        {
            var sut = new DynamicMemberClassImposter();
            sut[Arg<dynamic>.Any()].Getter().Returns(key => key + 1);

            ((int)sut.Instance()[2]).ShouldBe(3);
        }
    }
}
