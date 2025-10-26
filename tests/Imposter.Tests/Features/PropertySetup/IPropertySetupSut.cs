using Imposter.Abstractions;

namespace Imposter.CodeGenerator.Tests.Features.PropertySetup;

[GenerateImposter(typeof(IPropertySetupSut))]
public interface IPropertySetupSut
{
    int Age { get; set; }

    int Name { get; }

    int LastName { set; }
}