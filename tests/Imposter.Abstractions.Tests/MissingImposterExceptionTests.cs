using System;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests;

public class MissingImposterExceptionTests
{
    [Fact]
    public void Given_NoMethodName_When_Constructed_Should_LeaveMethodNameAndInnerExceptionNull()
    {
        var exception = new MissingImposterException();

        exception.MethodName.ShouldBeNull();
        exception.InnerException.ShouldBeNull();
    }

    [Theory]
    [InlineData("void IService.Execute()")]
    [InlineData("")]
    public void Given_MethodName_When_Constructed_Should_PreserveMethodNameAndMessage(
        string methodName
    )
    {
        var exception = new MissingImposterException(methodName);

        exception.MethodName.ShouldBe(methodName);
        exception.Message.ShouldBe($"No imposter setup was configured for '{methodName}'.");
        exception.InnerException.ShouldBeNull();
    }

    [Fact]
    public void Given_MethodNameAndInnerException_When_Constructed_Should_PreserveBothAndMessage()
    {
        const string methodName = "void IService.Execute()";
        var innerException = new InvalidOperationException("Underlying failure.");

        var exception = new MissingImposterException(methodName, innerException);

        exception.MethodName.ShouldBe(methodName);
        exception.Message.ShouldBe($"No imposter setup was configured for '{methodName}'.");
        exception.InnerException.ShouldBeSameAs(innerException);
    }
}
