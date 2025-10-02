using System.Collections.Generic;
using Imposter.Abstractions;
using Machine.Specifications;
using Shouldly;

namespace Imposter.CodeGenerator.Tests.TypeCriteriaTests.AnyCriteriaTests;

[Subject(nameof(TypeCriteria))]
public class AnyAsMultiArgumentGenericInterfaceArgument
{
    public static TypeCriteria typeCriteria;

    Because TypeCriteriaCreated = () => 
        typeCriteria = TypeCriteria.Create<IDictionary<ArgType.Any, List<string>>>();

    It ShouldMatchWhenKeyIsValueType = () => 
        typeCriteria.Matches(typeof(Dictionary<int, List<string>>)).ShouldBeTrue();

    It ShouldMatchWhenKeyIsReferenceType = () => 
        typeCriteria.Matches(typeof(Dictionary<BaseClass, List<string>>)).ShouldBeTrue();
    
    It ShouldMatchWhenKeyIsArrayType = () => 
        typeCriteria.Matches(typeof(Dictionary<string[], List<string>>)).ShouldBeTrue();
    
    It ShouldFailWhenInnerTypeIsIncorrect = () => 
        typeCriteria.Matches(typeof(Dictionary<string, List<int>>)).ShouldBeFalse();
    
    It ShouldFailWhenInnerGenericDefinitionIsDifferent = () => 
        typeCriteria.Matches(typeof(Dictionary<string, IEnumerable<string>>)).ShouldBeFalse();
    
    It ShouldFailWhenOuterGenericIsWrong = () => 
        typeCriteria.Matches(typeof(List<IDictionary<string, List<string>>>)).ShouldBeFalse();
    
    class BaseClass { }
}
