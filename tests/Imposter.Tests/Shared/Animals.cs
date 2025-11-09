using System.Text.RegularExpressions;
using Imposter.Abstractions;
using Imposter.Tests.Shared;

[assembly: GenerateImposter(typeof(IIndexerSetupPoc))]

namespace Imposter.Tests.Shared
{
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

    // TODO
    public interface IIndexerSetupPoc
    {
        string IndexerMethod(int name, string lastname, in Regex dog);
    }
}