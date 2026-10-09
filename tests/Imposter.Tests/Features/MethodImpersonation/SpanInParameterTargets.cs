using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.MethodImpersonation;

[assembly: GenerateImposter(typeof(ISpanInParameterSut))]

#if USE_CSHARP14
[assembly: GenerateImposter(typeof(ISpanRefReadOnlyParameterSut))]

#endif

namespace Imposter.Tests.Features.MethodImpersonation
{
    public interface ISpanInParameterSut
    {
        int Count(in ReadOnlySpan<byte> data);

        int Sum<T>(in ReadOnlySpan<T> items);
    }

#if USE_CSHARP14
    public interface ISpanRefReadOnlyParameterSut
    {
        int Count(ref readonly ReadOnlySpan<byte> data);
    }
#endif
}
