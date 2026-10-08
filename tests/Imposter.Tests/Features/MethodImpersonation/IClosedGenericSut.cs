using Imposter.Abstractions;
using Imposter.Tests.Features.MethodImpersonation;

[assembly: GenerateImposter(typeof(IClosedGenericSut<int, string>))]

namespace Imposter.Tests.Features.MethodImpersonation
{
    // Registered closed on purpose so the closed-target generation path keeps compiling;
    // IMP006 recommends the open registration for real code.
#pragma warning disable IMP006
    interface IClosedGenericSut<TInput, TOutput>
#pragma warning restore IMP006
    {
        TOutput GenericMethod(TInput age);
    }
}
