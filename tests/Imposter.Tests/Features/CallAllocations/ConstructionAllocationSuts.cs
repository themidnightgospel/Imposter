using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.CallAllocations;

[assembly: GenerateImposter(typeof(IPropertyAllocationSut))]
[assembly: GenerateImposter(typeof(IEventAllocationSut))]
[assembly: GenerateImposter(typeof(IIndexerAllocationSut))]

namespace Imposter.Tests.Features.CallAllocations
{
    public interface IPropertyAllocationSut
    {
        int Value { get; set; }
    }

    public interface IEventAllocationSut
    {
        event EventHandler Changed;
    }

    public interface IIndexerAllocationSut
    {
        int this[int key] { get; set; }
    }
}
