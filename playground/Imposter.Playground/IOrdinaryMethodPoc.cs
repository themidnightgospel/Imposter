using Imposter.Abstractions;

namespace Imposter.Playground;

public interface IGenericMethodPoc
{
    TResult GenericAllRefKind<TOut, TRef, TIn, TParams, TResult>(
        out TOut outValue,
        ref TRef refValue,
        in TIn inValue,
        params TParams[] paramsValues);
}