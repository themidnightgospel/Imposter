using Imposter.Abstractions;
using Imposter.Tests.Features.ClassImpersonation.Suts;

[assembly: GenerateImposter(typeof(ClassWithOverriddenMethod))]
[assembly: GenerateImposter(typeof(ClassInheritingOverriddenMethod))]
[assembly: GenerateImposter(typeof(ClassWithAbstractOverride))]
[assembly: GenerateImposter(typeof(ClassWithObjectMemberOverrides))]

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

    public class ClassWithObjectMemberOverrides
    {
        public const string Text = "real ToString";

        public const int HashCode = 42;

        public override string ToString() => Text;

        public override bool Equals(object? obj) => ReferenceEquals(this, obj);

        public override int GetHashCode() => HashCode;
    }
}
