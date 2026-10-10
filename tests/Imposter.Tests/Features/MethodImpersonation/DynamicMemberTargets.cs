using Imposter.Abstractions;
using Imposter.Tests.Features.MethodImpersonation;

[assembly: GenerateImposter(typeof(IDynamicMemberSut))]
[assembly: GenerateImposter(typeof(DynamicMemberClass))]

namespace Imposter.Tests.Features.MethodImpersonation
{
    public interface IDynamicMemberSut
    {
        dynamic Echo(dynamic value);

        void Use<T>(T first, dynamic second);
    }

    public class DynamicMemberClass
    {
        public virtual dynamic Describe(dynamic value) => "base " + value;

        public virtual dynamic? Value { get; set; }

        public virtual dynamic this[dynamic key]
        {
            get => "key " + key;
            set { }
        }
    }
}
