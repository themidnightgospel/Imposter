using System;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.IndexerImpersonation
{
    public class SpanIndexerTests
    {
        private readonly ISpanIndexerSutImposter _sut = new ISpanIndexerSutImposter();

        [Fact]
        public void GivenGetterSetupForKeyElements_WhenReadWithThoseElements_ShouldReturnItsValue()
        {
            _sut[ReadOnlySpanArg<char>.Is('a', 'b')].Getter().Returns(3);

            _sut.Instance()["ab".AsSpan()].ShouldBe(3);
        }

        [Fact]
        public void GivenGetterSetupForKeyElements_WhenReadWithOtherElements_ShouldReturnDefault()
        {
            _sut[ReadOnlySpanArg<char>.Is('a', 'b')].Getter().Returns(3);

            _sut.Instance()["ba".AsSpan()].ShouldBe(0);
        }

        [Fact]
        public void GivenGetterReturnsDelegate_WhenRead_ShouldPassItTheKeyElements()
        {
            _sut[ReadOnlySpanArg<char>.Any()].Getter().Returns(key => key.Length);

            _sut.Instance()["abc".AsSpan()].ShouldBe(3);
        }

        [Fact]
        public void GivenValueSetForKey_WhenReadWithAnotherKeyOfTheSameElements_ShouldReturnIt()
        {
            _sut.Instance()["ab".AsSpan()] = 5;

            _sut.Instance()[new[] { 'a', 'b' }].ShouldBe(5);
        }

        [Fact]
        public void GivenValueSet_WhenSetterIsVerifiedForTheKeyElements_ShouldCountIt()
        {
            _sut.Instance()["a".AsSpan()] = 1;

            _sut[ReadOnlySpanArg<char>.Is('a')].Setter().Called(Count.Once());
        }

        [Fact]
        public void GivenSetterCallback_WhenSet_ShouldPassItTheKeyElements()
        {
            char[]? receivedKey = null;
            _sut[ReadOnlySpanArg<char>.Any()].Setter().Callback((key, _) => receivedKey = key);

            _sut.Instance()["ab".AsSpan()] = 7;

            receivedKey.ShouldBe(new[] { 'a', 'b' });
        }

        [Fact]
        public void GivenSetterCallback_WhenSet_ShouldPassItTheValue()
        {
            var receivedValue = 0;
            _sut[ReadOnlySpanArg<char>.Any()]
                .Setter()
                .Callback((_, value) => receivedValue = value);

            _sut.Instance()["ab".AsSpan()] = 7;

            receivedValue.ShouldBe(7);
        }

        [Fact]
        public void GivenSpanValueGetterReturnsArray_WhenTheCallerWritesToTheSpan_ShouldWriteToTheArray()
        {
            var buffer = new byte[1];
            _sut[Arg<int>.Is(0)].Getter().Returns(buffer);

            _sut.Instance()[0][0] = 7;

            buffer[0].ShouldBe((byte)7);
        }

        [Fact]
        public void GivenSpanValueSet_WhenTheCallerChangesItsMemory_ShouldKeepACopy()
        {
            var source = new byte[] { 1 };
            _sut.Instance()[0] = source;

            source[0] = 9;

            _sut.Instance()[0][0].ShouldBe((byte)1);
        }

        [Fact]
        public void GivenClassGetterWithUseBaseImplementation_WhenRead_ShouldReturnTheBaseValue()
        {
            var imposter = new SpanIndexerClassImposter();
            imposter[ReadOnlySpanArg<char>.Any()].Getter().UseBaseImplementation();

            imposter.Instance()["a".AsSpan()].ShouldBe(1);
        }

        [Fact]
        public void GivenClassSetterWithUseBaseImplementation_WhenSet_ShouldSetTheBaseIndexer()
        {
            var imposter = new SpanIndexerClassImposter();
            imposter[ReadOnlySpanArg<char>.Any()].Setter().UseBaseImplementation();

            imposter.Instance()["b".AsSpan()] = 2;

            imposter.Instance().ValueOf("b").ShouldBe(2);
        }
    }
}
