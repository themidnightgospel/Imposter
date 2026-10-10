using Imposter.Abstractions;
using Imposter.Tests.Features.IndexerImpersonation;

[assembly: GenerateImposter(typeof(IRefStructIndexerSut))]
[assembly: GenerateImposter(typeof(RefStructIndexerClass))]

namespace Imposter.Tests.Features.IndexerImpersonation
{
    public ref struct Marker
    {
        public int Position;

        public Marker(int position) => Position = position;
    }

    public interface IRefStructIndexerSut
    {
        Marker this[int index] { get; set; }
    }

    public class RefStructIndexerClass
    {
        public int LastSet { get; private set; }

        public virtual Marker this[int index]
        {
            get => new Marker(index * 10);
            set => LastSet = value.Position;
        }
    }
}
