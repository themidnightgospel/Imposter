using System;
using Imposter.Abstractions;
using Machine.Specifications;
using Shouldly;

namespace Imposter.CodeGenerator.Tests.TypeCriteriaTests.IsCriteriaTests;

[Subject(nameof(TypeCriteria))]
public class IsBaseClassCriteria
{
    public static TypeCriteria typeCriteria;

    Because TypeCriteriaCreated = () =>
        typeCriteria = TypeCriteria.Create<ArgType.Is<BaseClass>>();

    It ShouldMatchBaseClassItself = () =>
        typeCriteria.Matches(typeof(BaseClass)).ShouldBeTrue();

    It ShouldNotMatchDerivedClass = () =>
        typeCriteria.Matches(typeof(DerivedClass)).ShouldBeFalse();

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

    public class UnrelatedClass
    {
    }
}