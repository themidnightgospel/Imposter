using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Imposter.Abstractions;
using Machine.Specifications;
using Shouldly;

namespace Imposter.CodeGenerator.Tests.TypeCriteriaTests.AnyCriteriaTests;

[Subject(nameof(TypeCriteria))]
public class AnyAsGenericClassArgument
{
    public static TypeCriteria typeCriteria;

    Because TypeCriteriaCreated = () => typeCriteria = TypeCriteria.Create<List<ArgType.Any>>();

    It ShouldMatchAnyInnerType = () => typeCriteria.Matches(typeof(List<Regex>)).ShouldBeTrue();

    It ShouldMatchAnyInnerValueType = () => typeCriteria.Matches(typeof(List<int>)).ShouldBeTrue();

    It ShouldNotMatchDifferentOuterType = () => typeCriteria.Matches(typeof(Array)).ShouldBeFalse();

    It ShouldMatchInnerInterfaceType = () => typeCriteria.Matches(typeof(List<IDisposable>)).ShouldBeTrue();

    It ShouldMatchInnerArrayType = () => typeCriteria.Matches(typeof(List<double[]>)).ShouldBeTrue();

    It ShouldMatchInnerClosedGenericType = () => typeCriteria.Matches(typeof(List<List<Regex>>)).ShouldBeTrue();
    
    It ShouldMatchInnerClosedmMultiLevelGenericType = () => typeCriteria.Matches(typeof(List<List<List<List<Regex>>>>)).ShouldBeTrue();

    It ShouldMatchInnerNullableType = () => typeCriteria.Matches(typeof(List<decimal?>)).ShouldBeTrue();

    It ShouldMatchInnerObjectType = () => typeCriteria.Matches(typeof(List<object>)).ShouldBeTrue();

    It ShouldNotMatchUnrelatedClosedGenericType = () => typeCriteria.Matches(typeof(ConcurrentBag<int>)).ShouldBeFalse();

    It ShouldNotMatchOpenGenericType = () => typeCriteria.Matches(typeof(List<>)).ShouldBeFalse();

    It ShouldMatchWhenInterfaceIsOnBaseType = () => typeCriteria.Matches(typeof(DerivedFromCurrent)).ShouldBeTrue();

    class DerivedFromCurrent : List<string>
    {
    }

    interface ICurrentInterface<T>
    {
    }

    interface IAnotherInterface
    {
    }
}