using Imposter.Abstractions;
using Imposter.Tests.Features.ClassImpersonation.Suts;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.ClassImpersonation
{
    public class UninferableGenericMethodsClassImposterTests
    {
        private readonly ClassWithUninferableGenericMethodsImposter _sut =
            new ClassWithUninferableGenericMethodsImposter();

        [Fact]
        public void GivenUseBaseImplementation_WhenMethodWithoutParametersIsCalled_ShouldCallBaseWithTypeArgument()
        {
            _sut.Describe<int>().UseBaseImplementation();

            _sut.Instance().Describe<int>().ShouldBe("Int32");
        }

        [Fact]
        public void GivenUseBaseImplementation_WhenMethodWithTypeParameterOnlyInReturnIsCalled_ShouldCallBaseWithTypeArguments()
        {
            _sut.Convert<int, string>(Arg<int>.Any()).UseBaseImplementation();

            _sut.Instance().Convert<int, string>(7).ShouldBe("7->String");
        }

        [Fact]
        public void GivenUseBaseImplementation_WhenVoidMethodIsCalled_ShouldCallBaseWithTypeArgument()
        {
            _sut.Execute<long>().UseBaseImplementation();
            var instance = _sut.Instance();

            instance.Execute<long>();

            instance.ExecutedTypes.ShouldBe(new[] { "Int64" });
        }
    }
}
