using System.Collections.Generic;
using Imposter.Abstractions;
using Imposter.Tests.Features.MethodImpersonation;

[assembly: GenerateImposter(typeof(IMyCollectionSut))]

namespace Imposter.Tests.Features.MethodImpersonation
{
    public interface IMyCollectionSut : IEnumerable<string>
    {
        void Add(string item);

        int Count { get; }
    }
}
