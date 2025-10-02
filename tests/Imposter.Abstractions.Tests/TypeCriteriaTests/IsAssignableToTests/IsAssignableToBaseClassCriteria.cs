using System;
using Imposter.Abstractions;
using Machine.Specifications;
using Shouldly;

namespace Imposter.CodeGenerator.Tests.TypeCriteriaTests.IsAssignableToTests;

[Subject(nameof(TypeCriteria))]
public class IsAssignableToBaseClassCriteria
{
    public static TypeCriteria typeCriteria;

    Because TypeCriteriaCreated = () =>
        typeCriteria = TypeCriteria.Create<ArgType.IsAssignableTo<BaseClass>>();

    It ShouldMatchDerivedClass = () =>
        typeCriteria.Matches(typeof(DerivedClass)).ShouldBeTrue();

    It ShouldMatchBaseClassItself = () =>
        typeCriteria.Matches(typeof(BaseClass)).ShouldBeTrue();


    It ShouldMatchIndirectSubclass = () =>
        typeCriteria.Matches(typeof(SecondDerived)).ShouldBeTrue();

    It ShouldNotMatchUnrelatedClass = () =>
        typeCriteria.Matches(typeof(UnrelatedClass)).ShouldBeFalse();

    public class BaseClass
    {
    }

    public class DerivedClass : BaseClass, IDisposable
    {
        public void Dispose()
        {
        }
    }

    class SecondDerived : DerivedClass
    {
    }

    public class UnrelatedClass
    {
    }
}