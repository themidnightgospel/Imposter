using System.Collections.Generic;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.IndexerImposter
{
    public class SetterTests
    {
        private readonly IIndexerSetupSutImposter _sut =
#if USE_CSHARP14
        IIndexerSetupSut.Imposter();
#else
        new IIndexerSetupSutImposter();
#endif

        [Fact]
        public void GivenMatchingSetterCriteria_WhenCallbacksConfigured_ShouldFireCallback()
        {
            var callbackCount = 0;

            _sut[Arg<int>.Is(x => x == 5), Arg<string>.Any(), Arg<object>.Any()]
                .Setter()
                .Callback(
                    (key1, key2, key3, value) =>
                    {
                        callbackCount++;
                        key1.ShouldBe(5);
                        value.ShouldBe(99);
                    }
                );

            _sut.Instance()[5, "match", new object()] = 99;
            _sut.Instance()[6, "miss", new object()] = 123;

            callbackCount.ShouldBe(1);
        }

        [Fact]
        public void GivenSetterInteractions_WhenVerifyingCalled_ShouldCountInvocations()
        {
            var instance = _sut.Instance();

            instance[1, "foo", new object()] = 10;
            instance[1, "foo", new object()] = 20;
            instance[2, "bar", new object()] = 30;

            Should.NotThrow(() =>
                _sut[Arg<int>.Any(), Arg<string>.Any(), Arg<object>.Any()]
                    .Setter()
                    .Called(Count.Exactly(3))
            );
        }

        [Fact]
        public void GivenGetterInteractions_WhenVerifyingCalled_ShouldCountInvocations()
        {
            _sut[Arg<int>.Any(), Arg<string>.Any(), Arg<object>.Any()].Getter().Returns(1);

            var instance = _sut.Instance();
            _ = instance[9, "times", new object()];
            _ = instance[9, "times", new object()];

            Should.NotThrow(() =>
                _sut[Arg<int>.Any(), Arg<string>.Any(), Arg<object>.Any()]
                    .Getter()
                    .Called(Count.Exactly(2))
            );
        }

        [Fact]
        public void GivenSetterInteractionCount_WhenVerificationMismatched_ShouldFailCalledVerification()
        {
            var setter = _sut[Arg<int>.Any(), Arg<string>.Any(), Arg<object>.Any()].Setter();

            _sut.Instance()[1, "one", new object()] = 1;

            Should.Throw<VerificationFailedException>(() => setter.Called(Count.Exactly(2)));
        }

        [Fact]
        public void GivenNoSetterInvocations_WhenVerifyingNever_ShouldPassVerification()
        {
            var builder = _sut[Arg<int>.Is(x => x == 8), Arg<string>.Any(), Arg<object>.Any()]
                .Setter();

            Should.NotThrow(() => builder.Called(Count.Never()));

            _sut.Instance()[8, "value", new object()] = 10;

            Should.Throw<VerificationFailedException>(() => builder.Called(Count.Never()));
        }

        [Fact]
        public void GivenSetterCountThreshold_WhenVerifyingAtLeast_ShouldPassAtThreshold()
        {
            var builder = _sut[Arg<int>.Any(), Arg<string>.Any(), Arg<object>.Any()].Setter();

            var instance = _sut.Instance();
            instance[1, "foo", new object()] = 5;
            instance[1, "foo", new object()] = 6;

            Should.NotThrow(() => builder.Called(Count.AtLeast(2)));
        }

        [Fact]
        public void GivenSetterCountThreshold_WhenVerifyingAtMost_ShouldPassBelowThreshold()
        {
            var builder = _sut[Arg<int>.Any(), Arg<string>.Any(), Arg<object>.Any()].Setter();

            _sut.Instance()[9, "bar", new object()] = 10;

            Should.NotThrow(() => builder.Called(Count.AtMost(2)));
        }

        [Fact]
        public void GivenMultipleSetterCriteria_WhenRegisteringCallbacks_ShouldMaintainSeparateScopes()
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
        public void GivenExplicitModeSetter_WhenNotConfigured_ShouldThrow()
        {
            var sut = new IIndexerSetupSutImposter(ImposterMode.Explicit);

            Should.Throw<MissingImposterException>(() =>
                sut.Instance()[1, "value", new object()] = 5
            );
        }

        [Fact]
        public void GivenSetterOnlyIndexer_WhenConfiguringCallbacks_ShouldSupportCallbacks()
        {
            var sut = new ISetterOnlyIndexerSetupSutImposter();
            var called = false;

            sut[Arg<int>.Any()]
                .Setter()
                .Callback(
                    (key, value) =>
                    {
                        called = true;
                        value.ShouldBe(50);
                    }
                );

            sut.Instance()[42] = 50;

            called.ShouldBeTrue();
        }

        [Fact]
        public void GivenSetterOnlyIndexer_WhenChainingThen_ShouldSupportChaining()
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
        public void GivenSingleArgumentIndexers_WhenSharingDefaults_ShouldUseSameBackstore()
        {
            var instance = _sut.Instance();

            instance[11] = 123;
            instance[11].ShouldBe(123);
        }

        [Fact]
        public void GivenSingleArgumentSetter_WhenUsingCallbacksAndCalled_ShouldWork()
        {
            var callbackHits = 0;

            var builder = _sut[Arg<int>.Is(x => x == 15)].Setter();

            builder.Callback(
                (key, value) =>
                {
                    callbackHits++;
                    key.ShouldBe(15);
                    value.ShouldBe(99);
                }
            );

            _sut.Instance()[15] = 99;

            callbackHits.ShouldBe(1);
            Should.NotThrow(() => builder.Called(Count.Exactly(1)));
        }

        [Fact]
        public void GivenSetterOnlyIndexer_WhenVerifyingCalled_ShouldWorkVerification()
        {
            var sut = new ISetterOnlyIndexerSetupSutImposter();

            var builder = sut[Arg<int>.Any()].Setter();

            sut.Instance()[7] = 77;

            Should.NotThrow(() => builder.Called(Count.Exactly(1)));
        }

        [Fact]
        public void GivenSetterOnlyIndexerInExplicitMode_WhenNotConfigured_ShouldThrow()
        {
            var sut = new ISetterOnlyIndexerSetupSutImposter(ImposterMode.Explicit);

            Should.Throw<MissingImposterException>(() => sut.Instance()[1] = 10);
        }
    }
}
