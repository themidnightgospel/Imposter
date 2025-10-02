using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Imposter.Abstractions;
using Machine.Specifications;
using Shouldly;

namespace Imposter.CodeGenerator.Tests.TypeCriteriaTests.AnyCriteriaTests;

[Subject(nameof(TypeCriteria))]
public class AnyAsMultiArgumentGenericClassArgument
{
    public static TypeCriteria typeCriteria;

    Because TypeCriteriaCreated = () => typeCriteria = TypeCriteria.Create<Dictionary<string, ArgType.Any>>();

    It ShouldMatchWhenValueIsReferenceType = () => typeCriteria.Matches(typeof(Dictionary<string, Regex>)).ShouldBeTrue();

    It ShouldMatchWhenValueIsValueType = () =>
        typeCriteria.Matches(typeof(Dictionary<string, int>)).ShouldBeTrue();

    It ShouldMatchWhenValueIsInterfaceType = () =>
        typeCriteria.Matches(typeof(Dictionary<string, IDisposable>)).ShouldBeTrue();

    It ShouldMatchWhenValueIsObjectType = () =>
        typeCriteria.Matches(typeof(Dictionary<string, object>)).ShouldBeTrue();

    It ShouldMatchWhenValueIsNestedGeneric = () =>
        typeCriteria.Matches(typeof(Dictionary<string, List<BaseClass>>)).ShouldBeTrue();

    It ShouldMatchWhenValueIsAnArray = () =>
        typeCriteria.Matches(typeof(Dictionary<string, int[]>)).ShouldBeTrue();

    It ShouldMatchWhenGenericTypeIsImplementedByBaseClass = () =>
        typeCriteria.Matches(typeof(DerivedDictionary)).ShouldBeTrue();

    It ShouldNotMatchWhenKeyTypeIsIncorrect = () => typeCriteria.Matches(typeof(Dictionary<int, string>)).ShouldBeFalse();

    It ShouldNotMatchWhenBothTypesAreDifferent = () => typeCriteria.Matches(typeof(Dictionary<int, bool>)).ShouldBeFalse();

    It ShouldNotMatchDifferentOuterGenericType = () => typeCriteria.Matches(typeof(List<string>)).ShouldBeFalse();

    It ShouldNotMatchOpenGenericTypeDefinition = () => typeCriteria.Matches(typeof(Dictionary<,>)).ShouldBeFalse();

     It ShouldNotMatchTypeWithWrongNumberOfArguments = () => typeCriteria.Matches(typeof(Tuple<string>)).ShouldBeFalse();

    class BaseClass
    {
    }

    class DerivedClass : BaseClass
    {
    }

    class DerivedDictionary : Dictionary<string, DerivedClass>
    {
    }
}