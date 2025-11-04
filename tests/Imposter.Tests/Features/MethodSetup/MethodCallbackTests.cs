using System.Collections.Generic;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.MethodSetup
{
    public class MethodCallbackTests
    {
        private readonly IMethodSetupFeatureSutImposter _sut = new IMethodSetupFeatureSutImposter();

        [Fact]
        public void GivenNonGenericCallback_WhenConfigured_ShouldRunAfterResultAndLastWins()
        {
            var stages = new List<string>();

            _sut
                .IntNoParams()
                .Returns(() =>
                {
                    stages.Add("return");
                    return 42;
                })
                .Callback(() => stages.Add("first"))
                .Callback(() => stages.Add("second"));

            _sut.Instance().IntNoParams().ShouldBe(42);
            stages.ShouldBe(new[] { "return", "second" });
        }

        [Fact]
        public void GivenCallbackWithoutExplicitReturn_ShouldStillExecuteAfterDefaultResult()
        {
            var callbackInvocations = 0;

            _sut
                .IntSingleParam(Arg<int>.Any())
                .Callback(_ => callbackInvocations++);

            _sut.Instance().IntSingleParam(123).ShouldBe(default);
            callbackInvocations.ShouldBe(1);
        }

        [Fact]
        public void GivenGenericCallbackChain_WhenConfigured_ShouldInvokeAllCallbacksInOrder()
        {
            var stages = new List<string>();

            _sut.GenericAllRefKind<int, string, double, bool, int>(
                    OutArg<int>.Any(),
                    Arg<string>.Any(),
                    Arg<double>.Any(),
                    Arg<bool[]>.Any()
                )
                .Returns((out int o, ref string r, in double input, bool[] values) =>
                {
                    o = 5;
                    stages.Add("return");
                    return 99;
                })
                .Callback((out int callbackOut, ref string callbackRef, in double callbackIn, bool[] callbackParams) =>
                {
                    callbackOut = 5;
                    stages.Add("callback-1");
                })
                .Callback((out int callbackOut, ref string callbackRef, in double callbackIn, bool[] callbackParams) =>
                {
                    callbackOut = 5;
                    stages.Add("callback-2");
                });

            string refValue = "seed";
            double input = 5.0;
            var paramsArg = new[] { true };

            var result = _sut.Instance().GenericAllRefKind<int, string, double, bool, int>(out var outValue, ref refValue, in input, paramsArg);

            result.ShouldBe(99);
            outValue.ShouldBe(5);
            stages.ShouldBe(new[] { "return", "callback-1", "callback-2" });
    }
}
}
