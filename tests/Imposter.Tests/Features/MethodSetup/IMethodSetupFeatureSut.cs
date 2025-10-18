using System.Text.RegularExpressions;
using Imposter.Abstractions;

namespace Imposter.CodeGenerator.Tests.Features.MethodSetup
{
    [GenerateImposter(typeof(IMethodSetupFeatureSut))]
    public interface IMethodSetupFeatureSut
    {
        void Void_NoParams();

        int Int_NoParams();

        int Int_SingleParam(int age);

        int Int_Params(int age, string name, Regex regex);

        int Int_OutParam(out int outValue);

        int Int_RefParam(ref int refValue);

        int Int_ParamsParam(params string[] paramsStrings);

        int Int_InParam(in string inStringValue);

        int Int_AllRefKinds(out int value, ref int refValue, in int inValue, string valueAsString, params string[] paramsStrings);

        void Generic_SingleParam<TValue>(TValue value);

        TResult Generic_OutParam<TValue, TResult>(out TValue value);

        TResult Generic_RefParam<TValue, TResult>(ref TValue value);

        TResult Generic_ParamsParam<TValue, TResult>(params TValue[] value);

        TResult Generic_AllRefKind<TOut, TRef, TIn, TParams, TResult>(
            out TOut outValue,
            ref TRef refValue,
            in TIn inValue,
            params TParams[] paramsValues);
    }

    public interface IAnimal
    {
        string Name { get; }
    }

    public class Animal : IAnimal
    {
        public Animal(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }

    public interface ICat : IAnimal
    {
    }

    public class Cat : Animal, ICat
    {
        public Cat(string name)
            : base(name)
        {
        }
    }

    public interface IDog : IAnimal
    {
    }

    public class Dog : Animal, IDog
    {
        public Dog(string name)
            : base(name)
        {
        }
    }

    public interface IGermanShepherd : IDog
    {
    }

    public class GermanShepherd : Dog, IGermanShepherd
    {
        public GermanShepherd(string name)
            : base(name)
        {
        }
    }
}