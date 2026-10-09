using Imposter.Abstractions;
using Imposter.Tests.Features.OpenGenericImposter;

[assembly: GenerateImposter(typeof(IHaveTExceptionTypeParameter<>))]

namespace Imposter.Tests.Features.OpenGenericImposter
{
    // Its type parameter has the name the builders' Throws<TException>() would otherwise declare.
    public interface IHaveTExceptionTypeParameter<TException>
    {
        TException Current { get; }

        TException this[int index] { get; }

        TException Get();
    }
}
