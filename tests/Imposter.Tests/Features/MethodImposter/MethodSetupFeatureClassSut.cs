using Imposter.Abstractions;

namespace Imposter.Tests.Features.MethodImposter
{
    [GenerateImposter(typeof(MethodSetupFeatureClassSut))]
    public class MethodSetupFeatureClassSut
    {
        internal int IntNoParams()
        {
            return 1;
        }

        internal virtual int IntSingleParam(int age)
        {
            return 1;
        }

    }
}