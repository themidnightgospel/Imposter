using Imposter.Abstractions;
using Imposter.Tests.Features.IndexerImpersonation;

[assembly: GenerateImposter(typeof(IInParameterIndexerSut))]

namespace Imposter.Tests.Features.IndexerImpersonation
{
    public interface IInParameterIndexerSut
    {
        int this[in int key] { get; set; }
    }
}
