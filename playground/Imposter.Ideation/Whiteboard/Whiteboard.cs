using System.Threading.Tasks;
using Moq;
using Xunit;

namespace Imposter.Ideation.Whiteboard
{
    public interface ITest
    {
        TResult GenericOutParam<TValue, TResult>(out TValue value);

        int Age { get; set; }
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

    public class Whiteboard
    {
        [Fact]
        public async Task Test()
        {
            var mock = new Mock<ITest>();

            mock.Setup(x => x.Age).Returns(() => mock.Object.Age == 1 ? 25 : 30);

            // Setup
            mock.Setup(x => x.GenericOutParam<Cat, int>(out It.Ref<Cat>.IsAny))
                .Returns(42);

            // Act
            mock.Object.GenericOutParam<Cat, int>(out Cat result);

            // This verification PASSES - exact type match
            mock.Verify(x => x.GenericOutParam<Cat, int>(out It.Ref<Cat>.IsAny), Times.Once);

            // This verification FAILS - different generic type parameters
            mock.Verify(x => x.GenericOutParam<IAnimal, int>(out It.Ref<IAnimal>.IsAny), Times.Once);
        }
    }
}