using System;
using System.Collections.Generic;
using Imposter.Abstractions;
using Imposter.CodeGenerator.Tests.Setup.Methods.Generics;
using Imposter.Ideation;
using Moq;
using Shouldly;
using Xunit;

namespace Imposter.Playground;

public class CustomArgType : List<int>, IArgType<List<int>>
{
    public static bool Matches(Type type)
    {
        return type.IsAssignableTo(typeof(List<int>));
    }

    public List<int> Value { get; }

    public CustomArgType(object? value)
    {
        Value = (List<int>)value;
    }
}

public class GenericIdeationTests
{
    [Fact]
    public void Test1()
    {
        var imposter = new ISutWithGenericMethodImposterPoc();

        object? anyArg = null;

        imposter
            .Print<ArgType.Any>(Arg<ArgType.Any>.Any, Arg<int>.Any)
            .Returns((any, age) =>
            {
                anyArg = any;
                return age + 10;
            });

        imposter.Instance().Print<string>("hello", 32).ShouldBe(42);
        anyArg.ShouldNotBeNull()
            .ShouldBeOfType<ArgType.Any>()
            .Value.ShouldNotBeNull()
            .ShouldBe("hello");

        imposter
            .Print<ArgType.Any>(Arg<ArgType.Any>.Any, Arg<int>.Any)
            .Called(Count.Once);
    }

    [Fact]
    public void Test2()
    {
        var imposter = new ISutWithGenericMethodImposterPoc();

        imposter
            .Print<ArgType.IsAssignableTo<ICurrent>>(
                Arg<ArgType.IsAssignableTo<ICurrent>>.Is(it => it.Value.Age == 80),
                Arg<int>.Any)
            .Returns((any, age) => age + 10);

        imposter
            .Print<ArgType.IsAssignableTo<ICurrent>>(
                Arg<ArgType.IsAssignableTo<ICurrent>>.Is(it => it.Value.Age == 90),
                Arg<int>.Any)
            .Returns((any, age) => age + 20);

        imposter.Instance().Print<ICurrent>(new Implementaton(80), 32).ShouldBe(42);

        imposter.Print<IParent2>(Arg<IParent2>.Is(it => it.Age == 80), Arg<int>.Any).Called(Count.Once);
        imposter.Print<ICurrent>(Arg<ICurrent>.Is(it => it.Age == 80), Arg<int>.Any).Called(Count.Once);
        imposter.Print<ArgType.IsAssignableTo<ICurrent>>(Arg<ArgType.IsAssignableTo<ICurrent>>.Is(it => it.Value.Age == 80), Arg<int>.Any).Called(Count.Once);
        imposter.Instance().Print<ICurrent>(new Implementaton(90), 32).ShouldBe(52);
    }

    [Fact]
    public void Test2Moq()
    {
        var imposter = new Mock<ISutWithGenericMethod>();

        imposter
            .Setup(it => it.Print<It.IsSubtype<ICurrent>>(
                It.IsAny<It.IsSubtype<ICurrent>>(),
                It.IsAny<int>()
            ))
            .Returns(99);

        var result = imposter.Object.Print<ICurrent>(new Implementaton(90), 32);

        Should.Throw<MockException>(() => imposter.Verify(p => p.Print<Implementaton>(It.IsAny<Implementaton>(), It.IsAny<int>()), Times.Once()));
        imposter.Verify(p => p.Print<It.IsSubtype<IParent2>>(It.IsAny<It.IsSubtype<IParent2>>(), It.IsAny<int>()), Times.Once());
        imposter.Verify(p => p.Print<It.IsSubtype<ICurrent>>(It.IsAny<It.IsSubtype<ICurrent>>(), It.IsAny<int>()), Times.Once());
        imposter.Verify(p => p.Print<ICurrent>(It.IsAny<ICurrent>(), It.IsAny<int>()), Times.Once());
        imposter.Verify(p => p.Print<IParent2>(It.IsAny<IParent2>(), It.IsAny<int>()), Times.Once());
    }
    
    interface IParent2
    {
        int Age { get; }
    }


    interface ICurrent : IParent2
    {
        int Age { get; }
    }

    class Implementaton : ICurrent
    {
        public Implementaton(int value)
        {
            Age = value;
        }

        public int Age { get; }
    }
}