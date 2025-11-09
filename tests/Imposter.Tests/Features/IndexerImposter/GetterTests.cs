using System;
using System.Collections.Generic;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.IndexerImposter
{
    public class GetterTests
    {
        private readonly IIndexerSetupSutImposter _sut = new IIndexerSetupSutImposter();

        [Fact]
        public void GivenConfiguredGetter_WhenAccessed_ThenReturnsConfiguredValue()
        {
            _sut[Arg<int>.Any(), Arg<string>.Any(), Arg<object>.Any()]
                .Getter()
                .Returns(42);

            var result = _sut.Instance()[1, "value", new object()];

            result.ShouldBe(42);
        }

        [Fact]
        public void GivenCriteriaSpecificGetterSetups_WhenConfigured_ThenRemainIsolated()
        {
            var alphaToken = new object();
            var betaToken = new object();

            _sut[Arg<int>.Is(x => x == 1), Arg<string>.Is(s => s == "alpha"), Arg<object>.Is(o => ReferenceEquals(o, alphaToken))]
                .Getter()
                .Returns(100);

            _sut[Arg<int>.Is(x => x == 2), Arg<string>.Is(s => s == "beta"), Arg<object>.Is(o => ReferenceEquals(o, betaToken))]
                .Getter()
                .Returns(200);

            var instance = _sut.Instance();
            instance[1, "alpha", alphaToken].ShouldBe(100);
            instance[2, "beta", betaToken].ShouldBe(200);
            Should.Throw<MissingImposterException>(() => _ = instance[3, "none", new object()]);
        }

        [Fact]
        public void GivenRepeatedGetterSetup_WhenConfiguredAgain_ThenLatestWins()
        {
            _sut[Arg<int>.Is(x => x == 10), Arg<string>.Is(s => s == "dup"), Arg<object>.Any()]
                .Getter()
                .Returns(111);

            _sut[Arg<int>.Is(x => x == 10), Arg<string>.Is(s => s == "dup"), Arg<object>.Any()]
                .Getter()
                .Returns(222);

            var result = _sut.Instance()[10, "dup", new object()];

            result.ShouldBe(222);
        }

        [Fact]
        public void GivenSequentialGetterSetup_WhenAccessed_ThenReturnsInOrder()
        {
            var builder = _sut[Arg<int>.Any(), Arg<string>.Any(), Arg<object>.Any()]
                .Getter();

            builder
                .Returns(10)
                .Then()
                .Returns(() => 20)
                .Then()
                .Returns((key1, key2, key3) => key1 + key2.Length + key3!.GetHashCode());

            var instance = _sut.Instance();
            var thirdArgument = new object();

            instance[1, "bar", thirdArgument].ShouldBe(10);
            instance[1, "bar", thirdArgument].ShouldBe(20);
            instance[1, "bar", thirdArgument].ShouldBe(1 + "bar".Length + thirdArgument.GetHashCode());

            Should.NotThrow(() => builder.Called(Count.Exactly(3)));
        }

        [Fact]
        public void GivenSequentialGetterSetup_WhenDepleted_ThenRepeatsLastValue()
        {
            _sut[Arg<int>.Any(), Arg<string>.Any(), Arg<object>.Any()]
                .Getter()
                .Returns(1)
                .Then()
                .Returns(() => 2);

            var instance = _sut.Instance();
            instance[1, "bar", new object()].ShouldBe(1);
            instance[1, "bar", new object()].ShouldBe(2);
            instance[1, "bar", new object()].ShouldBe(2);
            instance[1, "bar", new object()].ShouldBe(2);
        }

        [Fact]
        public void GivenSingleArgumentIndexer_WhenConfiguringGetter_ThenSupportsSetupsAndCallbacks()
        {
            var builder = _sut[Arg<int>.Is(x => x == 8)]
                .Getter();

            builder.Callback(key => key.ShouldBe(8));
            builder.Returns(80);

            var instance = _sut.Instance();

            instance[8].ShouldBe(80);
        }

        [Fact]
        public void GivenSingleArgumentIndexer_WhenUsingSequentialGetter_ThenSupportsReturnsAndCalled()
        {
            var builder = _sut[Arg<int>.Is(x => x > 0)]
                .Getter();

            builder
                .Returns(5)
                .Then()
                .Returns(() => 10);

            var instance = _sut.Instance();
            instance[1].ShouldBe(5);
            instance[2].ShouldBe(10);
            instance[3].ShouldBe(10);

            Should.NotThrow(() => builder.Called(Count.AtLeast(3)));
        }

        [Fact]
        public void GivenDefaultGetterBehaviour_WhenAccessed_ThenSharesSetterBackstore()
        {
            var instance = _sut.Instance();
            var thirdArgument = new object();

            instance[5, "foo", thirdArgument] = 123;
            instance[5, "foo", thirdArgument].ShouldBe(123);

            instance[5, "foo", thirdArgument] = 987;
            instance[5, "foo", thirdArgument].ShouldBe(987);
        }

        [Fact]
        public void GivenMultiArgumentIndexer_WhenConfigured_ThenDoesNotAffectSingleArgumentDefaults()
        {
            _sut[Arg<int>.Is(x => x == 1), Arg<string>.Any(), Arg<object>.Any()]
                .Getter()
                .Returns(20);

            var instance = _sut.Instance();

            instance[1, "anything", new object()].ShouldBe(20);
            instance[9].ShouldBe(0);
        }

        [Fact]
        public void GivenSingleArgumentIndexer_WhenConfigured_ThenDoesNotAffectThreeArgumentDefaults()
        {
            _sut[Arg<int>.Is(x => x == 5)]
                .Getter()
                .Returns(50);

            var instance = _sut.Instance();
            var arg3 = new object();

            instance[5].ShouldBe(50);
            instance[2, "missing", arg3].ShouldBe(0);
        }

        [Fact]
        public void GivenExplicitModeGetter_WhenNotConfigured_ThenThrows()
        {
            var sut = new IIndexerSetupSutImposter(ImposterInvocationBehavior.Explicit);

            var ex = Should.Throw<MissingImposterException>(() =>
            {
                _ = sut.Instance()[7, "missing", new object()];
            });

            ex.Message.ShouldContain("(getter)");
        }

        [Fact]
        public void GivenGetterCallCount_WhenVerificationMismatch_ThenCalledFails()
        {
            _sut[Arg<int>.Any(), Arg<string>.Any(), Arg<object>.Any()]
                .Getter()
                .Returns(1);

            _sut.Instance()[1, "test", new object()].ShouldBe(1);

            Should.Throw<VerificationFailedException>(() =>
                _sut[Arg<int>.Any(), Arg<string>.Any(), Arg<object>.Any()]
                    .Getter()
                    .Called(Count.Exactly(2)));
        }

        [Fact]
        public void GivenNoGetterCalls_WhenVerifyingNever_ThenVerificationPasses()
        {
            var builder = _sut[Arg<int>.Is(x => x == 3), Arg<string>.Any(), Arg<object>.Any()]
                .Getter();

            builder.Returns(5);

            Should.NotThrow(() => builder.Called(Count.Never()));

            _ = _sut.Instance()[3, "now", new object()];

            Should.Throw<VerificationFailedException>(() => builder.Called(Count.Never()));
        }

        [Fact]
        public void GivenGetterCountThreshold_WhenVerifyingAtLeast_ThenPassesAtThreshold()
        {
            var builder = _sut[Arg<int>.Any(), Arg<string>.Any(), Arg<object>.Any()]
                .Getter();

            builder.Returns(2);

            var instance = _sut.Instance();
            _ = instance[1, "a", new object()];
            _ = instance[2, "b", new object()];

            Should.NotThrow(() => builder.Called(Count.AtLeast(2)));
        }

        [Fact]
        public void GivenGetterCountThreshold_WhenVerifyingAtMost_ThenPassesBelowThreshold()
        {
            var builder = _sut[Arg<int>.Any(), Arg<string>.Any(), Arg<object>.Any()]
                .Getter();

            builder.Returns(7);

            var instance = _sut.Instance();
            _ = instance[4, "x", new object()];

            Should.NotThrow(() => builder.Called(Count.AtMost(2)));
        }

        [Fact]
        public void GivenThrowingGetter_WhenCountingCalled_ThenIncludesExceptions()
        {
            var builder = _sut[Arg<int>.Is(x => x == 1), Arg<string>.Any(), Arg<object>.Any()]
                .Getter();

            builder.Throws((key, name, payload) => new InvalidOperationException($"boom:{key}:{name}:{payload.GetHashCode()}"));

            Should.Throw<InvalidOperationException>(() => _ = _sut.Instance()[1, "fail", new object()]);

            Should.NotThrow(() => builder.Called(Count.Exactly(1)));
        }

        [Fact]
        public void GivenGetterCallbacks_WhenInvoked_ThenFireBeforeValueResolution()
        {
            var visitedKeys = new List<int>();

            var builder = _sut[Arg<int>.Any(), Arg<string>.Any(), Arg<object>.Any()]
                .Getter();

            builder.Callback((a, b, c) => visitedKeys.Add(a));
            builder.Returns((key1, key2, key3) => key1 + 1);

            var instance = _sut.Instance();
            _ = instance[5, "foo", new object()];
            _ = instance[7, "bar", new object()];

            visitedKeys.ShouldBe(new[] { 5, 7 });
        }

        [Fact]
        public void GivenGetterOnlyIndexer_WhenConfigured_ThenSupportsSetups()
        {
            var sut = new IGetterOnlyIndexerSetupSutImposter();

            sut[Arg<int>.Is(x => x == 42), Arg<string>.Any()]
                .Getter()
                .Returns(100);

            sut.Instance()[42, "anything"].ShouldBe(100);
        }

        [Fact]
        public void GivenGetterOnlyIndexer_WhenNotConfigured_ThenDefaultsToZero()
        {
            var sut = new IGetterOnlyIndexerSetupSutImposter();

            sut.Instance()[5, "nothing"].ShouldBe(0);
        }

        [Fact]
        public void GivenGetterOnlyIndexer_WhenRegisteringCallbacks_ThenCallbacksSupported()
        {
            var sut = new IGetterOnlyIndexerSetupSutImposter();
            var visitedKeys = new List<int>();

            var builder = sut[Arg<int>.Any(), Arg<string>.Any()]
                .Getter();

            builder.Callback((key, _) => visitedKeys.Add(key));
            builder.Returns((key, _) => key * 2);

            var instance = sut.Instance();
            _ = instance[3, "first"];
            _ = instance[5, "second"];

            visitedKeys.ShouldBe(new[] { 3, 5 });
        }

        [Fact]
        public void GivenGetterOnlyIndexer_WhenVerifyingCalled_ThenVerificationWorks()
        {
            var sut = new IGetterOnlyIndexerSetupSutImposter();

            sut[Arg<int>.Any(), Arg<string>.Any()]
                .Getter()
                .Returns(1);

            var instance = sut.Instance();
            _ = instance[9, "one"];
            _ = instance[10, "two"];

            Should.NotThrow(() =>
                sut[Arg<int>.Any(), Arg<string>.Any()]
                    .Getter()
                    .Called(Count.Exactly(2)));
        }

        [Fact]
        public void GivenGetterExceptionInstance_WhenConfigured_ThenThrowsSameInstance()
        {
            _sut[Arg<int>.Any(), Arg<string>.Any(), Arg<object>.Any()]
                .Getter()
                .Throws(new InvalidOperationException("boom"));

            var instance = _sut.Instance();

            var ex = Should.Throw<InvalidOperationException>(() => _ = instance[2, "err", new object()]);
            ex.Message.ShouldBe("boom");
        }

        [Fact]
        public void GivenGetterExceptionType_WhenConfigured_ThenThrowsNewInstance()
        {
            _sut[Arg<int>.Any(), Arg<string>.Any(), Arg<object>.Any()]
                .Getter()
                .Throws<InvalidOperationException>();

            var instance = _sut.Instance();

            Should.Throw<InvalidOperationException>(() => _ = instance[3, "err", new object()]);
        }

        [Fact]
        public void GivenImplicitModeGetter_WhenCallUnmatched_ThenThrows()
        {
            _sut[Arg<int>.Is(x => x == 5), Arg<string>.Is(s => s == "configured"), Arg<object>.Any()]
                .Getter()
                .Returns(500);

            var instance = _sut.Instance();

            instance[5, "configured", new object()].ShouldBe(500);

            var ex = Should.Throw<MissingImposterException>(() => _ = instance[1, "missing", new object()]);
            ex.Message.ShouldContain("(getter)");
        }
        [Fact]
        public void GivenGetterExceptionFactory_WhenInvoked_ThenThrowsGeneratedException()
        {
            _sut[Arg<int>.Any(), Arg<string>.Any(), Arg<object>.Any()]
                .Getter()
                .Throws((key1, key2, key3) => new InvalidOperationException($"{key1}:{key2}:{key3.GetHashCode()}"));

            var instance = _sut.Instance();
            var arg3 = new object();

            var ex = Should.Throw<InvalidOperationException>(() => _ = instance[7, "marker", arg3]);
            ex.Message.ShouldContain("7:marker");
        }

        [Fact]
        public void GivenMultipleGetterCallbacks_WhenInvoked_ThenExecuteAll()
        {
            var capture = new List<string>();

            var builder = _sut[Arg<int>.Is(x => x == 9), Arg<string>.Is(s => s == "multi"), Arg<object>.Any()]
                .Getter();

            builder.Callback((key, name, obj) => capture.Add($"first:{key}:{name}"));
            builder.Callback((key, name, obj) => capture.Add($"second:{key}:{name}:{obj.GetHashCode()}"));
            builder.Returns(900);

            var instance = _sut.Instance();
            var marker = new object();

            instance[9, "multi", marker].ShouldBe(900);

            capture.Count.ShouldBe(2);
            capture[0].ShouldBe("first:9:multi");
            capture[1].StartsWith("second:9:multi:").ShouldBeTrue();
        }

        [Fact]
        public void GivenGetterOnlyIndexerInExplicitMode_WhenNotConfigured_ThenThrows()
        {
            var sut = new IGetterOnlyIndexerSetupSutImposter(ImposterInvocationBehavior.Explicit);

            Should.Throw<MissingImposterException>(() =>
                _ = sut.Instance()[1, "missing"]);
        }
    }
}
