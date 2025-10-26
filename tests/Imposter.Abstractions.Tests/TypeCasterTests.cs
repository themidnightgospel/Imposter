using System;
using System.Collections.Generic;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests;

public class TypeCasterTests
{
    private interface IAnimal
    {
    }

    private class Animal : IAnimal
    {
    }

    private class Dog : Animal
    {
    }

    [Fact]
    public void Cast_WhenUpcastingReferenceType_ShouldSucceed()
    {
        var dog = new Dog();
        TypeCaster.Cast<Dog, Animal>(dog).ShouldBeSameAs(dog);
    }

    [Fact]
    public void Cast_WhenDowncastingValidReferenceType_ShouldSucceed()
    {
        Animal animal = new Dog();
        TypeCaster.Cast<Animal, Dog>(animal).ShouldBeSameAs(animal);
    }

    [Fact]
    public void Cast_ToInterface_ShouldSucceed()
    {
        var dog = new Dog();
        TypeCaster.Cast<Dog, IAnimal>(dog).ShouldBeSameAs(dog);
    }

    [Fact]
    public void Cast_NullToReferenceType_ShouldReturnNull()
    {
        TypeCaster.Cast<string, object>(null).ShouldBeNull();
    }

    [Fact]
    public void Cast_NullToNullableValueType_ShouldReturnNull()
    {
        TypeCaster.Cast<object, int?>(null).ShouldBeNull();
    }

    [Fact]
    public void Cast_StringToNumber_ShouldFail()
    {
        Should.Throw<InvalidCastException>(() => TypeCaster.Cast<string, int>("123"));
    }

    [Fact]
    public void Cast_WideningNumericConversion_ShouldFail()
    {
        Should.Throw<InvalidCastException>(() => TypeCaster.Cast<int, double>(42));
    }

    [Fact]
    public void Cast_NarrowingNumericConversion_WhenValid_ShouldSucceed()
    {
        Should.Throw<InvalidCastException>(() => TypeCaster.Cast<double, int>(42.0));
    }

    [Fact]
    public void Cast_WhenDowncastingInvalidReferenceType_ShouldThrow()
    {
        Animal animal = new Animal();
        Should.Throw<InvalidCastException>(() => TypeCaster.Cast<Animal, Dog>(animal));
    }

    [Fact]
    public void Cast_UnrelatedReferenceTypes_ShouldThrow()
    {
        var list = new List<int>();
        Should.Throw<InvalidCastException>(() => TypeCaster.Cast<List<int>, Dog>(list));
    }

    [Fact]
    public void Cast_InvalidStringToNumber_ShouldThrow()
    {
        Should.Throw<InvalidCastException>(() => TypeCaster.Cast<string, int>("abc"));
    }

    [Fact]
    public void Cast_NarrowingNumericConversion_WhenDataLoss_ShouldThrow()
    {
        Should.Throw<InvalidCastException>(() => TypeCaster.Cast<double, int>(42.5));
    }

    [Fact]
    public void Cast_NullToNonNullableValueType_ShouldThrow()
    {
        Should.Throw<InvalidCastException>(() => TypeCaster.Cast<object, int>(null));
    }

    [Fact]
    public void TryCast_WhenUpcastingReferenceType_ShouldReturnTrue()
    {
        var dog = new Dog();
        TypeCaster.TryCast<Dog, Animal>(dog, out var result).ShouldBeTrue();
        result.ShouldBeSameAs(dog);
    }

    [Fact]
    public void TryCast_StringToNumber_ShouldReturnFalse()
    {
        TypeCaster.TryCast("123", out int _).ShouldBeFalse();
    }

    [Fact]
    public void TryCast_WideningNumericConversion_ShouldReturnTrue()
    {
        TypeCaster.TryCast(42, out double _).ShouldBeFalse();
    }

    [Fact]
    public void TryCast_NullToNullableValueType_ShouldReturnTrue()
    {
        TypeCaster.TryCast<object, int?>(null, out var result).ShouldBeTrue();
        result.ShouldBeNull();
    }

    [Fact]
    public void TryCast_WhenDowncastingInvalidReferenceType_ShouldReturnFalse()
    {
        Animal animal = new Animal();
        TypeCaster.TryCast<Animal, Dog>(animal, out var result).ShouldBeFalse();
        result.ShouldBeNull();
    }

    [Fact]
    public void TryCast_InvalidStringToNumber_ShouldReturnFalse()
    {
        TypeCaster.TryCast("abc", out int result).ShouldBeFalse();
        result.ShouldBe(default);
    }

    [Fact]
    public void TryCast_NarrowingNumericConversion_WhenDataLoss_ShouldReturnFalse()
    {
        TypeCaster.TryCast(42.5, out int result).ShouldBeFalse();
        result.ShouldBe(default);
    }

    [Fact]
    public void TryCast_NullToNonNullableValueType_ShouldReturnFalse()
    {
        TypeCaster.TryCast<object, int>(null, out var result).ShouldBeFalse();
        result.ShouldBe(default);
    }

    [Fact]
    public void TryCast_UnrelatedTypes_ShouldReturnFalse()
    {
        var guid = Guid.NewGuid();
        TypeCaster.TryCast<Guid, DateTime>(guid, out var result).ShouldBeFalse();
        result.ShouldBe(default);
    }
}