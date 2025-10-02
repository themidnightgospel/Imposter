using Imposter.Abstractions;
using Machine.Specifications;
using Shouldly;

namespace Imposter.CodeGenerator.Tests.TypeCriteriaTests.IsAssignableToTests;

[Subject(nameof(TypeCriteria))]
public class IsAssignableToNestedCriteria
{
    public static TypeCriteria typeCriteria;

    Because TypeCriteriaCreated = () =>
        typeCriteria = TypeCriteria.Create<ICurrentInterface<ArgType.IsAssignableTo<BaseClass>>>();

    It ShouldMatchWhenInnerTypeIsDerived = () =>
        typeCriteria.Matches(typeof(ICurrentInterface<DerivedClass>)).ShouldBeTrue();

    It ShouldMatchWhenInnerTypeIsSelf = () =>
        typeCriteria.Matches(typeof(ICurrentInterface<BaseClass>)).ShouldBeTrue();

    It ShouldFailWhenInnerTypeIsUnrelated = () =>
        typeCriteria.Matches(typeof(ICurrentInterface<IsAssignableToBaseClassCriteria.UnrelatedClass>)).ShouldBeFalse();

    It ShouldFailWhenInnerTypeIsInverse = () =>
    {
        var derivedCriteria = TypeCriteria.Create<ICurrentInterface<ArgType.IsAssignableTo<DerivedClass>>>();
        derivedCriteria.Matches(typeof(ICurrentInterface<BaseClass>)).ShouldBeFalse();
    };

    public class BaseClass
    {
    }

    public class DerivedClass : BaseClass
    {
    }

    public class UnrelatedClass
    {
    }

    public interface ICurrentInterface<T>
    {
    }
}