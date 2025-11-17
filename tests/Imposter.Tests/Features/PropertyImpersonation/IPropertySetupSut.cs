using Imposter.Abstractions;
using Imposter.Tests.Features.PropertyImpersonation;

[assembly: GenerateImposter(typeof(IPropertySetupSut))]

namespace Imposter.Tests.Features.PropertyImpersonation
{
    public interface IPropertySetupSut
    {
        int Age { get; set; }

        int Name { get; }

        int LastName { set; }
    }
}
