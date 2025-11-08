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
        public void Returns_Configured_Value()
        {
            _sut[Arg<int>.Any(), Arg<string>.Any(), Arg<object>.Any()]
                .Getter()
                .Returns(42);

            var result = _sut.Instance()[1, "value", new object()];

            result.ShouldBe(42);
        }

        [Fact]
        public void Criteria_Specific_Getter_Setups_Are_Isolated()
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
        public void Latest_Setup_Wins_For_Same_Criteria()
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
        public void Returns_Sequential_Values()
        {
            var sequence = _sut[Arg<int>.Any(), Arg<string>.Any(), Arg<object>.Any()]
                .Getter()
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

            Should.NotThrow(() => sequence.Called(Count.Exactly(3)));
        }

        [Fact]
        public void Sequential_Returns_Repeat_Last_Value_When_Depleted()
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
        public void SingleArgument_Indexer_Getter_Supports_Setups_And_Callbacks()
        {
            _sut[Arg<int>.Is(x => x == 8)]
                .Getter()
                .Callback(key => key.ShouldBe(8))
                .Returns(80);

            var instance = _sut.Instance();

            instance[8].ShouldBe(80);
        }

        [Fact]
        public void SingleArgument_Indexer_Getter_Supports_Sequential_Returns_And_Called()
        {
            var sequence = _sut[Arg<int>.Is(x => x > 0)]
                .Getter()
                .Returns(5)
                .Then()
                .Returns(() => 10);

            var instance = _sut.Instance();
            instance[1].ShouldBe(5);
            instance[2].ShouldBe(10);
            instance[3].ShouldBe(10);

            Should.NotThrow(() => sequence.Called(Count.AtLeast(3)));
        }

        [Fact]
        public void Default_Behaviour_Shares_Setter_Backstore()
        {
            var instance = _sut.Instance();
            var thirdArgument = new object();

            instance[5, "foo", thirdArgument] = 123;
            instance[5, "foo", thirdArgument].ShouldBe(123);

            instance[5, "foo", thirdArgument] = 987;
            instance[5, "foo", thirdArgument].ShouldBe(987);
        }

        [Fact]
        public void MultiArgument_Indexer_Does_Not_Affect_SingleArgument_Defaults()
        {
            _sut[Arg<int>.Is(x => x == 1), Arg<string>.Any(), Arg<object>.Any()]
                .Getter()
                .Returns(20);

            var instance = _sut.Instance();

            instance[1, "anything", new object()].ShouldBe(20);
            instance[9].ShouldBe(0);
        }

        [Fact]
        public void SingleArgument_Indexer_Does_Not_Affect_ThreeArgument_Defaults()
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
        public void Explicit_Mode_Throws_When_Not_Configured()
        {
            var sut = new IIndexerSetupSutImposter(ImposterInvocationBehavior.Explicit);

            var ex = Should.Throw<MissingImposterException>(() =>
            {
                _ = sut.Instance()[7, "missing", new object()];
            });

            ex.Message.ShouldContain("(getter)");
        }

        [Fact]
        public void Called_Verification_Fails_When_Count_Does_Not_Match()
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
        public void Getter_Count_Never_Passes_When_Not_Invoked()
        {
            var builder = _sut[Arg<int>.Is(x => x == 3), Arg<string>.Any(), Arg<object>.Any()]
                .Getter()
                .Returns(5);

            Should.NotThrow(() => builder.Called(Count.Never()));

            _ = _sut.Instance()[3, "now", new object()];

            Should.Throw<VerificationFailedException>(() => builder.Called(Count.Never()));
        }

        [Fact]
        public void Getter_Count_AtLeast_Passes_When_Threshold_Reached()
        {
            var builder = _sut[Arg<int>.Any(), Arg<string>.Any(), Arg<object>.Any()]
                .Getter()
                .Returns(2);

            var instance = _sut.Instance();
            _ = instance[1, "a", new object()];
            _ = instance[2, "b", new object()];

            Should.NotThrow(() => builder.Called(Count.AtLeast(2)));
        }

        [Fact]
        public void Getter_Count_AtMost_Passes_When_Below_Threshold()
        {
            var builder = _sut[Arg<int>.Any(), Arg<string>.Any(), Arg<object>.Any()]
                .Getter()
                .Returns(7);

            var instance = _sut.Instance();
            _ = instance[4, "x", new object()];

            Should.NotThrow(() => builder.Called(Count.AtMost(2)));
        }

        [Fact]
        public void Getter_Called_Count_Includes_Throwing_Invocations()
        {
            var builder = _sut[Arg<int>.Is(x => x == 1), Arg<string>.Any(), Arg<object>.Any()]
                .Getter()
                .Throws((key, name, payload) => new InvalidOperationException($"boom:{key}:{name}:{payload.GetHashCode()}"));

            Should.Throw<InvalidOperationException>(() => _ = _sut.Instance()[1, "fail", new object()]);

            Should.NotThrow(() => builder.Called(Count.Exactly(1)));
        }

        [Fact]
        public void Callback_Fires_For_Each_Invocation_Before_Value_Is_Resolved()
        {
            var visitedKeys = new List<int>();

            _sut[Arg<int>.Any(), Arg<string>.Any(), Arg<object>.Any()]
                .Getter()
                .Callback((a, b, c) => visitedKeys.Add(a))
                .Returns((key1, key2, key3) => key1 + 1);

            var instance = _sut.Instance();
            _ = instance[5, "foo", new object()];
            _ = instance[7, "bar", new object()];

            visitedKeys.ShouldBe(new[] { 5, 7 });
        }

        [Fact]
        public void GetterOnly_Indexer_Supports_Setups()
        {
            var sut = new IGetterOnlyIndexerSetupSutImposter();

            sut[Arg<int>.Is(x => x == 42), Arg<string>.Any()]
                .Getter()
                .Returns(100);

            sut.Instance()[42, "anything"].ShouldBe(100);
        }

        [Fact]
        public void GetterOnly_Indexer_Defaults_To_Zero_When_Not_Configured()
        {
            var sut = new IGetterOnlyIndexerSetupSutImposter();

            sut.Instance()[5, "nothing"].ShouldBe(0);
        }

        [Fact]
        public void GetterOnly_Indexer_Callbacks_Are_Supported()
        {
            var sut = new IGetterOnlyIndexerSetupSutImposter();
            var visitedKeys = new List<int>();

            sut[Arg<int>.Any(), Arg<string>.Any()]
                .Getter()
                .Callback((key, _) => visitedKeys.Add(key))
                .Returns((key, _) => key * 2);

            var instance = sut.Instance();
            _ = instance[3, "first"];
            _ = instance[5, "second"];

            visitedKeys.ShouldBe(new[] { 3, 5 });
        }

        [Fact]
        public void GetterOnly_Indexer_Called_Verification_Works()
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
        public void Getter_Throws_Configured_Exception_Instance()
        {
            _sut[Arg<int>.Any(), Arg<string>.Any(), Arg<object>.Any()]
                .Getter()
                .Throws(new InvalidOperationException("boom"));

            var instance = _sut.Instance();

            var ex = Should.Throw<InvalidOperationException>(() => _ = instance[2, "err", new object()]);
            ex.Message.ShouldBe("boom");
        }

        [Fact]
        public void Getter_Throws_Generic_Exception_Type()
        {
            _sut[Arg<int>.Any(), Arg<string>.Any(), Arg<object>.Any()]
                .Getter()
                .Throws<InvalidOperationException>();

            var instance = _sut.Instance();

            Should.Throw<InvalidOperationException>(() => _ = instance[3, "err", new object()]);
        }

        [Fact]
        public void Unmatched_Getter_Call_Throws_After_Setup_In_Implicit_Mode()
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
        public void Getter_Throws_Using_Exception_Generator()
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
        public void Multiple_Callbacks_Run_For_A_Getter_Setup()
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
        public void GetterOnly_Indexer_Throws_In_Explicit_Mode_When_Not_Configured()
        {
            var sut = new IGetterOnlyIndexerSetupSutImposter(ImposterInvocationBehavior.Explicit);

            Should.Throw<MissingImposterException>(() =>
                _ = sut.Instance()[1, "missing"]);
        }
    }
}
