using Imposter.Abstractions;
using Machine.Specifications;
using Shouldly;

namespace Imposter.CodeGenerator.Tests.TypeCriteriaTests;

[Subject(nameof(TypeCriteria))]
public class WhenCreatingWithIsSubClassPredicate
{
    public static TypeCriteria typeCriteria;

    Because TypeCriteriaCreated = () => typeCriteria = TypeCriteria.Create<ArgType.IsSubclassOf<CurrentClass>>();

    It ShouldNotMatchSelf = () => typeCriteria.Matches(typeof(CurrentClass)).ShouldBeFalse();

    It ShouldMatchChildClass = () => typeCriteria.Matches(typeof(ChildClass)).ShouldBeTrue();

    It ShouldMatchGrandChildClass = () => typeCriteria.Matches(typeof(GrandChildClass)).ShouldBeTrue();

    It ShouldNotMatchParentClass = () => typeCriteria.Matches(typeof(ParentClass)).ShouldBeFalse();

    class ParentClass
    {
    }

    class CurrentClass : ParentClass
    {
    }

    class ChildClass : CurrentClass
    {
    }

    class GrandChildClass : ChildClass
    {
    }
}