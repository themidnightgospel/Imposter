using Imposter.Abstractions;

namespace Imposter.Ideation;

[GenerateImposter(typeof(ISutWithGenericMethod))]
public interface ISutWithGenericMethod
{
    int Print<TIn>(TIn input, int age);
}