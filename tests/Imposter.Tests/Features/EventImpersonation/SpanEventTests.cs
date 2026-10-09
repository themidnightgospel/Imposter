using System;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.EventImpersonation
{
    public class SpanEventTests
    {
        private readonly ISpanEventSutImposter _sut = new ISpanEventSutImposter();

        [Fact]
        public void GivenSubscribedHandler_WhenRaisedWithSpan_ShouldPassItTheSpan()
        {
            string? receivedText = null;
            _sut.Instance().TextReceived += text => receivedText = text.ToString();

            _sut.TextReceived.Raise("ab".AsSpan());

            receivedText.ShouldBe("ab");
        }

        [Fact]
        public void GivenCallback_WhenRaisedWithSpan_ShouldPassItTheSpan()
        {
            string? receivedText = null;
            _sut.TextReceived.Callback(text => receivedText = text.ToString());

            _sut.TextReceived.Raise("ab".AsSpan());

            receivedText.ShouldBe("ab");
        }

        [Fact]
        public void GivenRaisedWithSpan_WhenRaisedIsVerifiedForItsElements_ShouldCountIt()
        {
            _sut.TextReceived.Raise("ab".AsSpan());

            _sut.TextReceived.Raised(ReadOnlySpanArg<char>.Is('a', 'b'), Count.Once());
            _sut.TextReceived.Raised(ReadOnlySpanArg<char>.Is('b', 'a'), Count.Never());
        }

        [Fact]
        public void GivenRaisedWithSpan_WhenRaisedIsVerifiedForOtherElements_ShouldThrow()
        {
            _sut.TextReceived.Raise("ab".AsSpan());

            Should.Throw<VerificationFailedException>(() =>
                _sut.TextReceived.Raised(ReadOnlySpanArg<char>.Is('x'), Count.Once())
            );
        }

        [Fact]
        public void GivenSpanChangedAfterRaise_WhenRaisedIsVerified_ShouldMatchTheElementsItWasRaisedWith()
        {
            var buffer = new byte[] { 1, 2 };
            _sut.BufferFilled.Raise(this, buffer);

            buffer[0] = 9;

            _sut.BufferFilled.Raised(Arg<object?>.Is(this), SpanArg<byte>.Is(1, 2), Count.Once());
        }

        [Fact]
        public void GivenHandlerThatWritesToTheSpan_WhenRaised_ShouldWriteToTheRaisersMemory()
        {
            var buffer = new byte[2];
            _sut.Instance().BufferFilled += (_, span) => span[0] = 7;

            _sut.BufferFilled.Raise(this, buffer);

            buffer[0].ShouldBe((byte)7);
        }

        [Fact]
        public void GivenSubscribedHandler_WhenRaisedWithSpan_ShouldCountTheHandlerInvocation()
        {
            BufferFilledHandler handler = (_, _) => { };
            _sut.Instance().BufferFilled += handler;

            _sut.BufferFilled.Raise(this, new byte[1]);

            _sut.BufferFilled.HandlerInvoked(Arg<BufferFilledHandler>.Is(handler), Count.Once());
        }

        [Fact]
        public void GivenClassEventUsingBaseImplementation_WhenTheClassRaisesIt_ShouldPassTheHandlerTheSpan()
        {
            var imposter = new SpanEventClassImposter();
            imposter.TextReceived.UseBaseImplementation();
            string? receivedText = null;
            var instance = imposter.Instance();
            instance.TextReceived += text => receivedText = text.ToString();

            instance.Receive("ab".AsSpan());

            receivedText.ShouldBe("ab");
        }
    }
}
