using System;
using System.Collections.Generic;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.OpenGenericImposter
{
    public class OpenGenericPropertyImposterTests
    {
        public static IEnumerable<object[]> PropertyImposterFactories()
        {
#if USE_CSHARP14
            yield return new object[]
            {
                (Func<IHasPropertyImposter<string>>)(() => IHasProperty<string>.Imposter()),
            };
#endif
            yield return new object[]
            {
                (Func<IHasPropertyImposter<string>>)(() => new IHasPropertyImposter<string>()),
            };
        }

        [Theory]
        [MemberData(nameof(PropertyImposterFactories))]
        public void GivenReadOnlyGenericProperty_WhenConfigured_ShouldReturnValue(
            Func<IHasPropertyImposter<string>> factory
        )
        {
            var sut = factory();
            sut.ReadOnlyValue.Getter().Returns("configured");

            var instance = sut.Instance();
            instance.ReadOnlyValue.ShouldBe("configured");

            Should.NotThrow(() => sut.ReadOnlyValue.Getter().Called(Count.Once()));
        }

        [Fact]
        public void GivenReadWriteGenericProperty_WhenSetterInvoked_ShouldRecordCalls()
        {
            var sut = CreatePropertyImposter<string>();
            var setter = sut.Value.Setter(Arg<string>.Any());

            var instance = sut.Instance();
            instance.Value = "alpha";
            instance.Value = "beta";

            Should.NotThrow(() => setter.Called(Count.Exactly(2)));
        }

        [Fact]
        public void GivenReadWriteGenericProperty_WhenSpecificValueSet_ShouldAllowMatcherVerification()
        {
            var sut = CreatePropertyImposter<string>();
            var setter = sut.Value.Setter(Arg<string>.Is(value => value == "beta"));

            var instance = sut.Instance();
            instance.Value = "alpha";
            instance.Value = "beta";

            Should.NotThrow(() => setter.Called(Count.Exactly(1)));
            Should.Throw<VerificationFailedException>(() => setter.Called(Count.Exactly(2)));
        }

        [Theory]
        [MemberData(nameof(PropertyImposterFactories))]
        public void GivenReadWriteGenericProperty_WhenUsingFactories_ShouldRespectMatchers(
            Func<IHasPropertyImposter<string>> factory
        )
        {
            var sut = factory();
            var setter = sut.Value.Setter(
                Arg<string>.Is(value =>
                    value.StartsWith("match", StringComparison.OrdinalIgnoreCase)
                )
            );

            var instance = sut.Instance();
            instance.Value = "skip";
            instance.Value = "matched";

            Should.NotThrow(() => setter.Called(Count.Exactly(1)));
            Should.Throw<VerificationFailedException>(() => setter.Called(Count.Exactly(2)));
        }

        [Fact]
        public void GivenPropertyWithoutExplicitSetup_WhenValueSet_ShouldBehaveLikeNormalProperty()
        {
            var sut = CreatePropertyImposter<string>();
            var instance = sut.Instance();

            instance.Value = "alpha";
            instance.Value.ShouldBe("alpha");

            instance.Value = "beta";
            instance.Value.ShouldBe("beta");
        }

        [Fact]
        public void GivenExplicitGetterSetup_WhenValueIsSet_ShouldPreferConfiguredReturn()
        {
            var sut = CreatePropertyImposter<string>();
            var instance = sut.Instance();
            instance.Value = "alpha";

            sut.Value.Getter().Returns("forced");

            instance.Value.ShouldBe("forced");
        }

        [Fact]
        public void GivenNonGenericProperty_WhenConfiguredAfterGenericInteraction_ShouldReturnConfiguredCount()
        {
            var sut = CreatePropertyImposter<string>();
            var seenValues = new List<string>();
            sut.Value.Setter(Arg<string>.Any()).Callback(value => seenValues.Add(value));
            sut.Count.Getter().Returns(() => seenValues.Count);

            var instance = sut.Instance();
            instance.Value = "alpha";
            instance.Value = "beta";

            instance.Count.ShouldBe(2);
        }

        [Fact]
        public void GivenPropertyReturningConstructedGenericType_WhenConfigured_ShouldReturnSequence()
        {
            var sut = CreatePropertyImposter<string>();
            sut.Items.Getter().Returns(new[] { "first", "second" });

            sut.Instance().Items.ShouldBe(new[] { "first", "second" });
        }

        [Theory]
        [MemberData(nameof(PropertyImposterFactories))]
        public void GivenEnumerableProperty_WhenUsingFactories_ShouldReturnSequence(
            Func<IHasPropertyImposter<string>> factory
        )
        {
            var sut = factory();
            sut.Items.Getter().Returns(new[] { "alpha", "beta" });

            sut.Instance().Items.ShouldBe(new[] { "alpha", "beta" });
        }

        private static IHasPropertyImposter<T> CreatePropertyImposter<T>()
            where T : class =>
#if USE_CSHARP14
            IHasProperty<T>.Imposter();
#else
            new IHasPropertyImposter<T>();
#endif
    }
}
