using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.MethodImpersonation;

[assembly: GenerateImposter(typeof(ISpanReturnSut))]
[assembly: GenerateImposter(typeof(SpanReturnClass))]

namespace Imposter.Tests.Features.MethodImpersonation
{
    public interface ISpanReturnSut
    {
        Span<byte> Rent(int size);

        ReadOnlySpan<char> Trim(ReadOnlySpan<char> text);

        ReadOnlySpan<T> Repeat<T>(T item, int count);
    }

    public class SpanReturnClass
    {
        public virtual ReadOnlySpan<char> Name() => "base".AsSpan();
    }
}
