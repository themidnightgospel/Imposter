using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.MethodImposter
{
    public class ClassMethodBaseImplementationTests
    {
        private readonly MethodSetupFeatureClassSutImposter _classSut = new MethodSetupFeatureClassSutImposter();

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
        public void GivenBaseImplementationFollowedByReturns_WhenConfigured_ThenLastCallWins()
        {
            _classSut.IntSingleParam(Arg<int>.Is(5))
                .UseBaseImplementation()
                .Returns(11);

            var instance = _classSut.Instance();
            
            instance.IntSingleParam(5).ShouldBe(11);
        }

        [Fact]
        public void GivenReturnsFollowedByBaseImplementation_WhenConfigured_ThenLastCallWins()
        {
            _classSut.IntSingleParam(Arg<int>.Is(6))
                .Returns(99)
                .UseBaseImplementation();

            var instance = _classSut.Instance();

            instance.IntSingleParam(6).ShouldBe(12);
        }

        [Fact]
        public void GivenVoidMethodUseBaseImplementation_WhenInvoked_ThenRunsBaseAndCallback()
        {
            var callbackArgument = 0;

            _classSut.VoidWithSideEffect(Arg<int>.Is(3))
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

            _classSut.SumAsync(Arg<int>.Is(2), Arg<int>.Is(3))
                .UseBaseImplementation()
                .Callback((int left, int right) =>
                {
                    sumCallback = left + right;
                    return Task.CompletedTask;
                });

            _classSut.BuildLabelAsync(Arg<string>.Any(), Arg<string>.Any())
                .UseBaseImplementation()
                .Callback((string prefix, string suffix) =>
                {
                    labelCallback = prefix + suffix;
                    return Task.CompletedTask;
                });

            var instance = _classSut.Instance();

            var sumResult = await instance.SumAsync(2, 3);
            var labelResult = await instance.BuildLabelAsync("foo", "bar");

            sumResult.ShouldBe(5);
            labelResult.ShouldBe("foo-bar");
            sumCallback.ShouldBe(5);
            labelCallback.ShouldBe("foobar");
        }

        [Fact]
        public void GivenRefOutMethodUseBaseImplementation_WhenInvoked_ThenPassesThroughAssignments()
        {
            _classSut.RefOutWithParams(
                    Arg<int>.Any(),
                    OutArg<int>.Any(),
                    Arg<int[]>.Any())
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
    }
}
