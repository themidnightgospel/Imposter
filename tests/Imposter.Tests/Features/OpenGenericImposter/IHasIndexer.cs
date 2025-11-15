using Imposter.Abstractions;
using Imposter.Tests.Features.OpenGenericImposter;

[assembly: GenerateImposter(typeof(IHasIndexer<>))]

namespace Imposter.Tests.Features.OpenGenericImposter
{
    public interface IHasIndexer<T>
        where T : class
    {
        T this[int index] { get; set; }

        int this[T key] { get; set; }

        T this[T key, int index] { get; set; }
    }
}
