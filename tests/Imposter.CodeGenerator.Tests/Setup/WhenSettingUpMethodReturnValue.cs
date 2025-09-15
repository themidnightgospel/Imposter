using System;
using System.Linq;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Xunit;
using Imposters.Imposter.CodeGenerator.Tests.Setup;
using Shouldly;

namespace Imposter.CodeGenerator.Tests.Setup;

[GenerateImposter(typeof(ISut))]
public class WhenSettingUpMethodReturnValue
{
    [Fact]
    public async Task TimeoutTest()
    {
        await Task.Delay(TimeSpan.FromSeconds(4));
    }
    
    [Fact]
    public void MethodWithHasNoParameters_MethodIsInvoked_SetupReturnValueIsReturned()
    {
        // Arrange
        var sut = new ISutImposter();
        sut
            .NoParametersWithReturnType()
            .Returns(21);

        // Act
        var result = sut.Instance().NoParametersWithReturnType();

        // Assert
        result.ShouldBe(21);
    }

    [Fact]
    public void MethodWithParameters_MethodIsInvoked_SetupReturnValueIsReturned()
    {
        // Arrange
        var sut = new ISutImposter();
        sut
            .ParameterAndReturnType(Arg<int>.Any, Arg<int>.Any)
            .Returns((a, b) => a + b);

        // Act
        var result = sut.Instance().ParameterAndReturnType(3, 5);

        // Assert
        result.ShouldBe(8);
    }

    [Fact]
    public void MethodWithRefParameters_MethodIsInvoked_SetupReturnValueIsReturned()
    {
        // Arrange
        var sut = new ISutImposter();
        sut
            .MethodWithRefParameter(Arg<int>.Any)
            .Returns((ref int a) => a + 2);

        // Act
        var value = 10;
        var result = sut.Instance().MethodWithRefParameter(ref value);

        // Assert
        result.ShouldBe(12);
    }

    [Fact]
    public void MethodWithOutParameters_MethodIsInvoked_SetupReturnValueIsReturned()
    {
        // Arrange
        var sut = new ISutImposter();
        sut
            .MethodWithOutParameter(OutArg<int>.Any)
            .Returns((out int a) =>
            {
                a = 10;
                return 11;
            });

        // Act
        var result = sut.Instance().MethodWithOutParameter(out var value);

        // Assert
        value.ShouldBe(10);
        result.ShouldBe(11);
    }

    [Fact]
    public void MethodWithParamsParameters_MethodIsInvoked_SetupReturnValueIsReturned()
    {
        // Arrange
        var sut = new ISutImposter();
        sut
            .MethodWithParamsParameter(Arg<int[]>.Any)
            .Returns((params int[] args) => args.Sum());

        // Act
        var result = sut.Instance().MethodWithParamsParameter(1, 2, 3);

        // Assert
        result.ShouldBe(6);
    }

    [Fact]
    public void MethodWithTaskReturnType_MethodIsInvoked_SetupReturnValueIsReturned()
    {
        // Arrange
        var sut = new ISutImposter();
        var setupTask = Task.Delay(1);
        sut
            .ReturnsTypeTask()
            .Returns(setupTask);

        // Act
        var result = sut.Instance().ReturnsTypeTask();

        // Assert
        result.ShouldBe(setupTask);
    }

    [Fact]
    public void MultipleSetups_MethodInvokedWithMatchingParameters_MatchingSetupIsInvoked()
    {
        // Arrange
        var sut = new ISutImposter();

        sut
            .ParameterAndReturnType(Arg<int>.Is(it => it == 1), Arg<int>.Is(it => it == 2))
            .Returns((a, b) => 1);

        sut
            .ParameterAndReturnType(Arg<int>.Is(it => it == 5), Arg<int>.Is(it => it == 6))
            .Returns((a, b) => 2);

        // Act
        var result = sut.Instance().ParameterAndReturnType(5, 6);

        // Assert
        result.ShouldBe(2);
    }

    [Fact]
    public void MultipleMatchingSetups_MethodInvokedWithMatchingParameters_LatestSetupInvoked()
    {
        // Arrange
        var sut = new ISutImposter();

        sut
            .ParameterAndReturnType(Arg<int>.Any, Arg<int>.Any)
            .Returns((a, b) => 1);

        sut
            .ParameterAndReturnType(Arg<int>.Is(it => it > 1), Arg<int>.Is(it => it > 2))
            .Returns((a, b) => 2);

        sut
            .ParameterAndReturnType(Arg<int>.Is(it => it > 5), Arg<int>.Is(it => it > 6))
            .Returns((a, b) => 3);

        sut
            .ParameterAndReturnType(Arg<int>.Is(it => it > 9), Arg<int>.Is(it => it < 11))
            .Returns((a, b) => 4);

        // Act
        var result = sut.Instance().ParameterAndReturnType(10, 11);

        // Assert
        result.ShouldBe(3);
    }

    [Fact]
    public void MultipleSetups_MethodInvokedWithNonMatchingParameters_DefaultValueIsReturned()
    {
        // Arrange
        var sut = new ISutImposter();

        sut
            .ParameterAndReturnType(Arg<int>.Is(it => it == 1), Arg<int>.Is(it => it == 2))
            .Returns((a, b) => a - b);

        sut
            .ParameterAndReturnType(Arg<int>.Is(it => it == 5), Arg<int>.Is(it => it == 6))
            .Returns((a, b) => a + b);

        // Act
        var result = sut.Instance().ParameterAndReturnType(9, 10);

        // Assert
        result.ShouldBe(default(int));
    }

    [Fact]
    public void MethodWithNoParametersIsSetupToReturnMultipleResults_MethodIsInvokedMultipleTimes_SetupValuesReturnedInOrder()
    {
        // Arrange
        var sut = new ISutImposter();
        sut
            .NoParametersWithReturnType()
            .Returns(1)
            .Returns(2)
            .Returns(3);

        // Act
        var resultOne = sut.Instance().NoParametersWithReturnType();
        var resultTWo = sut.Instance().NoParametersWithReturnType();
        var resultThree = sut.Instance().NoParametersWithReturnType();
        var resultFour = sut.Instance().NoParametersWithReturnType();

        // Assert
        resultOne.ShouldBe(1);
        resultTWo.ShouldBe(2);
        resultThree.ShouldBe(3);
        resultFour.ShouldBe(3);
    }

    public interface ISut
    {
        int NoParametersWithReturnType();

        int ParameterAndReturnType(int a, int b);

        int MethodWithRefParameter(ref int a);

        int MethodWithOutParameter(out int a);

        int MethodWithParamsParameter(params int[] a);

        Task ReturnsTypeTask();
    }
}