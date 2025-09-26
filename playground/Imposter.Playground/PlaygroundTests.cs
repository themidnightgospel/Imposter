using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Shouldly;
using Xunit;


public class IOrderApiServiceImposterTests
{
    [Fact]
    public void NoSetup_MethodWithTaskGenericReturnType_ReturnsTask()
    {
        var mock = new Mock<ICalculator>();
        var res = mock.Object.DoGenericAsync();

        res.IsCompleted.ShouldBeTrue();
        res.Result.ShouldBe(0);
    }

    [Fact]
    public void NoSetup_MethodWithTaskReturnType_ReturnsTask()
    {
        var mock = new Mock<ICalculator>();
        Task res = mock.Object.DoAsync();

        res.IsCompleted.ShouldBeTrue();
    }

    [Fact]
    public void MultiSetup_FirstMatchInReverseInvoked()
    {
        var mock = new Mock<ICalculator>();

        mock.Setup(x => x.Add(1, 2))
            .Returns(1);

        mock.Setup(x => x.Add(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(2);

        var res = mock.Object.Add(1, 2);
        res.ShouldBe(2);
    }

    [Fact]
    public void EmptySetup_OverwritesExistingOne()
    {
        var mock = new Mock<ICalculator>();

        mock.Setup(x => x.Add(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(11);

        mock.Setup(x => x.Add(It.IsAny<int>(), It.IsAny<int>()));

        var res = mock.Object.Add(1, 1);
        res.ShouldBe(0);
    }

    [Fact]
    public void ThreadSleepInCallback_BlocksCall()
    {
        var mock = new Mock<ICalculator>();

        mock.Setup(x => x.Add(It.IsAny<int>(), It.IsAny<int>()))
            .Callback(() =>
            {
                Thread.Sleep(200000); // blocks 2 seconds
                Console.WriteLine("Finished sleeping");
            })
            .Returns(11);

        mock.Object.Add(1, 1);
    }


    /*
    [Fact]
    public void DoWork_WhenSetup_ShouldInvokeCallbacks()
    {
        // Arrange
        var imposter = new IOrderApiServiceImposter();
        var calledBefore = false;
        var calledAfter = false;

        imposter
            .DoWork()
            .CallBefore(() => calledBefore = true)
            .CallAfter(() => calledAfter = true);

        var instance = imposter.Instance();

        // Act
        instance.DoWork();

        // Assert
        calledBefore.ShouldBeTrue();
        calledAfter.ShouldBeTrue();
        imposter.Verify().DoWork().WasInvoked(InvocationCount.Once);

        bool Matches(in int value, string name)
        {
            return true;
        }
    }

    [Fact]
    public void DoWork_WhenExceptionSetup_ShouldThrow()
    {
        // Arrange
        var imposter = new IOrderApiServiceImposter();
        imposter.DoWork().Throws<InvalidOperationException>();
        var instance = imposter.Instance();

        // Act & Assert
        Should.Throw<InvalidOperationException>(() => instance.DoWork());
    }

    [Fact]
    public void GetNumber_WhenReturnsValue_ShouldReturnThatValue()
    {
        // Arrange
        var imposter = new IOrderApiServiceImposter();
        imposter.GetNumber().Returns(42);
        var instance = imposter.Instance();

        // Act
        var result = instance.GetNumber();

        // Assert
        result.ShouldBe(42);
        imposter.Verify().GetNumber().WasInvoked(InvocationCount.Once);
    }

    [Fact]
    public void GetNumber_WhenNotSetup_ShouldReturnDefault()
    {
        // Arrange
        var imposter = new IOrderApiServiceImposter();
        var instance = imposter.Instance();

        // Act
        var result = instance.GetNumber();

        // Assert
        result.ShouldBe(0);
    }

    [Fact]
    public void SingleInParameter_WithMatchingArg_ShouldInvokeBehavior()
    {
        // Arrange
        var imposter = new IOrderApiServiceImposter();
        var receivedValue = 0;

        imposter.SingleInParameter(Arg<int>.Is(10))
            .CallBefore((in int value) => receivedValue = value);

        var instance = imposter.Instance();

        // Act
        var value = 10;
        instance.SingleInParameter(in value);

        // Assert
        receivedValue.ShouldBe(value);
    }

    [Fact]
    public void SingleInParameter_WithNonMatchingArg_ShouldNotInvokeBehavior()
    {
        // Arrange
        var imposter = new IOrderApiServiceImposter();
        var wasCalled = false;

        imposter.SingleInParameter(Arg<int>.Is(10))
            .CallBefore((in int value) => wasCalled = true);

        var instance = imposter.Instance();

        // Act
        var value = 20;
        instance.SingleInParameter(in value);

        // Assert
        wasCalled.ShouldBeFalse();
    }

    [Fact]
    public void Modify_WithRefParameter_ShouldModifyValue()
    {
        // Arrange
        var imposter = new IOrderApiServiceImposter();
        imposter.Modify(Arg<int>.Any)
            .CallBefore((ref int value) => value *= 2);

        var instance = imposter.Instance();
        var value = 5;

        // Act
        instance.Modify(ref value);

        // Assert
        value.ShouldBe(10);
    }

    [Fact]
    public void Initialize_WithOutParameter_ShouldSetValue()
    {
        // Arrange
        var imposter = new IOrderApiServiceImposter();
        imposter.Initialize(OutArg<int>.Any)
            .CallBefore((out int value) => value = 100);

        var instance = imposter.Instance();

        // Act
        instance.Initialize(out var result);

        // Assert
        result.ShouldBe(100);
    }

    [Fact]
    public async Task SaveAsync_WhenNotSetup_ShouldReturnCompletedTask()
    {
        // Arrange
        var imposter = new IOrderApiServiceImposter();
        var instance = imposter.Instance();

        // Act
        var task = instance.SaveAsync();

        // Assert
        task.ShouldNotBeNull();
        task.IsCompleted.ShouldBeTrue();
        await task; // Should not throw
    }

    [Fact]
    public async Task SaveAsync_WhenReturnsTask_ShouldReturnThatTask()
    {
        // Arrange
        var imposter = new IOrderApiServiceImposter();
        var expectedTask = Task.Delay(1);
        imposter.SaveAsync().Returns(expectedTask);
        var instance = imposter.Instance();

        // Act
        var result = instance.SaveAsync();

        // Assert
        result.ShouldBe(expectedTask);
    }

    [Fact]
    public async Task TrySaveAsync_WhenNotSetup_ShouldReturnDefaultValueTask()
    {
        // Arrange
        var imposter = new IOrderApiServiceImposter();
        var instance = imposter.Instance();

        // Act
        var result = await instance.TrySaveAsync();

        // Assert
        result.ShouldBeFalse(); // Default bool is false
    }

    [Fact]
    public async Task TrySaveAsync_WhenReturnsValue_ShouldReturnThatValue()
    {
        // Arrange
        var imposter = new IOrderApiServiceImposter();
        imposter.TrySaveAsync().Returns(new ValueTask<bool>(true));
        var instance = imposter.Instance();

        // Act
        var result = await instance.TrySaveAsync();

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void FindUser_WithNullableString_ShouldHandleNull()
    {
        // Arrange
        var imposter = new IOrderApiServiceImposter();
        imposter.FindUser(Arg<string?>.Is("test")).Returns("found");
        var instance = imposter.Instance();

        // Act
        var result1 = instance.FindUser("test");
        var result2 = instance.FindUser(null);

        // Assert
        result1.ShouldBe("found");
        result2.ShouldBeNull();
    }

    [Fact]
    public void Calculate_WithTupleReturn_ShouldReturnTuple()
    {
        // Arrange
        var imposter = new IOrderApiServiceImposter();
        imposter.Calculate(Arg<int[]>.Any).Returns((15, 3));
        var instance = imposter.Instance();

        // Act
        var result = instance.Calculate([1, 2, 3]);

        // Assert
        result.ShouldBe((15, 3));
        result.sum.ShouldBe(15);
        result.count.ShouldBe(3);
    }

    [Fact]
    public void MethodWithMultipleArguments_WithMultipleParameters_ShouldMatchCorrectly()
    {
        // Arrange
        var imposter = new IOrderApiServiceImposter();
        imposter.MethodWithMultipleArguments(Arg<int>.Is(1), Arg<int>.Is(2), Arg<int>.Is(3))
            .Returns(6);

        var instance = imposter.Instance();

        // Act
        var result = instance.MethodWithMultipleArguments(1, 2, 3);

        // Assert
        result.ShouldBe(6);
    }

    [Fact]
    public void MethodWithOutParameter_ShouldSetOutParameterAndReturnValue()
    {
        // Arrange
        var imposter = new IOrderApiServiceImposter();
        imposter.MethodWithOutParameter(Arg<int>.Is(5), OutArg<int>.Any)
            .Returns((int a, out int b) =>
            {
                b = a * 2;
                return a + b;
            });

        var instance = imposter.Instance();

        // Act
        var result = instance.MethodWithOutParameter(5, out var outValue);

        // Assert
        result.ShouldBe(15); // 5 + (5*2)
        outValue.ShouldBe(10); // 5*2
    }

    [Fact]
    public void InOutRef_WithComplexParameters_ShouldWorkCorrectly()
    {
        // Arrange
        var imposter = new IOrderApiServiceImposter();
        imposter.InOutRef(Arg<int>.Is(10), OutArg<int>.Any, Arg<int>.Is(5))
            .CallBefore((in int input, out int output, ref int temp) =>
            {
                output = input * 2;
                temp += 5;
            });

        var instance = imposter.Instance();
        var input = 10;
        var temp = 5;

        // Act
        instance.InOutRef(in input, out var output, ref temp);

        // Assert
        output.ShouldBe(20);
        temp.ShouldBe(10);
    }

    [Fact]
    public void Verify_WasInvoked_ShouldTrackInvocationsCorrectly()
    {
        // Arrange
        var imposter = new IOrderApiServiceImposter();
        var instance = imposter.Instance();

        // Act
        instance.DoWork();
        instance.DoWork();
        instance.GetNumber();

        // Assert
        imposter.Verify().DoWork().WasInvoked(InvocationCount.Exactly(2));
        imposter.Verify().GetNumber().WasInvoked(InvocationCount.Once);

        // Verify that exception is thrown for wrong count
        Should.Throw<VerificationFailedException>(() =>
            imposter.Verify().DoWork().WasInvoked(InvocationCount.Once));
    }

    [Fact]
    public void Verify_WithArgumentMatching_ShouldFilterCorrectly()
    {
        // Arrange
        var imposter = new IOrderApiServiceImposter();
        var instance = imposter.Instance();

        // Act
        var valueOne = 10;
        instance.SingleInParameter(in valueOne);
        var valueTwo = 20;
        instance.SingleInParameter(in valueTwo);
        var valueThree = 10;
        instance.SingleInParameter(in valueThree);

        // Assert
        imposter.Verify().SingleInParameter(Arg<int>.Is(10)).WasInvoked(InvocationCount.Exactly(2));
        imposter.Verify().SingleInParameter(Arg<int>.Is(20)).WasInvoked(InvocationCount.Once);
    }

    [Fact]
    public void OverloadedMethods_ShouldBeHandledSeparately()
    {
        // Arrange
        var imposter = new IOrderApiServiceImposter();
        imposter.Log(Arg<string>.Is("simple")).CallBefore(message => { });
        imposter.Log(Arg<string>.Is("with level"), Arg<int>.Is(2)).CallBefore((message, level) => { });

        var instance = imposter.Instance();

        // Act
        instance.Log("simple");
        instance.Log("with level", 2);

        // Assert
        imposter.Verify().Log(Arg<string>.Is("simple")).WasInvoked(InvocationCount.Once);
        imposter.Verify().Log(Arg<string>.Is("with level"), Arg<int>.Is(2)).WasInvoked(InvocationCount.Once);
    }

    [Fact]
    public void ParamsParameter_ShouldHandleArrayArguments()
    {
        // Arrange
        var imposter = new IOrderApiServiceImposter();
        var receivedNumbers = Array.Empty<int>();
        imposter.AddNumbers(Arg<int[]>.Any)
            .CallBefore(numbers => receivedNumbers = numbers);

        var instance = imposter.Instance();

        // Act
        instance.AddNumbers(1, 2, 3, 4, 5);

        // Assert
        receivedNumbers.ShouldBe(new[] { 1, 2, 3, 4, 5 });
    }
*/
}

// Extension method to simplify instance access
/*public static class ImposterExtensions
{
    public static IOrderApiService Instance(this IOrderApiServiceImposter imposter)
    {
        return ((IHaveImposterInstance<IOrderApiService>)imposter).Instance();
    }
}*/