using System.Threading.Tasks;
using Imposter.Abstractions;

namespace Imposter.Tests.Features.MethodImposter
{
    [GenerateImposter(typeof(MethodSetupFeatureClassSut))]
    public class MethodSetupFeatureClassSut
    {
        public int VoidSideEffectCount { get; private set; }

        internal int IntNoParams()
        {
            return 1;
        }

        internal virtual int IntSingleParam(int age)
        {
            return age * 2;
        }

        internal virtual void VoidWithSideEffect(int delta)
        {
            VoidSideEffectCount += delta;
        }

        internal virtual Task<int> SumAsync(int left, int right)
        {
            return Task.FromResult(left + right);
        }

        internal virtual async ValueTask<string> BuildLabelAsync(string prefix, string suffix)
        {
            await Task.Yield();
            return $"{prefix}-{suffix}";
        }

        internal virtual int RefOutWithParams(ref int seed, out int doubled, params int[] adjustments)
        {
            var applied = 0;
        if (adjustments != null && adjustments.Length > 0)
        {
                foreach (var adjustment in adjustments)
                {
                    applied += adjustment;
                }
            }

            seed += applied;
            doubled = seed * 2;
            return seed + doubled;
        }
    }
}
