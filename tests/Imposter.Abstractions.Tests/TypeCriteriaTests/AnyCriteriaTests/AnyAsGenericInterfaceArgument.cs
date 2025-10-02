using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Imposter.Abstractions;
using Machine.Specifications;
using Shouldly;

namespace Imposter.CodeGenerator.Tests.TypeCriteriaTests.AnyCriteriaTests;

[Subject(nameof(TypeCriteria))]
public class AnyAsGenericInterfaceArgument
{
    public static TypeCriteria typeCriteria;

    Because TypeCriteriaCreated = () => typeCriteria = TypeCriteria.Create<ICurrentInterface<ArgType.Any>>();

    It ShouldMatchAnyInnerType = () => typeCriteria.Matches(typeof(ICurrentInterface<Regex>)).ShouldBeTrue();

    It ShouldMatchAnyInnerValueType = () => typeCriteria.Matches(typeof(ICurrentInterface<int>)).ShouldBeTrue();

    It ShouldNotMatchDifferentOuterType = () => typeCriteria.Matches(typeof(IAnotherInterface)).ShouldBeFalse();

    It ShouldMatchInnerInterfaceType = () => typeCriteria.Matches(typeof(ICurrentInterface<IDisposable>)).ShouldBeTrue();

    It ShouldMatchInnerArrayType = () => typeCriteria.Matches(typeof(ICurrentInterface<double[]>)).ShouldBeTrue();

    It ShouldMatchInnerClosedGenericType = () => typeCriteria.Matches(typeof(ICurrentInterface<List<BaseClass>>)).ShouldBeTrue();
    
    It ShouldMatchInnerClosedmMultiLevelGenericType = () => typeCriteria.Matches(typeof(ICurrentInterface<List<List<List<BaseClass>>>>)).ShouldBeTrue();

    It ShouldMatchInnerNullableType = () => typeCriteria.Matches(typeof(ICurrentInterface<decimal?>)).ShouldBeTrue();

    It ShouldMatchInnerObjectType = () => typeCriteria.Matches(typeof(ICurrentInterface<object>)).ShouldBeTrue();

    It ShouldNotMatchUnrelatedClosedGenericType = () => typeCriteria.Matches(typeof(List<int>)).ShouldBeFalse();

    It ShouldNotMatchOpenGenericType = () => typeCriteria.Matches(typeof(ICurrentInterface<>)).ShouldBeFalse();

    It ShouldMatchWhenInterfaceIsOnBaseType = () => typeCriteria.Matches(typeof(DerivedFromCurrent)).ShouldBeTrue();

    class BaseClass
    {
    }

    class DerivedFromCurrent : BaseClass, ICurrentInterface<string>
    {
    }

    interface ICurrentInterface<T>
    {
    }

    interface IAnotherInterface
    {
    }
}