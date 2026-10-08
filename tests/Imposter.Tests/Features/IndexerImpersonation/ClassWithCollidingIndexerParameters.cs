using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.IndexerImpersonation;

[assembly: GenerateImposter(typeof(ClassWithCollidingIndexerParameters))]

namespace Imposter.Tests.Features.IndexerImpersonation
{
    public class ClassWithCollidingIndexerParameters
    {
        public int[] LastSet { get; private set; } = Array.Empty<int>();

        public virtual int this[
            int arguments,
            int baseImplementation,
            int invokedBaseImplementation,
            int matchedCallback,
            int registration,
            int getterInvocationImposter,
            int criteria
        ]
        {
            get =>
                arguments
                + baseImplementation
                + invokedBaseImplementation
                + matchedCallback
                + registration
                + getterInvocationImposter
                + criteria;
            set =>
                LastSet = new[]
                {
                    arguments,
                    baseImplementation,
                    invokedBaseImplementation,
                    matchedCallback,
                    registration,
                    getterInvocationImposter,
                    criteria,
                    value,
                };
        }
    }
}
