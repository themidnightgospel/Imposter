#if USE_CSHARP14
using Imposter.Abstractions;
using Imposter.Tests.Features.IndexerImpersonation;

[assembly: GenerateImposter(typeof(IRefReadOnlyIndexerSut))]
[assembly: GenerateImposter(typeof(RefReadOnlyIndexerClass))]

namespace Imposter.Tests.Features.IndexerImpersonation
{
    public interface IRefReadOnlyIndexerSut
    {
        int this[ref readonly int key] { get; set; }
    }

    public class RefReadOnlyIndexerClass
    {
        public (int Key, int Value)? LastSet { get; private set; }

        public virtual int this[ref readonly int key]
        {
            get => key * 10;
            set => LastSet = (key, value);
        }
    }
}
#endif
