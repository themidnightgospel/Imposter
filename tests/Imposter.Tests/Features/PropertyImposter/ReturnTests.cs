using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.PropertyImposter
{
    public class ReturnTests
    {
        private readonly IPropertySetupSutImposter _sut = new IPropertySetupSutImposter();

        [Fact]
        public void GivenSetupToReturnAValue_WhenPropertyIsAccessed_ShouldReturnSetupValue()
        {
            _sut.Age.Getter().Returns(33);

            _sut.Instance().Age.ShouldBe(33);
        }

        [Fact]
        public void GivenSetupToReturnAValue_WhenPropertyIsAccessedMultipleTimes_ShouldReturnSetupValue()
        {
            _sut.Age.Getter().Returns(33);

            _sut.Instance().Age.ShouldBe(33);
            _sut.Instance().Age.ShouldBe(33);
            _sut.Instance().Age.ShouldBe(33);
            _sut.Instance().Age.ShouldBe(33);
        }

        [Fact]
        public void GivenNoSetup_WhenPropertyIsAccessed_ShouldActAsNormalProperty()
        {
            var sutInstance = _sut.Instance();

            sutInstance.Age.ShouldBe(0);

            sutInstance.Age = 33;
            sutInstance.Age.ShouldBe(33);

            sutInstance.Age = 44;
            sutInstance.Age.ShouldBe(44);
        }

        [Fact]
        public void GivenFunctionSetup_WhenPropertyIsAccessedMultipleTimes_ShouldCallFunctionEachTime()
        {
            var counter = 0;
            _sut.Age.Getter().Returns(() => ++counter);

            _sut.Instance().Age.ShouldBe(1);
            _sut.Instance().Age.ShouldBe(2);
        }

        [Fact]
        public void GivenMixedFunctionAndValueSetup_WhenPropertyIsAccessedSequentially_ShouldWorkInSequence()
        {
            _sut.Age.Getter().Returns(10).Then().Returns(() => 20).Then().Returns(30);

            _sut.Instance().Age.ShouldBe(10);
            _sut.Instance().Age.ShouldBe(20);
            _sut.Instance().Age.ShouldBe(30);
        }

        [Fact]
        public void GivenSequentialSetup_WhenMultipleCallsMade_ShouldReturnInSequence()
        {
            _sut.Age.Getter().Returns(10).Then().Returns(20).Then().Returns(30);

            _sut.Instance().Age.ShouldBe(10);
            _sut.Instance().Age.ShouldBe(20);
            _sut.Instance().Age.ShouldBe(30);
        }

        [Fact]
        public void GivenSequentialSetupExhausted_WhenPropertyIsAccessed_ShouldReturnLastValue()
        {
            _sut.Age.Getter().Returns(10).Then().Returns(20);

            _sut.Instance().Age.ShouldBe(10);
            _sut.Instance().Age.ShouldBe(20);
            _sut.Instance().Age.ShouldBe(20); // Should repeat last value
            _sut.Instance().Age.ShouldBe(20); // Should repeat last value
        }
    }
}
