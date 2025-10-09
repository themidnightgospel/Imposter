using Imposter.Abstractions;

namespace Imposter.CodeGenerator.Tests.Setup.Methods.Generics;

[GenerateImposter(typeof(ISutWithGenericMethod))]
public interface ISutWithGenericMethod
{
    TResult Print<
        TOrdinaryParam,
        TOutParam,
        TInParam,
        TRefParam,
        TResult,
        TParamsParam
    >(
        TOrdinaryParam ordinaryParam,
        out TOutParam outParam,
        in TInParam inParam,
        ref TRefParam refParam,
        TParamsParam[] paramsParam
    );

    TResult Hello<
        TOutParam,
        TInParam,
        TRefParam,
        TResult,
        TParamsParam
    >(
        int age,
        string name,
        out TOutParam outParam,
        in TInParam inParam,
        ref TRefParam refParam,
        TParamsParam[] paramsParam
    );

    void Bark(string name);
    
    int Sum(int left, int right);
}