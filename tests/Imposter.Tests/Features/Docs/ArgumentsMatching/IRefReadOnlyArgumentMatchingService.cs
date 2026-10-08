#if USE_CSHARP14
using Imposter.Abstractions;
using Imposter.Tests.Features.Docs.ArgumentsMatching;

[assembly: GenerateImposter(typeof(IRefReadOnlyArgumentMatchingService))]

namespace Imposter.Tests.Features.Docs.ArgumentsMatching
{
    public interface IRefReadOnlyArgumentMatchingService
    {
        int Double(ref readonly int value);
    }
}
#endif
