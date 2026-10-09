using Imposter.Abstractions;
using Imposter.Tests.Features.Docs.Methods.Overview;

[assembly: GenerateImposter(typeof(ICounterService))]

namespace Imposter.Tests.Features.Docs.Methods.Overview
{
    public interface ICounterService
    {
        int Count(int value);

        int Count(in int value);
    }
}
