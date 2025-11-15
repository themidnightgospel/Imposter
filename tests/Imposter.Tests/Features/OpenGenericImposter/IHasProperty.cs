using System.Collections.Generic;
using Imposter.Abstractions;
using Imposter.Tests.Features.OpenGenericImposter;

[assembly: GenerateImposter(typeof(IHasProperty<>))]

namespace Imposter.Tests.Features.OpenGenericImposter
{
    public interface IHasProperty<T>
        where T : class
    {
        T ReadOnlyValue { get; }

        T Value { get; set; }

        int Count { get; }

        IEnumerable<T> Items { get; }
    }
}
