using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.Docs.Methods.Overview;

[assembly: GenerateImposter(typeof(IBufferService))]

namespace Imposter.Tests.Features.Docs.Methods.Overview
{
    public interface IBufferService
    {
        Span<byte> Rent(int size);
    }
}
