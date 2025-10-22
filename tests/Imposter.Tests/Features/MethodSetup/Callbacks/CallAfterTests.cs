
using System.Collections.Generic;
using System.Linq;
using Imposter.Abstractions;
using Shouldly;
using System.Text.RegularExpressions;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.MethodSetup.Callbacks
{
    public class CallAfterTests
    {
        private readonly IMethodSetupFeatureSutImposter _sut = new IMethodSetupFeatureSutImposter();

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void VoidNoParams_MethodInvoked_CallbackInvoked(int invocationCount)
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
        public void IntNoParams_WhenMethodInvoked_CallbackIsInvoked(int invocationCount)
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
        public void IntSingleParam_WhenMethodInvoked_CallbackInvokedWithParameters(int invocationCount)
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
        public void IntParams_WhenMethodInvoked_CallbackInvokedWithParameters(int invocationCount)
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
        public void IntOutParam_OutParameterInitializedInCallback_OutParameterValueIsPropagated(int invocationCount)
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
        public void IntRefParam_WhenMethodInvoked_CallbackIsInvokedAndCanModifyRefValue(int invocationCount)
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
        public void IntParamsParam_WhenMethodInvoked_CallbackIsInvokedWithCorrectParameters(int invocationCount)
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
        public void IntInParam_WhenMethodInvoked_CallbackIsInvokedWithCorrectParameters(int invocationCount)
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
        public void IntAllRefKinds_WhenMethodInvoked_CallbackIsInvokedWithCorrectParameters(int invocationCount)
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
        public void GenericSingleParam_WhenMethodInvoked_CallbackIsInvokedWithCorrectParameter(int invocationCount)
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
        public void GenericOutParam_WhenMethodInvoked_CallbackIsInvokedAndValuePropagated(int invocationCount)
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
        public void GenericRefParam_WhenMethodInvoked_CallbackIsInvokedAndCanModifyValue(int invocationCount)
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
        public void GenericParamsParam_WhenMethodInvoked_CallbackIsInvokedWithCorrectParameters(int invocationCount)
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
        public void GenericAllRefKind_WhenMethodInvoked_CallbackIsInvokedWithCorrectParameters(int invocationCount)
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
        public void GenericSingleParam_WhenMethodInvokedWithDerivedType_CallbackIsInvoked()
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
        public void GenericSingleParam_WhenMethodInvokedWithBaseType_CallbackIsNotInvoked()
        {
            var callAfterCallbackInvokedCount = 0;

            _sut
                .GenericSingleParam<IAnimal>(Arg<IAnimal>.Any())
                .CallAfter(animal =>
                {
                    callAfterCallbackInvokedCount++;
                });

            _sut.Instance().GenericSingleParam<IMammal>(new Cat("mammal"));

            callAfterCallbackInvokedCount.ShouldBe(0);
        }

        [Fact]
        public void GenericOutParam_WhenMethodInvokedWithBaseType_CallbackIsInvoked()
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
        public void GenericOutParam_WhenMethodInvokedWithDerivedType_CallbackIsNotInvoked()
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
        public void GenericParamsParam_WhenMethodInvokedWithDerivedType_CallbackIsInvoked()
        {
            var captured = new List<IAnimal[]>();
            _sut
                .GenericParamsParam<IAnimal, int>(Arg<IAnimal[]>.Any())
                .CallAfter(animals =>
                {
                    captured.Add(animals);
                });

            _sut.Instance().GenericParamsParam<ICat, int>(new Cat("cat1"), new Cat("cat2"));
            _sut.Instance().GenericParamsParam<Dog, int>(new Dog("dog1"));

            captured.Count.ShouldBe(2);
            captured[0].Select(a => a.Name).ShouldBe(new[] { "cat1", "cat2" });
            captured[1].Select(a => a.Name).ShouldBe(new[] { "dog1" });
        }

        [Fact]
        public void GenericAllRefKind_InvokedWithCompatibleTypes_CallbackIsInvokedWithCorrectParameters()
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
    }
}