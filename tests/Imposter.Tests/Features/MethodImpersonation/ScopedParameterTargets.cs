#if USE_CSHARP14
using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.MethodImpersonation;

[assembly: GenerateImposter(typeof(IScopedParameterSut))]
[assembly: GenerateImposter(typeof(ScopedParameterClass))]

namespace Imposter.Tests.Features.MethodImpersonation
{
    public interface IScopedParameterSut
    {
        ReadOnlySpan<char> Name(scoped ReadOnlySpan<char> text);

        Span<int> Slice(scoped ref int start);

        int Copy(ref Span<byte> target, scoped ReadOnlySpan<byte> source);

        ReadOnlySpan<int> First(params ReadOnlySpan<int> values);
    }

    public class ScopedParameterClass
    {
        public virtual ReadOnlySpan<char> Name(scoped ReadOnlySpan<char> text) => "base".AsSpan();
    }
}
#endif
