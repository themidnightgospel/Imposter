using Imposter.Abstractions;
using Imposter.Tests.Features.IndexerImpersonation;

[assembly: GenerateImposter(typeof(ISetterOnlyIndexerSetupSut))]

namespace Imposter.Tests.Features.IndexerImpersonation
{
    public interface ISetterOnlyIndexerSetupSut
    {
        int this[int key] { set; }
    }
}
