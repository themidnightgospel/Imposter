using Imposter.Abstractions;
using Imposter.Tests.Features.ClassImpersonation.Suts;

[assembly: GenerateImposter(typeof(ClassWithConstructorDefaults))]

namespace Imposter.Tests.Features.ClassImpersonation.Suts
{
    public enum ConstructorDefaultLevel
    {
        Below = -1,
        Low = 1,
        High = 2,
    }

    public class ClassWithConstructorDefaults
    {
        public ClassWithConstructorDefaults(
            decimal? amount = 1.5m,
            ConstructorDefaultLevel? level = ConstructorDefaultLevel.High,
            double ratio = double.NaN,
            float limit = float.NegativeInfinity,
            ConstructorDefaultLevel floor = ConstructorDefaultLevel.Below,
            ConstructorDefaultLevel? ceiling = ConstructorDefaultLevel.Below
        )
        {
            Amount = amount;
            Level = level;
            Ratio = ratio;
            Limit = limit;
            Floor = floor;
            Ceiling = ceiling;
        }

        public decimal? Amount { get; }

        public ConstructorDefaultLevel? Level { get; }

        public double Ratio { get; }

        public float Limit { get; }

        public ConstructorDefaultLevel Floor { get; }

        public ConstructorDefaultLevel? Ceiling { get; }

        public virtual int Get() => 0;
    }
}
