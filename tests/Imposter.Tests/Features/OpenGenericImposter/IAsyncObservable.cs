using Imposter.Abstractions;
using Imposter.Tests.Features.OpenGenericImposter;

[assembly: GenerateImposter(typeof(IAsyncObservable<>))]

namespace Imposter.Tests.Features.OpenGenericImposter
{
    public interface IAsyncObservable<T>
    {
        void OnNext(T item);
    }
}
