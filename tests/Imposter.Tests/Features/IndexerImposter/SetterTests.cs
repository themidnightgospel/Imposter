using System.Collections.Generic;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.IndexerImposter
{
    public class SetterTests
    {
        private readonly IIndexerSetupSutImposter _sut = new IIndexerSetupSutImposter();

        [Fact]
        public void Callback_Fires_For_Matching_Criteria()
        {
            var callbackCount = 0;

            _sut[Arg<int>.Is(x => x == 5), Arg<string>.Any(), Arg<object>.Any()]
                .Setter()
                .Callback((key1, key2, key3, value) =>
                {
                    callbackCount++;
                    key1.ShouldBe(5);
                    value.ShouldBe(99);
                });

            _sut.Instance()[5, "match", new object()] = 99;
            _sut.Instance()[6, "miss", new object()] = 123;

            callbackCount.ShouldBe(1);
        }

        [Fact]
        public void Called_Verifies_Number_Of_Setter_Invocations()
        {
            var instance = _sut.Instance();

            instance[1, "foo", new object()] = 10;
            instance[1, "foo", new object()] = 20;
            instance[2, "bar", new object()] = 30;

            Should.NotThrow(() =>
                _sut[Arg<int>.Any(), Arg<string>.Any(), Arg<object>.Any()]
                    .Setter()
                    .Called(Count.Exactly(3)));
        }

        [Fact]
        public void Called_Verifies_Number_Of_Getter_Invocations()
        {
            _sut[Arg<int>.Any(), Arg<string>.Any(), Arg<object>.Any()]
                .Getter()
                .Returns(1);

            var instance = _sut.Instance();
            _ = instance[9, "times", new object()];
            _ = instance[9, "times", new object()];

            Should.NotThrow(() =>
                _sut[Arg<int>.Any(), Arg<string>.Any(), Arg<object>.Any()]
                    .Getter()
                    .Called(Count.Exactly(2)));
        }

        [Fact]
        public void Called_Verification_For_Setter_Fails_When_Count_Mismatched()
        {
            var setter = _sut[Arg<int>.Any(), Arg<string>.Any(), Arg<object>.Any()]
                .Setter();

            _sut.Instance()[1, "one", new object()] = 1;

            Should.Throw<VerificationFailedException>(() => setter.Called(Count.Exactly(2)));
        }

        [Fact]
        public void Setter_Count_Never_Passes_When_Not_Invoked()
        {
            var builder = _sut[Arg<int>.Is(x => x == 8), Arg<string>.Any(), Arg<object>.Any()]
                .Setter();

            Should.NotThrow(() => builder.Called(Count.Never()));

            _sut.Instance()[8, "value", new object()] = 10;

            Should.Throw<VerificationFailedException>(() => builder.Called(Count.Never()));
        }

        [Fact]
        public void Setter_Count_AtLeast_Passes_When_Threshold_Reached()
        {
            var builder = _sut[Arg<int>.Any(), Arg<string>.Any(), Arg<object>.Any()]
                .Setter();

            var instance = _sut.Instance();
            instance[1, "foo", new object()] = 5;
            instance[1, "foo", new object()] = 6;

            Should.NotThrow(() => builder.Called(Count.AtLeast(2)));
        }

        [Fact]
        public void Setter_Count_AtMost_Passes_When_Below_Threshold()
        {
            var builder = _sut[Arg<int>.Any(), Arg<string>.Any(), Arg<object>.Any()]
                .Setter();

            _sut.Instance()[9, "bar", new object()] = 10;

            Should.NotThrow(() => builder.Called(Count.AtMost(2)));
        }

        [Fact]
        public void Setter_Callbacks_AreScoped_By_Criteria()
        {
            var triggered = new List<int>();

            _sut[Arg<int>.Is(x => x == 1), Arg<string>.Any(), Arg<object>.Any()]
                .Setter()
                .Callback((k1, k2, k3, value) => triggered.Add(value));

            _sut[Arg<int>.Is(x => x == 2), Arg<string>.Any(), Arg<object>.Any()]
                .Setter()
                .Callback((k1, k2, k3, value) => triggered.Add(-value));

            var instance = _sut.Instance();
            instance[1, "match", new object()] = 10;
            instance[2, "also-match", new object()] = 20;
            instance[3, "miss", new object()] = 30;

            triggered.ShouldBe(new[] { 10, -20 });
        }

        [Fact]
        public void Setter_Throws_In_Explicit_Mode_When_Not_Configured()
        {
            var sut = new IIndexerSetupSutImposter(ImposterInvocationBehavior.Explicit);

            Should.Throw<MissingImposterException>(() =>
                sut.Instance()[1, "value", new object()] = 5);
        }
        
        [Fact]
        public void SetterOnly_Indexer_Supports_Callbacks()
        {
            var sut = new ISetterOnlyIndexerSetupSutImposter();
            var called = false;
            
            sut[Arg<int>.Any()]
                .Setter()
                .Callback((key, value) =>
                {
                    called = true;
                    value.ShouldBe(50);
                });

            sut.Instance()[42] = 50;

            called.ShouldBeTrue();
        }

        [Fact]
        public void SetterOnly_Indexer_Supports_Chained_Then()
        {
            var sut = new ISetterOnlyIndexerSetupSutImposter();
            var observed = new List<int>();

            sut[Arg<int>.Any()]
                .Setter()
                .Callback((key, value) => observed.Add(value))
                .Then()
                .Callback((key, value) => observed.Add(value * 2));

            sut.Instance()[7] = 3;

            observed.ShouldBe(new[] { 3, 6 });
        }

        [Fact]
        public void SingleArgument_Indexer_Share_Default_Backstore()
        {
            var instance = _sut.Instance();

            instance[11] = 123;
            instance[11].ShouldBe(123);
        }

        [Fact]
        public void SingleArgument_Indexer_Setter_Callbacks_And_Called_Work()
        {
            var callbackHits = 0;

            var builder = _sut[Arg<int>.Is(x => x == 15)]
                .Setter()
                .Callback((key, value) =>
                {
                    callbackHits++;
                    key.ShouldBe(15);
                    value.ShouldBe(99);
                });

            _sut.Instance()[15] = 99;

            callbackHits.ShouldBe(1);
            Should.NotThrow(() => builder.Called(Count.Exactly(1)));
        }
        [Fact]
        public void SetterOnly_Indexer_Called_Verification_Works()
        {
            var sut = new ISetterOnlyIndexerSetupSutImposter();

            var builder = sut[Arg<int>.Any()]
                .Setter();

            sut.Instance()[7] = 77;

            Should.NotThrow(() => builder.Called(Count.Exactly(1)));
        }

        [Fact]
        public void SetterOnly_Indexer_Throws_In_Explicit_Mode_When_Not_Configured()
        {
            var sut = new ISetterOnlyIndexerSetupSutImposter(ImposterInvocationBehavior.Explicit);

            Should.Throw<MissingImposterException>(() =>
                sut.Instance()[1] = 10);
        }
    }
}
