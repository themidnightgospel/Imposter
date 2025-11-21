using Imposter.Abstractions;
using Imposter.Tests.Features.MethodImpersonation;
using Shouldly;
using Xunit;

[assembly: GenerateImposter(typeof(IMethodParametersWithDefaultSut))]

namespace Imposter.Tests.Features.MethodImpersonation
{
    public class MethodParametersWithDefaultTests
    {
        private readonly IMethodParametersWithDefaultSutImposter _sut =
#if USE_CSHARP14
            IMethodParametersWithDefaultSut.Imposter();
#else
            new IMethodParametersWithDefaultSutImposter();
#endif

        [Fact]
        public void GivenBoolDefaultOptional_WhenInvoked_ShouldRespectDefaultValue()
        {
            _sut.BoolDefault(Arg<bool>.Is(false)).Returns(1);
            _sut.BoolDefault(Arg<bool>.Is(true)).Returns(2);

            _sut.Instance().BoolDefault().ShouldBe(2);
            _sut.Instance().BoolDefault(true).ShouldBe(2);
            _sut.Instance().BoolDefault(false).ShouldBe(1);
        }

        [Fact]
        public void GivenIntDefaultOptional_WhenInvoked_ShouldRespectDefaultValue()
        {
            _sut.IntDefault(Arg<int>.Is(5)).Returns(10);
            _sut.IntDefault(Arg<int>.Is(7)).Returns(14);

            _sut.Instance().IntDefault().ShouldBe(10);
            _sut.Instance().IntDefault(7).ShouldBe(14);
        }

        [Fact]
        public void GivenDoubleDefaultOptional_WhenInvoked_ShouldRespectDefaultValue()
        {
            _sut.DoubleDefault(Arg<double>.Is(2.5)).Returns(25);
            _sut.DoubleDefault(Arg<double>.Is(3.5)).Returns(35);

            _sut.Instance().DoubleDefault().ShouldBe(25);
            _sut.Instance().DoubleDefault(3.5).ShouldBe(35);
        }

        [Fact]
        public void GivenNullableDefaultOptional_WhenInvoked_ShouldRespectDefaultValue()
        {
            _sut.NullableDefault(Arg<string>.Is(val => val == null)).Returns(11);
            _sut.NullableDefault(Arg<string>.Is("value")).Returns(22);

            _sut.Instance().NullableDefault().ShouldBe(11);
            _sut.Instance().NullableDefault("value").ShouldBe(22);
        }

        [Fact]
        public void GivenFloatDefaultOptional_WhenInvoked_ShouldRespectDefaultValue()
        {
            _sut.FloatDefault(Arg<float>.Is(1.25f)).Returns(125);
            _sut.FloatDefault(Arg<float>.Is(2.5f)).Returns(250);

            _sut.Instance().FloatDefault().ShouldBe(125);
            _sut.Instance().FloatDefault(2.5f).ShouldBe(250);
        }

        [Fact]
        public void GivenDecimalDefaultOptional_WhenInvoked_ShouldRespectDefaultValue()
        {
            _sut.DecimalDefault(Arg<decimal>.Is(12.5m)).Returns(1250);
            _sut.DecimalDefault(Arg<decimal>.Is(5.5m)).Returns(550);

            _sut.Instance().DecimalDefault().ShouldBe(1250);
            _sut.Instance().DecimalDefault(5.5m).ShouldBe(550);
        }

        [Fact]
        public void GivenCharDefaultOptional_WhenInvoked_ShouldRespectDefaultValue()
        {
            _sut.CharDefault(Arg<char>.Is('Z')).Returns(26);
            _sut.CharDefault(Arg<char>.Is('A')).Returns(1);

            _sut.Instance().CharDefault().ShouldBe(26);
            _sut.Instance().CharDefault('A').ShouldBe(1);
        }

        [Fact]
        public void GivenEnumDefaultOptional_WhenInvoked_ShouldRespectDefaultValue()
        {
            _sut.EnumDefault(Arg<Issue2Enum>.Is(Issue2Enum.Second)).Returns(20);
            _sut.EnumDefault(Arg<Issue2Enum>.Is(Issue2Enum.First)).Returns(10);

            _sut.Instance().EnumDefault().ShouldBe(20);
            _sut.Instance().EnumDefault(Issue2Enum.First).ShouldBe(10);
        }

        [Fact]
        public void GivenNameofDefaultOptional_WhenInvoked_ShouldRespectDefaultValue()
        {
            _sut.NameDefault(Arg<string>.Is(nameof(IMethodParametersWithDefaultSut))).Returns(100);
            _sut.NameDefault(Arg<string>.Is("other")).Returns(200);

            _sut.Instance().NameDefault().ShouldBe(100);
            _sut.Instance().NameDefault("other").ShouldBe(200);
        }
    }

    public interface IMethodParametersWithDefaultSut
    {
        int BoolDefault(bool flag = true);
        int IntDefault(int value = 5);
        int DoubleDefault(double value = 2.5);
        int NullableDefault(string? text = null);
        int FloatDefault(float value = 1.25f);
        int DecimalDefault(decimal value = 12.5m);
        int CharDefault(char value = 'Z');
        int EnumDefault(Issue2Enum mode = Issue2Enum.Second);

        int NameDefault(string name = nameof(IMethodParametersWithDefaultSut));
    }

    public enum Issue2Enum
    {
        First,
        Second,
        Third,
    }
}
