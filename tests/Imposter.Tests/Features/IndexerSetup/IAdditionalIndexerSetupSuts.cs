using Imposter.Abstractions;

namespace Imposter.CodeGenerator.Tests.Features.IndexerSetup
{
    [GenerateImposter(typeof(IGetterOnlyIndexerSetupSut))]
    public interface IGetterOnlyIndexerSetupSut
    {
        int this[int key, string name] { get; }
    }

    [GenerateImposter(typeof(ISetterOnlyIndexerSetupSut))]
    public interface ISetterOnlyIndexerSetupSut
    {
        int this[int key] { set; }
    }
}
