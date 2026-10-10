#if USE_CSHARP14
using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.Docs.ArgumentsMatching;

[assembly: GenerateImposter(typeof(IScopedSpanReader))]

namespace Imposter.Tests.Features.Docs.ArgumentsMatching
{
    public interface IScopedSpanReader
    {
        ReadOnlySpan<char> Trim(scoped ReadOnlySpan<char> text);
    }
}
#endif
