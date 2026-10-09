using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.EventImpersonation;

[assembly: GenerateImposter(typeof(ISpanEventSut))]
[assembly: GenerateImposter(typeof(SpanEventClass))]

namespace Imposter.Tests.Features.EventImpersonation
{
    public delegate void TextReceivedHandler(ReadOnlySpan<char> text);

    public delegate void BufferFilledHandler(object? sender, Span<byte> buffer);

    public interface ISpanEventSut
    {
        event TextReceivedHandler TextReceived;

        event BufferFilledHandler BufferFilled;
    }

    public class SpanEventClass
    {
        public virtual event TextReceivedHandler? TextReceived;

        public void Receive(ReadOnlySpan<char> text) => TextReceived?.Invoke(text);
    }
}
