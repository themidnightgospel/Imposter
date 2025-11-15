using Imposter.Abstractions;
using Imposter.Tests.Features.PropertyImposter;

[assembly: GenerateImposter(typeof(ClassWithInitialValueStringProperty))]

namespace Imposter.Tests.Features.PropertyImposter
{
    public class ClassWithInitialValueStringProperty
    {
        public virtual string S { get; set; } = "hello";
    }
}
