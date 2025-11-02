using Imposter.Abstractions;

namespace Imposter.Playground;

[GenerateImposter(typeof(IPropertySetupSut))]
public interface IPropertySetupSut
{
    int Age { get; set; }

    int Name { get; }

    int LastName { set; }
}

[GenerateImposter(typeof(IIndexerSetupPocSut))]
public interface IIndexerSetupPocSut
{
    string Indexer(int value1, string value2, object value3);
}
