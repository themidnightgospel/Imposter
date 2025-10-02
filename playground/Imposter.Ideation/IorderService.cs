using Imposter.Abstractions;

namespace Imposter.Ideation;

[GenerateImposter(typeof(ISutWithGenericMethod))]
public interface ISutWithGenericMethod
{
    TOut ConvertTo<TIn, TOut>(TIn input);
}