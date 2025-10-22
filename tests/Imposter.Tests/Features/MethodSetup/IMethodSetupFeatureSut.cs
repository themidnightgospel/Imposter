using System.Text.RegularExpressions;
using Imposter.Abstractions;

namespace Imposter.CodeGenerator.Tests.Features.MethodSetup
{
    [GenerateImposter(typeof(IMethodSetupFeatureSut))]
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

        int IntAllRefKinds(out int value, ref int refValue, in int inValue, string valueAsString, params string[] paramsStrings);

        void GenericSingleParam<TValue>(TValue value);

        TResult GenericOutParam<TValue, TResult>(out TValue value);

        TResult GenericRefParam<TValue, TResult>(ref TValue value);

        TResult GenericParamsParam<TValue, TResult>(params TValue[] value);

        TResult GenericAllRefKind<TOut, TRef, TIn, TParams, TResult>(
            out TOut outValue,
            ref TRef refValue,
            in TIn inValue,
            params TParams[] paramsValues);
    }


    public interface IMammal
    {
        
    }

    public interface IAnimal : IMammal
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

    public class Tiger : Cat
    {
        public Tiger(string name) : base(name)
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