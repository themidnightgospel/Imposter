using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Ideation.GenericMethod;

public class GenericMethodImposterTests
{
    private class Animal
    {
        public string Name { get; set; }
    }

    private class Dog : Animal
    {
    }

    [Fact]
    public void SetupOnSpecificGenericTypes_InvokedWithThoseTypes_SetupExecuted()
    {
        // Arrange
        var imposter = new GenericMethodPoc();
        var expectedResult = "imposter_result";
        var callBeforeInvoked = false;
        var callAfterInvoked = false;

        imposter
            .Print<string, int, bool, double, string, object>(
                Arg<string>.Any, OutArg<int>.Any, Arg<bool>.Any, Arg<double>.Any, Arg<object[]>.Any)
            .CallBefore((param, out outParam, in b, ref d, paramsParam) =>
            {
                callBeforeInvoked = true;
                outParam = 42;
            })
            .CallAfter((param, out outParam, in b, ref d, paramsParam) =>
            {
                callAfterInvoked = true;
                // Note: 'outParam' is not carried over from CallBefore
                outParam = 84;
            })
            .Returns(expectedResult);

        var sut = imposter.Instance();

        var inParam = true;
        var refParam = 1.5;

        // Act
        var result = sut.Print<string, int, bool, double, string, object>(
            string.Empty, out var outValue, in inParam, ref refParam, [1, 2, 3]);

        // Assert
        result.ShouldBe(expectedResult);
        callBeforeInvoked.ShouldBeTrue();
        callAfterInvoked.ShouldBeTrue();
        outValue.ShouldBe(84); // The value from the last delegate to touch it
    }

    [Fact]
    public void SetupWithArgumentCriteria_InvokedWithValuesMatchingTheCriteria_SetupExecuted()
    {
        // Arrange
        var imposter = new GenericMethodPoc();
        var expectedResult = "imposter_result";

        var paramsParam = new[] { 1, 2, 3 };
        var stringParam = "test_string";
        var inParam = true;
        var refParam = 1.5;

        imposter
            .Print<string, int, bool, double, string, int>(
                Arg<string>.Is(stringParam), OutArg<int>.Any, Arg<bool>.Is(inParam), Arg<double>.Is(refParam), Arg<int[]>.Is(paramsParam))
            .Returns(expectedResult);

        var sut = imposter.Instance();

        // Act
        var result = sut.Print<string, int, bool, double, string, int>(
            stringParam, out _, in inParam, ref refParam, paramsParam);

        // Assert
        result.ShouldBe(expectedResult);
    }

    [Fact]
    public void SetupWithSpecificGenericTypes_PassedCompatibleTypeForTOrdinaryParam_SetupInvoked()
    {
        // Arrange
        var imposter = new GenericMethodPoc();
        imposter.Print<Animal, int, bool, double, string, object>(Arg<Animal>.Any, OutArg<int>.Any, Arg<bool>.Any, Arg<double>.Any, Arg<object[]>.Any)
            .Returns("matched_base");
        var sut = imposter.Instance();
        var inParam = true;
        var refParam = 1.5;

        // Act
        var result = sut.Print<Dog, int, bool, double, string, object>(new Dog(), out _, in inParam, ref refParam, []);

        // Assert
        result.ShouldBe("matched_base");
    }

    [Fact]
    public void SetupWithSpecificGenericTypes_PassedCompatibleTypeForTOutParam_SetupInvoked()
    {
        // Arrange
        var imposter = new GenericMethodPoc();
        imposter.Print<string, Dog, bool, double, string, object>(Arg<string>.Any, OutArg<Dog>.Any, Arg<bool>.Any, Arg<double>.Any, Arg<object[]>.Any)
            .Returns((string _, out Dog dog, in bool _, ref double _, object[] _) =>
            {
                dog = new Dog();
                return "ok";
            });
        var sut = imposter.Instance();
        var inParam = true;
        var refParam = 1.5;

        // Act
        sut.Print<string, Animal, bool, double, string, object>("", out Animal animal, in inParam, ref refParam, []);

        // Assert
        animal.ShouldNotBeNull();
        animal.ShouldBeOfType<Dog>();
    }

