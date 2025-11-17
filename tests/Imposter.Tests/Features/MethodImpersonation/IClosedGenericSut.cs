using Imposter.Abstractions;
using Imposter.Tests.Features.MethodImpersonation;

[assembly: GenerateImposter(typeof(IClosedGenericSut<int, string>))]

namespace Imposter.Tests.Features.MethodImpersonation
{
    interface IClosedGenericSut<TInput, TOutput>
    {
        TOutput GenericMethod(TInput age);
    }
}
