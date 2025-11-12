using System;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Ideation.IndexerSetupPoc
{
    public sealed class IndexerSetupPocTests
    {
        [Fact]
        public void Getter_WithMatchingSetup_ReturnsConfiguredValue()
        {
            var imposter = new IndexerSetupPocImposter();
            var key = new object();

            imposter[Arg<int>.Is(10), Arg<string>.Is("alpha"), Arg<object?>.Is(key)]
                .Getter()
                .Returns(99);

            imposter.Instance()[10, "alpha", key].ShouldBe(99);
        }

        [Fact]
        public void Getter_WithDelegateSetup_ReceivesInvocationArguments()
        {
            var imposter = new IndexerSetupPocImposter();

            imposter[Arg<int>.Any(), Arg<string>.Any(), Arg<object?>.Any()]
                .Getter()
                .Returns((value1, value2, value3) => value1 + value2.Length + ((value3 as int?) ?? 0));

            imposter.Instance()[2, "abc", 5].ShouldBe(10);
        }

        [Fact]
        public void Getter_WithSequentialSetup_ReturnsValuesInOrder()
        {
            var imposter = new IndexerSetupPocImposter();
            var builder = imposter[Arg<int>.Any(), Arg<string>.Any(), Arg<object?>.Any()]
                .Getter();

            builder.Returns(1).Then().Returns(5).Then().Returns(10);

            var instance = imposter.Instance();
            var key = new object();

            instance[0, string.Empty, key].ShouldBe(1);
            instance[0, string.Empty, key].ShouldBe(5);
            instance[0, string.Empty, key].ShouldBe(10);
            instance[0, string.Empty, key].ShouldBe(10);
        }

        [Fact]
        public void Getter_WhenNoSetup_UsesDefaultIndexerBehaviour()
        {
            var imposter = new IndexerSetupPocImposter();
            var instance = imposter.Instance();
            var firstKey = new object();
            var secondKey = new object();

            instance[1, "k1", firstKey] = 11;
            instance[2, "k2", secondKey] = 22;

            instance[1, "k1", firstKey].ShouldBe(11);
            instance[2, "k2", secondKey].ShouldBe(22);
        }

        [Fact]
        public void Getter_CallbackIsInvokedWithArguments()
        {
            var imposter = new IndexerSetupPocImposter();
            var key = new object();
            (int value1, string value2, object? value3) captured = default;

            imposter[Arg<int>.Any(), Arg<string>.Any(), Arg<object?>.Any()]
                .Getter()
                .Callback((value1, value2, value3) => captured = (value1, value2, value3))
                .Returns(1);

            var instance = imposter.Instance();
            instance[5, "cb", key].ShouldBe(1);

            captured.value1.ShouldBe(5);
            captured.value2.ShouldBe("cb");
            captured.value3.ShouldBe(key);
        }

        [Fact]
        public void Getter_LastMatchingSetupWins()
        {
            var imposter = new IndexerSetupPocImposter();
            var key = new object();

            imposter[Arg<int>.Any(), Arg<string>.Any(), Arg<object?>.Any()]
                .Getter()
                .Returns(10);

            imposter[Arg<int>.Is(7), Arg<string>.Any(), Arg<object?>.Any()]
                .Getter()
                .Returns(42);

            var instance = imposter.Instance();

            instance[7, "match", key].ShouldBe(42);
            instance[8, "mismatch", key].ShouldBe(10);
        }

        [Fact]
        public void Getter_CalledVerificationCountsInvocations()
        {
            var imposter = new IndexerSetupPocImposter();
            var builder = imposter[Arg<int>.Any(), Arg<string>.Any(), Arg<object?>.Any()]
                .Getter();

            builder.Returns(5);

            var instance = imposter.Instance();
            var key = new object();

            instance[1, "v", key].ShouldBe(5);
            instance[1, "v", key].ShouldBe(5);

            builder.Called(Count.Exactly(2));
        }

        [Fact]
        public void Getter_ThrowsConfiguredException()
        {
            var imposter = new IndexerSetupPocImposter();

            imposter[Arg<int>.Any(), Arg<string>.Any(), Arg<object?>.Any()]
                .Getter()
                .Throws<InvalidOperationException>();

            var instance = imposter.Instance();

            Should.Throw<InvalidOperationException>(() => instance[1, "err", new object()]);
        }

        [Fact]
        public void Getter_ExplicitInvocationWithoutSetup_Throws()
        {
            var imposter = new IndexerSetupPocImposter(ImposterMode.Explicit);
            var instance = imposter.Instance();

            Should.Throw<MissingImposterException>(() => instance[1, "missing", new object()]);
        }

        [Fact]
        public void Setter_CallbackRunsWhenCriteriaMatches()
        {
            var imposter = new IndexerSetupPocImposter();
            var key = new object();
            (int value1, string value2, object? value3, int value) captured = default;

            imposter[Arg<int>.Is(9), Arg<string>.Is("setter"), Arg<object?>.Is(key)]
                .Setter()
                .Callback((value1, value2, value3, value) => captured = (value1, value2, value3, value));

            var instance = imposter.Instance();
            instance[9, "setter", key] = 55;
            instance[8, "setter", key] = 99;

            captured.value1.ShouldBe(9);
            captured.value2.ShouldBe("setter");
            captured.value3.ShouldBe(key);
            captured.value.ShouldBe(55);
        }

        [Fact]
        public void Setter_CallbackIsNotInvokedForNonMatchingCriteria()
        {
            var imposter = new IndexerSetupPocImposter();
            var key = new object();
            var callbackInvoked = false;

            imposter[Arg<int>.Is(1), Arg<string>.Is("only"), Arg<object?>.Is(key)]
                .Setter()
                .Callback((value1, value2, value3, value) => callbackInvoked = true);

            var instance = imposter.Instance();
            instance[2, "other", key] = 10;

            callbackInvoked.ShouldBeFalse();
        }

        [Fact]
        public void Setter_CalledVerificationCountsInvocations()
        {
            var imposter = new IndexerSetupPocImposter();
            var builder = imposter[Arg<int>.Any(), Arg<string>.Any(), Arg<object?>.Any()]
                .Setter();

            var instance = imposter.Instance();
            var firstKey = new object();
            var secondKey = new object();

            instance[1, "a", firstKey] = 1;
            instance[2, "b", secondKey] = 2;

            builder.Called(Count.AtLeast(2));
        }

        [Fact]
        public void Setter_DefaultBehaviourPersistsValueForGetter()
        {
            var imposter = new IndexerSetupPocImposter();
            var instance = imposter.Instance();
            var key = new object();

            instance[1, "persist", key] = 77;

            instance[1, "persist", key].ShouldBe(77);
        }

        [Fact]
        public void Setter_ExplicitInvocationWithoutSetup_Throws()
        {
            var imposter = new IndexerSetupPocImposter(ImposterMode.Explicit);
            var instance = imposter.Instance();

            Should.Throw<MissingImposterException>(() => instance[1, "missing", new object()] = 10);
        }
    }
}
