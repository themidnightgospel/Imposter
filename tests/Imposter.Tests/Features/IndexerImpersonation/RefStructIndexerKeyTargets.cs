using Imposter.Abstractions;
using Imposter.Tests.Features.IndexerImpersonation;

[assembly: GenerateImposter(typeof(IRefStructIndexerKeySut))]
[assembly: GenerateImposter(typeof(RefStructIndexerKeyClass))]

namespace Imposter.Tests.Features.IndexerImpersonation
{
    public ref struct Locator
    {
        public int Offset;

        public Locator(int offset) => Offset = offset;
    }

    public interface IRefStructIndexerKeySut
    {
        int this[int row, Locator locator] { get; set; }

        string this[Locator locator] { get; set; }
    }

    public class RefStructIndexerKeyClass
    {
        public int LastSet { get; private set; }

        public virtual int this[int row, Locator locator]
        {
            get => row * 10 + locator.Offset;
            set => LastSet = value + locator.Offset;
        }
    }
}
