using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Imposter.Tests.Shared;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.MethodImposter
{
    public class ReturnValueSetupTests
    {
        private readonly IMethodSetupFeatureSutImposter _sut = new IMethodSetupFeatureSutImposter();

        [Fact]
        public void GivenMethodSetupWithReturnValue_WhenMethodIsInvoked_ShouldReturnSetupValue()
        {
            _sut
                .IntNoParams()
                .Returns(1);

            _sut.Instance().IntNoParams().ShouldBe(1);
            _sut.Instance().IntNoParams().ShouldBe(1);
            _sut.Instance().IntNoParams().ShouldBe(1);
        }

        [Fact]
        public void GivenOverriddenMethodSetup_WhenMethodIsInvoked_ShouldReturnLatestSetupValue()
        {
            _sut
                .IntNoParams()
                .Returns(1);

            _sut
                .IntNoParams()
                .Returns(2);

            _sut.Instance().IntNoParams().ShouldBe(2);
            _sut.Instance().IntNoParams().ShouldBe(2);
            _sut.Instance().IntNoParams().ShouldBe(2);
        }

        [Fact]
        public void GivenChainedMethodSetup_WhenMethodIsInvokedMultipleTimes_ShouldReturnValuesInSequence()
        {
            _sut
                .IntNoParams()
                .Returns(1)
                .Then()
                .Returns(2)
                .Then()
                .Returns(3);

            _sut.Instance().IntNoParams().ShouldBe(1);
            _sut.Instance().IntNoParams().ShouldBe(2);
            _sut.Instance().IntNoParams().ShouldBe(3);
            _sut.Instance().IntNoParams().ShouldBe(3);
        }

        [Fact]
        public void GivenNoMethodSetup_WhenMethodIsInvoked_ShouldReturnDefault()
        {
            var result = _sut.Instance().IntSingleParam(42);

            result.ShouldBe(default);
        }

        [Fact]
        public void GivenMethodSetupWithSpecificValue_WhenMethodIsInvokedWithMatchingValue_ShouldReturnSetupValue()
        {
            _sut
                .IntSingleParam(Arg<int>.Is(42))
                .Returns(100);

            _sut.Instance().IntSingleParam(42).ShouldBe(100);
            _sut.Instance().IntSingleParam(43).ShouldBe(default);
        }

        [Fact]
        public void GivenMethodSetupWithAnyValue_WhenMethodIsInvokedWithAnyValue_ShouldReturnSetupValue()
        {
            _sut
                .IntSingleParam(Arg<int>.Any())
                .Returns(200);

            _sut.Instance().IntSingleParam(1).ShouldBe(200);
            _sut.Instance().IntSingleParam(2).ShouldBe(200);
        }

        [Fact]
        public void GivenMethodSetupWithPredicate_WhenMethodIsInvokedWithMatchingArgs_ShouldReturnSetupValueForMatchingArgs()
        {
            _sut
                .IntSingleParam(Arg<int>.Is(a => a > 10))
                .Returns(123);

            _sut.Instance().IntSingleParam(11).ShouldBe(123);
            _sut.Instance().IntSingleParam(9).ShouldBe(default);
        }

        [Fact]
        public void GivenOverlappingSetups_WhenLaterSetupIsAdded_ShouldUseLatestMatch()
        {
            _sut
                .IntSingleParam(Arg<int>.Any())
                .Returns(100);

            _sut
                .IntSingleParam(Arg<int>.Is(value => value == 5))
                .Returns(500);

            _sut.Instance().IntSingleParam(5).ShouldBe(500);
            _sut.Instance().IntSingleParam(6).ShouldBe(100);
        }

        [Fact]
        public void GivenMethodSetupWithDelegate_WhenMethodIsInvoked_ShouldReturnComputedValue()
        {
            _sut
                .IntSingleParam(Arg<int>.Any())
                .Returns(age => age * 2);

            _sut.Instance().IntSingleParam(10).ShouldBe(20);
            _sut.Instance().IntSingleParam(21).ShouldBe(42);
        }

        [Fact]
        public void GivenChainedReturnsSetup_WhenMethodIsInvokedMultipleTimes_ShouldReturnValuesInSequence()
        {
            _sut
                .IntSingleParam(Arg<int>.Any())
                .Returns(1).Then().Returns(2).Then().Returns(3);

            _sut.Instance().IntSingleParam(1).ShouldBe(1);
            _sut.Instance().IntSingleParam(2).ShouldBe(2);
            _sut.Instance().IntSingleParam(3).ShouldBe(3);
            _sut.Instance().IntSingleParam(4).ShouldBe(3);
        }

        [Fact]
        public void GivenGenericRefParamMethodSetup_WhenMethodIsInvoked_ShouldReturnValue()
        {
            _sut
                .GenericRefParam<string, bool>(Arg<string>.Any())
                .Returns(true);

            var val = "test";
            var result = _sut.Instance().GenericRefParam<string, bool>(ref val);

            result.ShouldBeTrue();
        }

        [Fact]
        public void GivenGenericRefParamMethodSetupWithDelegate_WhenMethodIsInvoked_ShouldReturnComputedValue()
        {
            _sut
                .GenericRefParam<string, string>(Arg<string>.Is(s => s.StartsWith("ini")))
                .Returns((ref string s) =>
                {
                    var copy = s;
                    s = "modified";
                    return $"processed: {copy}";
                });

            var val = "initial";
            _sut.Instance().GenericRefParam<string, string>(ref val).ShouldBe("processed: initial");
            val.ShouldBe("modified");

            var val2 = "input";
            _sut.Instance().GenericRefParam<string, string>(ref val2).ShouldBe(default);
            val2.ShouldBe("input");
        }

        [Fact]
        public void GivenGenericAllRefKindMethodSetup_WhenMethodIsInvoked_ShouldReturnCorrectValue()
        {
            _sut.GenericAllRefKind<int, string, double, bool, long>(
                    OutArg<int>.Any(),
                    Arg<string>.Is("ref-val"),
                    Arg<double>.Is(d => d > 0),
                    Arg<bool[]>.Any()
                )
                .Returns(999L);

            int o;
            string r = "ref-val";
            double i = 1.0;
            var result = _sut.Instance().GenericAllRefKind<int, string, double, bool, long>(out o, ref r, in i, true);

            result.ShouldBe(999L);
        }

        [Fact]
        public void GivenGenericAllRefKindMethodSetupWithChainedDelegate_WhenMethodIsInvokedMultipleTimes_ShouldReturnValuesInSequence()
        {
            _sut.GenericAllRefKind<int, string, double, bool, long>(OutArg<int>.Any(), Arg<string>.Any(), Arg<double>.Any(), Arg<bool[]>.Any())
                .Returns(1L)
                .Then()
                .Returns((out int o, ref string r, in double i, bool[] p) =>
                {
                    o = (int)(i * 2);
                    r = "altered";
                    return 2L;
                });

            int o;
            string r = "original";
            double i = 10.5;

            var result1 = _sut.Instance().GenericAllRefKind<int, string, double, bool, long>(out o, ref r, in i);
            result1.ShouldBe(1L);
            o.ShouldBe(default);
            r.ShouldBe("original");

            var result2 = _sut.Instance().GenericAllRefKind<int, string, double, bool, long>(out o, ref r, in i);
            result2.ShouldBe(2L);
            o.ShouldBe(21);
            r.ShouldBe("altered");
        }

        [Fact]
        public void GivenGenericParamsParamMethodSetup_WhenMethodIsInvokedWithDerivedType_ShouldReturnValue()
        {
            _sut.GenericParamsParam<IAnimal, bool>(Arg<IAnimal[]>.Any()).Returns(true);

            var cats = new[] { new Cat("cat1"), new Cat("cat2") };
            var result = _sut.Instance().GenericParamsParam<Cat, bool>(cats);

            result.ShouldBe(true);
        }

        [Fact]
        public void GivenMultipleMethodSetups_WhenMethodIsInvokedWithDifferentValues_ShouldInvokeFirstMatchingSetup()
        {
            _sut
                .IntSingleParam(Arg<int>.Is(1))
                .Returns(11);

            _sut
                .IntSingleParam(Arg<int>.Is(2))
                .Returns(22);

            _sut
                .IntSingleParam(Arg<int>.Is(3))
                .Returns(33);

            _sut.Instance().IntSingleParam(1).ShouldBe(11);
            _sut.Instance().IntSingleParam(2).ShouldBe(22);
            _sut.Instance().IntSingleParam(3).ShouldBe(33);
        }

        [Fact]
        public void GivenMultipleMethodSetups_WhenMethodIsInvokedWithNoMatchingSetup_ShouldReturnDefault()
        {
            _sut
                .IntSingleParam(Arg<int>.Is(1))
                .Returns(11);

            _sut
                .IntSingleParam(Arg<int>.Is(2))
                .Returns(22);

            _sut
                .IntSingleParam(Arg<int>.Is(3))
                .Returns(33);

            _sut.Instance().IntSingleParam(4).ShouldBe(default);
        }

        [Fact]
        public void GivenOutParamMethodSetupWithValue_WhenMethodIsInvoked_ShouldReturnValueAndSetOutParameterToDefault()
        {
            _sut
                .IntOutParam(OutArg<int>.Any())
                .Returns(42);

            var result = _sut.Instance().IntOutParam(out var outValue);

            result.ShouldBe(42);
            outValue.ShouldBe(default); // Out parameter should be default when using simple Returns
        }

        [Fact]
        public void GivenOutParamMethodSetupWithDelegate_WhenMethodIsInvoked_ShouldReturnValueAndSetOutParameter()
        {
            _sut
                .IntOutParam(OutArg<int>.Any())
                .Returns((out int outValue) =>
                {
                    outValue = 100;
                    return 42;
                });

            var result = _sut.Instance().IntOutParam(out var outVal);

            result.ShouldBe(42);
            outVal.ShouldBe(100);
        }

        [Fact]
        public void GivenRefParamMethodSetupWithValue_WhenMethodIsInvoked_ShouldReturnValueAndNotModifyRef()
        {
            _sut
                .IntRefParam(Arg<int>.Any())
                .Returns(99);

            var refValue = 50;
            var result = _sut.Instance().IntRefParam(ref refValue);

            result.ShouldBe(99);
            refValue.ShouldBe(50); // Ref value unchanged with simple Returns
        }

        [Fact]
        public void GivenRefParamMethodSetupWithDelegate_WhenMethodIsInvoked_ShouldReturnValueAndModifyRef()
        {
            _sut
                .IntRefParam(Arg<int>.Any())
                .Returns((ref int refValue) =>
                {
                    var original = refValue;
                    refValue = refValue * 10;
                    return original + 1000;
                });

            var refValue = 5;
            var result = _sut.Instance().IntRefParam(ref refValue);

            result.ShouldBe(1005); // 5 + 1000
            refValue.ShouldBe(50); // 5 * 10
        }

        [Fact]
        public void GivenInParamMethodSetupWithValue_WhenMethodIsInvoked_ShouldReturnValue()
        {
            _sut
                .IntInParam(Arg<string>.Any())
                .Returns(123);

            var result = _sut.Instance().IntInParam("test");

            result.ShouldBe(123);
        }

        [Fact]
        public void GivenInParamMethodSetupWithDelegate_WhenMethodIsInvoked_ShouldReturnComputedValue()
        {
            _sut
                .IntInParam(Arg<string>.Any())
                .Returns((in string inValue) => inValue.Length * 10);

            _sut.Instance().IntInParam("hello").ShouldBe(50); // "hello".Length * 10
            _sut.Instance().IntInParam("test").ShouldBe(40); // "test".Length * 10
        }

        [Fact]
        public void GivenParamsArrayMethodSetupWithValue_WhenMethodIsInvoked_ShouldReturnValue()
        {
            _sut
                .IntParamsParam(Arg<string[]>.Any())
                .Returns(789);

            var result = _sut.Instance().IntParamsParam("a", "b", "c");

            result.ShouldBe(789);
        }

        [Fact]
        public void GivenParamsArrayMethodSetupWithDelegate_WhenMethodIsInvoked_ShouldReturnComputedValue()
        {
            _sut
                .IntParamsParam(Arg<string[]>.Any())
                .Returns(paramsArray => paramsArray.Length * 100);

            _sut.Instance().IntParamsParam("a", "b").ShouldBe(200); // 2 * 100
            _sut.Instance().IntParamsParam("x", "y", "z").ShouldBe(300); // 3 * 100
            _sut.Instance().IntParamsParam().ShouldBe(0); // 0 * 100
        }

        [Fact]
        public void GivenAllRefKindsMethodSetupWithValue_WhenMethodIsInvoked_ShouldReturnValueWithDefaultOutParameters()
        {
            _sut
                .IntAllRefKinds(OutArg<int>.Any(), Arg<int>.Any(), Arg<int>.Any(), Arg<string>.Any(), Arg<string[]>.Any())
                .Returns(555);

            var refValue = 10;
            int inValue = 20;
            var result = _sut.Instance().IntAllRefKinds(out var outValue, ref refValue, in inValue, "test", "a", "b");

            result.ShouldBe(555);
            outValue.ShouldBe(default);
            refValue.ShouldBe(10); // Unchanged
        }

        [Fact]
        public void GivenAllRefKindsMethodSetupWithDelegate_WhenMethodIsInvoked_ShouldReturnValueAndModifyParameters()
        {
            _sut
                .IntAllRefKinds(OutArg<int>.Any(), Arg<int>.Any(), Arg<int>.Any(), Arg<string>.Any(), Arg<string[]>.Any())
                .Returns((out int outValue, ref int refValue, in int inValue, string valueAsString, string[] paramsStrings) =>
                {
                    outValue = inValue * 2;
                    refValue = refValue + 100;
                    return valueAsString.Length + paramsStrings.Length;
                });

            var refValue = 50;
            int inValue = 20;
            var result = _sut.Instance().IntAllRefKinds(out var outValue, ref refValue, in inValue, "hello", "a", "b", "c");

            result.ShouldBe(8); // "hello".Length (5) + paramsStrings.Length (3)
            outValue.ShouldBe(40); // 20 * 2
            refValue.ShouldBe(150); // 50 + 100
        }

        [Fact]
        public void GivenGenericOutParamMethodSetupWithValue_WhenMethodIsInvoked_ShouldReturnValueWithDefaultOut()
        {
            _sut
                .GenericOutParam<string, int>(OutArg<string>.Any())
                .Returns(123);

            var result = _sut.Instance().GenericOutParam<string, int>(out var outValue);

            result.ShouldBe(123);
            outValue.ShouldBe(default);
        }
        
        [Fact]
        public void GivenMultipleGenericOutParamSetups_WhenMethodIsInvokedWithMatchingType_ShouldInvokeMatchingSetup()
        {
            _sut
                .GenericOutParam<string, int>(OutArg<string>.Any())
                .Returns(123);
            
            _sut
                .GenericOutParam<int, string>(OutArg<int>.Any())
                .Returns("hello");
            
            _sut
                .GenericOutParam<double, int>(OutArg<double>.Any())
                .Returns(321);

            var result = _sut.Instance().GenericOutParam<string, int>(out var outValue);

            result.ShouldBe(123);
            outValue.ShouldBe(default);
        }

        [Fact]
        public void GivenGenericOutParamMethodSetupWithDelegate_WhenMethodIsInvoked_ShouldReturnValueAndSetOut()
        {
            _sut
                .GenericOutParam<Cat, string>(OutArg<Cat>.Any())
                .Returns((out Cat outValue) =>
                {
                    outValue = new Cat("fluffy");
                    return "success";
                });

            var result = _sut.Instance().GenericOutParam<Cat, string>(out var outCat);

            result.ShouldBe("success");
            outCat.ShouldNotBeNull();
            outCat.Name.ShouldBe("fluffy");
        }

        [Fact]
        public void GivenGenericRefParamMethodSetupWithNullValue_WhenMethodIsInvoked_ShouldReturnNull()
        {
            _sut
                .GenericRefParam<string?, string?>(Arg<string?>.Any())
                .Returns((string?)null);

            var refValue = "test";
            var result = _sut.Instance().GenericRefParam<string?, string?>(ref refValue);

            result.ShouldBeNull();
            refValue.ShouldBe("test"); // Unchanged
        }

        [Fact]
        public void GivenGenericParamsParamMethodSetupWithDelegate_WhenMethodIsInvoked_ShouldReturnComputedValue()
        {
            _sut
                .GenericParamsParam<string, int>(Arg<string[]>.Any())
                .Returns(strings => strings.Sum(s => s.Length));

            var result = _sut.Instance().GenericParamsParam<string, int>("hello", "world");

            result.ShouldBe(10); // "hello".Length + "world".Length
        }

        [Fact]
        public void GivenGenericAllRefKindMethodSetupWithComplexDelegate_WhenMethodIsInvoked_ShouldModifyAllParameters()
        {
            _sut
                .GenericAllRefKind<Cat, IAnimal, Dog, IAnimal, string>(
                    OutArg<Cat>.Any(),
                    Arg<IAnimal>.Any(),
                    Arg<Dog>.Any(),
                    Arg<IAnimal[]>.Any()
                )
                .Returns((out Cat outValue, ref IAnimal refValue, in Dog inValue, IAnimal[] paramsValues) =>
                {
                    outValue = new Cat($"out-{inValue.Name}");
                    refValue = new Tiger($"ref-{refValue.Name}");
                    return $"processed-{paramsValues.Length}-animals";
                });

            IAnimal refAnimal = new Dog("original");
            var inDog = new Dog("input");
            var paramsAnimals = new IAnimal[] { new Cat("cat1"), new Dog("dog1") };

            var result = _sut.Instance().GenericAllRefKind<IAnimal, IAnimal, Dog, IAnimal, string>(
                out var outCat,
                ref refAnimal,
                in inDog,
                paramsAnimals
            );

            result.ShouldBe("processed-2-animals");
            outCat.ShouldBeOfType<Cat>().Name.ShouldBe("out-input");
            refAnimal.ShouldBeOfType<Tiger>().Name.ShouldBe("ref-original");
        }

        [Fact]
        public void GivenMethodSetupWithNullPredicate_WhenMethodIsInvokedWithVariousValues_ShouldHandleGracefully()
        {
            _sut
                .IntSingleParam(Arg<int>.Any())
                .Returns(100);

            // Test with various values
            _sut.Instance().IntSingleParam(0).ShouldBe(100);
            _sut.Instance().IntSingleParam(int.MinValue).ShouldBe(100);
            _sut.Instance().IntSingleParam(int.MaxValue).ShouldBe(100);
        }

        [Fact]
        public void GivenMultipleParamsMethodSetupWithValue_WhenMethodIsInvoked_ShouldReturnValue()
        {
            _sut
                .IntParams(Arg<int>.Any(), Arg<string>.Any(), Arg<Regex>.Any())
                .Returns(999);

            var result = _sut.Instance().IntParams(42, "test", new Regex(".*"));

            result.ShouldBe(999);
        }

        [Fact]
        public void GivenMultipleParamsMethodSetupWithDelegate_WhenMethodIsInvoked_ShouldReturnComputedValue()
        {
            _sut
                .IntParams(Arg<int>.Any(), Arg<string>.Any(), Arg<Regex>.Any())
                .Returns((age, name, regex) => age + name.Length);

            var result = _sut.Instance().IntParams(10, "hello", new Regex("test"));

            result.ShouldBe(15); // 10 + "hello".Length
        }

        [Fact]
        public void GivenMethodSetupOverriddenMultipleTimes_WhenMethodIsInvoked_ShouldReturnLastSetupValue()
        {
            _sut
                .IntSingleParam(Arg<int>.Is(42))
                .Returns(100);

            _sut
                .IntSingleParam(Arg<int>.Is(42))
                .Returns(200);

            _sut
                .IntSingleParam(Arg<int>.Is(42))
                .Returns(300);

            _sut.Instance().IntSingleParam(42).ShouldBe(300);
        }

        [Fact]
        public void GivenChainedMethodSetupWithMixedReturnsAndDelegates_WhenMethodIsInvokedMultipleTimes_ShouldReturnInSequence()
        {
            _sut
                .IntSingleParam(Arg<int>.Any())
                .Returns(100)
                .Then()
                .Returns(x => x * 2)
                .Then()
                .Returns(300);

            _sut.Instance().IntSingleParam(50).ShouldBe(100);
            _sut.Instance().IntSingleParam(50).ShouldBe(100); // 50 * 2
            _sut.Instance().IntSingleParam(50).ShouldBe(300);
            _sut.Instance().IntSingleParam(50).ShouldBe(300); // Repeats last
        }

        [Fact]
        public void GivenGenericParamsParamMethodSetup_WhenMethodIsInvokedWithEmptyParams_ShouldReturnSetupValue()
        {
            _sut
                .GenericParamsParam<string, int>(Arg<string[]>.Any())
                .Returns(42);

            var result = _sut.Instance().GenericParamsParam<string, int>();

            result.ShouldBe(42);
        }

        [Fact]
        public void GivenMethodSetupWithComplexPredicate_WhenMethodIsInvokedWithDifferentValues_ShouldMatchCorrectly()
        {
            _sut
                .IntSingleParam(Arg<int>.Is(x => x % 2 == 0 && x > 10))
                .Returns(1000);

            _sut.Instance().IntSingleParam(12).ShouldBe(1000); // Even and > 10
            _sut.Instance().IntSingleParam(11).ShouldBe(default); // Odd
            _sut.Instance().IntSingleParam(8).ShouldBe(default); // Even but <= 10
        }

        [Fact]
        public void GivenGenericAllRefKindMethodSetupWithTypeVariance_WhenMethodIsInvokedWithCompatibleTypes_ShouldHandleInheritanceCorrectly()
        {
            _sut
                .GenericAllRefKind<Cat, Dog, IAnimal, IAnimal, bool>( // ref parameter now matches
                    OutArg<Cat>.Any(),
                    Arg<Dog>.Any(), // Changed to Dog to match call
                    Arg<IAnimal>.Any(),
                    Arg<IAnimal[]>.Any()
                )
                .Returns(true);

            Dog refAnimal = new Dog("ref-dog");
            var inAnimal = new Tiger("in-tiger");
            var paramsAnimals = new Tiger[] { new Tiger("tiger1") };

            var result = _sut
                .Instance()
                .GenericAllRefKind<IAnimal, Dog, Tiger, Tiger, bool>(
                    out var outAnimal, // out Cat can accept IAnimal (contravariant)
                    ref refAnimal, // ref Dog matches ref Dog (invariant)
                    in inAnimal, // in Tiger can be passed as IAnimal (covariant)
                    paramsAnimals // params Tiger[] can be passed as IAnimal[] (covariant)
                );

            result.ShouldBe(true);
        }

        [Fact]
        public void GivenMethodSetupWithEmptyReturnsChain_WhenMethodIsInvoked_ShouldReturnDefault()
        {
            _sut.IntNoParams(); // No Returns() called

            _sut.Instance().IntNoParams().ShouldBe(default);
        }

        [Fact]
        public async Task GivenAsyncTaskMethodSetupWithAsyncDelegate_WhenMethodIsInvoked_ShouldReturnValue()
        {
            _sut
                .AsyncTaskIntNoParams()
                .Returns(async () =>
                {
                    await Task.Delay(TimeSpan.FromMicroseconds(1));
                    return 11;
                });

            var result = await _sut.Instance().AsyncTaskIntNoParams();
            result.ShouldBe(11);
        }
        
        [Fact]
        public async Task GivenNoAsyncTaskMethodSetup_WhenMethodIsInvoked_ShouldReturnCompletedTaskWithDefault()
        {
            var task = _sut.Instance().AsyncTaskIntNoParams();

            task.ShouldNotBeNull();
            
            var result = await task;
            result.ShouldBe(0);
        }

        [Fact]
        public async Task GivenAsyncTaskMethodSetupWithSyncValue_WhenMethodIsInvoked_ShouldReturnCompletedTask()
        {
            _sut
                .AsyncTaskIntNoParams()
                .Returns(Task.FromResult(42));

            var result = await _sut.Instance().AsyncTaskIntNoParams();
            result.ShouldBe(42);
        }

        [Fact]
        public async Task GivenAsyncTaskMethodSetupWithReturnsAsync_WhenMethodIsInvoked_ShouldWrapValueInTask()
        {
            _sut
                .AsyncTaskIntNoParams()
                .ReturnsAsync(4242);

            var task = _sut.Instance().AsyncTaskIntNoParams();

            task.ShouldNotBeNull();
            task.IsCompleted.ShouldBeTrue();

            var result = await task;
            result.ShouldBe(4242);
        }
        
        [Fact]
        public async Task GivenAsyncValueTaskMethodSetupWithReturnsAsync_WhenMethodIsInvoked_ShouldWrapValueInTask()
        {
            _sut
                .AsyncValueTaskIntNoParams()
                .ReturnsAsync(4242);

            var task = _sut.Instance().AsyncValueTaskIntNoParams();
            task.IsCompleted.ShouldBeTrue();

            var result = await task;
            result.ShouldBe(4242);
        }

        [Fact]
        public async Task GivenChainedAsyncTaskMethodSetupUsingReturnsAsync_WhenMethodIsInvokedMultipleTimes_ShouldReturnInSequence()
        {
            _sut
                .AsyncTaskIntNoParams()
                .ReturnsAsync(10)
                .Then()
                .ReturnsAsync(20)
                .Then()
                .ReturnsAsync(30);

            var first = await _sut.Instance().AsyncTaskIntNoParams();
            var second = await _sut.Instance().AsyncTaskIntNoParams();
            var third = await _sut.Instance().AsyncTaskIntNoParams();
            var fourth = await _sut.Instance().AsyncTaskIntNoParams();

            first.ShouldBe(10);
            second.ShouldBe(20);
            third.ShouldBe(30);
            fourth.ShouldBe(30);
        }

        [Fact]
        public async Task GivenAsyncTaskMethodSetupWithAsyncDelegate_WhenMethodIsInvoked_ShouldExecuteAsync()
        {
            _sut
                .AsyncTaskIntNoParams()
                .Returns(async () =>
                {
                    await Task.Delay(10); // Small delay to ensure async execution
                    return 123;
                });

            var result = await _sut.Instance().AsyncTaskIntNoParams();
            
            result.ShouldBe(123);
        }

        [Fact]
        public async Task GivenChainedAsyncTaskMethodSetup_WhenMethodIsInvokedMultipleTimes_ShouldReturnInSequence()
        {
            _sut
                .AsyncTaskIntNoParams()
                .Returns(Task.FromResult(100))
                .Then()
                .Returns(async () =>
                {
                    await Task.Delay(5);
                    return 200;
                })
                .Then()
                .Returns(Task.FromResult(300));

            var result1 = await _sut.Instance().AsyncTaskIntNoParams();
            var result2 = await _sut.Instance().AsyncTaskIntNoParams();
            var result3 = await _sut.Instance().AsyncTaskIntNoParams();
            var result4 = await _sut.Instance().AsyncTaskIntNoParams(); // Should repeat last

            result1.ShouldBe(100);
            result2.ShouldBe(200);
            result3.ShouldBe(300);
            result4.ShouldBe(300);
        }

        [Fact]
        public async Task GivenOverriddenAsyncTaskMethodSetup_WhenMethodIsInvoked_ShouldReturnLastSetupValue()
        {
            _sut
                .AsyncTaskIntNoParams()
                .Returns(Task.FromResult(111));

            _sut
                .AsyncTaskIntNoParams()
                .Returns(Task.FromResult(222));

            var result = await _sut.Instance().AsyncTaskIntNoParams();
            result.ShouldBe(222);
        }

        [Fact]
        public async Task GivenComplexAsyncTaskMethodSetup_WhenMethodIsInvokedConcurrently_ShouldHandleCorrectly()
        {
            var counter = 0;
            
            _sut
                .AsyncTaskIntNoParams()
                .Returns(async () =>
                {
                    // Simulate some async work
                    await Task.Run(() =>
                    {
                        Thread.Sleep(10);
                        counter++;
                    });
                    
                    return counter * 100;
                });

            var tasks = new[]
            {
                _sut.Instance().AsyncTaskIntNoParams(),
                _sut.Instance().AsyncTaskIntNoParams(),
                _sut.Instance().AsyncTaskIntNoParams()
            };

            var results = await Task.WhenAll(tasks);
            
            // All should have the same setup, but counter might vary due to concurrency
            results.ShouldAllBe(r => r > 0);
            counter.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task GivenAsyncTaskMethodSetupWithCancellation_WhenMethodIsInvokedWithCancellation_ShouldSupportCancellationToken()
        {
            using var cts = new CancellationTokenSource();
            var enteredDelegate = new TaskCompletionSource<object?>();

            _sut
                .AsyncTaskIntNoParams()
                .Returns(async () =>
                {
                    enteredDelegate.TrySetResult(null);
                    await Task.Delay(Timeout.Infinite, cts.Token);
                    return 999;
                });

            var callTask = _sut.Instance().AsyncTaskIntNoParams();
            await enteredDelegate.Task; // Ensure delegate is running before cancelling
            await cts.CancelAsync();

            await Should.ThrowAsync<OperationCanceledException>(async () => await callTask);
        }

        [Fact]
        public async Task GivenAsyncTaskMethodSetupWithTaskFromResult_WhenMethodIsInvoked_ShouldWorkWithOptimization()
        {
            _sut
                .AsyncTaskIntNoParams()
                .Returns(Task.FromResult(888));

            var task = _sut.Instance().AsyncTaskIntNoParams();
            
            task.ShouldNotBeNull();
            task.IsCompleted.ShouldBeTrue(); // Task.FromResult creates a completed task
            
            var result = await task;
            result.ShouldBe(888);
        }
    }
}
