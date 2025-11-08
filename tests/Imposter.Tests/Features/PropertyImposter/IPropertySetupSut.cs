using Imposter.Abstractions;

namespace Imposter.Tests.Features.PropertyImposter
{
    [GenerateImposter(typeof(IPropertySetupSut))]
    public interface IPropertySetupSut
    {
        int Age { get; set; }

        int Name { get; }

        int LastName { set; }
    }
}