using Imposter.Abstractions;
using Imposter.Playground;

[assembly: GenerateImposter(typeof(IPropertySetupSut))]
[assembly: GenerateImposter(typeof(IIndexerSetupPocSut))]

namespace Imposter.Playground
{
    public interface IPropertySetupSut
    {
        int Age { get; set; }

        int Name { get; }

        int LastName { set; }
    }

    public interface IIndexerSetupPocSut
    {
        string Indexer(int value1, string value2, object value3);
    }
}