using Imposter.Abstractions;
using Imposter.Tests.Features.MethodImpersonation;

[assembly: GenerateImposter(typeof(MethodNameCollisionService))]

namespace Imposter.Tests.Features.MethodImpersonation
{
    public abstract class MethodNameCollisionService
    {
        public virtual int Invoke(
            int invocationBehavior,
            string methodDisplayName,
            int invocationImposter
        ) => invocationBehavior + methodDisplayName.Length + invocationImposter;

        public abstract T AdaptRef<T>(ref int value, int valueAdapted);

        public abstract T AdaptOut<T>(out int value, int valueAdapted);

        public virtual Called Verify<Called>(Called value) => value;
    }
}
