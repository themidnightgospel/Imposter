// using Imposter.Abstractions;

namespace Imposter.CodeGenerator.Tests.Setup;

//[GenerateImposter(typeof(IGenericSut<>))]
public interface IGenericSut<TIn>
{
    TIn ConvertValue<TOut>(TIn input);
}