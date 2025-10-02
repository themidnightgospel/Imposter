using Imposter.Abstractions;
using Machine.Specifications;
using Shouldly;

namespace Imposter.CodeGenerator.Tests.TypeCriteriaTests.CustomArgType;

[Subject(nameof(TypeCriteria))]
public class CustomArgTypeMatchesImplementerCriteria
{
    public static TypeCriteria typeCriteria;

    Because TypeCriteriaCreated = () => 
        typeCriteria = TypeCriteria.Create<ImplementsMarkerArgType>();

    It ShouldMatchImplementingClass = () => 
        typeCriteria.Matches(typeof(Implementer)).ShouldBeTrue();

    It ShouldMatchDerivedClass = () => 
        typeCriteria.Matches(typeof(DerivedImplementer)).ShouldBeTrue();

    It ShouldNotMatchNonImplementingClass = () => 
        typeCriteria.Matches(typeof(NonImplementer)).ShouldBeFalse();
    
    It ShouldMatchMarkerInterfaceItself = () => 
        typeCriteria.Matches(typeof(IMarker)).ShouldBeTrue();
}