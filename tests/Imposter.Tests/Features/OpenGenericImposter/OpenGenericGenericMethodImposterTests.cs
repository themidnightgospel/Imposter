using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.OpenGenericImposter
{
    public class OpenGenericGenericMethodImposterTests
    {
        public static IEnumerable<object[]> GenericMethodFactories()
        {
#if USE_CSHARP14
            yield return new object[]
            {
                (Func<IOpenGenericWithMethodGenericsImposter<string>>)(
                    () => IOpenGenericWithMethodGenerics<string>.Imposter()
                ),
            };
#endif
            yield return new object[]
            {
                (Func<IOpenGenericWithMethodGenericsImposter<string>>)(
                    () => new IOpenGenericWithMethodGenericsImposter<string>()
                ),
            };
        }

        public static IEnumerable<object[]> NonGenericGenericMethodFactories()
        {
#if USE_CSHARP14
            yield return new object[]
            {
                (Func<IHaveGenericMethodsImposter>)(() => IHaveGenericMethods.Imposter()),
            };
#endif
            yield return new object[]
            {
                (Func<IHaveGenericMethodsImposter>)(() => new IHaveGenericMethodsImposter()),
            };
        }

        [Theory]
        [MemberData(nameof(GenericMethodFactories))]
        public void GivenOpenGenericMethodWithAdditionalTypeArgument_WhenInvoked_ShouldTrackEachGeneric(
            Func<IOpenGenericWithMethodGenericsImposter<string>> factory
        )
        {
            var sut = factory();
            var instance = sut.Instance();

            instance.DoSomething("alpha");
            instance.DoSomething(42);

            Should.NotThrow(() => sut.DoSomething<string>(Arg<string>.Any()).Called(Count.Once()));
            Should.NotThrow(() => sut.DoSomething<int>(Arg<int>.Any()).Called(Count.Once()));
        }

        [Fact]
        public void GivenTransformMethod_WhenConfiguredPerCombination_ShouldReturnPerType()
        {
            var sut = CreateGenericMethodImposter<string>();
            sut.Transform<int>(Arg<string>.Any()).Returns(100);
            sut.Transform<string>(Arg<string>.Any()).Returns("converted");

            var instance = sut.Instance();

            instance.Transform<int>("value").ShouldBe(100);
            instance.Transform<string>("value").ShouldBe("converted");
        }

        [Theory]
        [MemberData(nameof(GenericMethodFactories))]
        public void GivenTransformMethod_WhenFactoriesUsed_ShouldRespectTypeSpecificConfiguration(
            Func<IOpenGenericWithMethodGenericsImposter<string>> factory
        )
        {
            var sut = factory();
            sut.Transform<int>(Arg<string>.Is(value => value == "alpha")).Returns(42);
            sut.Transform<string>(Arg<string>.Any()).Returns("converted");

            var instance = sut.Instance();
            instance.Transform<int>("alpha").ShouldBe(42);
            instance.Transform<string>("beta").ShouldBe("converted");
        }

        [Fact]
        public void GivenMapMethod_WhenMultipleMethodGenericsUsed_ShouldTrackIndependently()
        {
            var sut = CreateGenericMethodImposter<string>();
            sut.Map<string, int>(Arg<string>.Is(s => s == "left")).Returns(1);
            sut.Map<int, string>(Arg<int>.Is(i => i == 2)).Returns("two");

            var instance = sut.Instance();

            instance.Map<string, int>("left").ShouldBe(1);
            instance.Map<int, string>(2).ShouldBe("two");
        }

        [Fact]
        public void GivenDifferentClosedMethodGenericArguments_WhenVerifying_ShouldNotCrossSatisfy()
        {
            var sut = CreateGenericMethodImposter<string>();
            sut.Instance().DoSomething(5);

            Should.Throw<VerificationFailedException>(() =>
                sut.DoSomething<string>(Arg<string>.Any()).Called(Count.Once())
            );
        }

        [Fact]
        public void GivenGenericMethodsOnNonGenericInterface_WhenConfigured_ShouldReturnPerType()
        {
            var sut = CreateNonGenericGenericMethodImposter();
            sut.GetValue<int>().Returns(5);
            sut.GetValue<string>().Returns("value");

            var instance = sut.Instance();

            instance.GetValue<int>().ShouldBe(5);
            instance.GetValue<string>().ShouldBe("value");
        }

        [Theory]
        [MemberData(nameof(NonGenericGenericMethodFactories))]
        public async Task GivenComplexGenericMethodOnNonGenericInterface_WhenFactoriesUsed_ShouldReturnPerCombination(
            Func<IHaveGenericMethodsImposter> factory
        )
        {
            var sut = factory();
            sut.ProcessComplexAsync<string, int>(Arg<IEnumerable<string>>.Any())
                .Returns(Task.FromResult(7));
            sut.ProcessComplexAsync<int, string>(Arg<IEnumerable<int>>.Any())
                .Returns(Task.FromResult("processed"));

            var instance = sut.Instance();
            (await instance.ProcessComplexAsync<string, int>(new[] { "item" })).ShouldBe(7);
            (await instance.ProcessComplexAsync<int, string>(new[] { 1 })).ShouldBe("processed");
        }

        [Fact]
        public void GivenGenericMethodWithArgument_WhenUsingMatchers_ShouldTrackPerType()
        {
            var sut = CreateNonGenericGenericMethodImposter();
            var stringVerifier = sut.AddItem<string>(Arg<string>.Is(s => s == "alpha"));
            var intVerifier = sut.AddItem<int>(Arg<int>.Is(i => i == 7));

            var instance = sut.Instance();
            instance.AddItem("alpha");
            instance.AddItem(7);

            Should.NotThrow(() => stringVerifier.Called(Count.Once()));
            Should.NotThrow(() => intVerifier.Called(Count.Once()));
        }

        [Fact]
        public async Task GivenAsyncGenericMethod_WhenConfigured_ShouldReturnConfiguredTask()
        {
            var sut = CreateNonGenericGenericMethodImposter();
            sut.ProcessAsync<string>(Arg<IEnumerable<string>>.Any())
                .Returns(Task.FromResult("result"));

            var instance = sut.Instance();
            var result = await instance.ProcessAsync(new[] { "value" });

            result.ShouldBe("result");
        }

        [Fact]
        public void GivenConstrainedReferenceMethod_WhenCalled_ShouldRespectConstraint()
        {
            var sut = CreateConstraintImposter();
            var verifier = sut.HandleReference<ReferencePayload>(Arg<ReferencePayload>.Any());

            var instance = sut.Instance();
            instance.HandleReference(new ReferencePayload());

            Should.NotThrow(() => verifier.Called(Count.Once()));
        }

        [Fact]
        public void GivenConstrainedStructMethod_WhenConfigured_ShouldReturnValue()
        {
            var sut = CreateConstraintImposter();
            sut.CloneStruct<SampleStruct>(Arg<SampleStruct>.Any())
                .Returns(value => new SampleStruct(value.Number + 1));

            var instance = sut.Instance();
            instance.CloneStruct(new SampleStruct(1)).Number.ShouldBe(2);
        }

        [Fact]
        public void GivenComparableConstraint_WhenVerifying_ShouldTrackCalls()
        {
            var sut = CreateConstraintImposter();
            var verifier = sut.CompareValues<ComparablePayload>(
                Arg<ComparablePayload>.Is(left => left.Score == 5),
                Arg<ComparablePayload>.Any()
            );

            var instance = sut.Instance();
            instance.CompareValues(new ComparablePayload(5), new ComparablePayload(10));

            Should.NotThrow(() => verifier.Called(Count.Once()));
        }

        [Fact]
        public void GivenNewConstraint_WhenConfigured_ShouldReturnCreatedInstance()
        {
            var sut = CreateConstraintImposter();
            sut.CreateInstance<ReferencePayload>()
                .Returns(new ReferencePayload { Value = "created" });

            sut.Instance().CreateInstance<ReferencePayload>().Value.ShouldBe("created");
        }

        private static IOpenGenericWithMethodGenericsImposter<T> CreateGenericMethodImposter<T>() =>
#if USE_CSHARP14
            IOpenGenericWithMethodGenerics<T>.Imposter();
#else
            new IOpenGenericWithMethodGenericsImposter<T>();
#endif

        private static IHaveGenericMethodsImposter CreateNonGenericGenericMethodImposter() =>
#if USE_CSHARP14
            IHaveGenericMethods.Imposter();
#else
            new IHaveGenericMethodsImposter();
#endif

        private static IGenericConstraintsTargetImposter CreateConstraintImposter() =>
#if USE_CSHARP14
            IGenericConstraintsTarget.Imposter();
#else
            new IGenericConstraintsTargetImposter();
#endif
    }
}
