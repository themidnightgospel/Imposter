using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.PerformedInvocationExceptionMessage
{
    public class IndexerPerformedInvocationExceptionMessageTests
    {
        private const string ThreeParameterIndexerDisplayName =
            "Imposter.Tests.Features.IndexerImposter.IIndexerSetupSut.this[int key1, string key2, object key3]";

        [Fact]
        public void GivenIndexerSetterCalls_WhenVerificationFails_ShouldRecordEachInvocation()
        {
            var sut = PerformedInvocationSutFactory.CreateIndexerSut();
            var setter = sut[Arg<int>.Any(), Arg<string>.Any(), Arg<object>.Any()].Setter();

            var instance = sut.Instance();
            instance[0, "k1", new object()] = 10;
            instance[1, "k2", new object()] = 20;

            var expectedCount = Count.Exactly(3);
            var exception = Should.Throw<VerificationFailedException>(() =>
                setter.Called(expectedCount)
            );

            var entries = exception.ReadEntries();
            entries.Length.ShouldBe(2);
            entries[0]
                .ShouldBe(
                    $"set {ThreeParameterIndexerDisplayName}[<1>, <k2>, <System.Object>] = <20>"
                );
            entries[1]
                .ShouldBe(
                    $"set {ThreeParameterIndexerDisplayName}[<0>, <k1>, <System.Object>] = <10>"
                );
            exception.MessageShouldDescribeCounts(expectedCount, 2);
        }

        [Fact]
        public void GivenIndexerGetterCalls_WhenVerificationFails_ShouldListEachAccess()
        {
            var sut = PerformedInvocationSutFactory.CreateIndexerSut();
            var getter = sut[Arg<int>.Any(), Arg<string>.Any(), Arg<object>.Any()].Getter();
            getter.Returns(42);

            var instance = sut.Instance();
            _ = instance[5, "first", new object()];
            _ = instance[6, "second", new object()];

            var expectedCount = Count.Exactly(3);
            var exception = Should.Throw<VerificationFailedException>(() =>
                getter.Called(expectedCount)
            );

            var entries = exception.ReadEntries();
            entries.Length.ShouldBe(2);
            entries[0]
                .ShouldBe(
                    $"get {ThreeParameterIndexerDisplayName}[<6>, <second>, <System.Object>]"
                );
            entries[1]
                .ShouldBe($"get {ThreeParameterIndexerDisplayName}[<5>, <first>, <System.Object>]");
            exception.MessageShouldDescribeCounts(expectedCount, 2);
        }

        [Fact]
        public void GivenIndexerPredicateVerificationFails_WhenVerificationFails_ShouldTrackAllInvocations()
        {
            var sut = PerformedInvocationSutFactory.CreateIndexerSut();
            sut[Arg<int>.Any(), Arg<string>.Any(), Arg<object>.Any()].Getter().Returns(0);

            var getter = sut[Arg<int>.Is(v => v > 5), Arg<string>.Any(), Arg<object>.Any()]
                .Getter();

            var instance = sut.Instance();
            _ = instance[2, "low", new object()];
            _ = instance[7, "match", new object()];
            _ = instance[9, "also-match", new object()];

            var expectedCount = Count.Exactly(3);
            var exception = Should.Throw<VerificationFailedException>(() =>
                getter.Called(expectedCount)
            );

            var entries = exception.ReadEntries();
            entries.Length.ShouldBe(3);
            entries[0]
                .ShouldBe(
                    $"get {ThreeParameterIndexerDisplayName}[<9>, <also-match>, <System.Object>]"
                );
            entries[1]
                .ShouldBe($"get {ThreeParameterIndexerDisplayName}[<7>, <match>, <System.Object>]");
            entries[2]
                .ShouldBe($"get {ThreeParameterIndexerDisplayName}[<2>, <low>, <System.Object>]");
            exception.MessageShouldDescribeCounts(expectedCount, 2);
        }
    }
}
