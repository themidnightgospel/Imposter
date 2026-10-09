using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.MethodImpersonation;

[assembly: GenerateImposter(typeof(ISpanRefOutParameterSut))]

namespace Imposter.Tests.Features.MethodImpersonation
{
    public interface ISpanRefOutParameterSut
    {
        bool TryRead(out ReadOnlySpan<byte> data);

        int Advance(ref ReadOnlySpan<byte> buffer);

        void Fill(ref Span<byte> buffer);

        bool TryGet<T>(out ReadOnlySpan<T> items);
    }
}
