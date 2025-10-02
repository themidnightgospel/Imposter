using System;
using Imposter.Abstractions;
using Machine.Specifications;
using Shouldly;

namespace Imposter.CodeGenerator.Tests.TypeCriteriaTests.IsCriteriaTests;

[Subject(nameof(TypeCriteria))]
public class IsNestedIntCriteria
{
    public static TypeCriteria typeCriteria;

    Because TypeCriteriaCreated = () => 
        typeCriteria = TypeCriteria.Create<ICurrentInterface<ArgType.Is<int>>>();

    It ShouldMatchWhenInnerTypeIsExactInt = () => 
        typeCriteria.Matches(typeof(ICurrentInterface<int>)).ShouldBeTrue();
    
    It ShouldNotMatchWhenInnerTypeIsString = () => 
        typeCriteria.Matches(typeof(ICurrentInterface<string>)).ShouldBeFalse();
    
    It ShouldNotMatchWhenInnerTypeIsObject = () => 
        typeCriteria.Matches(typeof(ICurrentInterface<object>)).ShouldBeFalse();
    
    public class BaseClass
    {
    }

    public class DerivedClass : BaseClass, IDisposable
    {
        public void Dispose()
        {
        }
    }

    public class UnrelatedClass
    {
    }
    
    public interface ICurrentInterface<T>
    {
    }
}