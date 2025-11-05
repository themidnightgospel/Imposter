using Imposter.Abstractions;

namespace Imposter.CodeGenerator.Tests.Features.IndexerImposter
{
    [GenerateImposter(typeof(IGetterOnlyIndexerSetupSut))]
    public interface IGetterOnlyIndexerSetupSut
    {
        int this[int key, string name] { get; }
    }
}