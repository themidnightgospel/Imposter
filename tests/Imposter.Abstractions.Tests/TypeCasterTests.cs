using System;
using System.Collections.Generic;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests;

public class TypeCasterTests
{
    private interface IAnimal { }

    private class Animal : IAnimal { }

    private class Dog : Animal { }

    [Fact]
    public void GivenReferenceTypeUpcast_WhenCasting_ShouldSucceed()
    {
        var dog = new Dog();
        TypeCaster.Cast<Dog, Animal>(dog).ShouldBeSameAs(dog);
    }

    [Fact]
    public void GivenValidDowncast_WhenCasting_ShouldReturnSameInstance()
    {
        Animal animal = new Dog();
        TypeCaster.Cast<Animal, Dog>(animal).ShouldBeSameAs(animal);
    }

    [Fact]
    public void GivenConcreteType_WhenCastingToInterface_ShouldSucceed()
    {
        var dog = new Dog();
        TypeCaster.Cast<Dog, IAnimal>(dog).ShouldBeSameAs(dog);
    }

    [Fact]
    public void GivenNullReference_WhenCastingToReferenceType_ShouldReturnNull()
    {
        TypeCaster.Cast<string, object>(null!).ShouldBeNull();
    }

    [Fact]
    public void GivenNullValue_WhenCastingToNullableValueType_ShouldReturnNull()
    {
        TypeCaster.Cast<object, int?>(null!).ShouldBeNull();
    }

    [Fact]
    public void GivenStringToNumberCast_WhenCasting_ShouldThrowInvalidCast()
    {
        Should.Throw<InvalidCastException>(() => TypeCaster.Cast<string, int>("123"));
    }

    [Fact]
    public void GivenWideningNumericConversion_WhenCasting_ShouldThrowInvalidCast()
    {
        Should.Throw<InvalidCastException>(() => TypeCaster.Cast<int, double>(42));
    }

    [Fact]
    public void GivenNarrowingNumericConversion_WhenCasting_ShouldThrowInvalidCast()
    {
        Should.Throw<InvalidCastException>(() => TypeCaster.Cast<double, int>(42.0));
    }

    [Fact]
    public void GivenInvalidDowncast_WhenCasting_ShouldThrow()
    {
        Animal animal = new Animal();
        Should.Throw<InvalidCastException>(() => TypeCaster.Cast<Animal, Dog>(animal));
    }

    [Fact]
    public void GivenUnrelatedReferenceTypes_WhenCasting_ShouldThrow()
    {
        var list = new List<int>();
        Should.Throw<InvalidCastException>(() => TypeCaster.Cast<List<int>, Dog>(list));
    }

    [Fact]
    public void GivenInvalidString_WhenCastingToNumber_ShouldThrow()
    {
        Should.Throw<InvalidCastException>(() => TypeCaster.Cast<string, int>("abc"));
    }

    [Fact]
    public void GivenNarrowingConversionWithDataLoss_WhenCasting_ShouldThrow()
    {
        Should.Throw<InvalidCastException>(() => TypeCaster.Cast<double, int>(42.5));
    }

    [Fact]
    public void GivenNullValue_WhenCastingToNonNullableValueType_ShouldThrow()
    {
        Should.Throw<InvalidCastException>(() => TypeCaster.Cast<object, int>(null!));
    }

    [Fact]
    public void GivenReferenceTypeUpcast_WhenTryCasting_ShouldReturnTrue()
    {
        var dog = new Dog();
        TypeCaster.TryCast<Dog, Animal>(dog, out var result).ShouldBeTrue();
        result.ShouldBeSameAs(dog);
    }

    [Fact]
    public void GivenStringToNumber_WhenTryCasting_ShouldReturnFalse()
    {
        TypeCaster.TryCast("123", out int _).ShouldBeFalse();
    }

    [Fact]
    public void GivenWideningNumericConversion_WhenTryCasting_ShouldReturnFalse()
    {
        TypeCaster.TryCast(42, out double _).ShouldBeFalse();
    }

    [Fact]
    public void GivenNullValue_WhenTryCastingToNullableValueType_ShouldReturnTrue()
    {
        TypeCaster.TryCast<object, int?>(null!, out var result).ShouldBeTrue();
        result.ShouldBeNull();
    }

    [Fact]
    public void GivenInvalidDowncast_WhenTryCasting_ShouldReturnFalse()
    {
        Animal animal = new Animal();
        TypeCaster.TryCast<Animal, Dog>(animal, out var result).ShouldBeFalse();
        result.ShouldBeNull();
    }

    [Fact]
    public void GivenInvalidString_WhenTryCastingToNumber_ShouldReturnFalse()
    {
        TypeCaster.TryCast("abc", out int result).ShouldBeFalse();
        result.ShouldBe(default);
    }

    [Fact]
    public void GivenNarrowingConversionWithDataLoss_WhenTryCasting_ShouldReturnFalse()
    {
        TypeCaster.TryCast(42.5, out int result).ShouldBeFalse();
        result.ShouldBe(default);
    }

    [Fact]
    public void GivenNullValue_WhenTryCastingToNonNullableValueType_ShouldReturnFalse()
    {
        TypeCaster.TryCast<object, int>(null!, out var result).ShouldBeFalse();
        result.ShouldBe(default);
    }

    [Fact]
    public void GivenUnrelatedTypes_WhenTryCasting_ShouldReturnFalse()
    {
        var guid = Guid.NewGuid();
        TypeCaster.TryCast<Guid, DateTime>(guid, out var result).ShouldBeFalse();
        result.ShouldBe(default);
    }
}
