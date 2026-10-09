using System.Threading.Tasks;
using Imposter.Abstractions;
using Imposter.Tests.Features.MethodImpersonation;

[assembly: GenerateImposter(typeof(IAwaitableByReferenceSut))]

namespace Imposter.Tests.Features.MethodImpersonation
{
    public interface IAwaitableByReferenceSut
    {
        Task<int> GetAsync(out int count);

        ValueTask<int> PeekAsync(in int key);

        Task<int> NextAsync(ref int position);
    }
}
