using System;
using System.Threading.Tasks;
using Imposters.Imposter.CodeGenerator.Tests;
using Xunit;

namespace Imposter.CodeGenerator.Tests;

[GenerateImposter(typeof(IOrderApiService))]
public class OrderApiServiceImposterTests
{
    [Fact]
    public void Log_TwoParameters_ShouldRecordInvocation()
    {
        // Arrange
        var imposter = new OrderApiServiceImposter();
        var service = imposter.Instance();
        var message = "Test log message";
        var level = 2;
        
        // Act
        service.Log(message, level);

        // Assert
        imposter.Verify().Log(Arg<string>.Is(message), Arg<int>.Is(level)).Once();
    }
    
    [Fact]
    public void DoWork_ShouldRecordInvocation()
    {
        // Arrange
        var imposter = new OrderApiServiceImposter();
        var service = imposter.Instance();

        // Act
        service.DoWork();

        // Assert
        imposter.Verify().DoWork().Once();
    }

    [Fact]
    public void DoWork_ShouldThrowException_WhenConfigured()
    {
        // Arrange
        var imposter = new OrderApiServiceImposter();
        var service = imposter.Instance();
        imposter.DoWork().Throws<InvalidOperationException>();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => service.DoWork());
        imposter.Verify().DoWork().Once();
    }

    [Fact]
    public void DoWork_CalledMultipleTimes_ShouldRecordAllInvocations()
    {
        // Arrange
        var imposter = new OrderApiServiceImposter();
        var service = imposter.Instance();

        // Act
        service.DoWork();
        service.DoWork();
        service.DoWork();

        // Assert
        imposter.Verify().DoWork().Times(3);
    }

    [Fact]
    public void GetNumber_ShouldReturnConfiguredValue()
    {
        // Arrange
        var imposter = new OrderApiServiceImposter();
        var service = imposter.Instance();
        var expectedValue = 42;
        imposter.GetNumber().Returns(expectedValue);

        // Act
        var result = service.GetNumber();

        // Assert
        Assert.Equal(expectedValue, result);
        imposter.Verify().GetNumber().Once();
    }

    [Fact]
    public void GetNumber_ShouldReturnValueFromGenerator()
    {
        // Arrange
        var imposter = new OrderApiServiceImposter();
        var service = imposter.Instance();
        var counter = 0;
        imposter.GetNumber().Returns(() => ++counter);

        // Act
        var result1 = service.GetNumber();
        var result2 = service.GetNumber();

        // Assert
        Assert.Equal(1, result1);
        Assert.Equal(2, result2);
        imposter.Verify().GetNumber().Times(2);
    }

    [Fact]
    public void GetNumber_ShouldThrowException_WhenConfigured()
    {
        // Arrange
        var imposter = new OrderApiServiceImposter();
        var service = imposter.Instance();
        imposter.GetNumber().Throws<NotImplementedException>();

        // Act & Assert
        Assert.Throws<NotImplementedException>(() => service.GetNumber());
        imposter.Verify().GetNumber().Once();
    }

    [Fact]
    public void Process_WithInParameter_ShouldRecordInvocation()
    {
        // Arrange
        var imposter = new OrderApiServiceImposter();
        var service = imposter.Instance();
        var value = 123;

        // Act
        service.Process(in value);

        // Assert
        imposter.Verify().Process(Arg<int>.Any).Once();
        imposter.Verify().Process(Arg<int>.Is(123)).Once();
    }

    [Fact]
    public void Process_WithSpecificValue_ShouldVerifyCorrectly()
    {
        // Arrange
        var imposter = new OrderApiServiceImposter();
        var service = imposter.Instance();

        // Act
        var value = 100;
        service.Process(in value);
        var i = 200;
        service.Process(in i);

        // Assert
        imposter.Verify().Process(Arg<int>.Is(value)).Once();
        imposter.Verify().Process(Arg<int>.Is(i)).Once();
        imposter.Verify().Process(Arg<int>.Any).Times(2);
    }

    [Fact]
    public void Modify_WithRefParameter_ShouldRecordInvocation()
    {
        // Arrange
        var imposter = new OrderApiServiceImposter();
        var service = imposter.Instance();
        var value = 456;

        // Act
        service.Modify(ref value);

        // Assert
        imposter.Verify().Modify(Arg<int>.Any).Once();
        imposter.Verify().Modify(Arg<int>.Is(456)).Once();
    }

    [Fact]
    public void Initialize_WithOutParameter_ShouldRecordInvocation()
    {
        // Arrange
        var imposter = new OrderApiServiceImposter();
        var service = imposter.Instance();

        // Act
        service.Initialize(out int value);

        // Assert
        imposter.Verify().Initialize(Arg<int>.Any).Once();
    }

    [Fact]
    public void AddNumbers_WithParamsArray_ShouldRecordInvocation()
    {
        // Arrange
        var imposter = new OrderApiServiceImposter();
        var service = imposter.Instance();
        var numbers = new[] { 1, 2, 3, 4, 5 };

        // Act
        service.AddNumbers(numbers);

        // Assert
        imposter.Verify().AddNumbers(Arg<int[]>.Any).Once();
    }

    [Fact]
    public void AddNumbers_WithParamsArguments_ShouldRecordInvocation()
    {
        // Arrange
        var imposter = new OrderApiServiceImposter();
        var service = imposter.Instance();

        // Act
        service.AddNumbers(1, 2, 3);

        // Assert
        imposter.Verify().AddNumbers(Arg<int[]>.Any).Once();
    }

    [Fact]
    public void Greet_WithDefaultParameter_ShouldRecordInvocation()
    {
        // Arrange
        var imposter = new OrderApiServiceImposter();
        var service = imposter.Instance();

        // Act
        service.Greet();

        // Assert
        imposter.Verify().Greet(Arg<string>.Any).Once();
    }

    [Fact]
    public void Greet_WithSpecificName_ShouldRecordInvocation()
    {
        // Arrange
        var imposter = new OrderApiServiceImposter();
        var service = imposter.Instance();
        var name = "John";

        // Act
        service.Greet(name);

        // Assert
        imposter.Verify().Greet(Arg<string>.Is("John")).Once();
    }

    [Fact]
    public void FindUser_ShouldReturnConfiguredValue()
    {
        // Arrange
        var imposter = new OrderApiServiceImposter();
        var service = imposter.Instance();
        var expectedUser = "user123";
        imposter.FindUser(Arg<string?>.Any).Returns(expectedUser);

        // Act
        var result = service.FindUser("test");

        // Assert
        Assert.Equal(expectedUser, result);
        imposter.Verify().FindUser(Arg<string?>.Is("test")).Once();
    }

    [Fact]
    public void FindUser_WithNullUsername_ShouldWork()
    {
        // Arrange
        var imposter = new OrderApiServiceImposter();
        var service = imposter.Instance();
        imposter.FindUser(Arg<string?>.IsDefault()).Returns("anonymous");

        // Act
        var result = service.FindUser(null);

        // Assert
        Assert.Equal("anonymous", result);
        // TODO make this work by making criteria nullable
        // imposter.Verify().FindUser(null).Once();
        
        imposter.Verify().FindUser(Arg<string?>.IsDefault()).Once();
    }

    /*
    [Fact]
    public void Echo_GenericMethod_ShouldReturnConfiguredValue()
    {
        // Arrange
        var imposter = new OrderApiServiceImposter();
        var service = imposter.ImposterInstance;
        var input = "test string";
        var expected = "echoed: test string";
        imposter.Echo(Arg<string>.Any).Returns(expected);

        // Act
        var result = service.Echo(input);

        // Assert
        Assert.Equal(expected, result);
        imposter.Verify().Echo(Arg<string>.Is("test string")).Once();
    }

    [Fact]
    public void Echo_GenericMethod_WithDifferentTypes()
    {
        // Arrange
        var imposter = new OrderApiServiceImposter();
        var service = imposter.ImposterInstance;
        
        imposter.Echo(Arg<int>.Any).Returns(42);
        imposter.Echo(Arg<string>.Any).Returns("echoed");

        // Act
        var intResult = service.Echo(10);
        var stringResult = service.Echo("hello");

        // Assert
        Assert.Equal(42, intResult);
        Assert.Equal("echoed", stringResult);
        imposter.Verify().Echo(Arg<int>.Is(10)).Once();
        imposter.Verify().Echo(Arg<string>.Is("hello")).Once();
    }

    [Fact]
    public void CreateInstance_GenericMethod_ShouldReturnConfiguredValue()
    {
        // Arrange
        var imposter = new OrderApiServiceImposter();
        var service = imposter.ImposterInstance;
        var expectedInstance = "new instance";
        imposter.CreateInstance<string>().Returns(expectedInstance);

        // Act
        var result = service.CreateInstance<string>();

        // Assert
        Assert.Equal(expectedInstance, result);
        imposter.Verify().CreateInstance<string>().Once();
    }

    [Fact]
    public void Calculate_WithTupleReturn_ShouldReturnConfiguredValue()
    {
        // Arrange
        var imposter = new OrderApiServiceImposter();
        var service = imposter.ImposterInstance;
        var numbers = new[] { 1, 2, 3, 4, 5 };
        var expectedResult = (sum: 15, count: 5);
        imposter.Calculate(Arg<int[]>.Any).Returns(expectedResult);

        // Act
        var result = service.Calculate(numbers);

        // Assert
        Assert.Equal(expectedResult.sum, result.sum);
        Assert.Equal(expectedResult.count, result.count);
        imposter.Verify().Calculate(Arg<int[]>.Any).Once();
    }

    [Fact]
    public async Task SaveAsync_ShouldReturnConfiguredTask()
    {
        // Arrange
        var imposter = new OrderApiServiceImposter();
        var service = imposter.ImposterInstance;
        var completedTask = Task.CompletedTask;
        imposter.SaveAsync().Returns(completedTask);

        // Act
        var result = service.SaveAsync();
        await result;

        // Assert
        Assert.Equal(completedTask, result);
        imposter.Verify().SaveAsync().Once();
    }

    [Fact]
    public async Task SaveAsync_ShouldThrowException_WhenConfigured()
    {
        // Arrange
        var imposter = new OrderApiServiceImposter();
        var service = imposter.ImposterInstance;
        imposter.SaveAsync().Throws<InvalidOperationException>();

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => service.SaveAsync());
        imposter.Verify().SaveAsync().Once();
    }

    [Fact]
    public async Task TrySaveAsync_ShouldReturnConfiguredValueTask()
    {
        // Arrange
        var imposter = new OrderApiServiceImposter();
        var service = imposter.ImposterInstance;
        var expectedResult = new ValueTask<bool>(true);
        imposter.TrySaveAsync().Returns(expectedResult);

        // Act
        var result = await service.TrySaveAsync();

        // Assert
        Assert.True(result);
        imposter.Verify().TrySaveAsync().Once();
    }

    [Fact]
    public void Log_SingleParameter_ShouldRecordInvocation()
    {
        // Arrange
        var imposter = new OrderApiServiceImposter();
        var service = imposter.ImposterInstance;
        var message = "Test log message";

        // Act
        service.Log(message);

        // Assert
        imposter.Verify().Log(Arg<string>.Is(message)).Once();
    }


    [Fact]
    public void OldMethod_ShouldRecordInvocation()
    {
        // Arrange
        var imposter = new OrderApiServiceImposter();
        var service = imposter.ImposterInstance;

        // Act
        service.OldMethod();

        // Assert
        imposter.Verify().OldMethod().Once();
    }

    [Fact]
    public void MixModifiers_WithInOutRefParameters_ShouldRecordInvocation()
    {
        // Arrange
        var imposter = new OrderApiServiceImposter();
        var service = imposter.ImposterInstance;
        var input = 10;
        var temp = 20;

        // Act
        service.MixModifiers(in input, out int output, ref temp);

        // Assert
        imposter.Verify().MixModifiers(
            Arg<int>.Is(10), 
            Arg<int>.Any, 
            Arg<int>.Is(20)
        ).Once();
    }

    [Fact]
    public void Verify_NeverCalled_ShouldPassVerification()
    {
        // Arrange
        var imposter = new OrderApiServiceImposter();

        // Act & Assert (no exception should be thrown)
        imposter.Verify().DoWork().Never();
        imposter.Verify().GetNumber().Never();
    }

    [Fact]
    public void Verify_CalledWrongNumberOfTimes_ShouldFail()
    {
        // Arrange
        var imposter = new OrderApiServiceImposter();
        var service = imposter.ImposterInstance;

        // Act
        service.DoWork();

        // Assert
        Assert.Throws<Exception>(() => imposter.Verify().DoWork().Times(2));
    }

    [Fact]
    public void MultipleMethodCalls_ShouldRecordAllInvocations()
    {
        // Arrange
        var imposter = new OrderApiServiceImposter();
        var service = imposter.ImposterInstance;

        // Setup returns
        imposter.GetNumber().Returns(42);
        imposter.FindUser(Arg<string?>.Any).Returns("user");

        // Act
        service.DoWork();
        var number = service.GetNumber();
        service.Process(in 100);
        var user = service.FindUser("test");
        service.Log("message");

        // Assert
        imposter.Verify().DoWork().Once();
        imposter.Verify().GetNumber().Once();
        imposter.Verify().Process(Arg<int>.Is(100)).Once();
        imposter.Verify().FindUser(Arg<string?>.Is("test")).Once();
        imposter.Verify().Log(Arg<string>.Is("message")).Once();

        Assert.Equal(42, number);
        Assert.Equal("user", user);
    }

    [Fact]
    public void ChainedConfiguration_ShouldWork()
    {
        // Arrange
        var imposter = new OrderApiServiceImposter();
        var service = imposter.ImposterInstance;

        // Setup method chain
        imposter.GetNumber().Returns(1);
        imposter.FindUser(Arg<string?>.Any).Returns("admin").Throws<UnauthorizedAccessException>();

        // Act
        var result = service.GetNumber();
        
        // This should throw since Throws is configured after Returns
        Assert.Throws<UnauthorizedAccessException>(() => service.FindUser("test"));

        // Assert
        Assert.Equal(1, result);
        imposter.Verify().GetNumber().Once();
        imposter.Verify().FindUser(Arg<string?>.Any).Once();
    }

    [Fact]
    public void ParameterMatching_WithSpecificCriteria_ShouldWork()
    {
        // Arrange
        var imposter = new OrderApiServiceImposter();
        var service = imposter.ImposterInstance;

        // Act
        service.Process(in 5);
        service.Process(in 10);
        service.Process(in 15);

        // Assert - verify specific values
        imposter.Verify().Process(Arg<int>.Is(5)).Once();
        imposter.Verify().Process(Arg<int>.Is(10)).Once();
        imposter.Verify().Process(Arg<int>.Is(15)).Once();
        
        // Assert - verify range or condition
        imposter.Verify().Process(Arg<int>.Match(x => x > 10)).Times(2); // 15 matches
        imposter.Verify().Process(Arg<int>.Match(x => x <= 10)).Times(2); // 5, 10 match
    }
*/
}