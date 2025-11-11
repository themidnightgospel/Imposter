using Imposter.Abstractions;
using Imposter.Playground;

[assembly: GenerateImposter(typeof(IGenericMethodPocV1))]

namespace Imposter.Playground
{
    public interface IGenericMethodPocV1
    {
        TResult GenericAllRefKind<TOut, TRef, TIn, TParams, TResult>(
            out TOut outValue,
            ref TRef refValue,
            in TIn inValue,
            params TParams[] paramsValues);
    }
}
