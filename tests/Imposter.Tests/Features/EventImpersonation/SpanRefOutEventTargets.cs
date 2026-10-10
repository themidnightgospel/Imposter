using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.EventImpersonation;

[assembly: GenerateImposter(typeof(ISpanRefOutEventSut))]

namespace Imposter.Tests.Features.EventImpersonation
{
    public delegate void DataReadHandler(ref ReadOnlySpan<byte> data);

    public delegate void BufferRentedHandler(out Span<byte> buffer);

    public interface ISpanRefOutEventSut
    {
        event DataReadHandler DataRead;

        event BufferRentedHandler BufferRented;
    }
}
