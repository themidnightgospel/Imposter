#if USE_CSHARP14
using Imposter.Abstractions;
using Imposter.Tests.Features.MethodImpersonation;

[assembly: GenerateImposter(typeof(IRefReadOnlyParameterSut))]
[assembly: GenerateImposter(typeof(RefReadOnlyParameterClass))]
[assembly: GenerateImposter(typeof(RefReadOnlyConstructorClass))]

namespace Imposter.Tests.Features.MethodImpersonation
{
    public interface IRefReadOnlyParameterSut
    {
        int Read(ref readonly int key);

        bool Accepts<T>(ref readonly T value);
    }

    public class RefReadOnlyParameterClass
    {
        public virtual int Read(ref readonly int key) => key * 10;

        public int InvokeProtectedRead(int key) => ProtectedRead(in key);

        protected virtual int ProtectedRead(ref readonly int key) => key + 1;
    }

    public class RefReadOnlyConstructorClass
    {
        public RefReadOnlyConstructorClass(ref readonly int seed)
        {
            Seed = seed;
        }

        public int Seed { get; }

        public virtual int Read() => Seed;
    }
}
#endif
