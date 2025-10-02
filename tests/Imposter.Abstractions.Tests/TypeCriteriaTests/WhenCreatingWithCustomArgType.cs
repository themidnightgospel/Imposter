using System;
using Imposter.Abstractions;
using Machine.Specifications;
using Shouldly;

namespace Imposter.CodeGenerator.Tests.TypeCriteriaTests;

[Subject(nameof(TypeCriteria))]
public class WhenCreatingWithCustomArgType
{
    public static TypeCriteria typeCriteria;

    Because TypeCriteriaCreated = () => typeCriteria = TypeCriteria.Create<GenericConstraintArgType>();

    It ShouldMatchGenericConstraintType = () => typeCriteria.Matches(typeof(GenericConstraintType)).ShouldBeTrue();

    It ShouldMatchChildClass = () => typeCriteria.Matches(typeof(ChildGenericConstraintType)).ShouldBeTrue();

    It ShouldNotMatchParentClass = () => typeCriteria.Matches(typeof(ParentClass)).ShouldBeFalse();

    class ParentClass
    { }

    class GenericConstraintType : ParentClass
    { } 
    
    class ChildGenericConstraintType : GenericConstraintType
    { }
    
    class GenericConstraintArgType : GenericConstraintType, IArgType<GenericConstraintType>
    {
        public static bool Matches(Type type)
        {
            return typeof(GenericConstraintType).IsAssignableFrom(type);
        }
        
        public GenericConstraintType Value { get; }

        public GenericConstraintArgType(object value)
        {
            Value = (GenericConstraintType)value;
        }
    }
}