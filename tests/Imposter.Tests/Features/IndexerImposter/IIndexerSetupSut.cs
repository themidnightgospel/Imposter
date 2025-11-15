using Imposter.Abstractions;
using Imposter.Tests.Features.IndexerImposter;

[assembly: GenerateImposter(typeof(IIndexerSetupSut))]

namespace Imposter.Tests.Features.IndexerImposter
{
    public interface IIndexerSetupSut
    {
        int this[int key1, string key2, object key3] { get; set; }

        int this[int key1] { get; set; }
    }
}
