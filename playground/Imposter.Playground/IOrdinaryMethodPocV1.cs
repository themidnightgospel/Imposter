using Imposter.Abstractions;

namespace Imposter.Playground;

[GenerateImposter(typeof(IOrdinaryMethodPocV1))]
public interface IOrdinaryMethodPocV1
{
    int Add(int a, int b);
}