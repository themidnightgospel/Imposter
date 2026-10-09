using System;
using System.Collections.Generic;
using Imposter.Abstractions;
using Imposter.Tests.Features.IndexerImpersonation;

[assembly: GenerateImposter(typeof(ISpanIndexerSut))]
[assembly: GenerateImposter(typeof(SpanIndexerClass))]

namespace Imposter.Tests.Features.IndexerImpersonation
{
    public interface ISpanIndexerSut
    {
        int this[ReadOnlySpan<char> key] { get; set; }

        Span<byte> this[int index] { get; set; }
    }

    public class SpanIndexerClass
    {
        private readonly Dictionary<string, int> _values = new() { ["a"] = 1 };

        public virtual int this[ReadOnlySpan<char> key]
        {
            get => _values.TryGetValue(key.ToString(), out var value) ? value : 0;
            set => _values[key.ToString()] = value;
        }

        public int ValueOf(string key) => _values[key];
    }
}
