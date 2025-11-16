using System;
using System.Collections.Generic;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.OpenGenericImposter
{
    public class OpenGenericMethodImposterTests
    {
        public static IEnumerable<object[]> MethodImposterFactories()
        {
#if USE_CSHARP14
            yield return new object[]
            {
                (Func<IOpenGenericMethodTargetImposter<string>>)(
                    () => IOpenGenericMethodTarget<string>.Imposter()
                ),
            };
#endif
            yield return new object[]
            {
                (Func<IOpenGenericMethodTargetImposter<string>>)(
                    () => new IOpenGenericMethodTargetImposter<string>()
                ),
            };
        }

        public static IEnumerable<object[]> MethodImposterValueTypeFactories()
        {
#if USE_CSHARP14
            yield return new object[]
            {
                (Func<IOpenGenericMethodTargetImposter<int>>)(
                    () => IOpenGenericMethodTarget<int>.Imposter()
                ),
            };
#endif
            yield return new object[]
            {
                (Func<IOpenGenericMethodTargetImposter<int>>)(
                    () => new IOpenGenericMethodTargetImposter<int>()
                ),
            };
        }

        [Fact]
        public void GivenOpenGenericInterface_WhenOnNextIsCalled_ShouldVerifyCallCount()
        {
            var sut = CreateObservableImposter<string>();

            sut.Instance().OnNext("first");
            sut.Instance().OnNext("second");

            Should.NotThrow(() => sut.OnNext(Arg<string>.Any()).Called(Count.Exactly(2)));
        }

        [Theory]
        [MemberData(nameof(MethodImposterFactories))]
        public void GivenMethodReturningTypeParameter_WhenConfigured_ShouldReturnValuesAndVerify(
            Func<IOpenGenericMethodTargetImposter<string>> factory
        )
        {
            var sut = factory();
            sut.GetNext().Returns("alpha").Then().Returns("beta");

            var instance = sut.Instance();

            instance.GetNext().ShouldBe("alpha");
            instance.GetNext().ShouldBe("beta");

            Should.NotThrow(() => sut.GetNext().Called(Count.Exactly(2)));
        }

        [Fact]
        public void GivenMethodReturningNonGenericType_WhenConfigured_ShouldUseTypeParameterArgument()
        {
            var sut = CreateMethodTargetImposter<string>();
            sut.CountFor(Arg<string>.Any()).Returns(10);

            sut.Instance().CountFor("payload").ShouldBe(10);
        }

        [Fact]
        public void GivenMethodReturningBool_WhenUsingGenericMatchers_ShouldOnlyMatchExpectedPayload()
        {
            var sut = CreateMethodTargetImposter<string>();
            sut.TryAdd(
                    Arg<string>.Is(value =>
                        value.StartsWith("a", StringComparison.OrdinalIgnoreCase)
                    )
                )
                .Returns(true);

            sut.Instance().TryAdd("alpha").ShouldBeTrue();
            sut.Instance().TryAdd("beta").ShouldBeFalse();
        }

        [Theory]
        [MemberData(nameof(MethodImposterFactories))]
        public void GivenNonGenericReturn_WhenUsingFactories_ShouldRespectMatchers(
            Func<IOpenGenericMethodTargetImposter<string>> factory
        )
        {
            var sut = factory();
            sut.CountFor(
                    Arg<string>.Is(value =>
                        value.StartsWith("a", StringComparison.OrdinalIgnoreCase)
                    )
                )
                .Returns(2);
            sut.CountFor(
                    Arg<string>.Is(value =>
                        value.StartsWith("b", StringComparison.OrdinalIgnoreCase)
                    )
                )
                .Returns(4);

            var instance = sut.Instance();
            instance.CountFor("alpha").ShouldBe(2);
            instance.CountFor("bravo").ShouldBe(4);
            instance.CountFor("charlie").ShouldBe(0);

            Should.NotThrow(() =>
                sut.CountFor(
                        Arg<string>.Is(value =>
                            value.StartsWith("a", StringComparison.OrdinalIgnoreCase)
                        )
                    )
                    .Called(Count.Once())
            );
            Should.NotThrow(() =>
                sut.CountFor(
                        Arg<string>.Is(value =>
                            value.StartsWith("b", StringComparison.OrdinalIgnoreCase)
                        )
                    )
                    .Called(Count.Once())
            );
        }

        [Fact]
        public void GivenMethodWithMixedParameters_WhenArgumentsMatch_ShouldTrackInvocation()
        {
            var sut = CreateMethodTargetImposter<string>();
            var verifier = sut.Add(
                Arg<string>.Is(value => value == "item"),
                Arg<int>.Is(index => index == 2)
            );

            sut.Instance().Add("item", 2);

            Should.NotThrow(() => verifier.Called(Count.Exactly(1)));
        }

        [Theory]
        [MemberData(nameof(MethodImposterValueTypeFactories))]
        public void GivenValueTypeParameter_WhenConfigured_ShouldReturnAndTrack(
            Func<IOpenGenericMethodTargetImposter<int>> factory
        )
        {
            var sut = factory();
            sut.GetNext().Returns(5).Then().Returns(10);
            var addVerifier = sut.Add(
                Arg<int>.Is(value => value == 7),
                Arg<int>.Is(index => index == 1)
            );

            var instance = sut.Instance();
            instance.GetNext().ShouldBe(5);
            instance.GetNext().ShouldBe(10);
            instance.Add(7, 1);

            Should.NotThrow(() => sut.GetNext().Called(Count.Exactly(2)));
            Should.NotThrow(() => addVerifier.Called(Count.Exactly(1)));
        }

        [Fact]
        public void GivenArgumentMatchers_WhenMixingGenericAndNonGenericArguments_ShouldRespectMatchers()
        {
            var sut = CreateMethodTargetImposter<string>();
            var verifier = sut.Publish(
                Arg<string>.Any(),
                Arg<string>.Is(label => label == "alpha"),
                Arg<int>.Is(priority => priority > 0)
            );

            var instance = sut.Instance();
            instance.Publish("payload", "alpha", 1);

            Should.NotThrow(() => verifier.Called(Count.Exactly(1)));
            Should.Throw<VerificationFailedException>(() => verifier.Called(Count.Exactly(2)));
        }

        [Fact]
        public void GivenSequentialReturnsForGenericMethod_WhenConfigured_ShouldReturnInOrder()
        {
            var sut = CreateMethodTargetImposter<string>();
            sut.GetNext().Returns("one").Then().Returns("two").Then().Returns("three");

            var instance = sut.Instance();
            instance.GetNext().ShouldBe("one");
            instance.GetNext().ShouldBe("two");
            instance.GetNext().ShouldBe("three");
            instance.GetNext().ShouldBe("three");
        }

        [Fact]
        public void GivenSequentialReturnsForNonGenericMethod_WhenConfigured_ShouldReturnInOrder()
        {
            var sut = CreateMethodTargetImposter<string>();
            sut.CountFor(Arg<string>.Any()).Returns(1).Then().Returns(2);

            var instance = sut.Instance();
            instance.CountFor("first").ShouldBe(1);
            instance.CountFor("second").ShouldBe(2);
            instance.CountFor("third").ShouldBe(2);
        }

        [Fact]
        public void GivenSequentialThrowsAndReturns_WhenConfigured_ShouldRespectSequence()
        {
            var sut = CreateMethodTargetImposter<string>();
            sut.ResilientFetch().Throws<InvalidOperationException>().Then().Returns("recovered");

            var instance = sut.Instance();

            Should.Throw<InvalidOperationException>(() => instance.ResilientFetch());
            instance.ResilientFetch().ShouldBe("recovered");
        }

        [Fact]
        public void GivenVerification_WhenExpectedCallsMissing_ShouldThrow()
        {
            var sut = CreateMethodTargetImposter<string>();

            Should.Throw<VerificationFailedException>(() =>
                sut.Add(Arg<string>.Any(), Arg<int>.Any()).Called(Count.Once())
            );
        }

        [Fact]
        public void GivenDifferentClosedGenericImposters_WhenCallsMade_ShouldTrackIndependently()
        {
            var stringObservable = CreateObservableImposter<string>();
            var intObservable = CreateObservableImposter<int>();

            var stringInstance = stringObservable.Instance();
            stringInstance.OnNext("payload");
            stringInstance.OnNext("another");

            intObservable.Instance().OnNext(42);

            Should.NotThrow(() =>
                stringObservable.OnNext(Arg<string>.Any()).Called(Count.Exactly(2))
            );
            Should.NotThrow(() => intObservable.OnNext(Arg<int>.Any()).Called(Count.Exactly(1)));
        }

        [Fact]
        public void GivenDifferentTypeParameter_WhenCallsMadeOnAnotherType_ShouldNotSatisfyVerification()
        {
            var stringVerifier = CreateMethodTargetImposter<string>();
            var intInstance = CreateMethodTargetImposter<int>().Instance();

            intInstance.Add(7, 1);

            Should.Throw<VerificationFailedException>(() =>
                stringVerifier.Add(Arg<string>.Any(), Arg<int>.Any()).Called(Count.Once())
            );
        }

        private static IOpenGenericMethodTargetImposter<T> CreateMethodTargetImposter<T>() =>
#if USE_CSHARP14
            IOpenGenericMethodTarget<T>.Imposter();
#else
            new IOpenGenericMethodTargetImposter<T>();
#endif

        private static IAsyncObservableImposter<T> CreateObservableImposter<T>() =>
#if USE_CSHARP14
            IAsyncObservable<T>.Imposter();
#else
            new IAsyncObservableImposter<T>();
#endif
    }
}
