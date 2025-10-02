using Imposter.Abstractions;
using Imposter.CodeGenerator.Tests.Setup.Methods.Generics;
using Shouldly;
using Xunit;

namespace Imposter.Ideation;

public interface IParent
{
    public int Age { get; }
}

public interface ICurrent : IParent
{
}

public interface IOther
{
}

public class Implementaton : ICurrent, IOther
{
    public int Age { get; set; }
    public Implementaton(int age) => Age = age;
}

public interface IGenericSut
{
    int Print<TInput>(TInput input, int age);
}

public class GenericMethodImposterTests
{
    [Fact]
    public void ShouldHandleComplexTypeConstraintMatching()
    {
        var imposter = new ISutWithGenericMethodImposter();

        imposter
            .Print<ICurrent>(
                Arg<ICurrent>.Is(it => it.Age == 80),
                Arg<int>.Any)
            .Returns(42);

        imposter
            .Print<ICurrent>(
                Arg<ICurrent>.Is(it => it.Age == 90),
                Arg<int>.Any)
            .Returns(52);

        ShouldBeTestExtensions.ShouldBe(imposter.Instance().Print(new Implementaton(80), 32), 42);

        ShouldBeTestExtensions.ShouldBe(imposter.Instance().Print(new Implementaton(90), 32), 52);

        imposter.Print<Implementaton>(Arg<Implementaton>.Is(it => it.Age == 80), Arg<int>.Any).Called(Count.Once);
        imposter.Print<Implementaton>(Arg<Implementaton>.Is(it => it.Age == 90), Arg<int>.Any).Called(Count.Once);
        imposter.Print<IParent>(Arg<IParent>.Any, Arg<int>.Any).Called(Count.Exactly(2));
    }

    [Fact]
    public void ShouldHandleSimpleReturnsAndDefaultInvocation()
    {
        var imposter = new ISutWithGenericMethodImposter();

        imposter
            .Print<string>(Arg<string>.Any, Arg<int>.Is(i => i > 10))
            .Returns(300);

        imposter
            .Print<int>(Arg<int>.Is(i => i < 100), Arg<int>.Any)
            .Returns((input, age) => input + age + 10);

        ShouldBeTestExtensions.ShouldBe(imposter.Instance().Print("hello", 15), 300);

        ShouldBeTestExtensions.ShouldBe(imposter.Instance().Print(50, 5), 65);

        ShouldBeTestExtensions.ShouldBe(imposter.Instance().Print("world", 5), 0);
    }

    [Fact]
    public void ShouldAllowSequentialSetupsAndRespectLIFOPrecedence()
    {
        var imposter = new ISutWithGenericMethodImposter();

        imposter.Print<string>(Arg<string>.Any, Arg<int>.Any).Returns(100);

        imposter
            .Print<string>(Arg<string>.Is(s => s == "specific"), Arg<int>.Any)
            .Returns(200);

        ShouldBeTestExtensions.ShouldBe(imposter.Instance().Print("specific", 1), 200);

        ShouldBeTestExtensions.ShouldBe(imposter.Instance().Print("general", 1), 100);
    }

    [Fact]
    public void ShouldHandleThrowingExceptions()
    {
        var imposter = new ISutWithGenericMethodImposter();
        var expectedException = new InvalidOperationException("Mocked Error");

        imposter.Print<bool>(Arg<bool>.Any, Arg<int>.Any).Throws(expectedException);

        imposter
            .Print<double>(Arg<double>.Any, Arg<int>.Any)
            .Throws((input, age) => new ArgumentException($"Value: {input}"));

        Should.Throw<InvalidOperationException>(() => imposter.Instance().Print(true, 1))
            .ShouldBeSameAs(expectedException);

        Should.Throw<ArgumentException>(() => imposter.Instance().Print(99.5, 10))
            .Message.ShouldContain("Value: 99.5");
    }

    [Fact]
    public void ShouldExecuteBeforeAndAfterCallbacks()
    {
        var imposter = new ISutWithGenericMethodImposter();
        var callHistory = new List<string>();

        imposter
            .Print<int>(Arg<int>.Any, Arg<int>.Any)
            .CallBefore((i, a) => callHistory.Add($"Before:{i}"))
            .CallAfter((i, a) => callHistory.Add($"After:{i}"))
            .Returns(99);

        ShouldBeTestExtensions.ShouldBe(imposter.Instance().Print(5, 5), 99);

        callHistory.ShouldBe(new[] { "Before:5", "After:5" });
    }