    [Fact]
    public void SetupWithSpecificGenericTypes_PassedCompatibleTypeForTInParam_SetupInvoked()
    {
        // Arrange
        var imposter = new GenericMethodPoc();
        imposter.Print<string, int, Animal, double, string, object>(Arg<string>.Any, OutArg<int>.Any, Arg<Animal>.Any, Arg<double>.Any, Arg<object[]>.Any)
            .Returns("matched_base");
        var sut = imposter.Instance();
        var inParam = new Dog();
        var refParam = 1.5;

        // Act
        var result = sut.Print<string, int, Dog, double, string, object>("", out _, in inParam, ref refParam, []);

        // Assert
        result.ShouldBe("matched_base");
    }

    [Fact]
    public void SetupWithSpecificGenericTypes_PassedCompatibleTypeForResult_SetupInvoked()
    {
        // Arrange
        var imposter = new GenericMethodPoc();
        imposter.Print<string, int, bool, double, Dog, object>(Arg<string>.Any, OutArg<int>.Any, Arg<bool>.Any, Arg<double>.Any, Arg<object[]>.Any)
            .Returns(new Dog());
        var sut = imposter.Instance();
        var inParam = true;
        var refParam = 1.5;

        // Act
        Animal result = sut.Print<string, int, bool, double, Animal, object>("", out _, in inParam, ref refParam, []);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeOfType<Dog>();
    }

    [Fact]
    public void SetupWithSpecificGenericTypes_PassedCompatibleTypeForTParamsParam_SetupInvoked()
    {
        // Arrange
        var imposter = new GenericMethodPoc();
        imposter.Print<string, int, bool, double, string, Animal>(Arg<string>.Any, OutArg<int>.Any, Arg<bool>.Any, Arg<double>.Any, Arg<Animal[]>.Any)
            .Returns("matched_base");
        var sut = imposter.Instance();
        var inParam = true;
        var refParam = 1.5;
        var dogParams = new[] { new Dog(), new Dog() };

        // Act
        var result = sut.Print<string, int, bool, double, string, Dog>("", out _, in inParam, ref refParam, dogParams);

        // Assert
        result.ShouldBe("matched_base");
    }

    [Fact]
    public void SetupWithObjectType_PassedAnyType_SetupInvoked()
    {
        // Arrange
        var imposter = new GenericMethodPoc();
        var expectedResult = "matched_object";
        // Setup to match 'object' for the ordinary parameter
        imposter.Print<object, int, bool, double, string, object>(
                Arg<object>.Any,
                OutArg<int>.Any,
                Arg<bool>.Any,
                Arg<double>.Any,
                Arg<object[]>.Any)
            .Returns(expectedResult);

        var sut = imposter.Instance();

        var inParam = true;
        var refParam = 1.5;

        // Act
        // Call with various different types for the first generic parameter
        var resultForString = sut.Print<string, int, bool, double, string, object>(
            "a string", out _, in inParam, ref refParam, []);

        var resultForInt = sut.Print<int, int, bool, double, string, object>(
            123, out _, in inParam, ref refParam, []);

        var resultForCustomClass = sut.Print<Dog, int, bool, double, string, object>(
            new Dog(), out _, in inParam, ref refParam, []);

        // Assert
        // All calls should be caught by the 'object'-based setup
        resultForString.ShouldBe(expectedResult);
        resultForInt.ShouldBe(expectedResult);
        resultForCustomClass.ShouldBe(expectedResult);
    }

