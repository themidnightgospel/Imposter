using Imposter.Abstractions;
using Imposter.Tests.Features.ClassImpersonation.Suts;

[assembly: GenerateImposter(typeof(ClassWithOverriddenMethod))]
[assembly: GenerateImposter(typeof(ClassInheritingOverriddenMethod))]
[assembly: GenerateImposter(typeof(ClassWithAbstractOverride))]

namespace Imposter.Tests.Features.ClassImpersonation.Suts
{
    public class ClassWithVirtualMethodToOverride
    {
        public virtual int Get() => 1;
    }

    public class ClassWithOverriddenMethod : ClassWithVirtualMethodToOverride
    {
        public override int Get() => 2;
    }

    public class ClassInheritingOverriddenMethod : ClassWithOverriddenMethod { }

    public abstract class ClassWithAbstractOverride : ClassWithVirtualMethodToOverride
    {
        public abstract override int Get();
    }
}
