using Imposter.Abstractions;

namespace Imposter.CodeGenerator.Tests.Features.IndexerSetup
{
    [GenerateImposter(typeof(IIndexerSetupSut))]
    public interface IIndexerSetupSut
    {
        int this[int key1, string key2, object key3] { get; set; }
    }
}
