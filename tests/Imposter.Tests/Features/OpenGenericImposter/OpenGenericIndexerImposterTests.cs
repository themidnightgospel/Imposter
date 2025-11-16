using System;
using System.Collections.Generic;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.OpenGenericImposter
{
    public class OpenGenericIndexerImposterTests
    {
        public static IEnumerable<object[]> IndexerImposterFactories()
        {
#if USE_CSHARP14
            yield return new object[]
            {
                (Func<IHaveIndexerImposter<string>>)(() => IHaveIndexer<string>.Imposter()),
            };
#endif
            yield return new object[]
            {
                (Func<IHaveIndexerImposter<string>>)(() => new IHaveIndexerImposter<string>()),
            };
        }

        [Theory]
        [MemberData(nameof(IndexerImposterFactories))]
        public void GivenIntIndexerOfGenericType_WhenConfigured_ShouldReturnAndVerify(
            Func<IHaveIndexerImposter<string>> factory
        )
        {
            var sut = factory();
            sut[Arg<int>.Is(index => index == 1)].Getter().Returns("payload");
            var setter = sut[Arg<int>.Is(index => index == 1)].Setter();

            var instance = sut.Instance();
            instance[1].ShouldBe("payload");
            instance[1] = "updated";

            Should.NotThrow(() => setter.Called(Count.Once()));
        }

        [Fact]
        public void GivenIndexerUsingTypeParameterAsKey_WhenConfigured_ShouldHandleNonGenericReturn()
        {
            var sut = CreateIndexerImposter<string>();
            sut[Arg<string>.Is(key => key == "alpha")].Getter().Returns(10);
            var setter = sut[Arg<string>.Is(key => key == "alpha")].Setter();

            var instance = sut.Instance();
            instance["alpha"].ShouldBe(10);
            instance["alpha"] = 42;

            Should.NotThrow(() => setter.Called(Count.Once()));
        }

        [Fact]
        public void GivenMultiParameterIndexer_WhenConfigured_ShouldRespectMixedArguments()
        {
            var sut = CreateIndexerImposter<string>();
            sut[Arg<string>.Is(key => key == "alpha"), Arg<int>.Is(index => index == 5)]
                .Getter()
                .Returns("value");
            var setter = sut[
                Arg<string>.Is(key => key == "alpha"),
                Arg<int>.Is(index => index == 5)
            ]
                .Setter();

            var instance = sut.Instance();
            instance["alpha", 5].ShouldBe("value");
            instance["alpha", 5] = "new";

            Should.NotThrow(() => setter.Called(Count.Once()));
        }

        [Theory]
        [MemberData(nameof(IndexerImposterFactories))]
        public void GivenCompositeIndexer_WhenUsingFactories_ShouldRespectMatchers(
            Func<IHaveIndexerImposter<string>> factory
        )
        {
            var sut = factory();
            sut[
                Arg<string>.Is(key => key.StartsWith("alpha", StringComparison.OrdinalIgnoreCase)),
                Arg<int>.Is(index => index == 7)
            ]
                .Getter()
                .Returns("value");
            var setter = sut[
                Arg<string>.Is(key => key.StartsWith("alpha", StringComparison.OrdinalIgnoreCase)),
                Arg<int>.Is(index => index == 7)
            ]
                .Setter();

            var instance = sut.Instance();
            instance["alpha", 7].ShouldBe("value");
            instance["alpha", 7] = "updated";

            Should.NotThrow(() => setter.Called(Count.Once()));
        }

        [Fact]
        public void GivenIndexerArgumentMatchers_WhenUsingGenericAndNonGenericArgs_ShouldRespectMatchers()
        {
            var sut = CreateIndexerImposter<string>();
            var getter = sut[Arg<string>.Any(), Arg<int>.Is(index => index > 0)].Getter();
            getter.Returns("value");

            var instance = sut.Instance();
            instance["payload", 1].ShouldBe("value");
            instance["payload", 0].ShouldBe(default);

            Should.NotThrow(() => getter.Called(Count.Once()));
            Should.Throw<VerificationFailedException>(() => getter.Called(Count.Exactly(2)));
        }

        [Fact]
        public void GivenDifferentClosedGenericIndexers_WhenAccessed_ShouldBeIndependent()
        {
            var stringIndexer = CreateIndexerImposter<string>();
            var payloadIndexer = CreateIndexerImposter<IndexerPayload>();

            var stringGetter = stringIndexer[Arg<int>.Is(index => index == 1)].Getter();
            stringGetter.Returns("string-value");
            var payloadGetter = payloadIndexer[Arg<int>.Is(index => index == 1)].Getter();
            payloadGetter.Returns(new IndexerPayload { Value = "payload" });

            stringIndexer.Instance()[1].ShouldBe("string-value");
            payloadIndexer.Instance()[1].Value.ShouldBe("payload");

            Should.NotThrow(() => stringGetter.Called(Count.Once()));
            Should.NotThrow(() => payloadGetter.Called(Count.Once()));
        }

        private static IHaveIndexerImposter<T> CreateIndexerImposter<T>()
            where T : class =>
#if USE_CSHARP14
            IHaveIndexer<T>.Imposter();
#else
            new IHaveIndexerImposter<T>();
#endif

        private sealed class IndexerPayload
        {
            public string Value { get; set; } = string.Empty;
        }
    }
}
