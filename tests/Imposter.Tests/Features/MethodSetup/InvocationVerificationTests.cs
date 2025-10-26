using System.Text.RegularExpressions;
using Imposter.Abstractions;
using Imposter.CodeGenerator.Tests.Shared;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.MethodSetup
{
    public class InvocationVerificationTests
    {
        private readonly IMethodSetupFeatureSutImposter _sut = new IMethodSetupFeatureSutImposter();

        [Fact]
        public void IntNoParams_MethodInvoked_VerifiedAllInvocations()
        {
            _sut
                .IntNoParams()
                .Returns(1);

            _sut.Instance().IntNoParams().ShouldBe(1);
            _sut.Instance().IntNoParams().ShouldBe(1);
            _sut.Instance().IntNoParams().ShouldBe(1);

            _sut.IntNoParams().Called(Count.Exactly(3));
        }

        [Fact]
        public void VoidNoParams_WhenCalled_VerifiesCallCount()
        {
            _sut.Instance().VoidNoParams();
            _sut.Instance().VoidNoParams();

            _sut.VoidNoParams().Called(Count.Exactly(2));
        }

        [Fact]
        public void VoidNoParams_WhenNotCalled_VerifiesNeverCalled()
        {
            _sut.VoidNoParams().Called(Count.Never());
        }

        [Fact]
        public void IntNoParams_WhenCalledOnce_VerifiesOnce()
        {
            _sut.Instance().IntNoParams();

            _sut.IntNoParams().Called(Count.Once());
        }

        [Fact]
        public void IntNoParams_WhenNotCalled_VerifiesNever()
        {
            _sut.IntNoParams().Called(Count.Never());
        }

        [Fact]
        public void IntNoParams_WhenCalledMultipleTimes_VerifiesAtLeast()
        {
            _sut.Instance().IntNoParams();
            _sut.Instance().IntNoParams();
            _sut.Instance().IntNoParams();

            _sut.IntNoParams().Called(Count.AtLeast(2));
            _sut.IntNoParams().Called(Count.AtLeast(3));
        }

        [Fact]
        public void IntNoParams_WhenCalledMultipleTimes_VerifiesAtMost()
        {
            _sut.Instance().IntNoParams();
            _sut.Instance().IntNoParams();

            _sut.IntNoParams().Called(Count.AtMost(3));
            _sut.IntNoParams().Called(Count.AtMost(2));
        }

        [Fact]
        public void IntSingleParam_WhenCalledWithDifferentValues_VerifiesWithArgMatching()
        {
            _sut.Instance().IntSingleParam(42);
            _sut.Instance().IntSingleParam(43);
            _sut.Instance().IntSingleParam(42);

            _sut.IntSingleParam(Arg<int>.Is(42)).Called(Count.Exactly(2));
            _sut.IntSingleParam(Arg<int>.Is(43)).Called(Count.Once());
            _sut.IntSingleParam(Arg<int>.Is(44)).Called(Count.Never());
        }

        [Fact]
        public void IntSingleParam_WhenCalledWithAnyValue_VerifiesWithAnyArg()
        {
            _sut.Instance().IntSingleParam(10);
            _sut.Instance().IntSingleParam(20);
            _sut.Instance().IntSingleParam(30);

            _sut.IntSingleParam(Arg<int>.Any()).Called(Count.Exactly(3));
        }

        [Fact]
        public void IntSingleParam_WhenCalledWithPredicate_VerifiesWithPredicateMatching()
        {
            _sut.Instance().IntSingleParam(15);
            _sut.Instance().IntSingleParam(25);
            _sut.Instance().IntSingleParam(5);

            _sut.IntSingleParam(Arg<int>.Is(x => x > 10)).Called(Count.Exactly(2));
            _sut.IntSingleParam(Arg<int>.Is(x => x <= 10)).Called(Count.Once());
        }

        [Fact]
        public void IntParams_WhenCalledWithDifferentArgs_VerifiesCorrectly()
        {
            var regex1 = new Regex("test1");
            var regex2 = new Regex("test2");

            _sut.Instance().IntParams(1, "name1", regex1);
            _sut.Instance().IntParams(2, "name2", regex2);
            _sut.Instance().IntParams(1, "name1", regex1);

            _sut.IntParams(Arg<int>.Is(1), Arg<string>.Is("name1"), Arg<Regex>.Any()).Called(Count.Exactly(2));
            _sut.IntParams(Arg<int>.Is(2), Arg<string>.Any(), Arg<Regex>.Any()).Called(Count.Once());
            _sut.IntParams(Arg<int>.Is(3), Arg<string>.Any(), Arg<Regex>.Any()).Called(Count.Never());
        }

        [Fact]
        public void IntOutParam_WhenCalled_VerifiesCorrectly()
        {
            _sut.Instance().IntOutParam(out var outVal1);
            _sut.Instance().IntOutParam(out var outVal2);

            _sut.IntOutParam(OutArg<int>.Any()).Called(Count.Exactly(2));
        }

        [Fact]
        public void IntRefParam_WhenCalledWithDifferentRefValues_VerifiesCorrectly()
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
        public void IntInParam_WhenCalledWithDifferentValues_VerifiesCorrectly()
        {
            _sut.Instance().IntInParam("hello");
            _sut.Instance().IntInParam("world");
            _sut.Instance().IntInParam("hello");

            _sut.IntInParam(Arg<string>.Is("hello")).Called(Count.Exactly(2));
            _sut.IntInParam(Arg<string>.Is("world")).Called(Count.Once());
            _sut.IntInParam(Arg<string>.Is("foo")).Called(Count.Never());
        }

        [Fact]
        public void IntParamsParam_WhenCalledWithDifferentArrays_VerifiesCorrectly()
        {
            _sut.Instance().IntParamsParam("a", "b");
            _sut.Instance().IntParamsParam("x", "y", "z");
            _sut.Instance().IntParamsParam("a", "b");

            _sut.IntParamsParam(Arg<string[]>.Is(arr => arr.Length == 2)).Called(Count.Exactly(2));
            _sut.IntParamsParam(Arg<string[]>.Is(arr => arr.Length == 3)).Called(Count.Once());
            _sut.IntParamsParam(Arg<string[]>.Any()).Called(Count.Exactly(3));
        }

        [Fact]
        public void IntAllRefKinds_WhenCalledWithComplexParameters_VerifiesCorrectly()
        {
            var refValue1 = 100;
            var refValue2 = 200;

            int inVal = 50;
            _sut.Instance().IntAllRefKinds(out var outVal1, ref refValue1, in inVal, "test1", "a", "b");
            int inVal2 = 60;
            _sut.Instance().IntAllRefKinds(out var outVal2, ref refValue2, in inVal2, "test2", "x", "y", "z");

            _sut.IntAllRefKinds(
                OutArg<int>.Any(),
                Arg<int>.Any(),
                Arg<int>.Any(),
                Arg<string>.Any(),
                Arg<string[]>.Any()
            ).Called(Count.Exactly(2));

            _sut.IntAllRefKinds(
                OutArg<int>.Any(),
                Arg<int>.Is(100),
                Arg<int>.Is(50),
                Arg<string>.Is("test1"),
                Arg<string[]>.Is(arr => arr.Length == 2)
            ).Called(Count.Once());
        }

        [Fact]
        public void GenericSingleParam_WhenCalledWithDifferentTypes_VerifiesCorrectly()
        {
            _sut.Instance().GenericSingleParam<int>(42);
            _sut.Instance().GenericSingleParam<string>("hello");
            _sut.Instance().GenericSingleParam<int>(100);

            _sut.GenericSingleParam<int>(Arg<int>.Any()).Called(Count.Exactly(2));
            _sut.GenericSingleParam<string>(Arg<string>.Any()).Called(Count.Once());
            _sut.GenericSingleParam<double>(Arg<double>.Any()).Called(Count.Never());
        }

        [Fact]
        public void GenericSingleParam_WhenCalledWithSpecificValues_VerifiesCorrectly()
        {
            _sut.Instance().GenericSingleParam<int>(42);
            _sut.Instance().GenericSingleParam<int>(43);
            _sut.Instance().GenericSingleParam<int>(42);

            _sut.GenericSingleParam<int>(Arg<int>.Is(42)).Called(Count.Exactly(2));
            _sut.GenericSingleParam<int>(Arg<int>.Is(43)).Called(Count.Once());
            _sut.GenericSingleParam<int>(Arg<int>.Is(x => x > 40)).Called(Count.Exactly(3));
        }

        [Fact]
        public void GenericOutParam_WhenCalled_VerifiesCorrectly()
        {
            _sut.Instance().GenericOutParam<string, int>(out var outVal1);
            _sut.Instance().GenericOutParam<int, string>(out var outVal2);
            _sut.Instance().GenericOutParam<string, int>(out var outVal3);

            _sut.GenericOutParam<string, int>(OutArg<string>.Any()).Called(Count.Exactly(2));
            _sut.GenericOutParam<int, string>(OutArg<int>.Any()).Called(Count.Once());
            _sut.GenericOutParam<double, bool>(OutArg<double>.Any()).Called(Count.Never());
        }

        [Fact]
        public void GenericRefParam_WhenCalledWithDifferentRefValues_VerifiesCorrectly()
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
        public void GenericParamsParam_WhenCalledWithDifferentArrays_VerifiesCorrectly()
        {
            _sut.Instance().GenericParamsParam<string, int>("a", "b");
            _sut.Instance().GenericParamsParam<int, string>(1, 2, 3);
            _sut.Instance().GenericParamsParam<string, int>("x", "y", "z");

            _sut.GenericParamsParam<string, int>(Arg<string[]>.Any()).Called(Count.Exactly(2));
            _sut.GenericParamsParam<int, string>(Arg<int[]>.Any()).Called(Count.Once());
            _sut.GenericParamsParam<string, int>(Arg<string[]>.Is(arr => arr.Length == 2)).Called(Count.Once());
        }

        [Fact]
        public void GenericAllRefKind_WhenCalledWithComplexGenericParameters_VerifiesCorrectly()
        {
            IAnimal refAnimal1 = new Cat("cat1");
            IAnimal refAnimal2 = new Dog("dog1");

            var inVal = new Dog("input1");
            _sut.Instance().GenericAllRefKind<Cat, IAnimal, Dog, Tiger, bool>(
                out var outCat1,
                ref refAnimal1,
                in inVal,
                new Tiger("tiger1"), new Tiger("tiger2")
            );

            var inVal2 = new Cat("input2");
            _sut.Instance().GenericAllRefKind<Dog, IAnimal, Cat, Tiger, string>(
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
            ).Called(Count.Once());

            _sut.GenericAllRefKind<Dog, IAnimal, Cat, Tiger, string>(
                OutArg<Dog>.Any(),
                Arg<IAnimal>.Any(),
                Arg<Cat>.Any(),
                Arg<Tiger[]>.Any()
            ).Called(Count.Once());
        }

        [Fact]
        public void GenericSingleParam_WhenCalledWithInheritanceTypes_VerifiesCorrectly()
        {
            _sut.Instance().GenericSingleParam<IAnimal>(new Cat("fluffy"));
            _sut.Instance().GenericSingleParam<IAnimal>(new Dog("buddy"));
            _sut.Instance().GenericSingleParam<Cat>(new Cat("whiskers"));

            _sut.GenericSingleParam<IAnimal>(Arg<IAnimal>.Any()).Called(Count.Exactly(2));
            
            _sut.GenericSingleParam<Cat>(Arg<Cat>.Any()).Called(Count.Exactly(1));
            _sut.GenericSingleParam<IAnimal>(Arg<IAnimal>.Is(it => it is Cat)).Called(Count.Once());
            _sut.GenericSingleParam<IAnimal>(Arg<IAnimal>.Is(it => it is Dog)).Called(Count.Once());
            _sut.GenericSingleParam<IAnimal>(Arg<IAnimal>.Is(a => a.Name.StartsWith("fluf"))).Called(Count.Once());
        }
        
        [Fact]
        public void GenericOutParam_WhenCalledWithInheritanceTypes_VerifiesCorrectly()
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
        public void IntSingleParam_WhenVerificationFails_ThrowsException()
        {
            _sut.Instance().IntSingleParam(42);

            Should.Throw<VerificationFailedException>(() =>
                _sut.IntSingleParam(Arg<int>.Is(42)).Called(Count.Exactly(2))
            );
        }

        [Fact]
        public void VoidNoParams_WhenVerificationFails_ThrowsException()
        {
            _sut.Instance().VoidNoParams();

            Should.Throw<VerificationFailedException>(() =>
                _sut.VoidNoParams().Called(Count.Never())
            );
        }

        [Fact]
        public void IntNoParams_WhenVerificationFailsAtLeast_ThrowsException()
        {
            _sut.Instance().IntNoParams();

            Should.Throw<VerificationFailedException>(() =>
                _sut.IntNoParams().Called(Count.AtLeast(2))
            );
        }

        [Fact]
        public void IntNoParams_WhenVerificationFailsAtMost_ThrowsException()
        {
            _sut.Instance().IntNoParams();
            _sut.Instance().IntNoParams();
            _sut.Instance().IntNoParams();

            Should.Throw<VerificationFailedException>(() =>
                _sut.IntNoParams().Called(Count.AtMost(2))
            );
        }

        [Fact]
        public void GenericSingleParam_WhenVerificationFails_ThrowsException()
        {
            _sut.Instance().GenericSingleParam<string>("test");

            Should.Throw<VerificationFailedException>(() =>
                _sut.GenericSingleParam<int>(Arg<int>.Any()).Called(Count.Once())
            );
        }

        [Fact]
        public void IntParams_WhenCalledWithNullValues_VerifiesCorrectly()
        {
            _sut.Instance().IntParams(42, null, new Regex("test"));

            _sut.IntParams(Arg<int>.Is(42), Arg<string>.Is((string)null), Arg<Regex>.Any()).Called(Count.Once());
            _sut.IntParams(Arg<int>.Any(), Arg<string>.Any(), Arg<Regex>.Any()).Called(Count.Once());
        }

        [Fact]
        public void IntParamsParam_WhenCalledWithEmptyArray_VerifiesCorrectly()
        {
            _sut.Instance().IntParamsParam(new string[0]);

            _sut.IntParamsParam(Arg<string[]>.Is(arr => arr.Length == 0)).Called(Count.Once());
            _sut.IntParamsParam(Arg<string[]>.Any()).Called(Count.Once());
        }

        [Fact]
        public void MultipleMethodCalls_WhenVerifyingDifferentMethods_VerifiesIndependently()
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
        public void IntSingleParam_WithMultipleSetups_VerifiesCorrectly()
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