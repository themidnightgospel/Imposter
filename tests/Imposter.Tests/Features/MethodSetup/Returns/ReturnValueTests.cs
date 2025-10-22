using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.MethodSetup.Returns
{
    public class ReturnValueTests
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
    }
}