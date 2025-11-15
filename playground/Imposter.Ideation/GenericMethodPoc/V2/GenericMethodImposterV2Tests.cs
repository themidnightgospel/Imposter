using System.Collections.Generic;
using System.Linq;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Ideation.GenericMethodPoc.V2
{
    public class GenericMethodImposterV2Tests
    {
        private readonly IGenericMethodPocImposter _sut = new IGenericMethodPocImposter();

        private class Animal
        {
            public string Name { get; set; } = string.Empty;
        }

        private class Dog : Animal { }

        [Fact]
        public void GivenSetupUsingDerivedOutType_WhenInvokedThroughBaseType_ShouldAdaptOutValueAndReturnResult()
        {
            _sut.GenericAllRefKind<Dog, string, int, string, string>(
                    OutArg<Dog>.Any(),
                    Arg<string>.Is("ref-value"),
                    Arg<int>.Is(i => i > 0),
                    Arg<string[]>.Any()
                )
                .Returns(
                    (
                        out Dog outValue,
                        ref string refValue,
                        in int inValue,
                        string[] paramsValues
                    ) =>
                    {
                        outValue = new Dog { Name = $"dog-{inValue}" };
                        refValue = $"{refValue}-updated";
                        return $"result-{paramsValues.Length}";
                    }
                );

            var target = _sut.Instance();

            string refArgument = "ref-value";
            var inArgument = 42;

            var result = target.GenericAllRefKind<Animal, string, int, string, string>(
                out var outAnimal,
                ref refArgument,
                in inArgument,
                "one",
                "two"
            );

            result.ShouldBe("result-2");
            refArgument.ShouldBe("ref-value-updated");
            outAnimal.ShouldBeOfType<Dog>();
            outAnimal.Name.ShouldBe("dog-42");
        }

        [Fact]
        public void GivenCallbackSetup_WhenInvoked_ShouldRunForEveryInvocation()
        {
            var callbackInvocations =
                new List<(
                    int OutValue,
                    string RefValue,
                    int InValue,
                    IReadOnlyList<string> Params
                )>();

            _sut.GenericAllRefKind<int, string, int, string, int>(
                    OutArg<int>.Any(),
                    Arg<string>.Any(),
                    Arg<int>.Any(),
                    Arg<string[]>.Any()
                )
                .Returns(
                    (
                        out int outValue,
                        ref string refValue,
                        in int inValue,
                        string[] paramsValues
                    ) =>
                    {
                        outValue = inValue;
                        refValue = $"{refValue}-from-result";
                        return paramsValues.Length;
                    }
                )
                .Callback(
                    (
                        out int callbackOut,
                        ref string callbackRef,
                        in int callbackIn,
                        string[] callbackParams
                    ) =>
                    {
                        callbackOut = callbackIn * 10;
                        callbackRef = $"{callbackRef}-from-callback";
                        callbackInvocations.Add(
                            (callbackOut, callbackRef, callbackIn, callbackParams.ToArray())
                        );
                    }
                );

            var target = _sut.Instance();

            var firstInValue = 3;
            var firstRef = "first";
            var firstResult = target.GenericAllRefKind<int, string, int, string, int>(
                out var firstOut,
                ref firstRef,
                in firstInValue,
                "alpha"
            );

            var secondInValue = 5;
            var secondRef = "second";
            var secondResult = target.GenericAllRefKind<int, string, int, string, int>(
                out var secondOut,
                ref secondRef,
                in secondInValue,
                "beta",
                "gamma"
            );

            firstResult.ShouldBe(1);
            secondResult.ShouldBe(2);
            firstOut.ShouldBe(30);
            secondOut.ShouldBe(50);
            firstRef.ShouldBe("first-from-result-from-callback");
            secondRef.ShouldBe("second-from-result-from-callback");

            callbackInvocations.Count.ShouldBe(2);
            callbackInvocations[0].OutValue.ShouldBe(30);
            callbackInvocations[0].RefValue.ShouldBe("first-from-result-from-callback");
            callbackInvocations[0].InValue.ShouldBe(3);
            callbackInvocations[0].Params.ShouldBe(new[] { "alpha" });

            callbackInvocations[1].OutValue.ShouldBe(50);
            callbackInvocations[1].RefValue.ShouldBe("second-from-result-from-callback");
            callbackInvocations[1].InValue.ShouldBe(5);
            callbackInvocations[1].Params.ShouldBe(new[] { "beta", "gamma" });
        }

        [Fact]
        public void GivenMultipleInvocationSetups_WhenInvokedMoreTimesThanConfigured_ShouldReuseLastSetup()
        {
            _sut.GenericAllRefKind<int, string, int, string, long>(
                    OutArg<int>.Any(),
                    Arg<string>.Any(),
                    Arg<int>.Any(),
                    Arg<string[]>.Any()
                )
                .Returns(10L)
                .Then(default)
                .Returns(20L)
                .Then(default)
                .Returns(30L);

            var target = _sut.Instance();
            var results = new List<long>();

            for (var i = 0; i < 4; i++)
            {
                var refValue = "value";
                var inValue = 1;
                results.Add(
                    target.GenericAllRefKind<int, string, int, string, long>(
                        out _,
                        ref refValue,
                        in inValue,
                        "only"
                    )
                );
            }

            results.ShouldBe(new[] { 10L, 20L, 30L, 30L });
        }

        [Fact]
        public void GivenInvocationVerification_WhenCountsDoNotMatch_ShouldThrowVerificationFailedException()
        {
            var builder = _sut.GenericAllRefKind<int, string, int, string, long>(
                OutArg<int>.Any(),
                Arg<string>.Is("tracked"),
                Arg<int>.Any(),
                Arg<string[]>.Any()
            );

            builder.Returns(99L);

            var target = _sut.Instance();

            var refValue = "tracked";
            var inValue = 7;
            target.GenericAllRefKind<int, string, int, string, long>(
                out _,
                ref refValue,
                in inValue,
                "param"
            );

            builder.Called(Count.Once());

            Should.Throw<VerificationFailedException>(() => builder.Called(Count.Exactly(2)));
        }
    }
}
