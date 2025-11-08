using Imposter.Abstractions;

namespace Imposter.Tests.Features.IndexerImposter
{
    [GenerateImposter(typeof(ISetterOnlyIndexerSetupSut))]
    public interface ISetterOnlyIndexerSetupSut
    {
        int this[int key] { set; }
    }
}
