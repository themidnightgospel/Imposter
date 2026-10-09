using System;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Imposter.Tests.Features.MethodImpersonation;

[assembly: GenerateImposter(typeof(ISpanParameterSut))]
[assembly: GenerateImposter(typeof(SpanParameterClass))]
[assembly: GenerateImposter(typeof(ISpanOverloadSut))]

namespace Imposter.Tests.Features.MethodImpersonation
{
    public interface ISpanParameterSut
    {
        int Parse(ReadOnlySpan<char> text, int start);

        void Write(Span<byte> buffer);

        bool Contains<T>(ReadOnlySpan<T> items, T item);

        Task<int> CountAsync(ReadOnlySpan<byte> data);

        void Fill<T>(Span<byte> buffer, T value);
    }

    public class SpanParameterClass
    {
        public virtual int Count(ReadOnlySpan<char> text) => text.Length;
    }

    public interface ISpanOverloadSut
    {
        int Write(Span<byte> buffer);

        int Write(ReadOnlySpan<byte> data);
    }
}
