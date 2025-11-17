using Imposter.Abstractions;
using Imposter.Tests.Features.PropertyImpersonation;

[assembly: GenerateImposter(typeof(ClassWithInitialValueStringProperty))]

namespace Imposter.Tests.Features.PropertyImpersonation
{
    public class ClassWithInitialValueStringProperty
    {
        public virtual string S { get; set; } = "hello";
    }
}