    [Fact]
    public void MultipleSetups_WithVaryingCompatibility_LastMatchingSetupIsUsedForMultipleCalls()
    {
        // Arrange
        var imposter = new GenericMethodPoc();
        var firstCompatibleResult = "first_compatible";
        var incompatibleResult = "incompatible";
        var lastCompatibleResult = "last_compatible";

        // Setup 1: A compatible setup using a base type.
        imposter.Print<Animal, int, bool, double, string, object>(
                Arg<Animal>.Any, OutArg<int>.Any, Arg<bool>.Any, Arg<double>.Any, Arg<object[]>.Any)
            .Returns(firstCompatibleResult);

        // Setup 2: An incompatible setup with a completely different type.
        imposter.Print<string, int, bool, double, string, object>(
                Arg<string>.Any, OutArg<int>.Any, Arg<bool>.Any, Arg<double>.Any, Arg<object[]>.Any)
            .Returns(incompatibleResult);

        // Setup 3: The last compatible setup, which should be used.
        imposter.Print<Animal, int, bool, double, string, object>(
                Arg<Animal>.Any, OutArg<int>.Any, Arg<bool>.Any, Arg<double>.Any, Arg<object[]>.Any)
            .Returns(lastCompatibleResult);

        var sut = imposter.Instance();
        var inParam = true;
        var refParam = 1.5;

        // Act
        // Call the method twice with a derived type that matches the compatible setups.
        var result1 = sut.Print<Dog, int, bool, double, string, object>(
            new Dog(), out _, in inParam, ref refParam, []);

        var result2 = sut.Print<Dog, int, bool, double, string, object>(
            new Dog(), out _, in inParam, ref refParam, []);

        // Assert
        // Both calls should be handled by the last matching setup.
        result1.ShouldBe(lastCompatibleResult);
        result2.ShouldBe(lastCompatibleResult);
    }

    [Fact]
    public void MultipleSetups_WithOneMatching_ShouldInvokeTheMatchingSetup()
    {
        // Arrange
        var imposter = new GenericMethodPoc();
        var compatibleResult = "compatible";
        var incompatibleResult = "incompatible";

        // Setup 1: A compatible setup using a base type 'Animal'.
        imposter.Print<Animal, int, bool, double, string, object>(
                Arg<Animal>.Any, OutArg<int>.Any, Arg<bool>.Any, Arg<double>.Any, Arg<object[]>.Any)
            .Returns(compatibleResult);

        // Setup 2: An incompatible setup using type 'string'.
        imposter.Print<string, int, bool, double, string, object>(
                Arg<string>.Any, OutArg<int>.Any, Arg<bool>.Any, Arg<double>.Any, Arg<object[]>.Any)
            .Returns(incompatibleResult);

        var sut = imposter.Instance();
        var inParam = true;
        var refParam = 1.5;

        // Act
        // Call the method with a 'Dog' type, which is compatible with the 'Animal' setup.
        var result = sut.Print<Dog, int, bool, double, string, object>(
            new Dog(), out _, in inParam, ref refParam, []);

        // Assert
        // The result should come from the first, compatible setup.
        result.ShouldBe(compatibleResult);
    }

