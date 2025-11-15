using Imposter.Abstractions;
using Imposter.Tests.Features.MethodImposter;

[assembly: GenerateImposter(typeof(IClosedGenericSut<int, string>))]

namespace Imposter.Tests.Features.MethodImposter
{
    interface IClosedGenericSut<TInput, TOutput>
    {
        TOutput GenericMethod(TInput age);
    }
}
