using Imposter.Abstractions;
using Imposter.Tests.Features.PropertyImposter;

[assembly: GenerateImposter(typeof(IPropertySetupSut))]

namespace Imposter.Tests.Features.PropertyImposter
{
    public interface IPropertySetupSut
    {
        int Age { get; set; }

        int Name { get; }

        int LastName { set; }
    }
}
