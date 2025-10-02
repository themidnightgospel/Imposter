using System;
using Imposter.Abstractions;
using Machine.Specifications;
using Shouldly;

namespace Imposter.CodeGenerator.Tests.TypeCriteriaTests.IsCriteriaTests;


[Subject(nameof(TypeCriteria))]
public class IsIDisposableCriteria
{
    public static TypeCriteria typeCriteria;

    Because TypeCriteriaCreated = () => 
        typeCriteria = TypeCriteria.Create<ArgType.Is<IDisposable>>();

    It ShouldMatchInterfaceItself = () => 
        typeCriteria.Matches(typeof(IDisposable)).ShouldBeTrue();
    
    It ShouldNotMatchImplementingClass = () => 
        typeCriteria.Matches(typeof(DerivedClass)).ShouldBeFalse();
    
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
}