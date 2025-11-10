using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.ClassImposter.Suts;

[assembly: GenerateImposter(typeof(MultiConstructorClass))]

namespace Imposter.Tests.Features.ClassImposter.Suts
{
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