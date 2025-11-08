using Imposter.Abstractions;

namespace Imposter.Tests.Features.MethodImposter
{
    [GenerateImposter(typeof(IClosedGenericSut<int, string>))]
    interface IClosedGenericSut<TInput, TOutput>
    {
        TOutput GenericMethod(TInput age);
    }
}