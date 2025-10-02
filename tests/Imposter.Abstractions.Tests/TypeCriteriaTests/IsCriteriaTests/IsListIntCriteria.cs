using System.Collections.Generic;
using Imposter.Abstractions;
using Machine.Specifications;
using Shouldly;

namespace Imposter.CodeGenerator.Tests.TypeCriteriaTests.IsCriteriaTests;

[Subject(nameof(TypeCriteria))]
public class IsListIntCriteria
{
    public static TypeCriteria typeCriteria;

    Because TypeCriteriaCreated = () => 
        typeCriteria = TypeCriteria.Create<ArgType.Is<List<int>>>();

    It ShouldMatchExactGenericType = () => 
        typeCriteria.Matches(typeof(List<int>)).ShouldBeTrue();
    
    It ShouldNotMatchDifferentGenericArgument = () => 
        typeCriteria.Matches(typeof(List<string>)).ShouldBeFalse();
    
    It ShouldNotMatchOpenGeneric = () => 
        typeCriteria.Matches(typeof(List<>)).ShouldBeFalse();
}