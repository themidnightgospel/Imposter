using Imposter.Abstractions;
using Machine.Specifications;
using Shouldly;

namespace Imposter.CodeGenerator.Tests.TypeCriteriaTests;

[Subject(nameof(TypeCriteria))]
public class WhenCreatingWithIsPredicate
{
    public static TypeCriteria typeCriteria;

    Because TypeCriteriaCreated = () => typeCriteria = TypeCriteria.Create<ArgType.Is<ICurrentInterface>>();

    It ShouldMatchSelf = () => typeCriteria.Matches(typeof(ICurrentInterface)).ShouldBeTrue();

    It ShouldNotMatchChildClass = () => typeCriteria.Matches(typeof(ChildClass)).ShouldBeFalse();

    It ShouldNotMatchChildInterface = () => typeCriteria.Matches(typeof(IChildInterface)).ShouldBeFalse();

    It ShouldNotMatchParentInterface = () => typeCriteria.Matches(typeof(IParentInterface)).ShouldBeFalse();

    interface IParentInterface
    {
    }

    interface ICurrentInterface : IParentInterface
    {
    }

    class ChildClass : ICurrentInterface
    {
    }

    interface IChildInterface : ICurrentInterface
    {
    }
}