using Imposter.Abstractions;
using Machine.Specifications;
using Shouldly;

namespace Imposter.CodeGenerator.Tests.TypeCriteriaTests.IsCriteriaTests;

[Subject(nameof(TypeCriteria))]
public class IsIntCriteria
{
    public static TypeCriteria typeCriteria;

    Because TypeCriteriaCreated = () => 
        typeCriteria = TypeCriteria.Create<ArgType.Is<int>>();

    It ShouldMatchIntType = () => 
        typeCriteria.Matches(typeof(int)).ShouldBeTrue();
    
    It ShouldNotMatchDoubleType = () => 
        typeCriteria.Matches(typeof(double)).ShouldBeFalse();
    
    It ShouldNotMatchBoxedObject = () => 
        typeCriteria.Matches(typeof(object)).ShouldBeFalse();
}