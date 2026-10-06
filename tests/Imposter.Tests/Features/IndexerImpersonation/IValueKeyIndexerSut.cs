using Imposter.Abstractions;
using Imposter.Tests.Features.IndexerImpersonation;

[assembly: GenerateImposter(typeof(IValueKeyIndexerSut))]

namespace Imposter.Tests.Features.IndexerImpersonation
{
    public interface IValueKeyIndexerSut
    {
        int this[IndexerKey key] { get; set; }
    }

    // Value equality through Equals/GetHashCode only, without an == operator.
    public sealed class IndexerKey
    {
        public IndexerKey(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public override bool Equals(object? obj) => obj is IndexerKey other && other.Value == Value;

        public override int GetHashCode() => Value;
    }
}
