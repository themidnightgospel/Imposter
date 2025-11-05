using Imposter.Abstractions;
using System.Collections.Generic;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.IndexerSetup
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
        public void Callback_Fires_For_Each_Invocation_Before_Value_Is_Resolved()
        {
            var visitedKeys = new List<int>();

            _sut[Arg<int>.Any(), Arg<string>.Any(), Arg<object>.Any()]
                .Getter()
                .Callback((a, b, c) => visitedKeys.Add(a))
                .Returns((key1, key2, key3) => key1 + 1);

            var instance = _sut.Instance();
            instance[5, "foo", new object()];
            instance[7, "bar", new object()];

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
        public void GetterOnly_Indexer_Throws_In_Explicit_Mode_When_Not_Configured()
        {
            var sut = new IGetterOnlyIndexerSetupSutImposter(ImposterInvocationBehavior.Explicit);

            Should.Throw<MissingImposterException>(() =>
                _ = sut.Instance()[1, "missing"]);
        }
    }
}
