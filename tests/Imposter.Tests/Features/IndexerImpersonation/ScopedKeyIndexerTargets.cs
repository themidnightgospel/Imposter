#if USE_CSHARP14
using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.IndexerImpersonation;

[assembly: GenerateImposter(typeof(IScopedKeyIndexerSut))]

namespace Imposter.Tests.Features.IndexerImpersonation
{
    public interface IScopedKeyIndexerSut
    {
        ReadOnlySpan<char> this[scoped ReadOnlySpan<char> key] { get; set; }

        Span<int> this[params ReadOnlySpan<int> keys] { get; set; }
    }
}
#endif
