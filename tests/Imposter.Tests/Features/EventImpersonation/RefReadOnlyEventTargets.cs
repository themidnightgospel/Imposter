#if USE_CSHARP14
using System.Threading.Tasks;
using Imposter.Abstractions;
using Imposter.Tests.Features.EventImpersonation;

[assembly: GenerateImposter(typeof(IRefReadOnlyEventSut))]

namespace Imposter.Tests.Features.EventImpersonation
{
    public delegate void RefReadOnlyHandler(ref readonly int value);

    public delegate Task RefReadOnlyTaskHandler(ref readonly int value);

    public interface IRefReadOnlyEventSut
    {
        event RefReadOnlyHandler Happened;

        event RefReadOnlyTaskHandler TaskHappened;
    }
}
#endif
