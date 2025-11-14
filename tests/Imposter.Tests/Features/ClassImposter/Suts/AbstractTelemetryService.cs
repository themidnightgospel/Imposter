using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.ClassImposter.Suts;

[assembly: GenerateImposter(typeof(AbstractTelemetryService))]

namespace Imposter.Tests.Features.ClassImposter.Suts
{
    public abstract class AbstractTelemetryService
    {
        protected AbstractTelemetryService()
        {
        }

        public abstract int Compute(int value);
        
        public abstract int? ComputeNullable(int value);

        public virtual string Name { get; set; } = "abstract";
        
        public virtual string? NameNullable { get; set; } = "abstract";

        public abstract int this[int index] { get; set; }
        
        public abstract int? this[int index, int indexNullable] { get; set; }

        public abstract event EventHandler? StreamAdvanced;
    }
}