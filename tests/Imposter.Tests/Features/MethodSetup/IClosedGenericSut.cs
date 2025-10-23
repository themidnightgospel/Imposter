using Imposter.Abstractions;

namespace Imposter.CodeGenerator.Tests.Features.MethodSetup;

[GenerateImposter(typeof(IClosedGenericSut<int,string>))]
interface IClosedGenericSut<TInput, TOutput>
{
    TOutput GenericMethod(TInput age);
}