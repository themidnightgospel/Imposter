using Imposter.Abstractions;
using Imposter.Tests.Features.IndexerImpersonation;

[assembly: GenerateImposter(typeof(IGetterOnlyIndexerSetupSut))]

namespace Imposter.Tests.Features.IndexerImpersonation
{
    public interface IGetterOnlyIndexerSetupSut
    {
        int this[int key, string name] { get; }
    }
}
