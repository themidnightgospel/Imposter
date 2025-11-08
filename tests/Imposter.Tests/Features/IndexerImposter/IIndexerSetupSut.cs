using Imposter.Abstractions;

namespace Imposter.Tests.Features.IndexerImposter
{
    [GenerateImposter(typeof(IIndexerSetupSut))]
    public interface IIndexerSetupSut
    {
        int this[int key1, string key2, object key3] { get; set; }
        
        int this[int key1] { get; set; }
    }
}
