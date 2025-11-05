using Imposter.Abstractions;

namespace Imposter.CodeGenerator.Tests.Features.IndexerSetup
{
    [GenerateImposter(typeof(ISetterOnlyIndexerSetupSut))]
    public interface ISetterOnlyIndexerSetupSut
    {
        int this[int key] { set; }
    }
}
