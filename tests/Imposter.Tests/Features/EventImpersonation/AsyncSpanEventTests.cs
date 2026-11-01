using System;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.EventImpersonation
{
    public class AsyncSpanEventTests
    {
        private readonly IAsyncSpanEventSutImposter _sut = new IAsyncSpanEventSutImposter();

        [Fact]
        public async Task GivenInSpanHandlerAndCallback_WhenRaisedAsyncWithArray_ShouldPassBothTheElements()
        {
            string? callbackText = null;
            string? handlerText = null;
            _sut.InTextReceived.Callback(
                (in ReadOnlySpan<char> text) =>
                {
                    callbackText = text.ToString();
                    return Task.CompletedTask;
                }
            );
            _sut.Instance().InTextReceived += (in ReadOnlySpan<char> text) =>
            {
                handlerText = text.ToString();
                return Task.CompletedTask;
            };

            await _sut.InTextReceived.RaiseAsync(new[] { 'a', 'b' });

            callbackText.ShouldBe("ab");
            handlerText.ShouldBe("ab");
            _sut.InTextReceived.Raised(ReadOnlySpanArg<char>.Is('a', 'b'), Count.Once());
        }

        [Fact]
        public async Task GivenSubscribedHandler_WhenRaisedAsyncWithArray_ShouldPassItASpanOverTheArray()
        {
            string? receivedText = null;
            _sut.Instance().TextReceived += (_, text) =>
            {
                receivedText = text.ToString();
                return Task.CompletedTask;
            };

            await _sut.TextReceived.RaiseAsync(this, new[] { 'a', 'b' });

            receivedText.ShouldBe("ab");
        }

        [Fact]
        public async Task GivenCallback_WhenRaisedAsyncWithArray_ShouldPassItASpanOverTheArray()
        {
            string? receivedText = null;
            _sut.TextReceived.Callback(
                (_, text) =>
                {
                    receivedText = text.ToString();
                    return Task.CompletedTask;
                }
            );

            await _sut.TextReceived.RaiseAsync(this, new[] { 'a' });

            receivedText.ShouldBe("a");
        }

        [Fact]
        public async Task GivenRaisedAsync_WhenRaisedIsVerifiedForTheElements_ShouldCountIt()
        {
            await _sut.TextReceived.RaiseAsync(this, new[] { 'a', 'b' });

            _sut.TextReceived.Raised(
                Arg<object?>.Is(this),
                ReadOnlySpanArg<char>.Is('a', 'b'),
                Count.Once()
            );
            _sut.TextReceived.Raised(
                Arg<object?>.Any(),
                ReadOnlySpanArg<char>.Is('b'),
                Count.Never()
            );
        }

        [Fact]
        public async Task GivenHandlerThatWritesToTheSpan_WhenRaisedAsync_ShouldWriteToTheRaisersArray()
        {
            var buffer = new byte[2];
            _sut.Instance().BufferFilled += span =>
            {
                span[0] = 7;
                return default;
            };

            await _sut.BufferFilled.RaiseAsync(buffer);

            buffer[0].ShouldBe((byte)7);
        }

        [Fact]
        public async Task GivenArrayChangedAfterRaiseAsync_WhenRaisedIsVerified_ShouldMatchTheElementsItWasRaisedWith()
        {
            var buffer = new byte[] { 1, 2 };
            await _sut.BufferFilled.RaiseAsync(buffer);

            buffer[0] = 9;

            _sut.BufferFilled.Raised(SpanArg<byte>.Is(1, 2), Count.Once());
        }
    }
}
