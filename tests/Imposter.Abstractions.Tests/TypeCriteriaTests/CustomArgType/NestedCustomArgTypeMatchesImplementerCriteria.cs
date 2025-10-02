using Imposter.Abstractions;
using Machine.Specifications;
using Shouldly;

namespace Imposter.CodeGenerator.Tests.TypeCriteriaTests.CustomArgType;

[Subject(nameof(TypeCriteria))]
public class NestedCustomArgTypeMatchesImplementerCriteria
{
    public static TypeCriteria typeCriteria;

    Because TypeCriteriaCreated = () => 
        typeCriteria = TypeCriteria.Create<ICurrentInterface<ImplementsMarkerArgType>>();

    It ShouldMatchWhenInnerTypeImplementsMarker = () => 
        typeCriteria.Matches(typeof(ICurrentInterface<Implementer>)).ShouldBeTrue();

    It ShouldNotMatchWhenInnerTypeDoesNotImplementMarker = () => 
        typeCriteria.Matches(typeof(ICurrentInterface<NonImplementer>)).ShouldBeFalse();
    
    It ShouldNotMatchWrongInterfaceStructure = () => 
        typeCriteria.Matches(typeof(Implementer)).ShouldBeFalse();
}