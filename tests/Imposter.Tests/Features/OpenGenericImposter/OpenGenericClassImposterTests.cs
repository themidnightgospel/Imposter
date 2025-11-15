using System;
using System.Collections.Generic;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.OpenGenericImposter
{
    public class OpenGenericClassImposterTests
    {
        public static IEnumerable<object[]> OpenGenericClassFactories()
        {
#if USE_CSHARP14
            yield return new object[]
            {
                (Func<OpenGenericClassImposter<string>>)(() => OpenGenericClass<string>.Imposter()),
            };
#endif
            yield return new object[]
            {
                (Func<OpenGenericClassImposter<string>>)(
                    () => new OpenGenericClassImposter<string>()
                ),
            };
        }

        [Theory]
        [MemberData(nameof(OpenGenericClassFactories))]
        public void GivenOpenGenericClassMethod_WhenConfigured_ShouldReturnValue(
            Func<OpenGenericClassImposter<string>> factory
        )
        {
            var imposter = factory();
            imposter.Echo(Arg<string>.Any()).Returns("configured");

            var instance = imposter.Instance();
            instance.Echo("input").ShouldBe("configured");

            Should.NotThrow(() => imposter.Echo(Arg<string>.Any()).Called(Count.Once()));
        }

        [Fact]
        public void GivenVirtualMethod_WhenForwardedToBase_ShouldInvokeBaseImplementation()
        {
            var imposter = CreateClassImposter<string>();
            imposter.Describe(Arg<string>.Any()).UseBaseImplementation();

            var instance = imposter.Instance();
            instance.Describe("value").ShouldBe("base:value");
        }

        [Fact]
        public void GivenGenericProperty_WhenSetterUsesBase_ShouldPersistValue()
        {
            var imposter = CreateClassImposter<string>();
            imposter.Current.Setter(Arg<string>.Any()).UseBaseImplementation();

            var instance = imposter.Instance();
            instance.Current = "alpha";
            instance.Current.ShouldBe("alpha");
        }

        [Theory]
        [MemberData(nameof(OpenGenericClassFactories))]
        public void GivenClassEvent_WhenUsingFactories_ShouldNotifyHandlers(
            Func<OpenGenericClassImposter<string>> factory
        )
        {
            var imposter = factory();
            var received = new List<string>();
            var instance = imposter.Instance();
            instance.ValueChanged += (sender, args) => received.Add(args.Payload);

            imposter.ValueChanged.Raise(this, new GenericEventArgs<string>("first"));
            imposter.ValueChanged.Raise(this, new GenericEventArgs<string>("second"));

            received.ShouldBe(new[] { "first", "second" });
        }

        [Fact]
        public void GivenGenericProperty_WhenGetterConfigured_ShouldReturnValue()
        {
            var imposter = CreateClassImposter<string>();
            imposter.Current.Getter().Returns("configured");

            imposter.Instance().Current.ShouldBe("configured");
        }

        [Fact]
        public void GivenSingleParameterIndexer_WhenConfigured_ShouldRespectArguments()
        {
            var imposter = CreateClassImposter<string>();
            imposter[Arg<int>.Is(index => index == 1)].Getter().Returns("value");
            var setter = imposter[Arg<int>.Is(index => index == 1)].Setter();

            var instance = imposter.Instance();
            instance[1].ShouldBe("value");
            instance[1] = "updated";

            Should.NotThrow(() => setter.Called(Count.Once()));
        }

        [Fact]
        public void GivenCompositeIndexer_WhenConfigured_ShouldRespectGenericAndNonGenericArguments()
        {
            var imposter = CreateClassImposter<string>();
            imposter[Arg<string>.Is(key => key == "key"), Arg<int>.Is(index => index == 5)]
                .Getter()
                .Returns("value");
            var setter = imposter[
                Arg<string>.Is(key => key == "key"),
                Arg<int>.Is(index => index == 5)
            ]
                .Setter();

            var instance = imposter.Instance();
            instance["key", 5].ShouldBe("value");
            instance["key", 5] = "update";

            Should.NotThrow(() => setter.Called(Count.Once()));
        }

        [Fact]
        public void GivenClassEvent_WhenRaised_ShouldNotifyHandlers()
        {
            var imposter = CreateClassImposter<string>();
            var received = new List<string>();
            imposter.Instance().ValueChanged += (sender, args) => received.Add(args.Payload);

            imposter.ValueChanged.Raise(this, new GenericEventArgs<string>("payload"));

            received.ShouldBe(new[] { "payload" });
        }

        [Fact]
        public void GivenOpenGenericPairClass_WhenUsingMultipleTypeParameters_ShouldRespectBothArguments()
        {
            var imposter = CreatePairClassImposter<PairKey, PairValue>();
            var key = new PairKey("primary");
            var resolved = new PairValue("resolved");
            imposter
                .Resolve(Arg<PairKey>.Is(g => g == key), Arg<PairValue>.Any())
                .Returns(resolved);

            var instance = imposter.Instance();
            instance.Resolve(key, new PairValue("fallback")).ShouldBeSameAs(resolved);
        }

        [Fact]
        public void GivenOpenGenericPairClass_WhenUsingPropertiesAndBaseImplementation_ShouldPersistPairs()
        {
            var imposter = CreatePairClassImposter<PairKey, PairValue>();
            var pair = new KeyValuePair<PairKey, PairValue>(
                new PairKey("alpha"),
                new PairValue("value")
            );
            imposter
                .CurrentPair.Setter(Arg<KeyValuePair<PairKey, PairValue>>.Any())
                .UseBaseImplementation();

            var instance = imposter.Instance();
            instance.CurrentPair = pair;
            instance.CurrentPair.ShouldBe(pair);
        }

        [Fact]
        public void GivenOpenGenericPairClass_WhenUsingDescribeMethod_ShouldMixTypeParameters()
        {
            var imposter = CreatePairClassImposter<PairKey, PairValue>();
            imposter.DescribePair(Arg<PairKey>.Any(), Arg<PairValue>.Any()).UseBaseImplementation();

            imposter
                .Instance()
                .DescribePair(new PairKey("left"), new PairValue("right"))
                .ShouldBe("left:right");
        }

        private static OpenGenericClassImposter<T> CreateClassImposter<T>()
            where T : class =>
#if USE_CSHARP14
            OpenGenericClass<T>.Imposter();
#else
            new OpenGenericClassImposter<T>();
#endif

        private static OpenGenericPairClassImposter<TKey, TValue> CreatePairClassImposter<
            TKey,
            TValue
        >()
            where TKey : class
            where TValue : class =>
#if USE_CSHARP14
            OpenGenericPairClass<TKey, TValue>.Imposter();
#else
            new OpenGenericPairClassImposter<TKey, TValue>();
#endif

        private sealed class PairKey
        {
            public PairKey(string value)
            {
                Value = value;
            }

            public string Value { get; }

            public override string ToString()
            {
                return Value;
            }
        }

        private sealed class PairValue
        {
            public PairValue(string value)
            {
                Value = value;
            }

            public string Value { get; }

            public override string ToString()
            {
                return Value;
            }
        }
    }
}
