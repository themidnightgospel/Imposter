using Imposter.Abstractions;
using Imposter.Tests.Features.IndexerImposter;

[assembly:GenerateImposter(typeof(ISetterOnlyIndexerSetupSut))]

namespace Imposter.Tests.Features.IndexerImposter
{
    public interface ISetterOnlyIndexerSetupSut
    {
        int this[int key] { set; }
    }
}
