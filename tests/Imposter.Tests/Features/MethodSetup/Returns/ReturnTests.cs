using System.Linq;
using System.Text.RegularExpressions;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.MethodSetup.Returns
{
    public class ReturnTests
    {
        private readonly IMethodSetupFeatureSutImposter _sut = new IMethodSetupFeatureSutImposter();

        [Fact]
        public void IntNoParams_MethodInvoked_SetupReturnValueReturned()
        {
            _sut
                .IntNoParams()
                .Returns(1);

            _sut.Instance().IntNoParams().ShouldBe(1);
            _sut.Instance().IntNoParams().ShouldBe(1);
            _sut.Instance().IntNoParams().ShouldBe(1);
        }

        [Fact]
        public void IntNoParams_SetupOverridenAndInvoked_SetupReturnValueReturned()
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
        public void IntNoParams_SetupChainedAndInvoked_SetupReturnValueAsSequence()
        {
            _sut
                .IntNoParams()
                .Returns(1)
                .Returns(2)
                .Returns(3);

            _sut.Instance().IntNoParams().ShouldBe(1);
            _sut.Instance().IntNoParams().ShouldBe(2);
            _sut.Instance().IntNoParams().ShouldBe(3);
            _sut.Instance().IntNoParams().ShouldBe(3);
        }

        [Fact]
        public void IntSingleParam_WhenNoSetup_ReturnsDefault()
        {
            var result = _sut.Instance().IntSingleParam(42);

            result.ShouldBe(default);
        }

        [Fact]
        public void IntSingleParam_WhenSetupWithSpecificValue_ReturnsSetupValue()
        {
            _sut
                .IntSingleParam(Arg<int>.Is(42))
                .Returns(100);

            _sut.Instance().IntSingleParam(42).ShouldBe(100);
            _sut.Instance().IntSingleParam(43).ShouldBe(default);
        }

        [Fact]
        public void IntSingleParam_WhenSetupWithAnyValue_ReturnsSetupValue()
        {
            _sut
                .IntSingleParam(Arg<int>.Any())
                .Returns(200);

            _sut.Instance().IntSingleParam(1).ShouldBe(200);
            _sut.Instance().IntSingleParam(2).ShouldBe(200);
        }

        [Fact]
        public void IntSingleParam_WhenSetupWithPredicate_ReturnsSetupValueForMatchingArgs()
        {
            _sut
                .IntSingleParam(Arg<int>.Is(a => a > 10))
                .Returns(123);

            _sut.Instance().IntSingleParam(11).ShouldBe(123);
            _sut.Instance().IntSingleParam(9).ShouldBe(default);
        }

        [Fact]
        public void IntSingleParam_WhenSetupWithDelegate_ReturnsComputedValue()
        {
            _sut
                .IntSingleParam(Arg<int>.Any())
                .Returns(age => age * 2);

            _sut.Instance().IntSingleParam(10).ShouldBe(20);
            _sut.Instance().IntSingleParam(21).ShouldBe(42);
        }

        [Fact]
        public void IntSingleParam_WhenChainedReturns_ReturnsValuesInSequence()
        {
            _sut
                .IntSingleParam(Arg<int>.Any())
                .Returns(1).Returns(2).Returns(3);

            _sut.Instance().IntSingleParam(1).ShouldBe(1);
            _sut.Instance().IntSingleParam(2).ShouldBe(2);
            _sut.Instance().IntSingleParam(3).ShouldBe(3);
            _sut.Instance().IntSingleParam(4).ShouldBe(3);
        }

        [Fact]
        public void GenericRefParam_WhenSetup_ReturnsValue()
        {
            _sut
                .GenericRefParam<string, bool>(Arg<string>.Any())
                .Returns(true);

            var val = "test";
            var result = _sut.Instance().GenericRefParam<string, bool>(ref val);

            result.ShouldBeTrue();
        }

        [Fact]
        public void GenericRefParam_WhenSetupWithDelegate_ReturnsComputedValue()
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
        public void GenericAllRefKind_WhenSetup_ReturnsCorrectValue()
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
        public void GenericAllRefKind_WhenChainedReturnsWithDelegate_ReturnsValuesInSequence()
        {
            _sut.GenericAllRefKind<int, string, double, bool, long>(OutArg<int>.Any(), Arg<string>.Any(), Arg<double>.Any(), Arg<bool[]>.Any())
                .Returns(1L)
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
        public void GenericParamsParam_WhenCalledWithDerivedType_ReturnsValue()
        {
            _sut.GenericParamsParam<IAnimal, bool>(Arg<IAnimal[]>.Any()).Returns(true);

            var cats = new[] { new Cat("cat1"), new Cat("cat2") };
            var result = _sut.Instance().GenericParamsParam<Cat, bool>(cats);

            result.ShouldBe(true);
        }

        [Fact]
        public void IntSingleParam_WhenMultipleSetups_FirstMatchingSetupInvoked()
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
        public void IntSingleParam_NoMatchingSetup_ReturnsDefalt()
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
        public void IntOutParam_WhenSetupWithValue_ReturnsValueAndSetsOutParameter()
        {
            _sut
                .IntOutParam(OutArg<int>.Any())
                .Returns(42);

            var result = _sut.Instance().IntOutParam(out var outValue);

            result.ShouldBe(42);
            outValue.ShouldBe(default); // Out parameter should be default when using simple Returns
        }

        [Fact]
        public void IntOutParam_WhenSetupWithDelegate_ReturnsValueAndSetsOutParameter()
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
        public void IntRefParam_WhenSetupWithValue_ReturnsValueAndDoesNotModifyRef()
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
        public void IntRefParam_WhenSetupWithDelegate_ReturnsValueAndModifiesRef()
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
        public void IntInParam_WhenSetupWithValue_ReturnsValue()
        {
            _sut
                .IntInParam(Arg<string>.Any())
                .Returns(123);

            var result = _sut.Instance().IntInParam("test");

            result.ShouldBe(123);
        }

        [Fact]
        public void IntInParam_WhenSetupWithDelegate_ReturnsComputedValue()
        {
            _sut
                .IntInParam(Arg<string>.Any())
                .Returns((in string inValue) => inValue.Length * 10);

            _sut.Instance().IntInParam("hello").ShouldBe(50); // "hello".Length * 10
            _sut.Instance().IntInParam("test").ShouldBe(40); // "test".Length * 10
        }

        [Fact]
        public void IntParamsParam_WhenSetupWithValue_ReturnsValue()
        {
            _sut
                .IntParamsParam(Arg<string[]>.Any())
                .Returns(789);

            var result = _sut.Instance().IntParamsParam("a", "b", "c");

            result.ShouldBe(789);
        }

        [Fact]
        public void IntParamsParam_WhenSetupWithDelegate_ReturnsComputedValue()
        {
            _sut
                .IntParamsParam(Arg<string[]>.Any())
                .Returns(paramsArray => paramsArray.Length * 100);

            _sut.Instance().IntParamsParam("a", "b").ShouldBe(200); // 2 * 100
            _sut.Instance().IntParamsParam("x", "y", "z").ShouldBe(300); // 3 * 100
            _sut.Instance().IntParamsParam().ShouldBe(0); // 0 * 100
        }

        [Fact]
        public void IntAllRefKinds_WhenSetupWithValue_ReturnsValueWithDefaultOutParameters()
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
        public void IntAllRefKinds_WhenSetupWithDelegate_ReturnsValueAndModifiesParameters()
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
        public void GenericOutParam_WhenSetupWithValue_ReturnsValueWithDefaultOut()
        {
            _sut
                .GenericOutParam<string, int>(OutArg<string>.Any())
                .Returns(123);

            var result = _sut.Instance().GenericOutParam<string, int>(out var outValue);

            result.ShouldBe(123);
            outValue.ShouldBe(default);
        }

        [Fact]
        public void GenericOutParam_WhenSetupWithDelegate_ReturnsValueAndSetsOut()
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
        public void GenericRefParam_WhenSetupWithNullValue_ReturnsNull()
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
        public void GenericParamsParam_WhenSetupWithDelegate_ReturnsComputedValue()
        {
            _sut
                .GenericParamsParam<string, int>(Arg<string[]>.Any())
                .Returns(strings => strings.Sum(s => s.Length));

            var result = _sut.Instance().GenericParamsParam<string, int>("hello", "world");

            result.ShouldBe(10); // "hello".Length + "world".Length
        }

        [Fact]
        public void GenericAllRefKind_WhenSetupWithComplexDelegate_ModifiesAllParameters()
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
        public void IntSingleParam_WhenSetupWithNullPredicate_HandledGracefully()
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
        public void IntParams_WhenSetupWithValue_ReturnsValue()
        {
            _sut
                .IntParams(Arg<int>.Any(), Arg<string>.Any(), Arg<Regex>.Any())
                .Returns(999);

            var result = _sut.Instance().IntParams(42, "test", new Regex(".*"));

            result.ShouldBe(999);
        }

        [Fact]
        public void IntParams_WhenSetupWithDelegate_ReturnsComputedValue()
        {
            _sut
                .IntParams(Arg<int>.Any(), Arg<string>.Any(), Arg<Regex>.Any())
                .Returns((age, name, regex) => age + name.Length);

            var result = _sut.Instance().IntParams(10, "hello", new Regex("test"));

            result.ShouldBe(15); // 10 + "hello".Length
        }

        [Fact]
        public void IntSingleParam_WhenSetupOverriddenMultipleTimes_LastSetupWins()
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
        public void IntSingleParam_WhenChainedWithMixedReturnsAndDelegates_ReturnsInSequence()
        {
            _sut
                .IntSingleParam(Arg<int>.Any())
                .Returns(100)
                .Returns(x => x * 2)
                .Returns(300);

            _sut.Instance().IntSingleParam(50).ShouldBe(100);
            _sut.Instance().IntSingleParam(50).ShouldBe(100); // 50 * 2
            _sut.Instance().IntSingleParam(50).ShouldBe(300);
            _sut.Instance().IntSingleParam(50).ShouldBe(300); // Repeats last
        }

        [Fact]
        public void GenericParamsParam_WhenCalledWithEmptyParams_ReturnsSetupValue()
        {
            _sut
                .GenericParamsParam<string, int>(Arg<string[]>.Any())
                .Returns(42);

            var result = _sut.Instance().GenericParamsParam<string, int>();

            result.ShouldBe(42);
        }

        [Fact]
        public void IntSingleParam_WhenSetupWithComplexPredicate_MatchesCorrectly()
        {
            _sut
                .IntSingleParam(Arg<int>.Is(x => x % 2 == 0 && x > 10))
                .Returns(1000);

            _sut.Instance().IntSingleParam(12).ShouldBe(1000); // Even and > 10
            _sut.Instance().IntSingleParam(11).ShouldBe(default); // Odd
            _sut.Instance().IntSingleParam(8).ShouldBe(default); // Even but <= 10
        }

        [Fact]
        public void GenericAllRefKind_WhenSetupWithTypeVariance_HandlesInheritanceCorrectly()
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
        public void IntNoParams_WhenSetupWithEmptyReturnsChain_ReturnsDefault()
        {
            _sut.IntNoParams(); // No Returns() called

            _sut.Instance().IntNoParams().ShouldBe(default);
        }
    }
}