using Imposter.Abstractions;

namespace Imposter.CodeGenerator.Tests.Setup.Methods.Generics;

[GenerateImposter(typeof(ISutWithGenericMethod))]
public interface ISutWithGenericMethod
{
    TOut ConvertTo<TIn, TOut>(TIn input);
}