using Imposter.Abstractions;
using Imposter.Tests.Features.ClassImpersonation.Suts;

[assembly: GenerateImposter(typeof(ClassWithRestrictedAccessors))]

namespace Imposter.Tests.Features.ClassImpersonation.Suts
{
    public class ClassWithRestrictedAccessors
    {
        public virtual int PrivateSetter { get; private set; }

        public virtual int ProtectedSetter { get; protected set; }

        public virtual int ProtectedGetter { protected get; set; }

        public virtual int this[int key]
        {
            get => key;
            private set { }
        }

        public void ResetProtectedSetter() => ProtectedSetter = 0;

        public int ReadProtectedGetter() => ProtectedGetter;
    }
}
