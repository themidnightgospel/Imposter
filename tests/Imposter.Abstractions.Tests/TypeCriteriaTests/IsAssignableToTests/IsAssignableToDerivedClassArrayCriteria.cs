using Imposter.Abstractions;
using Machine.Specifications;
using Shouldly;

namespace Imposter.CodeGenerator.Tests.TypeCriteriaTests.IsAssignableToTests;

[Subject(nameof(TypeCriteria))]
public class IsAssignableToDerivedClassArrayCriteria
{
    public static TypeCriteria typeCriteria;

    Because TypeCriteriaCreated = () =>
        typeCriteria = TypeCriteria.Create<ArgType.IsAssignableTo<DerivedClass[]>>();

    It ShouldNotMatchInverseArrayCovariance = () =>
        typeCriteria.Matches(typeof(BaseClass[])).ShouldBeFalse();

    public class BaseClass
    {
    }

    public class DerivedClass : BaseClass
    {
    }
}