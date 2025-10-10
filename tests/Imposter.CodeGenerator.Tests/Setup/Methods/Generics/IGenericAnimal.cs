using System.Collections.Generic;
using Imposter.Abstractions;

namespace Imposter.CodeGenerator.Tests.Setup.Methods.Generics;

[GenerateImposter(typeof(IGenericAnimal))]
public interface IGenericAnimal
{
    TResult Roar<
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

    TResult Chirp<
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
    
    TResult Meow<
        TOutParam,
        TInParam,
        TRefParam,
        TResult,
        TParamsParam
    >(
        int age,
        out List<TOutParam> outParam,
        in List<TInParam> inParam,
        ref Dictionary<int, TRefParam> refParam,
        TParamsParam[] paramsParam
    );

    void Bark(string name);
    
    int Sum(int left, int right);
}