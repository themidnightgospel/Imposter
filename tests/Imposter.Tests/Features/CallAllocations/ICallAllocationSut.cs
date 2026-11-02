using Imposter.Abstractions;
using Imposter.Tests.Features.CallAllocations;

[assembly: GenerateImposter(typeof(ICallAllocationSut))]

namespace Imposter.Tests.Features.CallAllocations
{
    public interface ICallAllocationSut
    {
        int Square(int value);

        void Log(string message);

        T Echo<T>(T value);

        int Value { get; set; }

        int this[int key] { get; set; }
    }
}
