using Imposter.Abstractions;
using Imposter.Tests.Features.PropertyImposter;

[assembly: GenerateImposter(typeof(ClassWithInitialValueProperty))]

namespace Imposter.Tests.Features.PropertyImposter
{
    public class ClassWithInitialValueProperty
    {
        public virtual int A { get; set; } = 11;
    }
}
