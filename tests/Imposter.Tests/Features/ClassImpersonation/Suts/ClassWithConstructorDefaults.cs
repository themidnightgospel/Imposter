using Imposter.Abstractions;
using Imposter.Tests.Features.ClassImpersonation.Suts;

[assembly: GenerateImposter(typeof(ClassWithConstructorDefaults))]

namespace Imposter.Tests.Features.ClassImpersonation.Suts
{
    public enum ConstructorDefaultLevel
    {
        Low = 1,
        High = 2,
    }

    public class ClassWithConstructorDefaults
    {
        public ClassWithConstructorDefaults(
            decimal? amount = 1.5m,
            ConstructorDefaultLevel? level = ConstructorDefaultLevel.High,
            double ratio = double.NaN,
            float limit = float.NegativeInfinity
        )
        {
            Amount = amount;
            Level = level;
            Ratio = ratio;
            Limit = limit;
        }

        public decimal? Amount { get; }

        public ConstructorDefaultLevel? Level { get; }

        public double Ratio { get; }

        public float Limit { get; }

        public virtual int Get() => 0;
    }
}
