using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.PropertyImpersonation;

[assembly: GenerateImposter(typeof(ISpanPropertySut))]
[assembly: GenerateImposter(typeof(SpanPropertyClass))]

namespace Imposter.Tests.Features.PropertyImpersonation
{
    public interface ISpanPropertySut
    {
        Span<byte> Buffer { get; set; }

        ReadOnlySpan<char> Name { get; }
    }

    public class SpanPropertyClass
    {
        private byte[] _buffer = { 1, 2 };

        public virtual Span<byte> Buffer
        {
            get => _buffer;
            set => _buffer = value.ToArray();
        }

        public byte[] BaseBuffer => _buffer;
    }
}
