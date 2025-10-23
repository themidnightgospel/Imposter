using System;
using System.Collections.Generic;
using System.Linq;
using Imposter.Abstractions;
using Shouldly;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Shared;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.MethodSetup.Callbacks
{
    public class CallBeforeTests
    {
        private readonly IMethodSetupFeatureSutImposter _sut = new IMethodSetupFeatureSutImposter();

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void VoidNoParams_MethodInvoked_CallbackInvoked(int invocationCount)
        {
            var callBeforeCallbackInvokedCount = 0;

            _sut
                .VoidNoParams()
                .CallBefore(() => ++callBeforeCallbackInvokedCount);

            for (var i = 0; i < invocationCount; i++)
            {
                _sut.Instance().VoidNoParams();
            }

            callBeforeCallbackInvokedCount.ShouldBe(invocationCount);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void IntNoParams_WhenMethodInvoked_CallbackIsInvoked(int invocationCount)
        {
            var callBeforeCallbackInvokedCount = 0;

            _sut
                .IntNoParams()
                .CallBefore(() => ++callBeforeCallbackInvokedCount);

            for (var i = 0; i < invocationCount; i++)
            {
                _sut.Instance().IntNoParams();
            }

            callBeforeCallbackInvokedCount.ShouldBe(invocationCount);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void IntSingleParam_WhenMethodInvoked_CallbackInvokedWithParameters(int invocationCount)
        {
            var callBeforeCallbackInvokedCount = 0;
            var parametersPassedToCallback = new List<int>();

            _sut
                .IntSingleParam(Arg<int>.Any())
                .CallBefore(age =>
                {
                    callBeforeCallbackInvokedCount++;
                    parametersPassedToCallback.Add(age);
                });

            for (var i = 0; i < invocationCount; i++)
            {
                _sut.Instance().IntSingleParam(42 + i);
            }

            callBeforeCallbackInvokedCount.ShouldBe(invocationCount);
            parametersPassedToCallback.ShouldBe(Enumerable.Range(42, invocationCount).ToList());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void IntParams_WhenMethodInvoked_CallbackInvokedWithParameters(int invocationCount)
        {
            var callBeforeCallbackInvokedCount = 0;

            var passedAgeParametersToCallback = new List<int>();
            var passedNameParametersToCallback = new List<string>();
            var passedRegexParametersToCallback = new List<Regex>();

            _sut
                .IntParams(Arg<int>.Any(), Arg<string>.Any(), Arg<Regex>.Any())
                .CallBefore((age, name, regex) =>
                {
                    callBeforeCallbackInvokedCount++;

                    passedAgeParametersToCallback.Add(age);
                    passedNameParametersToCallback.Add(name);
                    passedRegexParametersToCallback.Add(regex);
                });

            for (var i = 0; i < invocationCount; i++)
            {
                var regex = new Regex($"abc{i}");
                _sut.Instance().IntParams(i, $"name{i}", regex);
            }

            callBeforeCallbackInvokedCount.ShouldBe(invocationCount);
            passedAgeParametersToCallback.ShouldBe(Enumerable.Range(0, invocationCount).ToList());
            passedNameParametersToCallback.ShouldBe(Enumerable.Range(0, invocationCount).Select(i => $"name{i}").ToList());
            passedRegexParametersToCallback.Select(r => r.ToString()).ShouldBe(Enumerable.Range(0, invocationCount).Select(i => $"abc{i}"));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void IntOutParam_OutParameterInitializedInCallback_OutParameterValueDiscarded(int invocationCount)
        {
            var callBeforeCallbackInvokedCount = 0;

            _sut
                .IntOutParam(OutArg<int>.Any())
                .CallBefore((out int outValue) =>
                {
                    outValue = 123;
                    callBeforeCallbackInvokedCount++;
                });

            for (var i = 0; i < invocationCount; i++)
            {
                _sut.Instance().IntOutParam(out var outVal);
                outVal.ShouldBe(default);
            }

            callBeforeCallbackInvokedCount.ShouldBe(invocationCount);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void IntRefParam_WhenMethodInvoked_CallbackIsInvokedWithCorrectParameters(int invocationCount)
        {
            var callBeforeCallbackInvokedCount = 0;
            var refParametersPassedToCallback = new List<int>();

            _sut
                .IntRefParam(Arg<int>.Any())
                .CallBefore((ref int refParameter) =>
                {
                    callBeforeCallbackInvokedCount++;
                    refParametersPassedToCallback.Add(refParameter);
                });

            for (var i = 0; i < invocationCount; i++)
            {
                var refValue = 10 + i;
                _sut.Instance().IntRefParam(ref refValue);
            }

            callBeforeCallbackInvokedCount.ShouldBe(invocationCount);
            refParametersPassedToCallback.ShouldBe(Enumerable.Range(10, invocationCount).ToList());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void IntParamsParam_WhenMethodInvoked_CallbackIsInvokedWithCorrectParameters(int invocationCount)
        {
            var callBeforeCallbackInvokedCount = 0;
            var paramsParameterPassedToCallback = new List<string[]>();

            _sut
                .IntParamsParam(Arg<string[]>.Any())
                .CallBefore(arr =>
                {
                    callBeforeCallbackInvokedCount++;
                    paramsParameterPassedToCallback.Add(arr);
                });

            for (var i = 0; i < invocationCount; i++)
            {
                var inputArr = new[] { $"a{i}", $"b{i}" };
                _sut.Instance().IntParamsParam(inputArr);
            }

            callBeforeCallbackInvokedCount.ShouldBe(invocationCount);
            paramsParameterPassedToCallback.ShouldBe(Enumerable.Range(0, invocationCount).Select(i => new[] { $"a{i}", $"b{i}" }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void IntInParam_WhenMethodInvoked_CallbackIsInvokedWithCorrectParameters(int invocationCount)
        {
            var callBeforeCallbackInvokedCount = 0;
            var stringList = new List<string>();

            _sut
                .IntInParam(Arg<string>.Any())
                .CallBefore((in string s) =>
                {
                    callBeforeCallbackInvokedCount++;
                    stringList.Add(s);
                });

            for (var i = 0; i < invocationCount; i++)
            {
                _sut.Instance().IntInParam($"in{i}");
            }

            callBeforeCallbackInvokedCount.ShouldBe(invocationCount);
            stringList.ShouldBe(Enumerable.Range(0, invocationCount).Select(i => $"in{i}").ToList());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void IntAllRefKinds_WhenMethodInvoked_CallbackIsInvokedWithCorrectParameters(int invocationCount)
        {
            var callBeforeCallbackInvokedCount = 0;
            var outList = new List<int>();
            var refList = new List<int>();
            var inList = new List<int>();
            var strList = new List<string>();
            var paramsList = new List<string[]>();

            _sut
                .IntAllRefKinds(OutArg<int>.Any(), Arg<int>.Any(), Arg<int>.Any(), Arg<string>.Any(), Arg<string[]>.Any())
                .CallBefore((out int o, ref int r, in int i, string s, string[] p) =>
                {
                    o = 999;
                    callBeforeCallbackInvokedCount++;
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
            }

            callBeforeCallbackInvokedCount.ShouldBe(invocationCount);
            outList.ShouldBe(Enumerable.Repeat(999, invocationCount));
            refList.ShouldBe(Enumerable.Range(20, invocationCount).ToList());
            inList.ShouldBe(Enumerable.Range(100, invocationCount).ToList());
            strList.ShouldBe(Enumerable.Range(0, invocationCount).Select(i => $"val-{i}"));
            paramsList.ShouldBe(Enumerable.Range(0, invocationCount).Select(i => new[] { $"p{i}-1", $"p{i}-2" }), ignoreOrder: false);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void GenericSingleParam_WhenMethodInvoked_CallbackIsInvokedWithCorrectParameter(int invocationCount)
        {
            var callBeforeCallbackInvokedCount = 0;
            var passedParameterToCallback = new List<int>();

            _sut
                .GenericSingleParam<int>(Arg<int>.Any())
                .CallBefore(value =>
                {
                    callBeforeCallbackInvokedCount++;
                    passedParameterToCallback.Add(value);
                });

            for (var i = 0; i < invocationCount; i++)
            {
                _sut.Instance().GenericSingleParam<int>(100 + i);
            }

            callBeforeCallbackInvokedCount.ShouldBe(invocationCount);
            passedParameterToCallback.ShouldBe(Enumerable.Range(100, invocationCount).ToList());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(4)]
        public void GenericOutParam_WhenMethodInvoked_CallbackIsInvokedWithCorrectParameter(int invocationCount)
        {
            var callBeforeCallbackInvokedCount = 0;
            var outParams = new List<string>();

            _sut
                .GenericOutParam<string, int>(OutArg<string>.Any())
                .CallBefore((out string s) =>
                {
                    s = "hello";
                    callBeforeCallbackInvokedCount++;
                });

            for (var i = 0; i < invocationCount; i++)
            {
                _sut.Instance().GenericOutParam<string, int>(out var val);
            }

            callBeforeCallbackInvokedCount.ShouldBe(invocationCount);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(4)]
        public void GenericRefParam_WhenMethodInvoked_CallbackIsInvokedWithCorrectParameter(int invocationCount)
        {
            var callBeforeCallbackInvokedCount = 0;
            var refParams = new List<double>();

            _sut
                .GenericRefParam<double, string>(Arg<double>.Any())
                .CallBefore((ref double d) =>
                {
                    callBeforeCallbackInvokedCount++;
                    refParams.Add(d);
                });

            for (var i = 0; i < invocationCount; i++)
            {
                var v = 1.1 + i;
                _sut.Instance().GenericRefParam<double, string>(ref v);
            }

            callBeforeCallbackInvokedCount.ShouldBe(invocationCount);
            refParams.ShouldBe(Enumerable.Range(0, invocationCount).Select(i => 1.1 + i).ToList());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        public void GenericParamsParam_WhenMethodInvoked_CallbackIsInvokedWithCorrectParameters(int invocationCount)
        {
            var callBeforeCallbackInvokedCount = 0;
            var allArrays = new List<string[]>();

            _sut
                .GenericParamsParam<string, int>(Arg<string[]>.Any())
                .CallBefore(arr =>
                {
                    callBeforeCallbackInvokedCount++;
                    allArrays.Add(arr);
                });

            for (var i = 0; i < invocationCount; i++)
            {
                var values = new[] { $"str{i}_1", $"str{i}_2" };
                _sut.Instance().GenericParamsParam<string, int>(values);
            }

            callBeforeCallbackInvokedCount.ShouldBe(invocationCount);
            allArrays.ShouldBe(Enumerable.Range(0, invocationCount).Select(i => new[] { $"str{i}_1", $"str{i}_2" }), ignoreOrder: false);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GenericAllRefKind_WhenMethodInvoked_CallbackIsInvokedWithCorrectParameters(int invocationCount)
        {
            var callBeforeCallbackInvokedCount = 0;
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
                .CallBefore((out int o, ref string r, in double i, bool[] p) =>
                {
                    o = 111;
                    callBeforeCallbackInvokedCount++;
                    outParams.Add(o);
                    refParams.Add(r);
                    inParams.Add(i);
                    paramsParams.Add(p);
                });

            for (var idx = 0; idx < invocationCount; idx++)
            {
                int refValOut;
                string refVal = $"s{idx}";
                double inVal = idx + 0.5;
                bool[] arr = new[] { idx % 2 == 0, idx % 3 == 0 };
                _sut.Instance().GenericAllRefKind<int, string, double, bool, long>(
                    out refValOut,
                    ref refVal,
                    in inVal,
                    arr
                );
            }

            callBeforeCallbackInvokedCount.ShouldBe(invocationCount);
            outParams.ShouldBe(Enumerable.Repeat(111, invocationCount));
            refParams.ShouldBe(Enumerable.Range(0, invocationCount).Select(idx => $"s{idx}").ToList());
            inParams.ShouldBe(Enumerable.Range(0, invocationCount).Select(idx => idx + 0.5).ToList());
            paramsParams.ShouldBe(Enumerable.Range(0, invocationCount).Select(idx => new[] { idx % 2 == 0, idx % 3 == 0 }), ignoreOrder: false);
        }

        [Fact]
        public void GenericSingleParam_WhenMethodInvokedWithDerivedType_CallbackIsInvoked()
        {
            var callBeforeCallbackInvokedCount = 0;
            var captured = new List<IAnimal>();

            _sut
                .GenericSingleParam<IAnimal>(Arg<IAnimal>.Any())
                .CallBefore(animal =>
                {
                    callBeforeCallbackInvokedCount++;
                    captured.Add(animal);
                });

            _sut.Instance().GenericSingleParam<Cat>(new Cat("cat"));
            _sut.Instance().GenericSingleParam<Dog>(new Dog("dog"));

            callBeforeCallbackInvokedCount.ShouldBe(2);
            captured.Select(c => c.Name).ShouldBe(new[] { "cat", "dog" });
        }

        [Fact]
        public void GenericSingleParam_WhenMethodInvokedWithBaseType_CallbackIsNotInvoked()
        {
            var callBeforeCallbackInvokedCount = 0;

            _sut
                .GenericSingleParam<IAnimal>(Arg<IAnimal>.Any())
                .CallBefore(animal => { callBeforeCallbackInvokedCount++; });

            _sut.Instance().GenericSingleParam<IMammal>(new Cat("mammal"));

            callBeforeCallbackInvokedCount.ShouldBe(0);
        }

        [Fact]
        public void GenericOutParam_WhenMethodInvokedWithBaseType_CallbackIsInvoked()
        {
            var callBeforeCallbackInvokedCount = 0;

            _sut
                .GenericOutParam<Cat, int>(OutArg<Cat>.Any())
                .CallBefore((out Cat c) =>
                {
                    callBeforeCallbackInvokedCount++;
                    c = default;
                });

            _sut.Instance().GenericOutParam<IAnimal, int>(out var animal);

            callBeforeCallbackInvokedCount.ShouldBe(1);
        }

        [Fact]
        public void GenericOutParam_WhenMethodInvokedWithDerivedType_CallbackIsNotInvoked()
        {
            var callBeforeCallbackInvokedCount = 0;

            _sut
                .GenericOutParam<IAnimal, int>(OutArg<IAnimal>.Any())
                .CallBefore((out IAnimal c) =>
                {
                    callBeforeCallbackInvokedCount++;
                    c = default;
                });

            _sut.Instance().GenericOutParam<Cat, int>(out var tiger);

            callBeforeCallbackInvokedCount.ShouldBe(0);
        }

        [Fact]
        public void GenericParamsParam_WhenMethodInvokedWithDerivedType_CallbackIsInvoked()
        {
            var captured = new List<IAnimal[]>();
            _sut
                .GenericParamsParam<IAnimal, int>(Arg<IAnimal[]>.Any())
                .CallBefore(animals => { captured.Add(animals); });

            _sut.Instance().GenericParamsParam<ICat, int>(new Cat("cat1"), new Cat("cat2"));
            _sut.Instance().GenericParamsParam<Dog, int>(new Dog("dog1"));

            captured.Count.ShouldBe(2);
            captured[0].Select(a => a.Name).ShouldBe(new[] { "cat1", "cat2" });
            captured[1].Select(a => a.Name).ShouldBe(new[] { "dog1" });
        }

        [Fact]
        public void GenericAllRefKind_InvokedWithCompatibleTypes_CallbackIsInvokedWithCorrectParameters()
        {
            var callBeforeCallbackInvokedCount = 0;
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
                .CallBefore((out Cat outValue, ref IAnimal refValue, in IAnimal inValue, IAnimal[] paramsValue) =>
                {
                    callBeforeCallbackInvokedCount++;

                    outValue = default;
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

            callBeforeCallbackInvokedCount.ShouldBe(1);

            refAnimal.ShouldBeOfType<Dog>().Name.ShouldBe("ref-dog");
            inAnimal.ShouldBeOfType<GermanShepherd>().Name.ShouldBe("in-gs");
            paramsAnimals.ShouldNotBeNull().Length.ShouldBe(2);
            paramsAnimals[0].ShouldBeOfType<GermanShepherd>().Name.ShouldBe("params-gs1");
            paramsAnimals[1].ShouldBeOfType<GermanShepherd>().Name.ShouldBe("params-gs2");
        }


        [Fact]
        public void VoidNoParams_WhenHasMultipleCallBeforeCallbacks_AllCallbacksAreInvoked()
        {
            var firstCallbackInvokedCount = 0;
            var secondCallbackInvokedCount = 0;

            _sut
                .VoidNoParams()
                .CallBefore(() => ++firstCallbackInvokedCount)
                .CallBefore(() => ++secondCallbackInvokedCount);

            _sut.Instance().VoidNoParams();
            _sut.Instance().VoidNoParams();

            firstCallbackInvokedCount.ShouldBe(1);
            secondCallbackInvokedCount.ShouldBe(1);
        }

        [Fact]
        public void IntNoParams_WhenCallBeforeChainedWithReturns_CallbackIsInvokedBeforeReturn()
        {
            var callbackInvokedCount = 0;
            var returnValue = 0;

            _sut
                .IntNoParams()
                .CallBefore(() => { callbackInvokedCount++; })
                .Returns(42);

            returnValue = _sut.Instance().IntNoParams();

            callbackInvokedCount.ShouldBe(1);
            returnValue.ShouldBe(42);
        }

        [Fact]
        public void IntNoParams_WhenCallBeforeChainedWithMultipleReturns_CallbackIsInvokedBeforeEachReturn()
        {
            var callbackInvokedCount = 0;
            var capturedReturnValues = new List<int>();

            _sut
                .IntNoParams()
                .CallBefore(() => callbackInvokedCount++)
                .Returns(1)
                .CallBefore(() => callbackInvokedCount++)
                .Returns(2)
                .CallBefore(() => callbackInvokedCount++)
                .Returns(3);

            capturedReturnValues.Add(_sut.Instance().IntNoParams());
            capturedReturnValues.Add(_sut.Instance().IntNoParams());
            capturedReturnValues.Add(_sut.Instance().IntNoParams());
            capturedReturnValues.Add(_sut.Instance().IntNoParams()); // Should repeat last

            callbackInvokedCount.ShouldBe(4);
            capturedReturnValues.ShouldBe(new[] { 1, 2, 3, 3 });
        }

        [Fact]
        public void VoidNoParams_WhenCallBeforeThrows_ExceptionIsPropagated()
        {
            var expectedException = new InvalidOperationException("Test exception");

            _sut
                .VoidNoParams()
                .CallBefore(() => throw expectedException);

            var thrownException = Should.Throw<InvalidOperationException>(() => _sut.Instance().VoidNoParams());
            thrownException.ShouldBe(expectedException);
        }

        [Fact]
        public void IntSingleParam_WhenCallBeforeThrows_ExceptionIsPropagatedAndReturnValueNotReturned()
        {
            _sut
                .IntSingleParam(Arg<int>.Any())
                .CallBefore(age => throw new ArgumentException($"Invalid age: {age}"))
                .Returns(100);

            Should.Throw<ArgumentException>(() => _sut.Instance().IntSingleParam(42))
                .Message.ShouldBe("Invalid age: 42");
        }

        [Fact]
        public void IntRefParam_WhenCallBeforeModifiesRefParam_ModificationIsPropagatedToMethod()
        {
            var callBeforeInvokedCount = 0;
            var originalRefValues = new List<int>();

            _sut
                .IntRefParam(Arg<int>.Any())
                .CallBefore((ref int refParameter) =>
                {
                    originalRefValues.Add(refParameter);
                    refParameter += 100; // Modify the ref parameter
                    callBeforeInvokedCount++;
                })
                .Returns((ref int refVal) => refVal * 2); // This should see the modified value

            var refValue = 50;
            var result = _sut.Instance().IntRefParam(ref refValue);

            result.ShouldBe(300); // (50 + 100) * 2
            refValue.ShouldBe(150); // 50 + 100, modified by CallBefore
            callBeforeInvokedCount.ShouldBe(1);
            originalRefValues.ShouldBe(new[] { 50 });
        }

        [Fact]
        public void IntSingleParam_WhenSetupWithSpecificValueAndCallBefore_OnlyMatchingCallsInvokeCallback()
        {
            var callBeforeCount = 0;

            _sut
                .IntSingleParam(Arg<int>.Is(42))
                .CallBefore(age => callBeforeCount++)
                .Returns(100);

            _sut.Instance().IntSingleParam(42);
            _sut.Instance().IntSingleParam(43); // Should not invoke callback
            _sut.Instance().IntSingleParam(42);

            callBeforeCount.ShouldBe(2);
        }

        [Fact]
        public void IntSingleParam_WhenSetupWithPredicateAndCallBefore_OnlyMatchingCallsInvokeCallback()
        {
            var callBeforeCount = 0;
            var capturedValues = new List<int>();

            _sut
                .IntSingleParam(Arg<int>.Is(x => x > 10))
                .CallBefore(age =>
                {
                    callBeforeCount++;
                    capturedValues.Add(age);
                })
                .Returns(999);

            _sut.Instance().IntSingleParam(5); // Should not match
            _sut.Instance().IntSingleParam(15); // Should match
            _sut.Instance().IntSingleParam(20); // Should match

            callBeforeCount.ShouldBe(2);
            capturedValues.ShouldBe(new[] { 15, 20 });
        }

        [Fact]
        public void GenericSingleParam_WhenCallBeforeReceivesNullValue_CallbackIsInvokedWithNull()
        {
            var callBeforeCount = 0;
            string? capturedValue = "not-null";

            _sut
                .GenericSingleParam<string?>(Arg<string?>.Any())
                .CallBefore(value =>
                {
                    callBeforeCount++;
                    capturedValue = value;
                });

            _sut.Instance().GenericSingleParam<string?>(null);

            callBeforeCount.ShouldBe(1);
            capturedValue.ShouldBeNull();
        }

        [Fact]
        public void GenericRefParam_WhenCallBeforeModifiesRef_ValueIsPropagatedToMethod()
        {
            var callBeforeCount = 0;

            _sut
                .GenericRefParam<string?, int>(Arg<string?>.Any())
                .CallBefore((ref string? refParam) =>
                {
                    callBeforeCount++;
                    refParam = "modified-by-callback";
                })
                .Returns((ref string? s) => s?.Length ?? 0);

            var refValue = "initial";
            var result = _sut.Instance().GenericRefParam<string?, int>(ref refValue);

            result.ShouldBe(20); // "modified-by-callback".Length
            refValue.ShouldBe("modified-by-callback");
            callBeforeCount.ShouldBe(1);
        }

        [Fact]
        public void IntParams_WhenCallBeforeReceivesNullArgument_CallbackIsInvokedWithNull()
        {
            var callBeforeCount = 0;
            string? capturedName = "not-null";

            _sut
                .IntParams(Arg<int>.Any(), Arg<string?>.Any(), Arg<Regex>.Any())
                .CallBefore((age, name, regex) =>
                {
                    callBeforeCount++;
                    capturedName = name;
                });

            _sut.Instance().IntParams(42, null, new Regex("test"));

            callBeforeCount.ShouldBe(1);
            capturedName.ShouldBeNull();
        }

        [Fact]
        public void IntParamsParam_WhenCallBeforeReceivesEmptyArray_CallbackIsInvokedWithEmptyArray()
        {
            var callBeforeCount = 0;
            string[]? capturedArray = null;

            _sut
                .IntParamsParam(Arg<string[]>.Any())
                .CallBefore(arr =>
                {
                    callBeforeCount++;
                    capturedArray = arr;
                });

            _sut.Instance().IntParamsParam(Array.Empty<string>());

            callBeforeCount.ShouldBe(1);
            capturedArray.ShouldNotBeNull();
            capturedArray.Length.ShouldBe(0);
        }

        [Fact]
        public void IntOutParam_WhenCallBeforeAndReturnsChained_OutValueFromReturnsNotFromCallBefore()
        {
            var callBeforeInvokedCount = 0;

            _sut
                .IntOutParam(OutArg<int>.Any())
                .CallBefore((out int outValue) =>
                {
                    outValue = 999; // This should be discarded
                    callBeforeInvokedCount++;
                })
                .Returns((out int outValue) =>
                {
                    outValue = 777;
                    return 555;
                });

            var result = _sut.Instance().IntOutParam(out var outVal);

            result.ShouldBe(555);
            outVal.ShouldBe(777); // From Returns, not CallBefore
            callBeforeInvokedCount.ShouldBe(1);
        }

        [Fact]
        public void IntAllRefKinds_WhenCallBeforeModifiesRefParam_ModificationsArePropagatedToMethod()
        {
            var callBeforeCount = 0;
            var originalRefValues = new List<int>();

            _sut
                .IntAllRefKinds(OutArg<int>.Any(), Arg<int>.Any(), Arg<int>.Any(), Arg<string>.Any(), Arg<string[]>.Any())
                .CallBefore((out int o, ref int r, in int i, string s, string[] p) =>
                {
                    o = 888; // This will be discarded
                    originalRefValues.Add(r);
                    r += 500; // This should be propagated to the method
                    callBeforeCount++;
                })
                .Returns((out int o, ref int r, in int i, string s, string[] p) =>
                {
                    o = 999;
                    return r; // Return the modified ref value
                });

            var refVal = 100;
            var inVal = 200;
            var result = _sut.Instance().IntAllRefKinds(out var outVal, ref refVal, in inVal, "test", new[] { "a", "b" });

            callBeforeCount.ShouldBe(1);
            result.ShouldBe(600); // 100 + 500
            outVal.ShouldBe(999); // From Returns
            refVal.ShouldBe(600); // 100 + 500 from CallBefore
            originalRefValues.ShouldBe(new[] { 100 });
        }

        [Fact]
        public void GenericOutParam_WhenCallBeforeAndReturnsChained_OutValueFromReturnsNotFromCallBefore()
        {
            var callBeforeCount = 0;

            _sut
                .GenericOutParam<string, int>(OutArg<string>.Any())
                .CallBefore((out string s) =>
                {
                    s = "from-callback"; // This should be discarded
                    callBeforeCount++;
                })
                .Returns((out string s) =>
                {
                    s = "from-returns";
                    return 42;
                });

            var result = _sut.Instance().GenericOutParam<string, int>(out var outValue);

            result.ShouldBe(42);
            outValue.ShouldBe("from-returns"); // From Returns, not CallBefore
            callBeforeCount.ShouldBe(1);
        }

        [Fact]
        public void IntNoParams_WhenCallBeforeInSequence_CallbackIsInvokedForEachSequenceItem()
        {
            var callBeforeCount = 0;
            var setup = _sut.IntNoParams();

            setup
                .CallBefore(() => callBeforeCount++)
                .Returns(1)
                .CallBefore(() => callBeforeCount++)
                .Returns(2)
                .CallBefore(() => callBeforeCount++)
                .Returns(3);

            _sut.Instance().IntNoParams();
            _sut.Instance().IntNoParams();
            _sut.Instance().IntNoParams();

            callBeforeCount.ShouldBe(3);
        }

        [Fact]
        public void GenericAllRefKind_WhenCallBeforeWithComplexTypeModification_ModificationsArePropagated()
        {
            var callBeforeCount = 0;
            IAnimal? originalRefAnimal = null;

            _sut
                .GenericAllRefKind<Cat, IAnimal, IAnimal, IAnimal, bool>(
                    OutArg<Cat>.Any(),
                    Arg<IAnimal>.Any(),
                    Arg<IAnimal>.Any(),
                    Arg<IAnimal[]>.Any()
                )
                .CallBefore((out Cat outCat, ref IAnimal refAnimal, in IAnimal inAnimal, IAnimal[] paramsAnimals) =>
                {
                    callBeforeCount++;
                    outCat = new Cat("callback-out"); // This will be discarded
                    originalRefAnimal = refAnimal;
                    refAnimal = new Tiger("modified-by-callback");
                })
                .Returns((out Cat outCat, ref IAnimal refAnimal, in IAnimal inAnimal, IAnimal[] paramsAnimals) =>
                {
                    outCat = new Cat("returns-out");
                    // refAnimal should be the modified one from CallBefore
                    return refAnimal.Name == "modified-by-callback";
                });

            IAnimal refAnimal = new Dog("original-ref");
            var inAnimal = new Dog("in-animal");
            var paramsAnimals = new[] { new Cat("params1"), new Cat("params2") };

            var result = _sut.Instance().GenericAllRefKind<IAnimal, IAnimal, Dog, Cat, bool>(
                out var outValue,
                ref refAnimal,
                in inAnimal,
                paramsAnimals
            );

            callBeforeCount.ShouldBe(1);
            result.ShouldBeTrue();
            outValue.ShouldBeOfType<Cat>().Name.ShouldBe("returns-out");
            refAnimal.ShouldBeOfType<Tiger>().Name.ShouldBe("modified-by-callback");
            originalRefAnimal.ShouldBeOfType<Dog>().Name.ShouldBe("original-ref");
        }

        [Fact]
        public void GenericOutParam_WhenCallBeforeHasDifferentGenericConstraints_CallbackIsInvokedCorrectly()
        {
            var callBeforeCount = 0;

            _sut
                .GenericOutParam<Cat, string>(OutArg<Cat>.Any())
                .CallBefore((out Cat animal) =>
                {
                    callBeforeCount++;
                    animal = new Tiger("callback-tiger"); // This will be discarded
                })
                .Returns((out Cat animal) =>
                {
                    animal = new Cat("returns-cat");
                    return "success";
                });

            var result = _sut.Instance().GenericOutParam<Animal, string>(out var outAnimal);

            callBeforeCount.ShouldBe(1);
            result.ShouldBe("success");
            outAnimal.ShouldBeOfType<Cat>().Name.ShouldBe("returns-cat");
        }

        [Fact]
        public void VoidNoParams_WhenCallBeforeAndCallAfterChained_BothCallbacksAreInvoked()
        {
            var callBeforeCount = 0;
            var callAfterCount = 0;

            _sut
                .VoidNoParams()
                .CallBefore(() => callBeforeCount++)
                .CallAfter(() => callAfterCount++);

            _sut.Instance().VoidNoParams();

            callBeforeCount.ShouldBe(1);
            callAfterCount.ShouldBe(1);
        }

        [Fact]
        public void IntSingleParam_WhenCallBeforeAndCallAfterChained_BothCallbacksReceiveParameters()
        {
            var callBeforeValues = new List<int>();
            var callAfterValues = new List<int>();

            _sut
                .IntSingleParam(Arg<int>.Any())
                .CallBefore(age => callBeforeValues.Add(age))
                .Returns(age => age * 2)
                .CallAfter(age => callAfterValues.Add(age));

            _sut.Instance().IntSingleParam(42);

            callBeforeValues.ShouldBe(new[] { 42 });
            callAfterValues.ShouldBe(new[] { 42 });
        }
        
        [Fact]
        public async Task AsyncTaskIntNoParams_WithCallBefore_ExecutesCallback()
        {
            var callbackExecuted = false;
            
            _sut
                .AsyncTaskIntNoParams()
                .CallBefore(() =>
                {
                    callbackExecuted = true;
                    return Task.CompletedTask;
                })
                .Returns(Task.FromResult(50));

            var result = await _sut.Instance().AsyncTaskIntNoParams();
            
            result.ShouldBe(50);
            callbackExecuted.ShouldBeTrue();
        }
        
        [Fact]
        public async Task AsyncTaskIntNoParams_WithBothCallbacks_ExecutesInOrder()
        {
            var executionOrder = new List<string>();
            
            _sut
                .AsyncTaskIntNoParams()
                .CallBefore(() =>
                {
                    executionOrder.Add("before");
                    return Task.CompletedTask;
                })
                .Returns(async () =>
                {
                    executionOrder.Add("during");
                    await Task.Delay(1);
                    return 70;
                })
                .CallAfter(() =>
                {
                    executionOrder.Add("after");
                    return Task.CompletedTask;
                });

            var result = await _sut.Instance().AsyncTaskIntNoParams();
            
            result.ShouldBe(70);
            executionOrder.ShouldBe(new[] { "before", "during", "after" });
        }
    }
}