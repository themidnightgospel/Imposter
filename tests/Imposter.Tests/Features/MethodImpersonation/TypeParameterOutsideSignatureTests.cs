using System.Collections.Generic;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.MethodImpersonation
{
    public class TypeParameterOutsideSignatureTests
    {
        private readonly ITypeParameterOutsideSignatureSutImposter _sut =
            new ITypeParameterOutsideSignatureSutImposter();

        [Fact]
        public void GivenReturnsSetup_WhenCalledWithAnotherTypeArgument_ShouldReturnDefault()
        {
            _sut.Name<string>().Returns("string setup");

            _sut.Instance().Name<int>().ShouldBeNull();
            _sut.Instance().Name<string>().ShouldBe("string setup");
        }

        [Fact]
        public void GivenCallbackSetup_WhenCalledWithAnotherTypeArgument_ShouldNotRunIt()
        {
            var ran = false;
            _sut.Tag<string>(Arg<int>.Any()).Callback(_ => ran = true);

            _sut.Instance().Tag<int>(1);
            ran.ShouldBeFalse();

            _sut.Instance().Tag<string>(1);
            ran.ShouldBeTrue();
        }

        [Fact]
        public void GivenCallWithOneTypeArgument_WhenVerifyingAnother_ShouldNotCountIt()
        {
            _sut.Instance().Tag<int>(1);

            _sut.Tag<string>(Arg<int>.Any()).CallCount().ShouldBe(0);
            _sut.Tag<int>(Arg<int>.Any()).CallCount().ShouldBe(1);
        }

        [Fact]
        public void GivenMethodWithoutParametersOrResult_WhenCalledWithAnotherTypeArgument_ShouldKeepSetupsAndCallsApart()
        {
            var ran = false;
            _sut.Register<string>().Callback(() => ran = true);

            _sut.Instance().Register<int>();

            ran.ShouldBeFalse();
            _sut.Register<string>().CallCount().ShouldBe(0);
            _sut.Register<int>().CallCount().ShouldBe(1);
        }

        [Fact]
        public void GivenSetupForABaseTypeArgument_WhenTheOtherTypeArgumentDiffers_ShouldNotApplyIt()
        {
            var received = new List<object>();
            _sut.Put<object, int>(Arg<object>.Any()).Callback(value => received.Add(value));

            _sut.Instance().Put<string, int>("same key type");
            _sut.Instance().Put<string, long>("other key type");

            received.ShouldBe(new object[] { "same key type" });
        }

        [Fact]
        public void GivenSetupForABaseTypeArgument_WhenOnlyAConstraintRelatesTheOtherOne_ShouldNotApplyIt()
        {
            var received = new List<object>();
            _sut.Relate<object, string>(Arg<string>.Any()).Callback(value => received.Add(value));

            _sut.Instance().Relate<string, string>("other base type");
            _sut.Instance().Relate<object, string>("same types");

            received.ShouldBe(new object[] { "same types" });
        }
    }
}
