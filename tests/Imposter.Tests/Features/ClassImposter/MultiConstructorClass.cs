using System;
using Imposter.Abstractions;

namespace Imposter.Tests.Features.ClassImposter
{
    [GenerateImposter(typeof(MultiConstructorClass))]
    public class MultiConstructorClass
    {
        public string CtorSignature { get; }
        public int Seed { get; }

        public MultiConstructorClass()
        {
            CtorSignature = "default";
        }

        public MultiConstructorClass(int value, string label)
        {
            Seed = value;
            CtorSignature = $"value:{value}/label:{label}";
        }

        internal MultiConstructorClass(Guid correlationId)
        {
            CtorSignature = $"guid:{correlationId}";
        }

        protected MultiConstructorClass(bool enabled)
        {
            CtorSignature = $"flag:{enabled}";
        }

        public virtual int Calculate(int input) => Seed + input;
    }
}
