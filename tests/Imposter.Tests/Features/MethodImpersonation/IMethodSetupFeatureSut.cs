using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Imposter.Tests.Features.MethodImpersonation;

[assembly: GenerateImposter(typeof(IMethodSetupFeatureSut))]

namespace Imposter.Tests.Features.MethodImpersonation
{
    public interface IMethodSetupFeatureSut
    {
        void VoidNoParams();

        int IntNoParams();

        int IntSingleParam(int age);

        int IntParams(int age, string name, Regex regex);

        int IntOutParam(out int outValue);

        int IntRefParam(ref int refValue);

        int IntParamsParam(params string[] paramsStrings);

        int IntInParam(in string inStringValue);

        int IntAllRefKinds(
            out int value,
            ref int refValue,
            in int inValue,
            string valueAsString,
            params string[] paramsStrings
        );

        TValue GenericReturnType<TValue>();

        void GenericSingleParam<TValue>(TValue value);

        void GenericSingleRefParam<TValue>(ref TValue value);

        void GenericSingleOutParam<TValue>(out TValue value);

        void GenericInnerSingleParam<TValue>(List<TValue> value);

        TResult GenericOutParam<TValue, TResult>(out TValue value);

        Stack<TResult> GenericInnerOutParam<TValue, TResult>(out List<TValue> value);

        TResult GenericRefParam<TValue, TResult>(ref TValue value);

        Stack<TResult> GenericInnerRefParam<TValue, TResult>(ref List<TValue> value);

        TResult GenericParamsParam<TValue, TResult>(params TValue[] value);

        Stack<TResult> GenericInnerParamsParam<TValue, TResult>(params List<TValue>[] value);

        TResult GenericAllRefKind<TOut, TRef, TIn, TParams, TResult>(
            out TOut outValue,
            ref TRef refValue,
            in TIn inValue,
            params TParams[] paramsValues
        );

        Task<int> AsyncTaskIntNoParams();

        ValueTask<int> AsyncValueTaskIntNoParams();
    }
}
