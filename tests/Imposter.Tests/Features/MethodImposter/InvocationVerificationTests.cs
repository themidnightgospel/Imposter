using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Imposter.Tests.Shared;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.MethodImposter
{
    public class InvocationVerificationTests
    {
        private readonly IMethodSetupFeatureSutImposter _sut =
#if USE_CSHARP14
        IMethodSetupFeatureSut.Imposter();
#else
        new IMethodSetupFeatureSutImposter();
#endif

        [Fact]
        public void GivenIntNoParamsMethod_WhenInvoked_ShouldVerifyAllInvocations()
        {
            _sut.IntNoParams().Returns(1);

            _sut.Instance().IntNoParams().ShouldBe(1);
            _sut.Instance().IntNoParams().ShouldBe(1);
            _sut.Instance().IntNoParams().ShouldBe(1);

            _sut.IntNoParams().Called(Count.Exactly(3));
        }

        [Fact]
        public void GivenVoidNoParamsMethod_WhenCalled_ShouldVerifyCallCount()
        {
            _sut.Instance().VoidNoParams();
            _sut.Instance().VoidNoParams();

            _sut.VoidNoParams().Called(Count.Exactly(2));
        }

        [Fact]
        public void GivenVoidNoParamsMethod_WhenNotCalled_ShouldVerifyNever()
        {
            _sut.VoidNoParams().Called(Count.Never());
        }

        [Fact]
        public void GivenIntNoParamsMethod_WhenCalledOnce_ShouldVerifyOnce()
        {
            _sut.Instance().IntNoParams();

            _sut.IntNoParams().Called(Count.Once());
        }

        [Fact]
        public void GivenIntNoParamsMethod_WhenNotCalled_ShouldVerifyNever()
        {
            _sut.IntNoParams().Called(Count.Never());
        }

        [Fact]
        public void GivenIntNoParamsMethod_WhenCalledMultipleTimes_ShouldVerifyAtLeast()
        {
            _sut.Instance().IntNoParams();
            _sut.Instance().IntNoParams();
            _sut.Instance().IntNoParams();

            _sut.IntNoParams().Called(Count.AtLeast(2));
            _sut.IntNoParams().Called(Count.AtLeast(3));
            Should.Throw<VerificationFailedException>(() =>
                _sut.IntNoParams().Called(Count.AtLeast(4))
            );
        }

        [Fact]
        public void GivenIntNoParamsMethod_WhenCalledMultipleTimes_ShouldVerifyAtMost()
        {
            _sut.Instance().IntNoParams();
            _sut.Instance().IntNoParams();

            _sut.IntNoParams().Called(Count.AtMost(3));
            _sut.IntNoParams().Called(Count.AtMost(2));
            Should.Throw<VerificationFailedException>(() =>
                _sut.IntNoParams().Called(Count.AtMost(1))
            );
        }

        [Fact]
        public async Task GivenAsyncTaskIntNoParams_WhenAwaited_ShouldVerifyCallCount()
        {
            _sut.AsyncTaskIntNoParams().Returns(() => Task.FromResult(5));

            await _sut.Instance().AsyncTaskIntNoParams();
            await _sut.Instance().AsyncTaskIntNoParams();

            _sut.AsyncTaskIntNoParams().Called(Count.Exactly(2));
        }

        [Fact]
        public async Task GivenAsyncTaskIntNoParams_WhenVerificationMismatch_ShouldThrow()
        {
            _sut.AsyncTaskIntNoParams().Returns(() => Task.FromResult(1));

            await _sut.Instance().AsyncTaskIntNoParams();

            Should.Throw<VerificationFailedException>(() =>
                _sut.AsyncTaskIntNoParams().Called(Count.Exactly(2))
            );
        }

        [Fact]
        public async Task GivenAsyncTaskIntNoParams_WhenChainingThen_ShouldVerifyAcrossInvocations()
        {
            _sut.AsyncTaskIntNoParams()
                .Returns(() => Task.FromResult(10))
                .Then()
                .Returns(() => Task.FromResult(20));

            await _sut.Instance().AsyncTaskIntNoParams(); // first setup
            await _sut.Instance().AsyncTaskIntNoParams(); // second setup
            await _sut.Instance().AsyncTaskIntNoParams(); // repeats last setup

            _sut.AsyncTaskIntNoParams().Called(Count.Exactly(3));
            Should.Throw<VerificationFailedException>(() =>
                _sut.AsyncTaskIntNoParams().Called(Count.AtLeast(4))
            );
        }

        [Fact]
        public async Task GivenAsyncValueTaskIntNoParams_WhenAwaited_ShouldVerifyCallCount()
        {
            _sut.AsyncValueTaskIntNoParams().ReturnsAsync(7);

            await _sut.Instance().AsyncValueTaskIntNoParams();
            await _sut.Instance().AsyncValueTaskIntNoParams();
            await _sut.Instance().AsyncValueTaskIntNoParams();

            _sut.AsyncValueTaskIntNoParams().Called(Count.Exactly(3));
        }

        [Fact]
        public void GivenIntSingleParamMethod_WhenCalledWithDifferentValues_ShouldMatchArgs()
        {
            _sut.Instance().IntSingleParam(42);
            _sut.Instance().IntSingleParam(43);
            _sut.Instance().IntSingleParam(42);

            _sut.IntSingleParam(Arg<int>.Is(42)).Called(Count.Exactly(2));
            _sut.IntSingleParam(Arg<int>.Is(43)).Called(Count.Once());
            _sut.IntSingleParam(Arg<int>.Is(44)).Called(Count.Never());
        }

        [Fact]
        public void GivenIntSingleParamMethod_WhenUsingAnyMatcher_ShouldVerifyAnyArg()
        {
            _sut.Instance().IntSingleParam(10);
            _sut.Instance().IntSingleParam(20);
            _sut.Instance().IntSingleParam(30);

            _sut.IntSingleParam(Arg<int>.Any()).Called(Count.Exactly(3));
        }

        [Fact]
        public void GivenIntSingleParamMethod_WhenUsingPredicate_ShouldMatchPredicate()
        {
            _sut.Instance().IntSingleParam(15);
            _sut.Instance().IntSingleParam(25);
            _sut.Instance().IntSingleParam(5);

            _sut.IntSingleParam(Arg<int>.Is(x => x > 10)).Called(Count.Exactly(2));
            _sut.IntSingleParam(Arg<int>.Is(x => x <= 10)).Called(Count.Once());
        }

        [Fact]
        public void GivenIntParamsMethod_WhenCalledWithDifferentArgs_ShouldVerifyCorrectly()
        {
            var regex1 = new Regex("test1");
            var regex2 = new Regex("test2");

            _sut.Instance().IntParams(1, "name1", regex1);
            _sut.Instance().IntParams(2, "name2", regex2);
            _sut.Instance().IntParams(1, "name1", regex1);

            _sut.IntParams(Arg<int>.Is(1), Arg<string>.Is("name1"), Arg<Regex>.Any())
                .Called(Count.Exactly(2));
            _sut.IntParams(Arg<int>.Is(2), Arg<string>.Any(), Arg<Regex>.Any())
                .Called(Count.Once());
            _sut.IntParams(Arg<int>.Is(3), Arg<string>.Any(), Arg<Regex>.Any())
                .Called(Count.Never());
        }

        [Fact]
        public void GivenIntOutParamMethod_WhenCalled_ShouldVerifyCorrectly()
        {
            _sut.Instance().IntOutParam(out var outVal1);
            _sut.Instance().IntOutParam(out var outVal2);

            _sut.IntOutParam(OutArg<int>.Any()).Called(Count.Exactly(2));
        }

        [Fact]
        public void GivenIntRefParamMethod_WhenCalledWithDifferentValues_ShouldVerifyCorrectly()
        {
            var refValue1 = 10;
            var refValue2 = 20;
            var refValue3 = 10;

            _sut.Instance().IntRefParam(ref refValue1);
            _sut.Instance().IntRefParam(ref refValue2);
            _sut.Instance().IntRefParam(ref refValue3);

            _sut.IntRefParam(Arg<int>.Is(10)).Called(Count.Exactly(2));
            _sut.IntRefParam(Arg<int>.Is(20)).Called(Count.Once());
            _sut.IntRefParam(Arg<int>.Any()).Called(Count.Exactly(3));
        }

        [Fact]
        public void GivenIntInParamMethod_WhenCalledWithDifferentValues_ShouldVerifyCorrectly()
        {
            _sut.Instance().IntInParam("hello");
            _sut.Instance().IntInParam("world");
            _sut.Instance().IntInParam("hello");

            _sut.IntInParam(Arg<string>.Is("hello")).Called(Count.Exactly(2));
            _sut.IntInParam(Arg<string>.Is("world")).Called(Count.Once());
            _sut.IntInParam(Arg<string>.Is("foo")).Called(Count.Never());
        }

        [Fact]
        public void GivenIntParamsParamMethod_WhenCalledWithDifferentArrays_ShouldVerifyCorrectly()
        {
            _sut.Instance().IntParamsParam("a", "b");
            _sut.Instance().IntParamsParam("x", "y", "z");
            _sut.Instance().IntParamsParam("a", "b");

            _sut.IntParamsParam(Arg<string[]>.Is(arr => arr.Length == 2)).Called(Count.Exactly(2));
            _sut.IntParamsParam(Arg<string[]>.Is(arr => arr.Length == 3)).Called(Count.Once());
            _sut.IntParamsParam(Arg<string[]>.Any()).Called(Count.Exactly(3));
        }

        [Fact]
        public void GivenIntParamsParamMethod_WhenCalledWithSequence_ShouldVerifyExactMatch()
        {
            _sut.Instance().IntParamsParam("alpha", "beta", "gamma");

            _sut.IntParamsParam(
                    Arg<string[]>.Is(arr => arr.SequenceEqual(new[] { "alpha", "beta", "gamma" }))
                )
                .Called(Count.Once());
        }

        [Fact]
        public void GivenIntAllRefKindsMethod_WhenCalledWithComplexParameters_ShouldVerifyCorrectly()
        {
            var refValue1 = 100;
            var refValue2 = 200;

            int inVal = 50;
            _sut.Instance()
                .IntAllRefKinds(out var outVal1, ref refValue1, in inVal, "test1", "a", "b");
            int inVal2 = 60;
            _sut.Instance()
                .IntAllRefKinds(out var outVal2, ref refValue2, in inVal2, "test2", "x", "y", "z");

            _sut.IntAllRefKinds(
                    OutArg<int>.Any(),
                    Arg<int>.Any(),
                    Arg<int>.Any(),
                    Arg<string>.Any(),
                    Arg<string[]>.Any()
                )
                .Called(Count.Exactly(2));

            _sut.IntAllRefKinds(
                    OutArg<int>.Any(),
                    Arg<int>.Is(100),
                    Arg<int>.Is(50),
                    Arg<string>.Is("test1"),
                    Arg<string[]>.Is(arr => arr.Length == 2)
                )
                .Called(Count.Once());
        }

        [Fact]
        public void GivenGenericSingleParamMethod_WhenCalledWithDifferentTypes_ShouldVerifyCorrectly()
        {
            _sut.Instance().GenericSingleParam<int>(42);
            _sut.Instance().GenericSingleParam<string>("hello");
            _sut.Instance().GenericSingleParam<int>(100);

            _sut.GenericSingleParam<int>(Arg<int>.Any()).Called(Count.Exactly(2));
            _sut.GenericSingleParam<string>(Arg<string>.Any()).Called(Count.Once());
            _sut.GenericSingleParam<double>(Arg<double>.Any()).Called(Count.Never());
        }

        [Fact]
        public void GivenGenericSingleParamMethod_WhenCalledWithSpecificValues_ShouldVerifyCorrectly()
        {
            _sut.Instance().GenericSingleParam<int>(42);
            _sut.Instance().GenericSingleParam<int>(43);
            _sut.Instance().GenericSingleParam<int>(42);

            _sut.GenericSingleParam<int>(Arg<int>.Is(42)).Called(Count.Exactly(2));
            _sut.GenericSingleParam<int>(Arg<int>.Is(43)).Called(Count.Once());
            _sut.GenericSingleParam<int>(Arg<int>.Is(x => x > 40)).Called(Count.Exactly(3));
        }

        [Fact]
        public void GivenGenericOutParamMethod_WhenCalled_ShouldVerifyCorrectly()
        {
            _sut.Instance().GenericOutParam<string, int>(out var outVal1);
            _sut.Instance().GenericOutParam<int, string>(out var outVal2);
            _sut.Instance().GenericOutParam<string, int>(out var outVal3);

            _sut.GenericOutParam<string, int>(OutArg<string>.Any()).Called(Count.Exactly(2));
            _sut.GenericOutParam<int, string>(OutArg<int>.Any()).Called(Count.Once());
            _sut.GenericOutParam<double, bool>(OutArg<double>.Any()).Called(Count.Never());
        }

        [Fact]
        public void GivenGenericRefParamMethod_WhenCalledWithDifferentValues_ShouldVerifyCorrectly()
        {
            var refValue1 = "hello";
            var refValue2 = "world";
            var refValue3 = "hello";

            _sut.Instance().GenericRefParam<string, int>(ref refValue1);
            _sut.Instance().GenericRefParam<string, int>(ref refValue2);
            _sut.Instance().GenericRefParam<string, int>(ref refValue3);

            _sut.GenericRefParam<string, int>(Arg<string>.Is("hello")).Called(Count.Exactly(2));
            _sut.GenericRefParam<string, int>(Arg<string>.Is("world")).Called(Count.Once());
            _sut.GenericRefParam<string, int>(Arg<string>.Any()).Called(Count.Exactly(3));
        }

        [Fact]
        public void GivenGenericParamsParamMethod_WhenCalledWithDifferentArrays_ShouldVerifyCorrectly()
        {
            _sut.Instance().GenericParamsParam<string, int>("a", "b");
            _sut.Instance().GenericParamsParam<int, string>(1, 2, 3);
            _sut.Instance().GenericParamsParam<string, int>("x", "y", "z");

            _sut.GenericParamsParam<string, int>(Arg<string[]>.Any()).Called(Count.Exactly(2));
            _sut.GenericParamsParam<int, string>(Arg<int[]>.Any()).Called(Count.Once());
            _sut.GenericParamsParam<string, int>(Arg<string[]>.Is(arr => arr.Length == 2))
                .Called(Count.Once());
        }

        [Fact]
        public void GivenGenericAllRefKindMethod_WhenCalledWithComplexParameters_ShouldVerifyCorrectly()
        {
            IAnimal refAnimal1 = new Cat("cat1");
            IAnimal refAnimal2 = new Dog("dog1");

            var inVal = new Dog("input1");
            _sut.Instance()
                .GenericAllRefKind<Cat, IAnimal, Dog, Tiger, bool>(
                    out var outCat1,
                    ref refAnimal1,
                    in inVal,
                    new Tiger("tiger1"),
                    new Tiger("tiger2")
                );

            var inVal2 = new Cat("input2");
            _sut.Instance()
                .GenericAllRefKind<Dog, IAnimal, Cat, Tiger, string>(
                    out var outDog1,
                    ref refAnimal2,
                    in inVal2,
                    new Tiger("tiger3")
                );

            _sut.GenericAllRefKind<Cat, IAnimal, Dog, Tiger, bool>(
                    OutArg<Cat>.Any(),
                    Arg<IAnimal>.Any(),
                    Arg<Dog>.Any(),
                    Arg<Tiger[]>.Any()
                )
                .Called(Count.Once());

            _sut.GenericAllRefKind<Dog, IAnimal, Cat, Tiger, string>(
                    OutArg<Dog>.Any(),
                    Arg<IAnimal>.Any(),
                    Arg<Cat>.Any(),
                    Arg<Tiger[]>.Any()
                )
                .Called(Count.Once());
        }

        [Fact]
        public void GivenGenericSingleParamMethod_WhenCalledWithInheritanceTypes_ShouldVerifyCorrectly()
        {
            _sut.Instance().GenericSingleParam<IAnimal>(new Cat("fluffy"));
            _sut.Instance().GenericSingleParam<IAnimal>(new Dog("buddy"));
            _sut.Instance().GenericSingleParam<Cat>(new Cat("whiskers"));

            _sut.GenericSingleParam<IAnimal>(Arg<IAnimal>.Any()).Called(Count.Exactly(2));

            _sut.GenericSingleParam<Cat>(Arg<Cat>.Any()).Called(Count.Exactly(1));
            _sut.GenericSingleParam<IAnimal>(Arg<IAnimal>.Is(it => it is Cat)).Called(Count.Once());
            _sut.GenericSingleParam<IAnimal>(Arg<IAnimal>.Is(it => it is Dog)).Called(Count.Once());
            _sut.GenericSingleParam<IAnimal>(Arg<IAnimal>.Is(a => a.Name.StartsWith("fluf")))
                .Called(Count.Once());
        }

        [Fact]
        public void GivenGenericOutParamMethod_WhenCalledWithInheritanceTypes_ShouldVerifyCorrectly()
        {
            _sut.Instance().GenericOutParam<Cat, int>(out Cat car);
            _sut.Instance().GenericOutParam<Dog, string>(out Dog dog);
            _sut.Instance().GenericOutParam<Tiger, double>(out Tiger tiger);

            // Assert
            _sut.GenericOutParam<IAnimal, int>(OutArg<IAnimal>.Any()).Called(Count.Never());
            _sut.GenericOutParam<Animal, int>(OutArg<Animal>.Any()).Called(Count.Never());
            _sut.GenericOutParam<Cat, string>(OutArg<Cat>.Any()).Called(Count.Never());
            _sut.GenericOutParam<Cat, int>(OutArg<Cat>.Any()).Called(Count.Once());

            _sut.GenericOutParam<Dog, string>(OutArg<Dog>.Any()).Called(Count.Once());

            _sut.GenericOutParam<Tiger, double>(OutArg<Tiger>.Any()).Called(Count.Once());
        }

        [Fact]
        public void GivenIntSingleParamMethod_WhenVerificationFails_ShouldThrow()
        {
            _sut.Instance().IntSingleParam(42);

            Should.Throw<VerificationFailedException>(() =>
                _sut.IntSingleParam(Arg<int>.Is(42)).Called(Count.Exactly(2))
            );
        }

        [Fact]
        public void GivenVoidNoParamsMethod_WhenVerificationFails_ShouldThrow()
        {
            _sut.Instance().VoidNoParams();

            Should.Throw<VerificationFailedException>(() =>
                _sut.VoidNoParams().Called(Count.Never())
            );
        }

        [Fact]
        public void GivenIntNoParamsMethod_WhenAtLeastVerificationFails_ShouldThrow()
        {
            _sut.Instance().IntNoParams();

            Should.Throw<VerificationFailedException>(() =>
                _sut.IntNoParams().Called(Count.AtLeast(2))
            );
        }

        [Fact]
        public void GivenIntNoParamsMethod_WhenAtMostVerificationFails_ShouldThrow()
        {
            _sut.Instance().IntNoParams();
            _sut.Instance().IntNoParams();
            _sut.Instance().IntNoParams();

            Should.Throw<VerificationFailedException>(() =>
                _sut.IntNoParams().Called(Count.AtMost(2))
            );
        }

        [Fact]
        public void GivenGenericSingleParamMethod_WhenVerificationFails_ShouldThrow()
        {
            _sut.Instance().GenericSingleParam<string>("test");

            Should.Throw<VerificationFailedException>(() =>
                _sut.GenericSingleParam<int>(Arg<int>.Any()).Called(Count.Once())
            );
        }

        [Fact]
        public void GivenIntParamsMethod_WhenCalledWithNullValues_ShouldVerifyCorrectly()
        {
            _sut.Instance().IntParams(42, null!, new Regex("test"));

            _sut.IntParams(Arg<int>.Is(42), Arg<string>.Is(val => val == null!), Arg<Regex>.Any())
                .Called(Count.Once());
            _sut.IntParams(Arg<int>.Any(), Arg<string>.Any(), Arg<Regex>.Any())
                .Called(Count.Once());
        }

        [Fact]
        public void GivenIntParamsParamMethod_WhenCalledWithEmptyArray_ShouldVerifyCorrectly()
        {
            _sut.Instance().IntParamsParam(new string[0]);

            _sut.IntParamsParam(Arg<string[]>.Is(arr => arr.Length == 0)).Called(Count.Once());
            _sut.IntParamsParam(Arg<string[]>.Any()).Called(Count.Once());
        }

        [Fact]
        public void GivenMultipleMethods_WhenVerifyingSeparately_ShouldRemainIndependent()
        {
            _sut.Instance().VoidNoParams();
            _sut.Instance().IntNoParams();
            _sut.Instance().IntSingleParam(42);
            _sut.Instance().VoidNoParams();

            _sut.VoidNoParams().Called(Count.Exactly(2));
            _sut.IntNoParams().Called(Count.Once());
            _sut.IntSingleParam(Arg<int>.Is(42)).Called(Count.Once());
            _sut.IntSingleParam(Arg<int>.Is(43)).Called(Count.Never());
        }

        [Fact]
        public void GivenIntSingleParamMethod_WhenConfiguredMultipleTimes_ShouldVerifyCorrectly()
        {
            _sut.IntSingleParam(Arg<int>.Is(1)).Returns(10);
            _sut.IntSingleParam(Arg<int>.Is(2)).Returns(20);

            _sut.Instance().IntSingleParam(1);
            _sut.Instance().IntSingleParam(2);
            _sut.Instance().IntSingleParam(1);
            _sut.Instance().IntSingleParam(3); // No setup, returns default

            _sut.IntSingleParam(Arg<int>.Is(1)).Called(Count.Exactly(2));
            _sut.IntSingleParam(Arg<int>.Is(2)).Called(Count.Once());
            _sut.IntSingleParam(Arg<int>.Is(3)).Called(Count.Once());
            _sut.IntSingleParam(Arg<int>.Any()).Called(Count.Exactly(4));
        }
    }
}
