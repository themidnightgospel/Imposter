using System;
using Imposter.Abstractions;

namespace Imposter.Tests.Features.ClassImposter
{
    [GenerateImposter(typeof(AbstractTelemetryService))]
    public abstract class AbstractTelemetryService
    {
        protected AbstractTelemetryService()
        {
        }

        public abstract int Compute(int value);

        public virtual string Name { get; set; } = "abstract";

        public abstract int this[int index] { get; set; }

        public abstract event EventHandler? StreamAdvanced;
    }
}
