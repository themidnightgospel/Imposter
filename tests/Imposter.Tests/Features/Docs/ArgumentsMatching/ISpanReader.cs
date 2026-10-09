using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.Docs.ArgumentsMatching;

[assembly: GenerateImposter(typeof(ISpanReader))]

namespace Imposter.Tests.Features.Docs.ArgumentsMatching
{
    public interface ISpanReader
    {
        bool TryRead(out ReadOnlySpan<byte> data);
    }
}
