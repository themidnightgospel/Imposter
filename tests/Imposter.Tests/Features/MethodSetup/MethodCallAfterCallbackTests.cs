using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Imposter.CodeGenerator.Tests.Shared;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.MethodSetup
{
    public class MethodCallAfterCallbackTests
    {
        private readonly IMethodSetupFeatureSutImposter _sut = new IMethodSetupFeatureSutImposter();

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void GivenVoidMethodSetupWithCallAfter_WhenMethodIsInvoked_ShouldInvokeCallback(int invocationCount)
        {
            var callAfterCallbackInvokedCount = 0;

            _sut
                .VoidNoParams()
                .CallAfter(() => ++callAfterCallbackInvokedCount);

            for (var i = 0; i < invocationCount; i++)
            {
                _sut.Instance().VoidNoParams();
            }

            callAfterCallbackInvokedCount.ShouldBe(invocationCount);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void GivenIntMethodSetupWithCallAfter_WhenMethodIsInvoked_ShouldInvokeCallback(int invocationCount)
        {
            var callAfterCallbackInvokedCount = 0;

            _sut
                .IntNoParams()
                .CallAfter(() => ++callAfterCallbackInvokedCount);

            for (var i = 0; i < invocationCount; i++)
            {
                _sut.Instance().IntNoParams();
            }

            callAfterCallbackInvokedCount.ShouldBe(invocationCount);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void GivenSingleParamMethodSetupWithCallAfter_WhenMethodIsInvoked_ShouldInvokeCallbackWithParameters(int invocationCount)
        {
            var callAfterCallbackInvokedCount = 0;
            var parametersPassedToCallback = new List<int>();

            _sut
                .IntSingleParam(Arg<int>.Any())
                .CallAfter(age =>
                {
                    callAfterCallbackInvokedCount++;
                    parametersPassedToCallback.Add(age);
                });

            for (var i = 0; i < invocationCount; i++)
            {
                _sut.Instance().IntSingleParam(42 + i);
            }

            callAfterCallbackInvokedCount.ShouldBe(invocationCount);
            parametersPassedToCallback.ShouldBe(Enumerable.Range(42, invocationCount).ToList());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void GivenMultipleParamsMethodSetupWithCallAfter_WhenMethodIsInvoked_ShouldInvokeCallbackWithParameters(int invocationCount)
        {
            var callAfterCallbackInvokedCount = 0;

            var passedAgeParametersToCallback = new List<int>();
            var passedNameParametersToCallback = new List<string>();
            var passedRegexParametersToCallback = new List<Regex>();

            _sut
                .IntParams(Arg<int>.Any(), Arg<string>.Any(), Arg<Regex>.Any())
                .CallAfter((age, name, regex) =>
                {
                    callAfterCallbackInvokedCount++;

                    passedAgeParametersToCallback.Add(age);
                    passedNameParametersToCallback.Add(name);
                    passedRegexParametersToCallback.Add(regex);
                });

            for (var i = 0; i < invocationCount; i++)
            {
                var regex = new Regex($"abc{i}");
                _sut.Instance().IntParams(i, $"name{i}", regex);
            }

            callAfterCallbackInvokedCount.ShouldBe(invocationCount);
            passedAgeParametersToCallback.ShouldBe(Enumerable.Range(0, invocationCount).ToList());
            passedNameParametersToCallback.ShouldBe(Enumerable.Range(0, invocationCount).Select(i => $"name{i}").ToList());
            passedRegexParametersToCallback.Select(r => r.ToString()).ShouldBe(Enumerable.Range(0, invocationCount).Select(i => $"abc{i}"));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void GivenOutParamMethodSetupWithCallAfter_WhenCallbackSetsOutParameter_ShouldPropagateOutParameterValue(int invocationCount)
        {
            var callAfterCallbackInvokedCount = 0;

            _sut
                .IntOutParam(OutArg<int>.Any())
                .CallAfter((out int outValue) =>
                {
                    outValue = 123;
                    callAfterCallbackInvokedCount++;
                });

            for (var i = 0; i < invocationCount; i++)
            {
                _sut.Instance().IntOutParam(out var outVal);
                outVal.ShouldBe(123);
            }

            callAfterCallbackInvokedCount.ShouldBe(invocationCount);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void GivenRefParamMethodSetupWithCallAfter_WhenMethodIsInvoked_ShouldInvokeCallbackAndModifyRefValue(int invocationCount)
        {
            var callAfterCallbackInvokedCount = 0;
            var refParametersPassedToCallback = new List<int>();

            _sut
                .IntRefParam(Arg<int>.Any())
                .CallAfter((ref int refParameter) =>
                {
                    callAfterCallbackInvokedCount++;
                    refParametersPassedToCallback.Add(refParameter);
                    refParameter += 5;
                });

            for (var i = 0; i < invocationCount; i++)
            {
                var refValue = 10 + i;
                _sut.Instance().IntRefParam(ref refValue);
                refValue.ShouldBe(10 + i + 5);
            }

            callAfterCallbackInvokedCount.ShouldBe(invocationCount);
            refParametersPassedToCallback.ShouldBe(Enumerable.Range(10, invocationCount).ToList());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void GivenParamsArrayMethodSetupWithCallAfter_WhenMethodIsInvoked_ShouldInvokeCallbackWithCorrectParameters(int invocationCount)
        {
            var callAfterCallbackInvokedCount = 0;
            var paramsParameterPassedToCallback = new List<string[]>();

            _sut
                .IntParamsParam(Arg<string[]>.Any())
                .CallAfter(arr =>
                {
                    callAfterCallbackInvokedCount++;
                    paramsParameterPassedToCallback.Add(arr);
                });

            for (var i = 0; i < invocationCount; i++)
            {
                var inputArr = new[] { $"a{i}", $"b{i}" };
                _sut.Instance().IntParamsParam(inputArr);
            }

            callAfterCallbackInvokedCount.ShouldBe(invocationCount);
            paramsParameterPassedToCallback.ShouldBe(Enumerable.Range(0, invocationCount).Select(i => new[] { $"a{i}", $"b{i}" }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void GivenInParamMethodSetupWithCallAfter_WhenMethodIsInvoked_ShouldInvokeCallbackWithCorrectParameters(int invocationCount)
        {
            var callAfterCallbackInvokedCount = 0;
            var stringList = new List<string>();

            _sut
                .IntInParam(Arg<string>.Any())
                .CallAfter((in string s) =>
                {
                    callAfterCallbackInvokedCount++;
                    stringList.Add(s);
                });

            for (var i = 0; i < invocationCount; i++)
            {
                _sut.Instance().IntInParam($"in{i}");
            }

            callAfterCallbackInvokedCount.ShouldBe(invocationCount);
            stringList.ShouldBe(Enumerable.Range(0, invocationCount).Select(i => $"in{i}").ToList());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void GivenAllRefKindsMethodSetupWithCallAfter_WhenMethodIsInvoked_ShouldInvokeCallbackWithCorrectParameters(int invocationCount)
        {
            var callAfterCallbackInvokedCount = 0;
            var outList = new List<int>();
            var refList = new List<int>();
            var inList = new List<int>();
            var strList = new List<string>();
            var paramsList = new List<string[]>();

            _sut
                .IntAllRefKinds(OutArg<int>.Any(), Arg<int>.Any(), Arg<int>.Any(), Arg<string>.Any(), Arg<string[]>.Any())
                .CallAfter((out int o, ref int r, in int i, string s, string[] p) =>
                {
                    o = 999;
                    r += 1000;
                    callAfterCallbackInvokedCount++;
                    outList.Add(o);
                    refList.Add(r);
                    inList.Add(i);
                    strList.Add(s);
                    paramsList.Add(p);
                });

            for (var i = 0; i < invocationCount; i++)
            {
                var refVal = 20 + i;
                var inVal = 100 + i;
                var str = $"val-{i}";
                var arr = new[] { $"p{i}-1", $"p{i}-2" };
                _sut.Instance().IntAllRefKinds(out var outVal, ref refVal, in inVal, str, arr);
                outVal.ShouldBe(999);
                refVal.ShouldBe(20 + i + 1000);
            }

            callAfterCallbackInvokedCount.ShouldBe(invocationCount);
            outList.ShouldBe(Enumerable.Repeat(999, invocationCount));
            refList.ShouldBe(Enumerable.Range(20, invocationCount).Select(r => r + 1000).ToList());
            inList.ShouldBe(Enumerable.Range(100, invocationCount).ToList());
            strList.ShouldBe(Enumerable.Range(0, invocationCount).Select(i => $"val-{i}"));
            paramsList.ShouldBe(Enumerable.Range(0, invocationCount).Select(i => new[] { $"p{i}-1", $"p{i}-2" }), ignoreOrder: false);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void GivenGenericSingleParamMethodSetupWithCallAfter_WhenMethodIsInvoked_ShouldInvokeCallbackWithCorrectParameter(int invocationCount)
        {
            var callAfterCallbackInvokedCount = 0;
            var passedParameterToCallback = new List<int>();

            _sut
                .GenericSingleParam<int>(Arg<int>.Any())
                .CallAfter(value =>
                {
                    callAfterCallbackInvokedCount++;
                    passedParameterToCallback.Add(value);
                });

            for (var i = 0; i < invocationCount; i++)
            {
                _sut.Instance().GenericSingleParam<int>(100 + i);
            }

            callAfterCallbackInvokedCount.ShouldBe(invocationCount);
            passedParameterToCallback.ShouldBe(Enumerable.Range(100, invocationCount).ToList());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(4)]
        public void GivenGenericOutParamMethodSetupWithCallAfter_WhenMethodIsInvoked_ShouldInvokeCallbackAndPropagateValue(int invocationCount)
        {
            var callAfterCallbackInvokedCount = 0;

            _sut
                .GenericOutParam<string, int>(OutArg<string>.Any())
                .CallAfter((out string s) =>
                {
                    s = "hello";
                    callAfterCallbackInvokedCount++;
                });

            for (var i = 0; i < invocationCount; i++)
            {
                _sut.Instance().GenericOutParam<string, int>(out var val);
                val.ShouldBe("hello");
            }

            callAfterCallbackInvokedCount.ShouldBe(invocationCount);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(4)]
        public void GivenGenericRefParamMethodSetupWithCallAfter_WhenMethodIsInvoked_ShouldInvokeCallbackAndModifyValue(int invocationCount)
        {
            var callAfterCallbackInvokedCount = 0;
            var refParams = new List<double>();

            _sut
                .GenericRefParam<double, string>(Arg<double>.Any())
                .CallAfter((ref double d) =>
                {
                    callAfterCallbackInvokedCount++;
                    refParams.Add(d);
                    d += 0.1;
                });

            for (var i = 0; i < invocationCount; i++)
            {
                var v = 1.1 + i;
                _sut.Instance().GenericRefParam<double, string>(ref v);
                v.ShouldBe(1.1 + i + 0.1, 0.00001);
            }

            callAfterCallbackInvokedCount.ShouldBe(invocationCount);
            refParams.ShouldBe(Enumerable.Range(0, invocationCount).Select(i => 1.1 + i).ToList());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        public void GivenGenericParamsMethodSetupWithCallAfter_WhenMethodIsInvoked_ShouldInvokeCallbackWithCorrectParameters(int invocationCount)
        {
            var callAfterCallbackInvokedCount = 0;
            var allArrays = new List<string[]>();

            _sut
                .GenericParamsParam<string, int>(Arg<string[]>.Any())
                .CallAfter(arr =>
                {
                    callAfterCallbackInvokedCount++;
                    allArrays.Add(arr);
                });

            for (var i = 0; i < invocationCount; i++)
            {
                var values = new[] { $"str{i}_1", $"str{i}_2" };
                _sut.Instance().GenericParamsParam<string, int>(values);
            }

            callAfterCallbackInvokedCount.ShouldBe(invocationCount);
            allArrays.ShouldBe(Enumerable.Range(0, invocationCount).Select(i => new[] { $"str{i}_1", $"str{i}_2" }), ignoreOrder: false);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GivenGenericAllRefKindsMethodSetupWithCallAfter_WhenMethodIsInvoked_ShouldInvokeCallbackWithCorrectParameters(int invocationCount)
        {
            var callAfterCallbackInvokedCount = 0;
            var outParams = new List<int>();
            var refParams = new List<string>();
            var inParams = new List<double>();
            var paramsParams = new List<bool[]>();

            _sut
                .GenericAllRefKind<int, string, double, bool, long>(
                    OutArg<int>.Any(),
                    Arg<string>.Any(),
                    Arg<double>.Any(),
                    Arg<bool[]>.Any()
                )
                .CallAfter((out int o, ref string r, in double i, bool[] p) =>
                {
                    o = 111;
                    r += "-after";
                    callAfterCallbackInvokedCount++;
                    outParams.Add(o);
                    refParams.Add(r);
                    inParams.Add(i);
                    paramsParams.Add(p);
                });

            for (var idx = 0; idx < invocationCount; idx++)
            {
                string refVal = $"s{idx}";
                double inVal = idx + 0.5;
                bool[] arr = { idx % 2 == 0, idx % 3 == 0 };
                _sut.Instance().GenericAllRefKind<int, string, double, bool, long>(
                    out var refValOut,
                    ref refVal,
                    in inVal,
                    arr
                );
                refValOut.ShouldBe(111);
                refVal.ShouldBe($"s{idx}-after");
            }

            callAfterCallbackInvokedCount.ShouldBe(invocationCount);
            outParams.ShouldBe(Enumerable.Repeat(111, invocationCount));
            refParams.ShouldBe(Enumerable.Range(0, invocationCount).Select(idx => $"s{idx}-after").ToList());
            inParams.ShouldBe(Enumerable.Range(0, invocationCount).Select(idx => idx + 0.5).ToList());
            paramsParams.ShouldBe(Enumerable.Range(0, invocationCount).Select(idx => new[] { idx % 2 == 0, idx % 3 == 0 }), ignoreOrder: false);
        }

        [Fact]
        public void GivenGenericSingleParamSetupWithDerivedType_WhenMethodIsInvokedWithDerivedType_ShouldInvokeCallback()
        {
            var callAfterCallbackInvokedCount = 0;
            var captured = new List<IAnimal>();

            _sut
                .GenericSingleParam<IAnimal>(Arg<IAnimal>.Any())
                .CallAfter(animal =>
                {
                    callAfterCallbackInvokedCount++;
                    captured.Add(animal);
                });

            _sut.Instance().GenericSingleParam<Cat>(new Cat("cat"));
            _sut.Instance().GenericSingleParam<Dog>(new Dog("dog"));

            callAfterCallbackInvokedCount.ShouldBe(2);
            captured.Select(c => c.Name).ShouldBe(new[] { "cat", "dog" });
        }

        [Fact]
        public void GivenGenericSingleParamSetupWithSpecificType_WhenMethodIsInvokedWithBaseType_ShouldNotInvokeCallback()
        {
            var callAfterCallbackInvokedCount = 0;

            _sut
                .GenericSingleParam<IAnimal>(Arg<IAnimal>.Any())
                .CallAfter(animal => { callAfterCallbackInvokedCount++; });

            _sut.Instance().GenericSingleParam<IMammal>(new Cat("mammal"));

            callAfterCallbackInvokedCount.ShouldBe(0);
        }

        [Fact]
        public void GivenGenericOutParamSetupWithSpecificType_WhenMethodIsInvokedWithBaseType_ShouldInvokeCallback()
        {
            var callAfterCallbackInvokedCount = 0;

            _sut
                .GenericOutParam<Cat, int>(OutArg<Cat>.Any())
                .CallAfter((out Cat c) =>
                {
                    callAfterCallbackInvokedCount++;
                    c = default;
                });

            _sut.Instance().GenericOutParam<IAnimal, int>(out var animal);

            callAfterCallbackInvokedCount.ShouldBe(1);
        }

        [Fact]
        public void GivenGenericOutParamSetupWithBaseType_WhenMethodIsInvokedWithDerivedType_ShouldNotInvokeCallback()
        {
            var callAfterCallbackInvokedCount = 0;

            _sut
                .GenericOutParam<IAnimal, int>(OutArg<IAnimal>.Any())
                .CallAfter((out IAnimal c) =>
                {
                    callAfterCallbackInvokedCount++;
                    c = default;
                });

            _sut.Instance().GenericOutParam<Cat, int>(out var tiger);

            callAfterCallbackInvokedCount.ShouldBe(0);
        }

        [Fact]
        public void GivenGenericParamsParamSetupWithBaseType_WhenMethodIsInvokedWithDerivedType_ShouldInvokeCallback()
        {
            var captured = new List<IAnimal[]>();
            _sut
                .GenericParamsParam<IAnimal, int>(Arg<IAnimal[]>.Any())
                .CallAfter(animals => { captured.Add(animals); });

            _sut.Instance().GenericParamsParam<ICat, int>(new Cat("cat1"), new Cat("cat2"));
            _sut.Instance().GenericParamsParam<Dog, int>(new Dog("dog1"));

            captured.Count.ShouldBe(2);
            captured[0].Select(a => a.Name).ShouldBe(new[] { "cat1", "cat2" });
            captured[1].Select(a => a.Name).ShouldBe(new[] { "dog1" });
        }

        [Fact]
        public void GivenGenericAllRefKindsSetupWithCompatibleTypes_WhenMethodIsInvokedWithCompatibleTypes_ShouldInvokeCallbackWithCorrectParameters()
        {
            var callAfterCallbackInvokedCount = 0;
            IAnimal? refAnimal = null;
            IAnimal? inAnimal = null;
            IAnimal[]? paramsAnimals = null;

            _sut
                .GenericAllRefKind<Cat, IAnimal, IAnimal, IAnimal, int>(
                    OutArg<Cat>.Any(),
                    Arg<IAnimal>.Any(),
                    Arg<IAnimal>.Any(),
                    Arg<IAnimal[]>.Any()
                )
                .CallAfter((out Cat outValue, ref IAnimal refValue, in IAnimal inValue, IAnimal[] paramsValue) =>
                {
                    callAfterCallbackInvokedCount++;

                    outValue = new Cat("new-cat");
                    refValue = new Cat("new-ref-cat");
                    refAnimal = refValue;
                    inAnimal = inValue;
                    paramsAnimals = paramsValue;
                });

            IAnimal refValue = new Dog("ref-dog");
            var inValue = new GermanShepherd("in-gs");
            var paramsValues = new[] { new GermanShepherd("params-gs1"), new GermanShepherd("params-gs2") };

            _sut.Instance().GenericAllRefKind<IAnimal, IAnimal, GermanShepherd, GermanShepherd, int>(
                out var outValue,
                ref refValue,
                in inValue,
                paramsValues
            );

            callAfterCallbackInvokedCount.ShouldBe(1);

            outValue.ShouldBeOfType<Cat>().Name.ShouldBe("new-cat");
            refValue.ShouldBeOfType<Cat>().Name.ShouldBe("new-ref-cat");

            refAnimal.ShouldBeOfType<Cat>().Name.ShouldBe("new-ref-cat");
            inAnimal.ShouldBeOfType<GermanShepherd>().Name.ShouldBe("in-gs");
            paramsAnimals.ShouldNotBeNull().Length.ShouldBe(2);
            paramsAnimals[0].ShouldBeOfType<GermanShepherd>().Name.ShouldBe("params-gs1");
            paramsAnimals[1].ShouldBeOfType<GermanShepherd>().Name.ShouldBe("params-gs2");
        }

        [Fact]
        public void GivenChainedCallAfterSetups_WhenMethodIsInvoked_ShouldInvokeCallbacksInOrder()
        {
            var firstCallbackInvokedCount = 0;
            var secondCallbackInvokedCount = 0;

            _sut
                .VoidNoParams()
                .CallAfter(() => ++firstCallbackInvokedCount)
                .CallAfter(() => ++secondCallbackInvokedCount);

            _sut.Instance().VoidNoParams();
            firstCallbackInvokedCount.ShouldBe(1);

            _sut.Instance().VoidNoParams();
            secondCallbackInvokedCount.ShouldBe(1);
        }

        [Fact]
        public void GivenCallAfterChainedWithReturns_WhenMethodIsInvoked_ShouldInvokeCallbackAfterReturn()
        {
            var callbackInvokedCount = 0;
            var returnValue = 0;

            _sut
                .IntNoParams()
                .Returns(42)
                .CallAfter(() => { callbackInvokedCount++; });

            returnValue = _sut.Instance().IntNoParams();

            callbackInvokedCount.ShouldBe(1);
            returnValue.ShouldBe(42);
        }

        [Fact]
        public void GivenCallAfterChainedWithMultipleReturns_WhenMethodIsInvokedMultipleTimes_ShouldInvokeCallbackAfterEachReturn()
        {
            var callbackInvokedCount = 0;
            var capturedReturnValues = new List<int>();

            _sut
                .IntNoParams()
                .Returns(1)
                .CallAfter(() => callbackInvokedCount++)
                .Returns(2)
                .CallAfter(() => callbackInvokedCount++)
                .Returns(3)
                .CallAfter(() => callbackInvokedCount++);

            capturedReturnValues.Add(_sut.Instance().IntNoParams());
            capturedReturnValues.Add(_sut.Instance().IntNoParams());
            capturedReturnValues.Add(_sut.Instance().IntNoParams());
            capturedReturnValues.Add(_sut.Instance().IntNoParams()); // Should repeat last

            callbackInvokedCount.ShouldBe(4);
            capturedReturnValues.ShouldBe(new[] { 1, 2, 3, 3 });
        }

        [Fact]
        public void GivenCallAfterThatThrows_WhenMethodIsInvoked_ShouldPropagateException()
        {
            var expectedException = new InvalidOperationException("Test exception");

            _sut
                .VoidNoParams()
                .CallAfter(() => throw expectedException);

            var thrownException = Should.Throw<InvalidOperationException>(() => _sut.Instance().VoidNoParams());
            thrownException.ShouldBe(expectedException);
        }

        [Fact]
        public void GivenCallAfterThatThrowsWithReturnValue_WhenMethodIsInvoked_ShouldPropagateExceptionAndNotReturnValue()
        {
            _sut
                .IntSingleParam(Arg<int>.Any())
                .Returns(100)
                .CallAfter(age => throw new ArgumentException($"Invalid age: {age}"));

            Should.Throw<ArgumentException>(() => _sut.Instance().IntSingleParam(42))
                .Message.ShouldBe("Invalid age: 42");
        }

        [Fact]
        public void GivenOutParamMethodWithCallAfterAndReturns_WhenMethodIsInvoked_ShouldReturnCallAfterValueForOutParam()
        {
            var callAfterInvokedCount = 0;

            _sut
                .IntOutParam(OutArg<int>.Any())
                .Returns(999)
                .CallAfter((out int outValue) =>
                {
                    outValue = 777; // CallAfter should override Returns for out params
                    callAfterInvokedCount++;
                });

            var result = _sut.Instance().IntOutParam(out var outVal);

            result.ShouldBe(999); // Return value from Returns
            outVal.ShouldBe(777); // Out value from CallAfter
            callAfterInvokedCount.ShouldBe(1);
        }

        [Fact]
        public void GivenRefParamMethodWithCallAfterAndReturns_WhenMethodIsInvoked_ShouldExecuteBothCallAfterAndReturns()
        {
            var callAfterInvokedCount = 0;
            var originalRefValue = 100;

            _sut
                .IntRefParam(Arg<int>.Any())
                .Returns((ref int refVal) => refVal * 2)
                .CallAfter((ref int refParameter) =>
                {
                    refParameter += 50;
                    callAfterInvokedCount++;
                });

            var refValue = originalRefValue;
            var result = _sut.Instance().IntRefParam(ref refValue);

            result.ShouldBe(200); // 100 * 2
            refValue.ShouldBe(150); // 100 + 50
            callAfterInvokedCount.ShouldBe(1);
        }

        [Fact]
        public void GivenSpecificValueMatchSetupWithCallAfter_WhenMethodIsInvokedWithMatchingValues_ShouldOnlyInvokeCallbackForMatchingCalls()
        {
            var callAfterCount = 0;

            _sut
                .IntSingleParam(Arg<int>.Is(42))
                .Returns(100)
                .CallAfter(age => callAfterCount++);

            _sut.Instance().IntSingleParam(42);
            _sut.Instance().IntSingleParam(43); // Should not invoke callback
            _sut.Instance().IntSingleParam(42);

            callAfterCount.ShouldBe(2);
        }

        [Fact]
        public void GivenPredicateMatchSetupWithCallAfter_WhenMethodIsInvokedWithValues_ShouldOnlyInvokeCallbackForMatchingCalls()
        {
            var callAfterCount = 0;
            var capturedValues = new List<int>();

            _sut
                .IntSingleParam(Arg<int>.Is(x => x > 10))
                .Returns(999)
                .CallAfter(age =>
                {
                    callAfterCount++;
                    capturedValues.Add(age);
                });

            _sut.Instance().IntSingleParam(5); // Should not match
            _sut.Instance().IntSingleParam(15); // Should match
            _sut.Instance().IntSingleParam(20); // Should match

            callAfterCount.ShouldBe(2);
            capturedValues.ShouldBe(new[] { 15, 20 });
        }

        [Fact]
        public void GivenGenericSingleParamSetupWithCallAfter_WhenMethodIsInvokedWithNullValue_ShouldInvokeCallbackWithNull()
        {
            var callAfterCount = 0;
            string? capturedValue = "not-null";

            _sut
                .GenericSingleParam<string?>(Arg<string?>.Any())
                .CallAfter(value =>
                {
                    callAfterCount++;
                    capturedValue = value;
                });

            _sut.Instance().GenericSingleParam<string?>(null);

            callAfterCount.ShouldBe(1);
            capturedValue.ShouldBeNull();
        }

        [Fact]
        public void GivenGenericRefParamSetupWithCallAfter_WhenCallAfterModifiesRef_ShouldPropagateValue()
        {
            var callAfterCount = 0;

            _sut
                .GenericRefParam<string?, int>(Arg<string?>.Any())
                .Returns(42)
                .CallAfter((ref string? refParam) =>
                {
                    callAfterCount++;
                    refParam = "modified"; // Modify to null
                });

            var refValue = "initial";
            var result = _sut.Instance().GenericRefParam<string?, int>(ref refValue);

            result.ShouldBe(42);
            refValue.ShouldBe("modified");
            callAfterCount.ShouldBe(1);
        }

        [Fact]
        public void GivenMultipleParamsSetupWithCallAfter_WhenMethodIsInvokedWithNullArgument_ShouldInvokeCallbackWithNull()
        {
            var callAfterCount = 0;
            string? capturedName = "not-null";

            _sut
                .IntParams(Arg<int>.Any(), Arg<string?>.Any(), Arg<Regex>.Any())
                .CallAfter((age, name, regex) =>
                {
                    callAfterCount++;
                    capturedName = name;
                });

            _sut.Instance().IntParams(42, null, new Regex("test"));

            callAfterCount.ShouldBe(1);
            capturedName.ShouldBeNull();
        }

        [Fact]
        public void GivenParamsArraySetupWithCallAfter_WhenMethodIsInvokedWithEmptyArray_ShouldInvokeCallbackWithEmptyArray()
        {
            var callAfterCount = 0;
            string[]? capturedArray = null;

            _sut
                .IntParamsParam(Arg<string[]>.Any())
                .CallAfter(arr =>
                {
                    callAfterCount++;
                    capturedArray = arr;
                });

            _sut.Instance().IntParamsParam(Array.Empty<string>());

            callAfterCount.ShouldBe(1);
            capturedArray.ShouldNotBeNull();
            capturedArray.Length.ShouldBe(0);
        }

        [Fact]
        public void GivenGenericAllRefKindsSetupWithCallAfter_WhenCallAfterModifiesComplexTypes_ShouldPropagateAllModifications()
        {
            var callAfterCount = 0;
            var originalCats = new[] { new Cat("cat1"), new Cat("cat2") };

            _sut
                .GenericAllRefKind<Cat, IAnimal, IAnimal, IAnimal, bool>(
                    OutArg<Cat>.Any(),
                    Arg<IAnimal>.Any(),
                    Arg<IAnimal>.Any(),
                    Arg<IAnimal[]>.Any()
                )
                .Returns(true)
                .CallAfter((out Cat outCat, ref IAnimal refAnimal, in IAnimal inAnimal, IAnimal[] paramsAnimals) =>
                {
                    callAfterCount++;
                    outCat = new Cat("out-cat");
                    refAnimal = new Dog("ref-dog");

                    // Verify we can read the in parameter
                    inAnimal.Name.ShouldBe("in-animal");

                    // Verify params array contents
                    paramsAnimals.Length.ShouldBe(2);
                });

            IAnimal refAnimal = new Cat("ref-cat");
            var inAnimal = new Dog("in-animal");

            var result = _sut.Instance().GenericAllRefKind<IAnimal, IAnimal, Dog, Cat, bool>(
                out var outValue,
                ref refAnimal,
                in inAnimal,
                originalCats
            );

            callAfterCount.ShouldBe(1);
            result.ShouldBeTrue();
            outValue.ShouldBeOfType<Cat>().Name.ShouldBe("out-cat");
            refAnimal.ShouldBeOfType<Dog>().Name.ShouldBe("ref-dog");
        }

        [Fact]
        public void GivenSequentialCallAfterSetups_WhenMethodIsInvokedMultipleTimes_ShouldInvokeCallbackForEachSequenceItem()
        {
            var callAfterCount = 0;
            var setup = _sut.IntNoParams();

            setup
                .Returns(1)
                .CallAfter(() => callAfterCount++)
                .Returns(2)
                .CallAfter(() => callAfterCount++)
                .Returns(3)
                .CallAfter(() => callAfterCount++);

            _sut.Instance().IntNoParams();
            _sut.Instance().IntNoParams();
            _sut.Instance().IntNoParams();

            callAfterCount.ShouldBe(3);
        }

        [Fact]
        public void GivenGenericOutParamSetupWithDifferentGenericConstraints_WhenMethodIsInvoked_ShouldInvokeCallbackCorrectly()
        {
            var callAfterCount = 0;
            IAnimal? capturedOutValue = null;

            _sut
                .GenericOutParam<IAnimal, string>(OutArg<IAnimal>.Any())
                .Returns("success")
                .CallAfter((out IAnimal animal) =>
                {
                    callAfterCount++;
                    animal = new Tiger("callback-tiger");
                    capturedOutValue = animal;
                });

            var result = _sut.Instance().GenericOutParam<IMammal, string>(out var outMammal);

            callAfterCount.ShouldBe(1);
            result.ShouldBe("success");
            outMammal.ShouldBeOfType<Tiger>().Name.ShouldBe("callback-tiger");
            capturedOutValue.ShouldBeOfType<Tiger>();
        }
        
        [Fact]
        public async Task GivenAsyncTaskMethodSetupWithCallAfter_WhenMethodIsInvoked_ShouldExecuteCallback()
        {
            var callbackExecuted = false;
            
            _sut
                .AsyncTaskIntNoParams()
                .Returns(Task.FromResult(60))
                .CallAfter(() =>
                {
                    callbackExecuted = true;
                    return Task.CompletedTask;
                });

            var result = await _sut.Instance().AsyncTaskIntNoParams();
            
            result.ShouldBe(60);
            callbackExecuted.ShouldBeTrue();
        }

    }
}