using Imposter.Abstractions;
using Imposter.Tests.Features.OpenGenericImposter;

[assembly: GenerateImposter(typeof(IOpenGenericMethodTarget<>))]

namespace Imposter.Tests.Features.OpenGenericImposter
{
    public interface IOpenGenericMethodTarget<T>
    {
        T GetNext();

        bool TryAdd(T value);

        void Add(T item, int index);

        int CountFor(T item);

        T ResilientFetch();

        void Publish(T payload, string category, int priority);
    }
}
