using System;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Imposter.Tests.Features.EventImpersonation;

[assembly: GenerateImposter(typeof(IAsyncSpanEventSut))]

namespace Imposter.Tests.Features.EventImpersonation
{
    public delegate Task TextReceivedAsyncHandler(object? sender, ReadOnlySpan<char> text);

    public delegate ValueTask BufferFilledAsyncHandler(Span<byte> buffer);

    public interface IAsyncSpanEventSut
    {
        event TextReceivedAsyncHandler TextReceived;

        event BufferFilledAsyncHandler BufferFilled;
    }
}
