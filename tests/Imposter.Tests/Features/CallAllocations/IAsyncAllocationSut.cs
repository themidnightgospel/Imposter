using System.Threading.Tasks;
using Imposter.Abstractions;
using Imposter.Tests.Features.CallAllocations;

[assembly: GenerateImposter(typeof(IAsyncAllocationSut))]

namespace Imposter.Tests.Features.CallAllocations
{
    public interface IAsyncAllocationSut
    {
        Task<int> GetAsync(int key);
        ValueTask<int> PeekAsync(int key);
        Task RunAsync();
        ValueTask RunValueAsync();
        Task<int> GetByReferenceAsync(ref int key, out int count, in int extra);
        ValueTask<dynamic> GetDynamicAsync();
    }
}
