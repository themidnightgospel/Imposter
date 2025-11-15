using System;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Imposter.Tests.Features.MethodImposter;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.ClassImposter
{
    public class ClassMethodBaseImplementationTests
    {
        private readonly MethodSetupFeatureClassSutImposter _classSut =
            new MethodSetupFeatureClassSutImposter();

        [Fact]
        public void GivenClassMethodUseBaseImplementation_WhenInvoked_ThenReturnsBaseResult()
        {
            _classSut.IntSingleParam(Arg<int>.Is(4)).UseBaseImplementation();

            var instance = _classSut.Instance();

            instance.IntSingleParam(4).ShouldBe(8);
        }

        [Fact]
        public void GivenSpecificReturnSetup_WhenCombinedWithBaseImplementation_ThenSpecificWins()
        {
            _classSut.IntSingleParam(Arg<int>.Any()).UseBaseImplementation();
            _classSut.IntSingleParam(Arg<int>.Is(7)).Returns(42);

            var instance = _classSut.Instance();

            instance.IntSingleParam(2).ShouldBe(4);
            instance.IntSingleParam(7).ShouldBe(42);
        }

        [Fact]
        public void GivenVoidMethodUseBaseImplementation_WhenInvoked_ThenRunsBaseAndCallback()
        {
            var callbackArgument = 0;

            _classSut
                .VoidWithSideEffect(Arg<int>.Is(3))
                .UseBaseImplementation()
                .Callback(value => callbackArgument = value);

            var instance = _classSut.Instance();
            instance.VoidWithSideEffect(3);

            instance.VoidSideEffectCount.ShouldBe(3);
            callbackArgument.ShouldBe(3);
        }

        [Fact]
        public async Task GivenAsyncUseBaseImplementation_WhenInvoked_ThenPropagatesBaseResults()
        {
            var sumCallback = 0;
            var labelCallback = string.Empty;

            _classSut
                .SumAsync(Arg<int>.Is(2), Arg<int>.Is(3))
                .UseBaseImplementation()
                .Callback(
                    (int left, int right) =>
                    {
                        sumCallback = left + right;
                        return Task.CompletedTask;
                    }
                );

            _classSut
                .BuildLabelAsync(Arg<string>.Any(), Arg<string>.Any())
                .UseBaseImplementation()
                .Callback(
                    (string prefix, string suffix) =>
                    {
                        labelCallback = prefix + suffix;
                        return Task.CompletedTask;
                    }
                );

            var instance = _classSut.Instance();

            var sumResult = await instance.SumAsync(2, 3);
            var labelResult = await instance.BuildLabelAsync("foo", "bar");

            sumResult.ShouldBe(5);
            labelResult.ShouldBe("foo-bar");
            sumCallback.ShouldBe(5);
            labelCallback.ShouldBe("foobar");
        }

        [Fact]
        public async Task GivenValueTaskUseBaseImplementation_WhenInvoked_ThenReturnsBaseValueAndVerifiesCalls()
        {
            var callbackCount = 0;

            var builder = _classSut
                .BuildLabelAsync(Arg<string>.Any(), Arg<string>.Any())
                .UseBaseImplementation()
                .Callback(
                    (string prefix, string suffix) =>
                    {
                        callbackCount++;
                        return Task.CompletedTask;
                    }
                );

            var instance = _classSut.Instance();

            var first = await instance.BuildLabelAsync("alpha", "beta");
            var second = await instance.BuildLabelAsync("gamma", "delta");

            first.ShouldBe("alpha-beta");
            second.ShouldBe("gamma-delta");
            callbackCount.ShouldBe(2);

            Should.NotThrow(() =>
                _classSut
                    .BuildLabelAsync(Arg<string>.Any(), Arg<string>.Any())
                    .Called(Count.Exactly(2))
            );
        }

        [Fact]
        public void GivenUseBaseImplementation_WhenBaseThrows_ShouldPropagateException()
        {
            _classSut.ThrowingCalculation(Arg<int>.Any()).UseBaseImplementation();

            var instance = _classSut.Instance();

            var ex = Should.Throw<InvalidOperationException>(() => instance.ThrowingCalculation(9));
            ex.Message.ShouldBe("boom:9");
        }

        [Fact]
        public void GivenRefOutMethodUseBaseImplementation_WhenInvoked_ThenPassesThroughAssignments()
        {
            _classSut
                .RefOutWithParams(Arg<int>.Any(), OutArg<int>.Any(), Arg<int[]>.Any())
                .UseBaseImplementation();

            var instance = _classSut.Instance();

            var seed = 2;
            int doubled;
            var result = instance.RefOutWithParams(ref seed, out doubled, 1, 2, 3);

            seed.ShouldBe(8);
            doubled.ShouldBe(16);
            result.ShouldBe(24);
        }

        [Fact]
        public void GivenClassTargetUseBaseImplementation_WhenBaseAvailable_ThenDoesNotThrow()
        {
            _classSut.IntSingleParam(Arg<int>.Any()).UseBaseImplementation();

            var instance = _classSut.Instance();

            Should.NotThrow(() => instance.IntSingleParam(9));
        }

        [Fact]
        public void GivenClassMethodInvocations_WhenVerifyingCalled_ShouldRespectCounts()
        {
            var callVerifier = _classSut.IntSingleParam(Arg<int>.Any());
            var instance = _classSut.Instance();

            instance.IntSingleParam(1);
            instance.IntSingleParam(2);

            Should.NotThrow(() => callVerifier.Called(Count.Exactly(2)));
            Should.Throw<VerificationFailedException>(() => callVerifier.Called(Count.AtLeast(3)));
        }
    }
}