    [Fact]
    public void ShouldHandleTypeArgumentCovarianceAndConversion()
    {
        var imposter = new ISutWithGenericMethodImposter();

        imposter
            .Print<IParent>(
                Arg<IParent>.Is(p => p.Age > 50),
                Arg<int>.Any)
            .Returns(100);

        imposter
            .Print<IOther>(
                Arg<IOther>.Any,
                Arg<int>.Any)
            .Returns(200);

        ShouldBeTestExtensions.ShouldBe(imposter.Instance().Print(new Implementaton(60), 10), 100);

        ShouldBeTestExtensions.ShouldBe(imposter.Instance().Print(new Implementaton(10), 10), 200);
    }

    [Fact]
    public void ShouldTrackInvocationHistoryAndVerifyCounts()
    {
        var imposter = new ISutWithGenericMethodImposter();

        imposter.Print<ICurrent>(Arg<ICurrent>.Any, Arg<int>.Any).Returns(10);
        imposter.Print<string>(Arg<string>.Any, Arg<int>.Any).Returns(20);

        imposter.Instance().Print(new Implementaton(1), 5);
        imposter.Instance().Print("test", 5);
        imposter.Instance().Print(new Implementaton(2), 5);

        imposter.Print<Implementaton>(Arg<Implementaton>.Any, Arg<int>.Any).Called(Count.Exactly(2));
        imposter.Print<Implementaton>(Arg<Implementaton>.Is(i => i.Age == 1), Arg<int>.Any).Called(Count.Once);
        imposter.Print<IParent>(Arg<IParent>.Any, Arg<int>.Any).Called(Count.Exactly(2));

        Should.Throw<VerificationFailedException>(() =>
            imposter.Print<string>(Arg<string>.Any, Arg<int>.Any).Called(Count.Exactly(5)));
    }

    [Fact]
    public void ShouldHandleNullGenericArgument()
    {
        var imposter = new ISutWithGenericMethodImposter();

        imposter
            .Print<string>(Arg<string>.Is(s => s is null), Arg<int>.Any)
            .Returns(99);

        ShouldBeTestExtensions.ShouldBe(imposter.Instance().Print<string>(null, 10), 99);

        imposter.Print<string>(Arg<string>.Is(s => s is null), Arg<int>.Any).Called(Count.Once);
        imposter.Print<string>(Arg<string>.Is(s => s is not null), Arg<int>.Any).Called(Count.Never);
    }

    [Fact]
    public void ShouldHandleNullNonGenericArgument()
    {
        var imposter = new ISutWithGenericMethodImposter();

        imposter
            .Print<object>(Arg<object>.Any, Arg<int>.Any)
            .Returns(10);

        ShouldBeTestExtensions.ShouldBe(imposter.Instance().Print<object>(null, 10), 10);

        imposter.Print<object>(Arg<object>.Is(o => o is null), Arg<int>.Any).Called(Count.Once);
    }

    [Fact]
    public void ShouldHandleSequentialReturnsExhaustion()
    {
        var imposter = new ISutWithGenericMethodImposter();

        imposter
            .Print<int>(Arg<int>.Any, Arg<int>.Any)
            .Returns(10)
            .Returns(20);

        ShouldBeTestExtensions.ShouldBe(imposter.Instance().Print(1, 1), 10);

        ShouldBeTestExtensions.ShouldBe(imposter.Instance().Print(1, 1), 20);

        ShouldBeTestExtensions.ShouldBe(imposter.Instance().Print(1, 1), 0);

        ShouldBeTestExtensions.ShouldBe(imposter.Instance().Print(1, 1), 0);

        imposter.Print<int>(Arg<int>.Any, Arg<int>.Any).Called(Count.Exactly(4));
    }

    [Fact]
    public void ShouldVerifyCountTypes()
    {
        var imposter = new ISutWithGenericMethodImposter();
        imposter.Print<int>(Arg<int>.Any, Arg<int>.Any).Returns(1);

        imposter.Instance().Print(1, 1);
        imposter.Instance().Print(2, 2);
        imposter.Instance().Print(3, 3);

        imposter.Print<int>(Arg<int>.Any, Arg<int>.Any).Called(Count.Exactly(3));
        imposter.Print<int>(Arg<int>.Is(i => i > 0), Arg<int>.Any).Called(Count.AtLeast(3));
        imposter.Print<int>(Arg<int>.Is(i => i == 99), Arg<int>.Any).Called(Count.Never);

        Should.Throw<VerificationFailedException>(() =>
            imposter.Print<int>(Arg<int>.Any, Arg<int>.Any).Called(Count.Exactly(2)));

        Should.Throw<VerificationFailedException>(() =>
            imposter.Print<int>(Arg<int>.Any, Arg<int>.Any).Called(Count.Never));
    }
}