    [Fact]
    public void SequentialSetups_WithCallbacks_AreInvokedInOrderAndLastSetupRepeats()
    {
        // Arrange
        var imposter = new GenericMethodPoc();
        var events = new List<string>();
        var firstResult = "first_call";
        var secondResult = "second_call";
        var thirdResult = "third_call";

        imposter.Print<Animal, int, bool, double, string, object>(
                Arg<Animal>.Any, OutArg<int>.Any, Arg<bool>.Any, Arg<double>.Any, Arg<object[]>.Any)
            // --- First call setup ---
            .CallBefore((_, out p1, in _, ref _, _) =>
            {
                events.Add("before1");
                p1 = 1;
            })
            .Returns(firstResult)
            .CallAfter((_, out p2, in _, ref _, _) =>
            {
                events.Add("after1");
                p2 = 2;
            })
            // --- Second call setup ---
            .CallBefore((_, out p3, in _, ref _, _) =>
            {
                events.Add("before2");
                p3 = 3;
            })
            .Returns(secondResult)
            .CallAfter((_, out p4, in _, ref _, _) =>
            {
                events.Add("after2");
                p4 = 4;
            })
            // --- Third call setup ---
            .CallBefore((_, out p5, in _, ref _, _) =>
            {
                events.Add("before3");
                p5 = 5;
            })
            .Returns(thirdResult)
            .CallAfter((_, out p6, in _, ref _, _) =>
            {
                events.Add("after3");
                p6 = 6;
            });

        var sut = imposter.Instance();
        var inParam = true;
        var refParam = 1.5;

        // Act
        var result1 = sut.Print<Dog, int, bool, double, string, object>(
            new Dog(), out var outValue1, in inParam, ref refParam, []);

        var result2 = sut.Print<Dog, int, bool, double, string, object>(
            new Dog(), out var outValue2, in inParam, ref refParam, []);

        var result3 = sut.Print<Dog, int, bool, double, string, object>(
            new Dog(), out var outValue3, in inParam, ref refParam, []);

        var result4 = sut.Print<Dog, int, bool, double, string, object>(
            new Dog(), out var outValue4, in inParam, ref refParam, []);

        // Assert
        // Check that the return values match the defined sequence
        result1.ShouldBe(firstResult);
        result2.ShouldBe(secondResult);
        result3.ShouldBe(thirdResult);
        result4.ShouldBe(thirdResult); // The fourth call should repeat the last setup

        // The final 'out' value is set by the last delegate in the chain, which is CallAfter
        outValue1.ShouldBe(2);
        outValue2.ShouldBe(4);
        outValue3.ShouldBe(6);
        outValue4.ShouldBe(6); // from the repeated last setup

        // Check that all callbacks were fired in the correct order
        var expectedEvents = new[]
        {
            "before1", "after1",
            "before2", "after2",
            "before3", "after3",
            "before3", "after3" // Last setup repeated for the 4th call
        };
        events.ShouldBe(expectedEvents);
    }

    [Fact]
    public void RefParameterModifiedByCompatibleSetup_ShouldBePropagatedToCaller()
    {
        // Arrange
        var imposter = new GenericMethodPoc();
        var newAnimal = new Animal { Name = "New" };

        imposter.Print<Animal, int, bool, Animal, string, object>(
                Arg<Animal>.Any,
                OutArg<int>.Any,
                Arg<bool>.Any,
                Arg<Animal>.Any,
                Arg<object[]>.Any)
            .Returns((Animal p1, out int outParam, in bool p3, ref Animal refParam, object[] p5) =>
            {
                refParam = newAnimal;
                outParam = 123;

                return "result from base setup";
            });

        var sut = imposter.Instance();
        var inParam = true;
        var refParam = new Animal { Name = "Old" };

        // Act
        // Invoke the method with the 'Dog' derived type. The compatible 'Animal' setup should be used.
        sut.Print<Dog, int, bool, Animal, string, object>(
            new Dog(), out var outParam, in inParam, ref refParam, []);

        // Assert
        // The change to the ref parameter made in the 'Animal' setup should be propagated
        // back to the 'refParam' variable in the caller's scope.
        refParam.ShouldBeSameAs(newAnimal);
    }

    [Fact]
    public void OutParameter_IsOverwrittenByEachDelegateInChain()
    {
        // Arrange
        var imposter = new GenericMethodPoc();

        // Setup a complete chain of delegates, each setting a different value for the 'out' parameter.
        imposter.Print<Animal, int, bool, double, string, object>(
                Arg<Animal>.Any, OutArg<int>.Any, Arg<bool>.Any, Arg<double>.Any, Arg<object[]>.Any)
            .CallBefore((_, out p1, in _, ref _, _) =>
            {
                // This value should be overwritten.
                p1 = 1;
            })
            .Returns((_, out p2, in _, ref _, _) =>
            {
                // This value should also be overwritten.
                p2 = 2;
                return "result";
            })
            .CallAfter((_, out p3, in _, ref _, _) =>
            {
                // This is the final value that should be propagated to the caller.
                p3 = 3;
            });

        var sut = imposter.Instance();
        var inParam = true;
        var refParam = 1.5;

        // Act
        sut.Print<Dog, int, bool, double, string, object>(
            new Dog(), out var finalOutValue, in inParam, ref refParam, System.Array.Empty<object>());

        // Assert
        // The final value of the out parameter should be the one set by the last delegate in the chain (CallAfter).
        finalOutValue.ShouldBe(3);
    }
}