using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Imposter.Ideation.Whiteboard
{
    public interface ITest
    {
        TResult GenericOutParam<TValue, TResult>(out TValue value);

        int Age { get; set; }
    }

    public interface IMammal { }

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

    public interface ICat : IAnimal { }

    public class Cat : Animal, ICat
    {
        public Cat(string name)
            : base(name) { }
    }

    public class Tiger : Cat
    {
        public Tiger(string name)
            : base(name) { }
    }

    public interface IDog : IAnimal { }

    public class Dog : Animal, IDog
    {
        public Dog(string name)
            : base(name) { }
    }

    public interface IGermanShepherd : IDog { }

    public class GermanShepherd : Dog, IGermanShepherd
    {
        public GermanShepherd(string name)
            : base(name) { }
    }

    public interface IPhoneBook
    {
        string this[int name, string lastname, IAnimal dog] { get; set; }

        string this[string name] { get; set; }
    }

    public class PhoneBook
    {
        private Dictionary<string, string> _entries = new Dictionary<string, string>();

        public string this[int name, string lastname, IAnimal dog]
        {
            get => _entries.TryGetValue(name.ToString(), out var number) ? number : "Not found";
            set => _entries[name.ToString()] = value;
        }

        public string this[string name]
        {
            get => _entries.TryGetValue(name, out var number) ? number : "Not found";
            set => _entries[name] = value;
        }
    }

    public class Whiteboard
    {
        [Fact]
        public async Task Test()
        {
            /*
            var mock = new Mock<IPhoneBook>();
            mock.Setup(it => it[1, "Doe", new Tiger("Tiger")]).Callback()
            */
        }
    }
}
