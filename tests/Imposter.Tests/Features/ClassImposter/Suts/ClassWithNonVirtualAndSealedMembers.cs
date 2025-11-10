using Imposter.Abstractions;
using Imposter.Tests.Features.ClassImposter.Suts;

[assembly: GenerateImposter(typeof(ClassWithNonVirtualAndSealedMembers))]

namespace Imposter.Tests.Features.ClassImposter.Suts
{
    public class ClassWithNonVirtualAndSealedMembers : ClassImposterSealedOverrideBase
    {
        private string _nonVirtualProperty = "non-virtual-default";

        public int NonVirtualMethodCallCount { get; private set; }

        public int NonVirtualPropertySetCount { get; private set; }

        public int NonVirtualMethod(int value)
        {
            NonVirtualMethodCallCount++;
            return value * 2;
        }

        public string NonVirtualProperty
        {
            get => _nonVirtualProperty;
            set
            {
                _nonVirtualProperty = value;
                NonVirtualPropertySetCount++;
            }
        }
    }

    public class ClassImposterSealedOverrideBase : ClassImposterVirtualBase
    {
        public int SealedOverrideInvocationCount { get; private set; }

        public sealed override int VirtualMethod(int value)
        {
            SealedOverrideInvocationCount++;
            return base.VirtualMethod(value) + 5;
        }
    }

    public class ClassImposterVirtualBase
    {
        public virtual int VirtualMethod(int value) => value + 3;
    }
}
