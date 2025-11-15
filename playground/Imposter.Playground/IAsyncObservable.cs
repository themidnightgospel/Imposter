using Imposter.Abstractions;
using Imposter.Playground;

[assembly: GenerateImposter(typeof(IAsyncObservable<>))]

namespace Imposter.Playground
{
    public interface IAsyncObservable<T>
    {
        void OnNext(T item);
    }
}
