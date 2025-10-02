using System;
using Imposter.Abstractions;
using Machine.Specifications;
using Shouldly;

namespace Imposter.CodeGenerator.Tests.TypeCriteriaTests.IsAssignableToTests;

[Subject(nameof(TypeCriteria))]
public class IsAssignableToIDisposableCriteria
{
    public static TypeCriteria typeCriteria;

    Because TypeCriteriaCreated = () => 
        typeCriteria = TypeCriteria.Create<ArgType.IsAssignableTo<IDisposable>>();
    
    It ShouldMatchInterfaceImplementation = () =>
        typeCriteria.Matches(typeof(DerivedClass)).ShouldBeTrue();
    
    It ShouldMatchDerivedInterface = () =>
        typeCriteria.Matches(typeof(IDerivedInterface)).ShouldBeTrue();
    
    It ShouldMatchInterfaceItself = () =>
        typeCriteria.Matches(typeof(IDisposable)).ShouldBeTrue();
    
    It ShouldNotMatchUnrelatedClass = () =>
        typeCriteria.Matches(typeof(UnrelatedClass)).ShouldBeFalse();
    
    It ShouldNotMatchUnrelatedInterface = () =>
        typeCriteria.Matches(typeof(IUnrelatedClass)).ShouldBeFalse();
    
    public class BaseClass
    {
    }

    public class DerivedClass : BaseClass, IDisposable
    {
        public void Dispose()
        {
        }
    }

    public interface IDerivedInterface : IDisposable
    { }

    class SecondDerived : DerivedClass
    {
    }

    public class UnrelatedClass
    {
    }

    public interface IUnrelatedClass
    {
    }
}