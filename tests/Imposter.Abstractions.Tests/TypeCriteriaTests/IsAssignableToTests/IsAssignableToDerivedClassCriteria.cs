using Imposter.Abstractions;
using Machine.Specifications;
using Shouldly;

namespace Imposter.CodeGenerator.Tests.TypeCriteriaTests.IsAssignableToTests;

[Subject(nameof(TypeCriteria))]
public class IsAssignableToDerivedClassCriteria
{
    public static TypeCriteria typeCriteria;

    Because TypeCriteriaCreated = () =>
        typeCriteria = TypeCriteria.Create<ArgType.IsAssignableTo<DerivedClass>>();

    It ShouldNotMatchBaseClass = () =>
        typeCriteria.Matches(typeof(BaseClass)).ShouldBeFalse();

    public class BaseClass
    {
    }

    public class DerivedClass : BaseClass
    {
    }
}