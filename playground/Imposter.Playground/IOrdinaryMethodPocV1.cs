using Imposter.Abstractions;
using Imposter.Playground;

[assembly: GenerateImposter(typeof(IOrdinaryMethodPocV1))]

namespace Imposter.Playground;

public interface IOrdinaryMethodPocV1
{
    int Add(int a, int b);
}