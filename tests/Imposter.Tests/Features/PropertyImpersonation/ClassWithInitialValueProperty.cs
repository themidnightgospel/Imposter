using Imposter.Abstractions;
using Imposter.Tests.Features.PropertyImpersonation;

[assembly: GenerateImposter(typeof(ClassWithInitialValueProperty))]

namespace Imposter.Tests.Features.PropertyImpersonation
{
    public class ClassWithInitialValueProperty
    {
        public virtual int A { get; set; } = 11;
    }
}
