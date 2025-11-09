using Imposter.Abstractions;
using Imposter.Tests.Features.IndexerImposter;

[assembly: GenerateImposter(typeof(IGetterOnlyIndexerSetupSut))]

namespace Imposter.Tests.Features.IndexerImposter
{
    public interface IGetterOnlyIndexerSetupSut
    {
        int this[int key, string name] { get; }
    }
}