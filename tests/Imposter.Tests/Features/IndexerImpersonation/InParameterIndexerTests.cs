using System;
using System.Collections.Generic;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.IndexerImpersonation
{
    public class InParameterIndexerTests
    {
        private readonly IInParameterIndexerSutImposter _sut = new IInParameterIndexerSutImposter();

        [Fact]
        public void Given_InParameterIndexer_When_GetterDelegatesAreConfigured_Should_PassKeyToCallbackAndReturns()
        {
            var observed = new List<int>();
            var getter = _sut[Arg<int>.Any()].Getter();
            getter.Callback(key => observed.Add(key));
            getter.Returns(key => key * 2);

            _sut.Instance()[3].ShouldBe(6);
            _sut.Instance()[5].ShouldBe(10);

            observed.ShouldBe(new[] { 3, 5 });
            getter.Called(Count.Exactly(2));
        }

        [Fact]
        public void Given_InParameterIndexer_When_SetterCallbackIsConfigured_Should_PassKeyAndValue()
        {
            var observed = new List<(int Key, int Value)>();
            var setter = _sut[Arg<int>.Any()].Setter();
            setter.Callback((key, value) => observed.Add((key, value)));

            _sut.Instance()[3] = 7;
            _sut.Instance()[5] = 11;

            observed.ShouldBe(new[] { (3, 7), (5, 11) });
            setter.Called(Count.Exactly(2));
        }

        [Fact]
        public void Given_InParameterIndexer_When_ExceptionFactoryIsConfigured_Should_PassKey()
        {
            var getter = _sut[Arg<int>.Any()].Getter();
            getter.Throws(key => new InvalidOperationException($"key:{key}"));

            var exception = Should.Throw<InvalidOperationException>(() => _ = _sut.Instance()[7]);

            exception.Message.ShouldBe("key:7");
            getter.Called(Count.Once());
        }

        [Fact]
        public void Given_InParameterIndexer_When_ValueIsStored_Should_ReturnValueForMatchingKey()
        {
            var instance = _sut.Instance();
            instance[3] = 7;

            instance[3].ShouldBe(7);
            instance[5].ShouldBe(0);
        }
    }
}
