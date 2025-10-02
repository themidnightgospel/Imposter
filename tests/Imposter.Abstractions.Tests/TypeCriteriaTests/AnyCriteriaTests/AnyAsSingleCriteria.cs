using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Imposter.Abstractions;
using Machine.Specifications;
using Shouldly;

namespace Imposter.CodeGenerator.Tests.TypeCriteriaTests.AnyCriteriaTests;

[Subject(nameof(TypeCriteria))]
public class AnyAsSingleCriteria
{
    public static TypeCriteria typeCriteria;

    Because TypeCriteriaCreated = () => typeCriteria = TypeCriteria.Create<ArgType.Any>();

    It ShouldMatchValueType = () => typeCriteria.Matches(typeof(int)).ShouldBeTrue();

    It ShouldMatchRerenceType = () => typeCriteria.Matches(typeof(Regex)).ShouldBeTrue();
    
    It ShouldMatchInterfaceType = () => typeCriteria.Matches(typeof(IDisposable)).ShouldBeTrue();

    It ShouldMatchOpenGenericType = () => typeCriteria.Matches(typeof(List<>)).ShouldBeTrue();

    It ShouldMatchClosedGenericType = () => typeCriteria.Matches(typeof(List<string>)).ShouldBeTrue();

    It ShouldMatchNullableValueType = () => typeCriteria.Matches(typeof(int?)).ShouldBeTrue();

    It ShouldMatchArrayType = () => typeCriteria.Matches(typeof(string[])).ShouldBeTrue();

    It ShouldMatchObjectType = () => typeCriteria.Matches(typeof(object)).ShouldBeTrue();

    It ShouldMatchDelegateType = () => typeCriteria.Matches(typeof(Action)).ShouldBeTrue();
}