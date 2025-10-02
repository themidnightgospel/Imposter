using Imposter.Abstractions;
using Machine.Specifications;
using Shouldly;

namespace Imposter.CodeGenerator.Tests.TypeCriteriaTests.IsAssignableToTests;

[Subject(nameof(TypeCriteria))]
public class IsAssignableToBaseClassArrayCriteria
{
    public static TypeCriteria typeCriteria;

    Because TypeCriteriaCreated = () =>
        typeCriteria = TypeCriteria.Create<ArgType.IsAssignableTo<BaseClass[]>>();

    It ShouldMatchArrayCovariance = () =>
        typeCriteria.Matches(typeof(DerivedClass[])).ShouldBeTrue();
    
    It ShouldMatchSelf = () =>
        typeCriteria.Matches(typeof(BaseClass[])).ShouldBeTrue();

    public class BaseClass
    {
    }

    public class DerivedClass : BaseClass
    {
    }
}