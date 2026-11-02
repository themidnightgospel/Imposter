using System;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Imposter.Tests.Features.MethodImpersonation;

[assembly: GenerateImposter(typeof(IAsyncCallbackSut))]

namespace Imposter.Tests.Features.MethodImpersonation
{
    public interface IAsyncCallbackSut
    {
        Task RunAsync(int value);

        Task<int> GetAsync(int value);

        ValueTask RunValueAsync(int value);

        ValueTask<int> GetValueAsync(int value);

        Task<int> CountAsync(ReadOnlySpan<char> text);
    }
}
