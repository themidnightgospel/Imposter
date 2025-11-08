using Imposter.Abstractions;

namespace Imposter.Playground;

[GenerateImposter(typeof(IGenericMethodPocV1))]
public interface IGenericMethodPocV1
{
    TResult GenericAllRefKind<TOut, TRef, TIn, TParams, TResult>(
        out TOut outValue,
        ref TRef refValue,
        in TIn inValue,
        params TParams[] paramsValues);
}

