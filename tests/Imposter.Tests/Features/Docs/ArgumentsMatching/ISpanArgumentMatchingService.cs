using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.Docs.ArgumentsMatching;

[assembly: GenerateImposter(typeof(ISpanArgumentMatchingService))]

namespace Imposter.Tests.Features.Docs.ArgumentsMatching
{
    public interface ISpanArgumentMatchingService
    {
        int Parse(ReadOnlySpan<char> text);
    }
}
