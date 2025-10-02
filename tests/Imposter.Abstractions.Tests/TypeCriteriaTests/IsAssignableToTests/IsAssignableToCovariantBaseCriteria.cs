using Imposter.Abstractions;
using Machine.Specifications;
using Shouldly;

namespace Imposter.CodeGenerator.Tests.TypeCriteriaTests.IsAssignableToTests;

[Subject(nameof(TypeCriteria))]
public class IsAssignableToCovariantBaseCriteria
{
    public static TypeCriteria typeCriteria;

    Because TypeCriteriaCreated = () =>
        typeCriteria = TypeCriteria.Create<ArgType.IsAssignableTo<ICovariant<BaseClass>>>();

    It ShouldMatchWithCovariance = () =>
        typeCriteria.Matches(typeof(ICovariant<DerivedClass>)).ShouldBeTrue();

    It ShouldMatchWithCovarianceOnConcreteClass = () =>
        typeCriteria.Matches(typeof(CovariantImpl)).ShouldBeTrue();
    
    public interface ICovariant<out T> { }

    class CovariantImpl : ICovariant<DerivedClass> { }
    
    public class BaseClass
    {
    }

    public class DerivedClass : BaseClass
    {
    }

}
