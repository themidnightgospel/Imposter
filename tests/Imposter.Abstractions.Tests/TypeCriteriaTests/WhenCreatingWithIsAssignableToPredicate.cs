using System.Net.Http;
using Imposter.Abstractions;
using Machine.Specifications;
using Shouldly;

namespace Imposter.CodeGenerator.Tests.TypeCriteriaTests;

[Subject(nameof(TypeCriteria))]
public class WhenCreatingWithIsAssignableToPredicate
{
    public static TypeCriteria typeCriteria;

    Because TypeCriteriaCreated = () => typeCriteria = TypeCriteria.Create<ArgType.IsAssignableTo<ICurrentInterface>>();

    It ShouldMatchSelf = () => typeCriteria.Matches(typeof(ICurrentInterface)).ShouldBeTrue();

    It ShouldMatchChildClass = () => typeCriteria.Matches(typeof(ChildClass)).ShouldBeTrue();
    
    It ShouldMatchGrandChildClass = () => typeCriteria.Matches(typeof(GrandChildClass)).ShouldBeTrue();
    
    It ShouldMatchChildInterface = () => typeCriteria.Matches(typeof(IChildInterface)).ShouldBeTrue();
    
    It ShouldMatchGrandChildInterface = () => typeCriteria.Matches(typeof(IGrandChildInterface)).ShouldBeTrue();
    
    It ShouldNotMatchNonMatchingValueType = () => typeCriteria.Matches(typeof(int)).ShouldBeFalse();
    
    It ShouldNotMatchNonMatchingReferenceType = () => typeCriteria.Matches(typeof(HttpClient)).ShouldBeFalse();

    interface IParentInterface
    { }

    interface ICurrentInterface : IParentInterface
    { }
    
    class ChildClass : ICurrentInterface
    { }
    
    class GrandChildClass : ChildClass
    { }

    interface IChildInterface : ICurrentInterface
    { }

    interface IGrandChildInterface : IChildInterface
    { }
}