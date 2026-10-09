using System;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.PropertyImpersonation
{
    public class SpanPropertyTests
    {
        private readonly ISpanPropertySutImposter _sut = new ISpanPropertySutImposter();

        [Fact]
        public void GivenGetterReturnsArray_WhenRead_ShouldReturnASpanOverTheArray()
        {
            _sut.Buffer.Getter().Returns(new byte[] { 1, 2 });

            _sut.Instance().Buffer.ToArray().ShouldBe(new byte[] { 1, 2 });
        }

        [Fact]
        public void GivenGetterReturnsArray_WhenTheCallerWritesToTheSpan_ShouldWriteToTheArray()
        {
            var buffer = new byte[1];
            _sut.Buffer.Getter().Returns(buffer);

            _sut.Instance().Buffer[0] = 7;

            buffer[0].ShouldBe((byte)7);
        }

        [Fact]
        public void GivenGetterReturnsDelegate_WhenRead_ShouldReturnTheDelegatesArray()
        {
            _sut.Name.Getter().Returns(() => new[] { 'a', 'b' });

            _sut.Instance().Name.ToString().ShouldBe("ab");
        }

        [Fact]
        public void GivenNoSetup_WhenRead_ShouldReturnAnEmptySpan()
        {
            _sut.Instance().Name.IsEmpty.ShouldBeTrue();
        }

        [Fact]
        public void GivenValueSet_WhenRead_ShouldReturnTheElementsItWasSetTo()
        {
            _sut.Instance().Buffer = new byte[] { 3, 4 };

            _sut.Instance().Buffer.ToArray().ShouldBe(new byte[] { 3, 4 });
        }

        [Fact]
        public void GivenValueSet_WhenTheCallerChangesItsMemory_ShouldKeepACopy()
        {
            var source = new byte[] { 1 };
            _sut.Instance().Buffer = source;

            source[0] = 9;

            _sut.Instance().Buffer[0].ShouldBe((byte)1);
        }

        [Fact]
        public void GivenSetterCallback_WhenSetToMatchingElements_ShouldPassThemToTheCallback()
        {
            byte[]? received = null;
            _sut.Buffer.Setter(SpanArg<byte>.Is(1, 2)).Callback(value => received = value);

            _sut.Instance().Buffer = new byte[] { 1, 2 };

            received.ShouldBe(new byte[] { 1, 2 });
        }

        [Fact]
        public void GivenSetterCallback_WhenSetToOtherElements_ShouldNotRunIt()
        {
            var called = false;
            _sut.Buffer.Setter(SpanArg<byte>.Is(1, 2)).Callback(_ => called = true);

            _sut.Instance().Buffer = new byte[] { 2, 1 };

            called.ShouldBeFalse();
        }

        [Fact]
        public void GivenValueSet_WhenSetterIsVerified_ShouldCountMatchingElements()
        {
            _sut.Instance().Buffer = new byte[] { 1, 2 };

            _sut.Buffer.Setter(SpanArg<byte>.Is(1, 2)).Called(Count.Once());
        }

        [Fact]
        public void GivenValueSet_WhenSetterIsVerifiedWithOtherElements_ShouldThrowVerificationFailedException()
        {
            _sut.Instance().Buffer = new byte[] { 1, 2 };

            Should.Throw<VerificationFailedException>(() =>
                _sut.Buffer.Setter(SpanArg<byte>.Is(3)).Called(Count.Once())
            );
        }

        [Fact]
        public void GivenValueRead_WhenGetterIsVerified_ShouldCountIt()
        {
            _ = _sut.Instance().Name.Length;

            _sut.Name.Getter().Called(Count.Once());
        }

        [Fact]
        public void GivenGetterThrows_WhenRead_ShouldThrow()
        {
            _sut.Name.Getter().Throws<InvalidOperationException>();

            Should.Throw<InvalidOperationException>(() => _sut.Instance().Name.Length);
        }

        [Fact]
        public void GivenClassGetterWithUseBaseImplementation_WhenRead_ShouldReturnTheBaseElements()
        {
            var imposter = new SpanPropertyClassImposter();
            imposter.Buffer.Getter().UseBaseImplementation();

            imposter.Instance().Buffer.ToArray().ShouldBe(new byte[] { 1, 2 });
        }

        [Fact]
        public void GivenClassSetterWithUseBaseImplementation_WhenSet_ShouldSetTheBaseProperty()
        {
            var imposter = new SpanPropertyClassImposter();
            imposter.Buffer.Setter(SpanArg<byte>.Any()).UseBaseImplementation();

            imposter.Instance().Buffer = new byte[] { 5 };

            imposter.Instance().BaseBuffer.ShouldBe(new byte[] { 5 });
        }
    }
}
