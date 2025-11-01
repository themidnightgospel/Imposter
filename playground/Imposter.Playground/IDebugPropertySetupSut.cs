using Imposter.Abstractions;

namespace Imposter.Playground;

[GenerateImposter(typeof(IPropertySetupSut))]
public interface IPropertySetupSut
{
    int Age { get; set; }

    int Name { get; }

    int LastName { set; }
}
