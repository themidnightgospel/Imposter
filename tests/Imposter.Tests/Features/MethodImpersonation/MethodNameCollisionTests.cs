using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.MethodImpersonation
{
    public class MethodNameCollisionTests
    {
        private readonly MethodNameCollisionServiceImposter _sut =
            new MethodNameCollisionServiceImposter();

        [Fact]
        public void Given_CollidingInvocationParameters_When_Invoked_Should_ForwardArgumentsAndVerifyCalls()
        {
            var callbackArguments = (0, "", 0);
            _sut.Invoke(3, "display", 7)
                .Returns(
                    (int behavior, string display, int imposter) =>
                        behavior + display.Length + imposter
                )
                .Callback(
                    (int behavior, string display, int imposter) =>
                        callbackArguments = (behavior, display, imposter)
                );

            _sut.Instance().Invoke(3, "display", 7).ShouldBe(17);
            callbackArguments.ShouldBe((3, "display", 7));
            _sut.Invoke(3, "display", 7).Called(Count.Once());
            _sut.Invoke(4, "display", 7).Called(Count.Never());
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Given_ExplicitMode_When_CollidingMethodHasNoReturnSetup_Should_ReportActualMethod(
            bool addCallback
        )
        {
            var sut = new MethodNameCollisionServiceImposter(ImposterMode.Explicit);
            if (addCallback)
            {
                sut.Invoke(0, "user value", 2).Callback((int _, string _, int _) => { });
            }

            var exception = Should.Throw<MissingImposterException>(() =>
                sut.Instance().Invoke(0, "user value", 2)
            );

            exception.MethodName.ShouldNotBeNull();
            exception.MethodName.ShouldContain("MethodNameCollisionService.Invoke");
            exception.MethodName.ShouldNotBe("user value");
        }

        [Fact]
        public void Given_CollidingInvocationParameters_When_UsingBaseImplementation_Should_ForwardArguments()
        {
            _sut.Invoke(3, "base", 7).UseBaseImplementation();

            _sut.Instance().Invoke(3, "base", 7).ShouldBe(14);
            _sut.Invoke(3, "base", 7).Called(Count.Once());
        }

        [Fact]
        public void Given_CollidingRefAdapterLocal_When_InvokedWithCompatibleType_Should_CopyBackValue()
        {
            _sut.AdaptRef<string>(5, 7)
                .Returns(
                    (ref int value, int increment) =>
                    {
                        value += increment;
                        return "result";
                    }
                );
            var value = 5;

            _sut.Instance().AdaptRef<object>(ref value, 7).ShouldBe("result");

            value.ShouldBe(12);
            _sut.AdaptRef<string>(5, 7).Called(Count.Once());
        }

        [Fact]
        public void Given_CollidingOutAdapterLocal_When_InvokedWithCompatibleType_Should_CopyBackValue()
        {
            _sut.AdaptOut<string>(OutArg<int>.Any(), 7)
                .Returns(
                    (out int value, int input) =>
                    {
                        value = input;
                        return "result";
                    }
                );

            _sut.Instance().AdaptOut<object>(out var value, 7).ShouldBe("result");

            value.ShouldBe(7);
            _sut.AdaptOut<string>(OutArg<int>.Any(), 7).Called(Count.Once());
        }

        [Fact]
        public void Given_CalledTypeParameter_When_Invoked_Should_SupportVerification()
        {
            _sut.Verify<string>("input").Returns("output");

            _sut.Instance().Verify("input").ShouldBe("output");

            _sut.Verify<string>("input").Called(Count.Once());
            _sut.Verify<string>("input").CallCount().ShouldBe(1);
        }
    }
}
