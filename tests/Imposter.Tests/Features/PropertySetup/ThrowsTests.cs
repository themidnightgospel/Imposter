using System;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.PropertySetup;

public class ThrowsTests
{
    private readonly IPropertySetupSutImposter _sut = new IPropertySetupSutImposter();

    [Fact]
    public void GivenSetupToThrowExceptionInstance_WhenPropertyIsAccessed_ShouldThrowSameException()
    {
        var exception = new ArgumentException("Custom message");
        _sut.Age.Throws(exception);

        var thrownException = Should.Throw<ArgumentException>(() => _sut.Instance().Age);
        thrownException.ShouldBeSameAs(exception);
    }

    [Fact]
    public void GivenSetupToThrowGenericException_WhenPropertyIsAccessed_ShouldThrowNewInstance()
    {
        _sut.Age.Throws<InvalidOperationException>();

        Should.Throw<InvalidOperationException>(() => _sut.Instance().Age);
    }

    [Fact]
    public void GivenSequenceWithException_WhenPropertyIsAccessedSequentially_ShouldWorkCorrectly()
    {
        _sut.Age.Returns(10).Throws<ArgumentException>().Returns(30);

        _sut.Instance().Age.ShouldBe(10);
        Should.Throw<ArgumentException>(() => _sut.Instance().Age);
        _sut.Instance().Age.ShouldBe(30);
    }
    
    [Fact]
    public void GivenCallbackThatThrows_WhenPropertyIsSet_ValueShouldNotBeSet()
    {
        _sut.Age.SetterCallback(Arg<int>.Any(), _ => throw new InvalidOperationException("Callback error"));
        Should.Throw<InvalidOperationException>(() => _sut.Instance().Age = 42);

        _sut.Instance().Age.ShouldBe(default);
    }

    [Fact]
    public void GivenCallbackThatThrows_WhenPropertyIsSet_ShouldTrackSetHistory()
    {
        _sut.Age.SetterCallback(Arg<int>.Any(), _ => throw new InvalidOperationException("Callback error"));

        Should.Throw<InvalidOperationException>(() => _sut.Instance().Age = 42);

        _sut.Age.SetterCalled(Arg<int>.Is(it => it == 42), Count.Exactly(1));
    }

    [Fact]
    public void GivenGetterCallbackThatThrows_WhenPropertyIsAccessed_ShouldTrackGetHistory()
    {
        _sut.Age.GetterCallback(() => throw new InvalidOperationException("Getter callback error"));

        Should.Throw<InvalidOperationException>(() => _sut.Instance().Age);

        _sut.Age.GetterCalled(Count.Exactly(1));
    }
}